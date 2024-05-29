using FindaCook.Services;
using System;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Maui.Controls;

namespace FindaCook.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private readonly ICookRespository _cookService;
        private int _totalOrders;
        private int _ordersAccepted;
        private int _ordersRejected;

        public int TotalOrders
        {
            get => _totalOrders;
            set
            {
                _totalOrders = value;
                OnPropertyChanged(nameof(TotalOrders));
            }
        }

        public int OrdersAccepted
        {
            get => _ordersAccepted;
            set
            {
                _ordersAccepted = value;
                OnPropertyChanged(nameof(OrdersAccepted));
            }
        }

        public int OrdersRejected
        {
            get => _ordersRejected;
            set
            {
                _ordersRejected = value;
                OnPropertyChanged(nameof(OrdersRejected));
            }
        }

        public Command LoadDataCommand { get; }

        public DashboardViewModel(ICookRespository cookService)
        {
            _cookService = cookService;
            LoadDataCommand = new Command(async () => await LoadData());
        }

        public DashboardViewModel()
        {
            // Default constructor for use without dependency injection
        }

        private async Task LoadData()
        {
            try
            {
                await LoadAlertsData();
            }
            catch (Exception ex)
            {
                // Handle loading errors, e.g., log them or show an alert
                Console.WriteLine("Error loading data: " + ex.Message);
            }
        }

        private async Task LoadAlertsData()
        {
            var counts = await _cookService.GetAllCounts();
            TotalOrders = counts[2];  // Received orders
            OrdersAccepted = counts[0];  // Accepted orders
            OrdersRejected = counts[1];  // Rejected orders
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
