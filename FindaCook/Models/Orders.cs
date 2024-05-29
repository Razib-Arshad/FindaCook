using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Globalization;

namespace FindaCook.Models
{
    public class Orders
    {
        public string SelectedService { get; set; }
        public string Description { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public int Price { get; set; }
        public DateTime SelectedDate { get; set; }
        public TimeSpan SelectedTime { get; set; }

        public Orders(string selectedService, string description, string contactNumber, string address, int price, DateTime selectedDate, TimeSpan selectedTime)
        {
            SelectedService = selectedService;
            Description = description;
            ContactNumber = contactNumber;
            Address = address;
            Price = price;
            SelectedDate = selectedDate;
            SelectedTime = selectedTime;
        }

        // If needed, you can add additional methods or properties here.

        public DateTime GetOrderDateTime()
        {
            return SelectedDate.Add(SelectedTime);
        }
    }
    public partial class ClientOrder : ObservableObject
        {
            [ObservableProperty]
            private string itemCount;

            [ObservableProperty]
            private string itemName;

            [ObservableProperty]
            private string itemPrice;
        }
 












}
