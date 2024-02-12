

using FindaCook.ViewModels;

namespace FindaCook;

public partial class Order : ContentPage
{
    public Order()
    {
        InitializeComponent();
        BindingContext = new OrderViewModel();
    }


    private async void OnConfirmClicked(object sender, EventArgs e)
    {

        // await Navigation.PushAsync(new NewPage1());
    }
}