using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginApi.Models
{
    public class Favourite
    {
        [Key]
        public int FavouriteId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("CookInfo")]
        public string CookInfoId { get; set; }
        public virtual CookInfo CookInfo { get; set; }

    }

}
