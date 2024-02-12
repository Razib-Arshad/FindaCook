using FindaCook.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FindaCook.ViewModels
{
    public class CookProfileViewModel : ObservableObject
    {
        private CookProfile _cookProfile;

        public CookProfileViewModel(CookProfile cookProfile)
        {
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
    }
}