using FindaCook.Maui.Models;
using FindaCook.Maui.ViewModels;
using FindaCook.Views;

namespace FindaCook
{
    public partial class App : Application
    {
        public static User user;
        public App()
        {
            InitializeComponent();
<<<<<<< HEAD
            var loginPageViewModel = new LoginPageViewModel();
            MainPage = new NavigationPage(new Login(loginPageViewModel));

            //MainPage = new NavigationPage(new Login());
            //MainPage = new AppShell();
=======
            //var loginPageViewModel = new LoginPageViewModel();
            //MainPage = new NavigationPage(new Login(loginPageViewModel));

            //MainPage = new NavigationPage(new  Categories());
            MainPage = new AppShell();
>>>>>>> 817c5030796fd7c2983d6ceb52e742998fed081d
        }
    }
}