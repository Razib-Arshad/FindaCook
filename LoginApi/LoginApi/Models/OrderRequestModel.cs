using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoginApi.Models
{
    public class OrderRequestModel
    {
        [Required]
        public string Desc { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public string selectedService { get; set; }

        [Required]
        public int Price { get; set; }
        [Required]
        public string UserContact { get; set; }
        [Required]
        public string UserAddress { get; set; }

        public string Status;

        [Required]
        public string UserId { get; set; }

        [Required]
        public string CookInfoId { get; set; }
    }
}