using System.ComponentModel.DataAnnotations.Schema;

namespace LoginApi.Models
{
    public class FavouriteModel
    {
        public string UserId { get; set; }
        public string CookInfoId { get; set; }
    }
}
