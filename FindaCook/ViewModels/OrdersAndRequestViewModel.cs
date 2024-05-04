using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FindaCook.Services;
using FindaCook.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FindaCook.ViewModels
{
    public partial class OrdersAndRequestViewModel : ObservableObject
    {
        private readonly CookService _cookService;

        private ICollection<Orders> _requests;
        private ICollection<Orders> _orders;

        // Property to bind to the ListView
        public ICollection<Orders> DisplayedData { get; set; }
        public ICommand RequestsClickedCommand { get; }
        public ICommand OrdersClickedCommand { get; }
        public OrdersAndRequestViewModel()
        {
            _cookService = new CookService();
            RequestsClickedCommand = new Command(async () => await ExecuteGetRequests());
            OrdersClickedCommand = new Command(async () => await ExecuteGetOrders());
        }

        private async Task ExecuteGetRequests()
        {
            try
            {
                // Call the service method to get requests
                _requests = await _cookService.GetOrderRequests();

                // Process the received requests as needed
                // Update the displayed data
                DisplayedData = _requests;

                // Notify the UI that the property value has changed
                OnPropertyChanged(nameof(DisplayedData));
            }
            catch (Exception ex)
            {
                // Handle exception or log error
            }
        }

        private async Task ExecuteGetOrders()
        {
            try
            {
                // Call the service method to get orders
                _orders = await _cookService.GetOrders();

                // Process the received orders as needed
                // Update the displayed data
                DisplayedData = _orders;

                // Notify the UI that the property value has changed
                OnPropertyChanged(nameof(DisplayedData));
            }
            catch (Exception ex)
            {
                // Handle exception or log error
            }
        }
    }
}
