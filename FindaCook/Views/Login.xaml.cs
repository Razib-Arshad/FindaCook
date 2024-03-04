using FindaCook.Maui.ViewModels;
namespace FindaCook;

public partial class Login : ContentPage
{
	public Login(LoginPageViewModel loginPageViewModel)
	{
		InitializeComponent();
        BindingContext = loginPageViewModel;
    }
    private async void Register_Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UserRegisterPage());
    }
    private async void RegisterCook_Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Personal_info());
    }


}