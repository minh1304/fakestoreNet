using System.ComponentModel.DataAnnotations.Schema;

namespace fakestrore_Net.Models
{
    public class CartProduct
    {
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public Cart Cart { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }

}
