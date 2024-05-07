using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FindaCook.Models;
using FindaCook.Views;
using FindaCook.Services;
using FindaCook.Maui.ViewModels;

namespace FindaCook.ViewModels
{
    public partial class ProfessionalInfoViewModel : ObservableObject
    {

        QualificationInfo qualificationInfo;
     
        Person person;
        ProfessionalInfoModel _model;
        private readonly ICookRespository register_cook = new CookService();

        public ProfessionalInfoViewModel(Person perosn,QualificationInfo qual)
        {

            this.person = perosn;
            this.qualificationInfo= qual;

            _model = new ProfessionalInfoModel();
            NextCommand = new AsyncRelayCommand(OnNextClicked);
        }

        [ObservableProperty]
        private int _experience;

        [ObservableProperty]
        private string _signatureDishes;

        [ObservableProperty]
        private bool _chineseFood;

        [ObservableProperty]
        private bool _continentalFood;

        [ObservableProperty]
        private bool _thaiFood;

        [ObservableProperty]
        private bool _italianFood;

        [ObservableProperty]
        private bool _desiFood;

        [ObservableProperty]
        private bool _fastFood;

        [ObservableProperty]
        private bool _inHouseChefServices;

        [ObservableProperty]
        private bool _cookingClasses;

        [ObservableProperty]
        private bool _cookingWorkshops;

        public ICommand NextCommand { get; }

        private async Task OnNextClicked()
        {

            var selectedSkills = new List<string>
            {
                _chineseFood ? "Chinese Food" : "",
                _continentalFood ? "Continental Food" : "",
                _thaiFood ? "Thai Food" : "",
                _italianFood ? "Italian Food" : "",
                _desiFood ? "Desi Food" : "",
                _fastFood ? "Fast Food" : ""
            };

            var selectedServices = new List<string>
            {
                _inHouseChefServices ? "In-House Chef Services" : "",
                _cookingClasses ? "Cooking Classes" : "",
                _cookingWorkshops ? "Cooking Workshops" : ""
            };

            var skillsString = string.Join(", ", selectedSkills);
            var servicesString = string.Join(", ", selectedServices);

            _model.Experience = _experience;
            _model.SignatureDishes = _signatureDishes;
            _model.Skills = skillsString;
            _model.Services = servicesString;

           




            
            var success = await register_cook.RegisterCook(person, qualificationInfo, _model);

            if (success != null)
            {

                var appShell = new AppShell();  
                App.Current.MainPage = appShell;

            }
            else
            {
                // Handle the failure case
                await App.Current.MainPage.DisplayAlert("Error", "An error occurred during submission.", "OK");
            }
        }
    }
}
