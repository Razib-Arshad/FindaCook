using System;
using System.Collections.Generic;

namespace FindaCook.Models
{
    public class CookInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SkillsAndSpecialties { get; set; }
        public string SignatureDishes { get; set; }
        public string ServicesProvided { get; set; }
        public int ExperienceYears { get; set; }
        public string CulinarySchoolName { get; set; }
        public bool HasCulinaryDegree { get; set; }
        public string Degree { get; set; }
        public string Certificates { get; set; }
        public bool EligibleToWork { get; set; }
    }

    public class OrderRequest
    {
        public int RqID { get; set; }
        public string Desc { get; set; }
        public string selectedService { get; set; }
        public int Price { get; set; }
        public string UserAddress { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public CookInfo Cook { get; set; }
    }

    public class Data
    {
        public List<OrderRequest> OrderRequests { get; set; }
    }

  

    public class SimpleOrderDTO
    {
        public string Desc { get; set; }
        public DateTime Date { get; set; }
        public string SelectedService { get; set; }
        public int Price { get; set; }
        public string CookUserName { get; set; }
        public string ServicesProvided { get; set; }
    }
}
