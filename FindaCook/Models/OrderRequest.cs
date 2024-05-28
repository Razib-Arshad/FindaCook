using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class RequestApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Data Data { get; set; }
    }
}
