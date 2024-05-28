namespace FindaCook.Views;

public partial class CookProfilePage : ContentPage
{
	public CookProfilePage()
	{
		InitializeComponent();
	}
    private async void SwitchToUser_Button_Clicked(object sender, EventArgs e)
    {   
       var appShell = new AppShell();  
       App.Current.MainPage = appShell;
       
    }
}