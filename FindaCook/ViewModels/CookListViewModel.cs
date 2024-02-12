using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FindaCook.Models;
using FindaCook.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindaCook.ViewModels
{
    public partial class CookListViewModel : ObservableObject
    {
        private ObservableCollection<CookProfile> _cooks;

        public CookListViewModel(ObservableCollection<CookProfile> cooks)
        {
            _cooks = cooks;
            CookTappedCommand = new RelayCommand<CookProfile>(NavigateToCookDetails);


        }

        public ObservableCollection<CookProfile> Cooks
        {
            get => _cooks;
            set => SetProperty(ref _cooks, value);
        }
        private string? _firstName;
        public string? FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }
        private string? _SkillsAndSpecialties;
        public string? SkillsAndSpecialties
        {
            get => _SkillsAndSpecialties;
            set => SetProperty(ref _SkillsAndSpecialties, value);
        }

        private string? _ExperienceYears;
        public string? ExperienceYears
        {
            get => _ExperienceYears;
            set => SetProperty(ref _ExperienceYears, value);
        }

        
       

        public IRelayCommand<CookProfile> CookTappedCommand { get; }

        private async void NavigateToCookDetails(CookProfile selectedCook)
        {
            await App.Current.MainPage.Navigation.PushAsync(new CookProfiles(selectedCook));

            //var selectedCookViewModel = new CookProfileViewModel(selectedCook);
            //await Shell.Current.GoToAsync($"//cookdetails", false); // Navigate without a history entry

        }


    }
}


