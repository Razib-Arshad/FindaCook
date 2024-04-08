using FindaCook.ViewModels;

namespace FindaCook.Views;

public partial class OrderLists : ContentPage
{
	public OrderLists()
	{
		InitializeComponent();
		BindingContext = new OrderListViewModel();
	}
}