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

           
            MainPage = new NavigationPage(new Page1());
          

        }
    }
}