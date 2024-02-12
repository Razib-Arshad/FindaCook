using FindaCook.Maui.ViewModels;
namespace FindaCook;

public partial class Login : ContentPage
{
	public Login(LoginPageViewModel loginPageViewModel)
	{
		InitializeComponent();
        BindingContext = loginPageViewModel;
    }

   
    
}