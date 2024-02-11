using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginApi.Models
{
    public class CookInfo : ApplicationUser
    {
        // Personal Information
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string WhatsappNumber { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public bool EligibleToWork { get; set; }

        // Qualification Information
        public bool HasCulinaryDegree { get; set; }
        public string Degree { get; set; }
        public string Certificates { get; set; }
        public string CulinarySchoolName { get; set; }

        // Professional Information
        public int ExperienceYears { get; set; }

        // Professional Information
        public string SkillsAndSpecialties { get; set; }
        public string SignatureDishes { get; set; }
        public string ServicesProvided { get; set; }


        // One-to-Many relationship with OrderRequest
        public virtual ICollection<OrderRequest> OrderRequests { get; set; }

        // One-to-Many relationship with Favourite
        public virtual ICollection<Favourite> Favourites { get; set; }
    }
}
