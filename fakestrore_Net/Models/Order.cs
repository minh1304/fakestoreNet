using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace fakestrore_Net.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        // n - 1 : 1 usser có thể có nhiều order 
        public int UserId { get; set; }
        public User? User { get; set; }

        // n - 1 : 1 product có thể trong nhiều order
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }
    }
}
