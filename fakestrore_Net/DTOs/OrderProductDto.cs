using System.ComponentModel.DataAnnotations.Schema;

namespace fakestrore_Net.DTOs
{
    public class OrderProductDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Title { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;

    }
}
