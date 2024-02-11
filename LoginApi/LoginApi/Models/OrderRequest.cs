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
        public string Desc;

        [DataType(DataType.Date)]
        public DateTime Date;

        public int Price;
        public string UserContact;
        public string UserAddress;
        public string Status;

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
