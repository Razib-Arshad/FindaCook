using FindaCook.Maui.ViewModels;


namespace FindaCook.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
    {
		InitializeComponent();
        BindingContext = new HomePageViewModel();
       
    }

}