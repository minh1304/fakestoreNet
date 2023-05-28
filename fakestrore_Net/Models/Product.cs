using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;


namespace fakestrore_Net.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        //n-1
        public int CategoryID { get; set; }
        public Category? Category { get; set; }
        public string Image { get; set; } = string.Empty;
        //1-1
        public Rating? Rating { get; set; }

        // 1 - n: 1 product có thể thuộc nhiều đơn hàng khác nhau 
        public List<Order>? Orders { get; set; }



    }
}
