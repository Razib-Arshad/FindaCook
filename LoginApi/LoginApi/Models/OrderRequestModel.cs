using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoginApi.Models
{
    public class OrderRequestModel
    {
        public string Desc;

        public DateTime Date;

        public int Price;
        public string UserContact;
        public string UserAddress;
        public string Status;
        public string UserId { get; set; }
        public string CookInfoId { get; set; }
    }
}
