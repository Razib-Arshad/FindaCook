namespace FindaCook.Views;
using FindaCook.ViewModels;

public partial class RequestsAndOrders : ContentPage
{
	public RequestsAndOrders()
	{
		InitializeComponent();

		BindingContext = new OrdersAndRequestViewModel();
	}

    private async void RequestsClickedCommand(object sender, EventArgs e)
    {
        
    }

    private async void OrdersClickedCommand(object sender, EventArgs e)
    {


        await Navigation.PushAsync(new OrderLists());

    }
}