using fakestrore_Net.DTOs.RatingDTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace fakestrore_Net.DTOs.ProductDTO
{
    public class ProductCreateInCategoryDTO
    {
        public string Title { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; }
        public RatingCreateDTO Rating { get; set; }
    }
}
