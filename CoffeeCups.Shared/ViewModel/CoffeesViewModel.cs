using System;
using MvvmHelpers;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;
using System.Linq;
using CoffeeCups.Helpers;
using Microsoft.WindowsAzure.MobileServices;
using CoffeeCups.Authentication;

namespace CoffeeCups
{
    public class CoffeesViewModel : BaseViewModel
    {
        AzureService azureService;
        public CoffeesViewModel()
        {
            azureService = DependencyService.Get<AzureService>();
        }

        public ObservableRangeCollection<itemKopi> Coffees { get; } = new ObservableRangeCollection<itemKopi>();
        public ObservableRangeCollection<Grouping<string, itemKopi>> CoffeesGrouped { get; } = new ObservableRangeCollection<Grouping<string, itemKopi>>();

        string loadingMessage;
        public string LoadingMessage
        {
            get { return loadingMessage; }
            set { SetProperty(ref loadingMessage, value); }
        }

        ICommand loadCoffeesCommand;
        public ICommand LoadCoffeesCommand =>
            loadCoffeesCommand ?? (loadCoffeesCommand = new Command(async () => await ExecuteLoadCoffeesCommandAsync()));

        async Task ExecuteLoadCoffeesCommandAsync()
        {
            if (IsBusy || !(await LoginAsync()))
                return;


            try
            {
                LoadingMessage = "Memuat Kopi.....";
                IsBusy = true;
                var coffees = await azureService.GetCoffees();
                Coffees.ReplaceRange(coffees);


                SortCoffees();


            }
            catch (Exception ex)
            {
                Debug.WriteLine("Yah Gagal men!" + ex);

                await Application.Current.MainPage.DisplayAlert("Sync Error", "tidak bisa sinkronisasi kopi, kamu mungkin sedan offline", "OK");
            }
            finally
            {
                IsBusy = false;
            }


        }

        void SortCoffees()
        {
            var groups = from coffee in Coffees
                         orderby coffee.DateUtc descending
                         group coffee by coffee.DateDisplay
                into coffeeGroup
                         select new Grouping<string, itemKopi>($"{coffeeGroup.Key} ({coffeeGroup.Count()})", coffeeGroup);


            CoffeesGrouped.ReplaceRange(groups);
        }

        string orderName;
        public string OrderName
        {
            get { return orderName; }
            set { orderName = value; }
        }

        string orderMenu;
        public string OrderMenu
        {
            get { return orderMenu; }
            set { orderMenu = value; }
        }

        string orderLocation;
        public string OrderLocation
        {
            get { return orderLocation; }
            set { orderLocation = value; }
        }
        bool atHome;
        public bool AtHome
        {
            get { return atHome; }
            set { SetProperty(ref atHome, value); }
        }

        ICommand addCoffeeCommand;
        public ICommand AddCoffeeCommand =>
            addCoffeeCommand ?? (addCoffeeCommand = new Command(async () => await ExecuteAddCoffeeCommandAsync()));

        async Task ExecuteAddCoffeeCommandAsync()
        {
            if (IsBusy || !(await LoginAsync()))
                return;

            try
            {
                LoadingMessage = "Menambahkan Kopi.....";
               
                IsBusy = true;

                //orderName = orderNameEntry.Text;
                var coffee = await azureService.AddCoffee(AtHome, OrderName, OrderMenu, OrderLocation);
                Coffees.Add(coffee);
                SortCoffees();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Yah Gagal Men!" + ex);
            }
            finally
            {
                LoadingMessage = string.Empty;
                IsBusy = false;
            }

        }

        public Task<bool> LoginAsync()
        {
            if (Settings.IsLoggedIn)
                return Task.FromResult(true);


            return azureService.LoginAsync();
        }
    }
}

