using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Plugin.Connectivity;
using CoffeeCups.Helpers;

namespace CoffeeCups
{
    public partial class OrderPage : ContentPage
    {
        CoffeesViewModel vm;
        public OrderPage()
        {
            InitializeComponent();
            BindingContext = vm = new CoffeesViewModel();

            string name = orderNameEntry.Text;
            string location = orderLocationEntry.Text;
            string menu = orderMenuEntry.Text;

            CoffeesViewModel a = new CoffeesViewModel();
            if (name != "" && location != "" && menu != "")
            {
                a.OrderName = name;
                a.OrderLocation = location;
                a.OrderMenu = menu;

            }
            else if (name == "" && location != "" && menu != "")
            {
                a.OrderName = "Unknown";
                a.OrderLocation = location;
                a.OrderMenu = menu;
               
            }
            else if (name != "" && location == "" && menu != "")
            {
                a.OrderName = name;
                a.OrderLocation = "Unknown";
                a.OrderMenu = menu;

            }
            else if (name != "" && location != "" && menu == "")
            {
                a.OrderName = name;
                a.OrderLocation = location;
                a.OrderMenu = "Unknown";

            }
            else if (name == "" && location == "" && menu != "")
            {
                a.OrderName = "Unknown";
                a.OrderLocation = "Unknown";
                a.OrderMenu = menu;

            }
            else if (name == "" && location != "" && menu == "")
            {
                a.OrderName = "Unknown";
                a.OrderLocation = location;
                a.OrderMenu = "Unknown";

            }
            else if (name != "" && location == "" && menu == "")
            {
                a.OrderName = name;
                a.OrderLocation = "Unknown";
                a.OrderMenu = "Unknown";

            }
            else
            {
                a.OrderName = "Unknown";
                a.OrderLocation = "Unknown";
                a.OrderMenu = "Unknown";
            }
        }
        async void OnButtonClicked2(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new CoffeesPage());
        }

    }
}



