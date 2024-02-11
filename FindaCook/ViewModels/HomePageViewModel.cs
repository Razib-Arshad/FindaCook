using FindaCook.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FindaCook.Maui.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Cook> Cooks { get; set; }

        public HomePageViewModel()
        {
            Cooks = new ObservableCollection<Cook>
            {
                new Cook { FirstName = "John", PermanentAddress = "Doe", SignatureDishes = "test" },
                new Cook { FirstName = "Jane",  PermanentAddress = "Doe", SignatureDishes = "test"  }
            };
        }

        private string? _firstName;
        public string? FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string? _PermanentAddress;
        public string? PermanentAddress
        {
            get => _PermanentAddress;
            set
            {
                if (_PermanentAddress != value)
                {
                    _PermanentAddress = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}