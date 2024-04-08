using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using FindaCook.Models;

namespace FindaCook.ViewModels
{
    public class OrderListViewModel : ObservableObject
    {
        private ObservableCollection<ClientOrder> _clientOrders;

        public OrderListViewModel()
        {
            _clientOrders = new ObservableCollection<ClientOrder>();
            LoadOrdersAsync(); // Call the method to load orders
        }

        public ObservableCollection<ClientOrder> ClientOrders
        {
            get => _clientOrders;
            set => SetProperty(ref _clientOrders, value);
        }

        // Removed ICommand as it's not being used in the example provided
        private async Task LoadOrdersAsync()
        {
            // Simulate loading data
            var orders = new ObservableCollection<ClientOrder>
            {
                new ClientOrder { ItemCount = "15", ItemName = "Grilled Chicken Wrap", ItemPrice = "₹ 5.45" },
                new ClientOrder { ItemCount = "10", ItemName = "Vegan Burger", ItemPrice = "₹ 4.99" },
                // ... more placeholder orders
            };

            ClientOrders = orders;
        }
    }
}
