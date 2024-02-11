using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FindaCook.Maui.Models;
using FindaCook.Models;
using FindaCook.Services;
using Microsoft.Maui.Controls;

namespace FindaCook.Maui.ViewModels
{
    public partial class Personal_infoViewModel : ObservableObject
    {


        Person person;
       // private readonly IPersonalInfoRepository personalInfoService = new PersonalInfoService();

        public Personal_infoViewModel()
        {
            person = new Person();
            NextCommand = new AsyncRelayCommand(OnSubmitClicked);
        }
        [ObservableProperty]
        private string _firstName;

        [ObservableProperty]
        private string _lastName;

        [ObservableProperty]
        private string _contactNumber;

        [ObservableProperty]
        private string _whatsappNumber;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _currentAddress;

        [ObservableProperty]
        private string _permanentAddress;

        [ObservableProperty]
        private bool _eligibleToWork;

        public AsyncRelayCommand NextCommand { get; }

        private async Task OnSubmitClicked()
        {
           
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
            {
              
                await App.Current.MainPage.DisplayAlert("Error", "Invalid input", "OK");
                return;
            }
            else
            {
                person.FirstName = FirstName;
                person.LastName = LastName;
                person.ContactNumber = ContactNumber;
                person.WhatsappNumber = WhatsappNumber;
                person.CurrentAddress = CurrentAddress;
                person.PermanentAddress = PermanentAddress;
                person.EligibleToWork = EligibleToWork;

                // Navigate to the next page and pass the Person object
                await App.Current.MainPage.Navigation.PushAsync(new QualificationPage(person));
            }

        }

        
          /*  var success = await personalInfoService.SubmitPersonalInfo(
             FirstName,
             LastName,
             ContactNumber,
             WhatsappNumber,
             CurrentAddress,
             PermanentAddress,
             EligibleToWork,
             Email,
             Password
                );

            // Check the result and handle accordingly
            if (success!=null)
            {
                // Navigate to the next page upon successful submission
                await App.Current.MainPage.Navigation.PushAsync(new QualificationPage());
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushAsync(new QualificationPage());
                //(for now until the api problem not soled we will be moving to the next page even when failure occur
                    
                //string errorMessage = "An error occurred during submission.";

                //await App.Current.MainPage.DisplayAlert("Error", errorMessage, "OK");
            }*/
        
    }
}
