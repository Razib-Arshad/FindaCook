using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LoginApi.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        [Required]
        public int RqId { get; set; }
    }
}
