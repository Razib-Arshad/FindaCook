using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FindaCook.Models;
using FindaCook.Services;
using System.Threading.Tasks;

namespace FindaCook.ViewModels
{
    public partial class OrderDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        private SimpleOrderDTO _selectedOrder;

        private readonly ICookRespository _cookService;

        public OrderDetailsViewModel()
        {
            _cookService = new CookService();
        }

        public OrderDetailsViewModel(SimpleOrderDTO selectedOrder) : this()
        {
            SelectedOrder = selectedOrder;
        }

        [RelayCommand]
        private async Task AcceptOrder()
        {
            // Logic to accept the order
        }

        [RelayCommand]
        private async Task RejectOrder()
        {
            // Logic to reject the order
        }
    }
}
