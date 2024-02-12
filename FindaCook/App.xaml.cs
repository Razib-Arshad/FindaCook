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

            //var loginPageViewModel = new LoginPageViewModel();
            //MainPage = new NavigationPage(new Login(loginPageViewModel));

            //MainPage = new NavigationPage(new Login());
            //MainPage = new AppShell();
            MainPage = new NavigationPage(new Categories());

=======

            var loginPageViewModel = new LoginPageViewModel();
            MainPage = new NavigationPage(new Login(loginPageViewModel));

            //MainPage = new NavigationPage(new Login());
            //MainPage = new AppShell();

>>>>>>> 370c77b7f834ddb769208b9e8c90f572c1ff0fb2
        }
    }
}