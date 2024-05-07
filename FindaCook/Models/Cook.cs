using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindaCook.Models
{
    
    public class Cook
    {
        public string FirstName { get; set; }
        public string SignatureDishes { get; set; }
        public string PermanentAddress { get; set; }
    }
    public class CookProfile
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("skillsAndSpecialties")]
        public string SkillsAndSpecialties { get; set; }

        [JsonProperty("signatureDishes")]
        public string SignatureDishes { get; set; }

        [JsonProperty("servicesProvided")]
        public string ServicesProvided { get; set; }

        [JsonProperty("experienceYears")]
        public int ExperienceYears { get; set; }

        [JsonProperty("culinarySchoolName")]
        public string CulinarySchoolName { get; set; }

        [JsonProperty("hasCulinaryDegree")]
        public bool HasCulinaryDegree { get; set; }

        [JsonProperty("degree")]
        public string Degree { get; set; }

        [JsonProperty("certificates")]
        public string Certificates { get; set; }

        [JsonProperty("eligibleToWork")]
        public bool EligibleToWork { get; set; }
    }
    public class DataContainer
    {
        [JsonProperty("data")]
        public List<CookProfile> Data { get; set; }
    }

    public class OrderContainer
    {
        [JsonProperty("data")]
        public List<Orders> Data { get; set; }
    }
    public class FavouriteCookDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SkillsAndSpecialties { get; set; }
        public string ServicesProvided { get; set; }
        public string SignatureDishes { get; set; }
        public int ExperienceYears { get; set; }
    }


}
