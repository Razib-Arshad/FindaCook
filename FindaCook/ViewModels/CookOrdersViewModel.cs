using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FindaCook.Models;
using FindaCook.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace FindaCook.ViewModels
{
    public partial class CookOrdersViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<SimpleOrderDTO> _displayedData;

        [ObservableProperty]
        private bool _noRequests;

        [ObservableProperty]
        private bool _noAcceptedOrders;

        [ObservableProperty]
        private bool _noDeclinedOrders;

        readonly ICookRespository _cookService;

        public CookOrdersViewModel()
        {
            _displayedData = new ObservableCollection<SimpleOrderDTO>();
            _cookService = new CookService();
        }

        [RelayCommand]
        private async Task ExecuteGetAllRequests()
        {
            try
            {
                var requests = await _cookService.GetAllOrderRequests();
                DisplayedData.Clear();

                if (requests != null && requests.Any())
                {
                    foreach (var request in requests)
                    {
                        DisplayedData.Add(request);
                    }
                    NoRequests = false;
                }
                else
                {
                    NoRequests = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching all requests: {ex.Message}");
                NoRequests = true;
            }
        }

        [RelayCommand]
        private async Task ExecuteGetAcceptedOrders()
        {
            try
            {
                var acceptedOrders = await _cookService.GetAcceptedCookOrderRequests();
                DisplayedData.Clear();

                if (acceptedOrders != null && acceptedOrders.Any())
                {
                    NoAcceptedOrders = false;
                    foreach (var order in acceptedOrders)
                    {
                        DisplayedData.Add(order);
                    }
                }
                else
                {
                    NoAcceptedOrders = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching accepted orders: {ex.Message}");
                NoAcceptedOrders = true;
            }
        }

        [RelayCommand]
        private async Task ExecuteGetDeclinedOrders()
        {
            try
            {
                var declinedOrders = await _cookService.GetDeclinedCookOrderRequests();
                DisplayedData.Clear();

                if (declinedOrders != null && declinedOrders.Any())
                {
                    NoDeclinedOrders = false;
                    foreach (var order in declinedOrders)
                    {
                        DisplayedData.Add(order);
                    }
                }
                else
                {
                    NoDeclinedOrders = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching declined orders: {ex.Message}");
                NoDeclinedOrders = true;
            }
        }

        [RelayCommand]
        private async Task RefreshData()
        {
            await Task.WhenAll(
                ExecuteGetAllRequests(),
                ExecuteGetAcceptedOrders(),
                ExecuteGetDeclinedOrders()
            );
        }

        [RelayCommand]
        private async Task SelectOrder(SimpleOrderDTO selectedOrder)
        {
            if (selectedOrder != null)
            {
                var navigation = Application.Current.MainPage.Navigation;
                await navigation.PushAsync(new Views.OrderDetailsPage { BindingContext = new OrderDetailsViewModel(selectedOrder) });
            }
        }
    }
}
