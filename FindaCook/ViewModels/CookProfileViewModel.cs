﻿using FindaCook.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using FindaCook.Services;

namespace FindaCook.ViewModels
{
    public class CookProfileViewModel : ObservableObject
    {
        private CookProfile _cookProfile;
        readonly ICookRespository _CookRepository = new CookService();

        public CookProfileViewModel(CookProfile cookProfile)
        {
            AddToFavoritesCommand = new AsyncRelayCommand(AddToFavoritesAsync);
            OrderCommand = new RelayCommand(() => Task.Run(OrderAsync));
            _cookProfile = cookProfile;
        }

        public CookProfile CookProfile
        {
            get => _cookProfile;
            set => SetProperty(ref _cookProfile, value);
        }

        public string? FirstName => CookProfile.FirstName;

        public string? LastName => CookProfile.LastName;

        public string? Email => CookProfile.Email;

        public string? SkillsAndSpecialties => CookProfile.SkillsAndSpecialties;

        public string? SignatureDishes => CookProfile.SignatureDishes;

        public string? ServicesProvided => CookProfile.ServicesProvided;

        public string? ExperienceYears => CookProfile.ExperienceYears.ToString();

        public string? CulinarySchoolName => CookProfile.CulinarySchoolName;

        public string? HasCulinaryDegree => CookProfile.HasCulinaryDegree.ToString().ToUpper();

        public string? Degree => CookProfile.Degree;

        public string? Certificates => CookProfile.Certificates;

        public string? EligibleToWork => CookProfile.EligibleToWork.ToString().ToUpper();

        // Commands for binding to buttons
        public ICommand AddToFavoritesCommand { get; }
        public ICommand OrderCommand { get; }

        // Command implementations
        public async Task AddToFavoritesAsync()
        {
            var cookName = CookProfile.FirstName + " " + CookProfile.LastName;
            var result = await _CookRepository.AddToFavorites(cookName);

            if (result)
            {
                // Successfully added to favorites
                // Update the UI or notify the user as needed
            }
            else
            {
                // Failed to add to favorites
                // Handle the failure case, show an error message, etc.
            }
        }


        private async Task OrderAsync()
        {
            await App.Current.MainPage.Navigation.PushAsync(new UserHomePage());
        }
    }
}
