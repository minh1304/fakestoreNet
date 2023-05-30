using fakestrore_Net.DTOs.RatingDTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace fakestrore_Net.DTOs.ProductDTO
{
    public class ProductGetDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Description { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Category { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Image { get; set; }
        //1-1
        public RatingGetDTO? Rating { get; set; }

        public int Quantity { get; set; }
    }
}
