using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LoginApi.ViewModels
{
    public class CookInfoViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserPassword {  get; set; }

        // Personal Information
        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "ContactNumber is required.")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "WhatsappNumber is required.")]
        public string WhatsappNumber { get; set; }

        [Required(ErrorMessage = "CurrentAddress is required.")]
        public string CurrentAddress { get; set; }

        [Required(ErrorMessage = "PermanentAddress is required.")]
        public string PermanentAddress { get; set; }

        [Required(ErrorMessage = "EligibleToWork is required.")]
        public bool EligibleToWork { get; set; }

        // Qualification Information
        [Required(ErrorMessage = "HasCulinaryDegree is required.")]
        public bool HasCulinaryDegree { get; set; }

        public string? Degree { get; set; }
        public string? Certificates { get; set; }
        public string? CulinarySchoolName { get; set; }

        // Professional Information
        [Required(ErrorMessage = "ExperienceYears is required.")]
        public int ExperienceYears { get; set; }

        // Professional Information
        [Required(ErrorMessage = "SkillsAndSpecialties is required.")]
        public string SkillsAndSpecialties { get; set; }

        [Required(ErrorMessage = "SignatureDishes is required.")]
        public string SignatureDishes { get; set; }

        [Required(ErrorMessage = "ServicesProvided is required.")]
        public string ServicesProvided { get; set; }
    }
}
