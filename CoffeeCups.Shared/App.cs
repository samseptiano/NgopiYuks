using System;

using Xamarin.Forms;
namespace CoffeeCups
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = new NavigationPage(new CoffeesPage())
                {
                    BarTextColor = Color.White,
                    BarBackgroundColor = Color.FromHex("#8900F1")
                };
        }

        protected override void OnStart()
        {
           
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

