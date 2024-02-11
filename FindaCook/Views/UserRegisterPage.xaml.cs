using FindaCook.Views;
using FindaCook.Maui.Models;
using Microsoft.Maui.Controls;
using FindaCook.Maui.ViewModels;

namespace FindaCook;

public partial class UserRegisterPage : ContentPage
{
	public UserRegisterPage()
	{
		InitializeComponent();
        BindingContext = new UserRegistrationPageViewModel();
    }
    private async void NavigateToLoginPage(object sender, EventArgs e)
    {
        var loginPageViewModel = new LoginPageViewModel(); // Create an instance of LoginPageViewModel
        await Navigation.PushAsync(new Login(loginPageViewModel));
    }
}