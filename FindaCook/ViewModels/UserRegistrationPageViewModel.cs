using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FindaCook.Maui.Models;
using FindaCook.Views;
using FindaCook;
using FindaCook.Services;

namespace FindaCook.Maui.ViewModels
{
    public partial class UserRegistrationPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string retypePassword;

        readonly IRegistrationRepository registrationService = new RegistrationService();

        [RelayCommand]
        public async Task Register()
        {
            
            // Validate input (add your own validation logic)
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(retypePassword))
            {
                // Handle invalid input
                await App.Current.MainPage.DisplayAlert("Error", "Invalid input", "OK");
                return;
            }

            // Call the registration service to perform registration
            var result = await registrationService.Register(name, email, password, retypePassword);

            // Check the result and handle accordingly
            if (result != null && result.Success)
            {
                // Registration successful, navigate to the login page or do something else
                var loginPageViewModel = new LoginPageViewModel(); // create an instance of LoginPageViewModel
                //var loginPage = new Login(loginPageViewModel);
                await App.Current.MainPage.Navigation.PushAsync(new Login(loginPageViewModel));
            }
            else
            {
                // Handle registration failure, show an error message, etc.
                string errorMessage = result != null ? result.ErrorMessage : "An error occurred during registration.";

                await App.Current.MainPage.DisplayAlert("Error", errorMessage, "OK");
            }
        }

        private void DisplayAlert(string v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }
    }
}
