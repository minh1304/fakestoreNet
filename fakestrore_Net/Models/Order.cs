using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace fakestrore_Net.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Status { get; set; } = "pending";
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        // 1 - n: 1 user nhiều order 
        public int UserId { get; set; }
        public User User { get; set; }

        // 1 - n: Một order có thể có nhiều cart
        public List<Cart> Carts { get; set; }

    }
}
