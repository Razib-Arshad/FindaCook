using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginApi.Models
{

    public class ApplicationUser : IdentityUser
    {
        public string UserPassword { get; set; }
    }
}
