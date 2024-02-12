using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace FindaCook.ViewModels
{
    public class OrderViewModel : ObservableObject
    {
        private string _selectedService;
        private string _description;
        private string _contactNumber;
        private string _address;
        private decimal _price;
        private DateTime _selectedDate = DateTime.Now;
        private TimeSpan _selectedTime = TimeSpan.Zero;

        public OrderViewModel()
        {
            ConfirmCommand = new RelayCommand(async () => await ConfirmOrderAsync());
        }

        public string SelectedService
        {
            get => _selectedService;
            set => SetProperty(ref _selectedService, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string ContactNumber
        {
            get => _contactNumber;
            set => SetProperty(ref _contactNumber, value);
        }

        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        public decimal Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        public TimeSpan SelectedTime
        {
            get => _selectedTime;
            set => SetProperty(ref _selectedTime, value);
        }

        public ICommand ConfirmCommand { get; }

        private async Task ConfirmOrderAsync()
        {
            // Logic to process the order
            // For example, create an OrderDetails object and send it to a service to be processed
            var orderDetails = new
            {
                Service = SelectedService,
                Description,
                ContactNumber,
                Address,
                Price,
                Date = SelectedDate.Add(SelectedTime), // Combining date and time into a single DateTime
            };

            // Placeholder for actual order processing logic
            await Task.Delay(1000); // Simulate async work

            // Navigate to a confirmation page or display an alert to indicate success
            // await App.Current.MainPage.Navigation.PushAsync(new OrderConfirmationPage(orderDetails));
        }
    }
}
