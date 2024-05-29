namespace FindaCook.Views;

public partial class UserAccountPage : ContentPage
{
	public UserAccountPage()
	{
        InitializeComponent();
	}
    private async void RegisterCook_Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Personal_info());
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
       // await Navigation.PushAsync(new Favourites());
    }
    private void SwitchToCook_Button_Clicked(object sender, EventArgs e)
    {

        var appShell = new CookAppShell();  // New root shell/navigation
        App.Current.MainPage = appShell;
    }
    private async void Logout_Button_Clicked(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new Favourites());
    }


}