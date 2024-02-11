using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using FindaCook.Models;
using System.Windows.Input;

namespace FindaCook.ViewModels
{
    public partial class QualificationPageViewModel : ObservableObject
    {
        private QualificationInfo _qualificationInfo;
        private bool _isQualificationVisible;
        private Person person;

        public QualificationPageViewModel(Person person)
        {
            this.person = person;
            _qualificationInfo = new QualificationInfo();
            YesCommand = new Command(OnYesClicked);
            NoCommand = new Command(OnNoClicked);
            NextCommand = new AsyncRelayCommand(OnNextClicked);
        }

        [ObservableProperty]
        private string _degree;

        [ObservableProperty]
        private string _certificates;

        [ObservableProperty]
        private string _schoolName;

        public bool IsQualificationVisible
        {
            get => _isQualificationVisible;
            set => SetProperty(ref _isQualificationVisible, value);
        }

        public ICommand YesCommand { get; }
        public ICommand NoCommand { get; }
        public AsyncRelayCommand NextCommand { get; }

        private void OnYesClicked()
        {
            // Set IsQualificationVisible to true to show the qualification input fields
            IsQualificationVisible = true;
        }

        private async Task OnNextClicked()
        {
            if (IsQualificationVisible)
            {
                // Validate the input fields
                if (string.IsNullOrEmpty(Degree) || string.IsNullOrEmpty(Certificates) || string.IsNullOrEmpty(SchoolName))
                {
                    // Display an alert if any of the fields are empty
                    await App.Current.MainPage.DisplayAlert("Validation Error", "Please fill in all the qualification details.", "OK");
                    return;
                }

                // Set qualification information in the QualificationInfo object
                _qualificationInfo.Degree = Degree;
                _qualificationInfo.Certificates = Certificates;
                _qualificationInfo.SchoolName = SchoolName;
                await App.Current.MainPage.Navigation.PushAsync(new professional_info(person, _qualificationInfo));

            }


        }

        private async void OnNoClicked()
        {
            _qualificationInfo.Degree = "";
            _qualificationInfo.Certificates = "";
            _qualificationInfo.SchoolName = "";
            await App.Current.MainPage.Navigation.PushAsync(new professional_info(person, _qualificationInfo));
        }
    }
}
