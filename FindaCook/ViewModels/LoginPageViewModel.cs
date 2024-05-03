using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using FindaCook.Maui.Models;
using FindaCook.Views;
using FindaCook.Services;
using FindaCook;

namespace FindaCook.Maui.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;
        readonly ILoginRespository loginService = new LoginService();

        [RelayCommand]
        public async Task SignIn()
        {
            // Validate email and password (add your own validation logic)
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                // Handle invalid input
                await App.Current.MainPage.DisplayAlert("Error", "Invalid input", "OK");
                return;
            }

            // Call the authentication service to perform sign-in
            var result = await loginService.Login(email, password);

            // Check the result and handle accordingly
            if (result != null)
            {
                // Navigate to the next page upon successful sign-in

                var appShell = new AppShell();  // New root shell/navigation
                App.Current.MainPage = appShell;



                //await App.Current.MainPage.Navigation.PushAsync(new AppShell());
            }
            else
            {
                // Handle sign-in failure, show an error message, etc.
                string errorMessage =  "An error occurred during sign-in.";

                await App.Current.MainPage.DisplayAlert("Error", errorMessage, "OK");
                //await App.Current.MainPage.Navigation.PushAsync(new HomePage());

            }
        }

    }
}
