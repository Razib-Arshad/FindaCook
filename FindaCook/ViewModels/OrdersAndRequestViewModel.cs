using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FindaCook.Models;
using FindaCook.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindaCook.ViewModels
{
    [ObservableObject]
    public partial class OrdersAndRequestViewModel
    {
        [ObservableProperty]
        private ICollection<Orders> displayedData;

        readonly ICookRespository _cookService;  // Ensure this name matches your interface

        public OrdersAndRequestViewModel()
        {
            _cookService = new CookService();
        }

        [RelayCommand]
        private async Task ExecuteGetRequests()
        {
            try
            {
                var requests = await _cookService.GetOrderRequests();
                DisplayedData = requests;
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }

        [RelayCommand]
        private async Task ExecuteGetOrders()
        {
            try
            {
                var orders = await _cookService.GetOrders();
                DisplayedData = orders;
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }

        [RelayCommand]
        private async Task RefreshData()
        {
            await ExecuteGetRequests();
            await ExecuteGetOrders();
        }
    }
}
