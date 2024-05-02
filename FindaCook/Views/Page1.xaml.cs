namespace FindaCook;
using FindaCook.Maui.ViewModels;
public partial class Page1 : ContentPage
{
    public Page1()
    {
        InitializeComponent();
    }

    private async void OnGetStartedClicked(object sender, EventArgs e)
    {
        var loginPageViewModel = new LoginPageViewModel();
        await Navigation.PushAsync(new Login(loginPageViewModel));


      
    }
}
