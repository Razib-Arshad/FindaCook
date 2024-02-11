using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginApi.Models 
{
    public class User : ApplicationUser
    {
        public virtual ICollection<OrderRequest> OrderRequests { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
    }
}
