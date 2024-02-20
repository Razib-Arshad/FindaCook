using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using FindaCook.Models;
using FindaCook.Services;

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
        readonly ICookRespository _CookRepository = new CookService();

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

            Orders orderDetails = new Orders(
                    SelectedService,
                    Description,
                    ContactNumber,
                    Address,
                    Price,
                    SelectedDate,
                    SelectedTime
                );
            await _CookRepository.SendOrder(orderDetails);
  
        }
    }
}
