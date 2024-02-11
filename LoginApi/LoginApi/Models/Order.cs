using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginApi.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("OrderRequest")]
        public int RqId { get; set; }
        public virtual OrderRequest OrderRequest { get; set; }
    }
}
