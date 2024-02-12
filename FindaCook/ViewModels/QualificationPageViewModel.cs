using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using FindaCook.Models;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FindaCook.ViewModels
{
    public partial class QualificationPageViewModel : ObservableObject
    {
        QualificationInfo qualificationInfo;
        private bool _isQualificationVisible;
        Person person;

        public QualificationPageViewModel(Person person)
        {
            this.person = person;



            qualificationInfo = new QualificationInfo();
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
                if (string.IsNullOrEmpty(_degree) || string.IsNullOrEmpty(_certificates) || string.IsNullOrEmpty(SchoolName))
                {
                    // Display an alert if any of the fields are empty
                    await App.Current.MainPage.DisplayAlert("Validation Error", "Please fill in all the qualification details.", "OK");
                    return;
                }

                // Set qualification information in the QualificationInfo object
                qualificationInfo.Degree = _degree;
                qualificationInfo.Certificates = _certificates;
                qualificationInfo.SchoolName = _schoolName;
                await App.Current.MainPage.Navigation.PushAsync(new professional_info(person, qualificationInfo));

            }


        }

        private async void OnNoClicked()
        {
            qualificationInfo.Degree = "";
            qualificationInfo.Certificates = "";
            qualificationInfo.SchoolName = "";
           
            await App.Current.MainPage.Navigation.PushAsync(new professional_info(person, qualificationInfo));
            
        }
    }
}
