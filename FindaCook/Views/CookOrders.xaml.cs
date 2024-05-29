using FindaCook.Models;
using FindaCook.ViewModels;
using Microsoft.Maui.Controls;

namespace FindaCook.Views
{
    public partial class CookOrders : ContentPage
    {
        public CookOrders()
        {
            InitializeComponent();
        }

        private async void OnOrderSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is SimpleOrderDTO selectedOrder)
            {
                await ((CookOrdersViewModel)BindingContext).SelectOrderCommand.ExecuteAsync(selectedOrder);
            }
        }
    }
}
