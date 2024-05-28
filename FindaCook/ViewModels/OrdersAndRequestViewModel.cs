using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FindaCook.Models;
using FindaCook.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FindaCook.ViewModels
{
    public partial class OrdersAndRequestViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<SimpleOrderDTO> _displayedData;

        [ObservableProperty]
        private bool _noRequests;

        [ObservableProperty]
        private bool _noOrders;

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
                DisplayedData.Clear();


                var simpleOrderDtoList = new List<SimpleOrderDTO>();

                if (requests != null)
                {


                    foreach (var orderRequest in requests.Data.OrderRequests)
                    {

                        var simpleOrderDto = new SimpleOrderDTO
                        {
                            Desc = orderRequest.Desc,
                            Date = orderRequest.Date,
                            SelectedService = orderRequest.selectedService,
                            Price = orderRequest.Price,
                            CookUserName = $"{orderRequest.Cook.FirstName} {orderRequest.Cook.LastName}",
                            // Add if there's a contact number in the response
                        };
                        simpleOrderDtoList.Add(simpleOrderDto);
                    }
                }

                if (simpleOrderDtoList != null && simpleOrderDtoList.Any())
                {
                    foreach (var request in simpleOrderDtoList)
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
                System.Diagnostics.Debug.WriteLine($"Error fetching requests: {ex.Message}");
                NoRequests = true;
            }
        }

        [RelayCommand]
        private async Task ExecuteGetOrders()
        {
            try
            {
                var orders = await _cookService.GetOrders();
                DisplayedData.Clear();

                if (orders != null && orders.Any())
                {
                    NoOrders = false;
                    foreach (var order in orders)
                    {
                        DisplayedData.Add(order);
                    }
                }
                else
                {
                    NoOrders = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching orders: {ex.Message}");
                NoOrders = true;
            }
        }

        [RelayCommand]
        private async Task RefreshData()
        {
            await Task.WhenAll(ExecuteGetRequests(), ExecuteGetOrders());
        }
    }
}
