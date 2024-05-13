using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FindaCook.Models;
using FindaCook.Services;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FindaCook.ViewModels
{
    public partial class OrdersAndRequestViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<SimpleOrderDTO> _displayedData;

        readonly ICookRespository _cookService;

        public OrdersAndRequestViewModel()
        {
            _displayedData = new ObservableCollection<SimpleOrderDTO>();
            _cookService = new CookService();
        }

        [RelayCommand]
        private async Task ExecuteGetRequests()
        {
            try
            {
                var requests = await _cookService.GetOrderRequests();
                if (requests != null)
                {
                    DisplayedData.Clear();
                    foreach (var request in requests)
                    {
                        DisplayedData.Add(request);
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Error fetching orders");

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching orders: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task ExecuteGetOrders()
        {
            try
            {
                var orders = await _cookService.GetOrders();
                if (orders != null)
                {
                    DisplayedData.Clear();
                    foreach (var order in orders)
                    {
                        DisplayedData.Add(order);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching orders: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task RefreshData()
        {
            await Task.WhenAll(ExecuteGetRequests(), ExecuteGetOrders());
        }

    }
}
