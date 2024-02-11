using System.ComponentModel.DataAnnotations;

namespace LoginApi.ViewModels
{
    public class FavouriteViewModel
    {
        [Required(ErrorMessage = "CookInfoEmail is required.")]
        public string CookEmail { get; set; }
    }
}
