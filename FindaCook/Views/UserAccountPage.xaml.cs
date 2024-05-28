using FindaCook.Maui.ViewModels;
using FindaCook.Services;
using Microsoft.Maui.Controls;

namespace FindaCook.Views
{
    public partial class UserAccountPage : ContentPage
    {
        private readonly LoginService _loginService;

        public UserAccountPage()
        {
            InitializeComponent();
            _loginService = new LoginService();
        }

        private async void RegisterCook_Button_Clicked(object sender, EventArgs e)
        {
            var email = Preferences.Get("UserEmail", string.Empty);
            if (await _loginService.CheckUserRegisteredAsCookByEmail(email))
            {
                await DisplayAlert("Error", "You are already registered as a cook.", "OK");
            }
            else
            {
                await Navigation.PushAsync(new Personal_info());
            }

            
        }

        private async void ResetPassword_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResetPassword());
        }

        private async void FavouritesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Favourites());
        }

        private async void TermsPolicyButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Favourites());
        }

        private async void SwitchToCook_Button_Clicked(object sender, EventArgs e)
        {
    

            var email = Preferences.Get("UserEmail", string.Empty);
            if (await _loginService.CheckUserRegisteredAsCookByEmail(email))
            {
                string cookInfoId = Preferences.Get("CookInfoId", string.Empty);

                var appShell = new CookAppShell(cookInfoId);  // New root shell/navigation
                App.Current.MainPage = appShell;
            }
            else
            {
                await DisplayAlert("Error", "Email is not registered as a cook.", "OK");
                await Navigation.PushAsync(new Personal_info());
            }
        }

        private async void Logout_Button_Clicked(object sender, EventArgs e)
        {
            var loginPageViewModel = new LoginPageViewModel();
            await Navigation.PushAsync(new Login(loginPageViewModel));
        }
    }
}
