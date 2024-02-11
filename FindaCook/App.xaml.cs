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
            //var loginPageViewModel = new LoginPageViewModel();
            //MainPage = new NavigationPage(new Login(loginPageViewModel));

            //MainPage = new NavigationPage(new  Categories());
            MainPage = new AppShell();
        }
    }
}