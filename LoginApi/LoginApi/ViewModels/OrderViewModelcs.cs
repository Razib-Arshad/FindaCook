using System.ComponentModel.DataAnnotations;

namespace LoginApi.ViewModels
{
    public class OrderViewModelcs
    {
        [Required(ErrorMessage = "OrderId is required.")]
        public int OrderId { get; set; }

    }
}
