using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace fakestrore_Net.Models
{
    public class Rating
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Rate { get; set; }
        public int Count { get; set; }

        [ForeignKey("Product")] // Specify the foreign key property name
        public int ProductId { get; set; } // Foreign key property
        public Product? Product { get; set; }


    }
}
