namespace FindaCook.Views;
using FindaCook.ViewModels;

public partial class RequestsAndOrders : ContentPage
{
	public RequestsAndOrders()
	{
		InitializeComponent();

		BindingContext = new OrdersAndRequestViewModel();
	}
}