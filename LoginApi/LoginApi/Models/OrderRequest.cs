using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginApi.Models
{
    public class OrderRequest
    {
        [Key]
        public int RqID { get; set; }

        [StringLength(500)]
        public string Desc { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        public string selectedService { get; set; }

        public int Price { get; set; }
        public string UserContact { get; set; }
        public string UserAddress { get; set; }
        public string Status { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("CookInfo")]
        public string CookInfoId { get; set; }
        public virtual CookInfo CookInfo { get; set; }

        // One-to-One relationship with Order
        public virtual Order Order { get; set; }
    }
}