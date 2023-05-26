using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace fakestrore_Net.DTOs
{
    public class ProductGetDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { set; get; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        //1-1
        public required RatingGetDTO Rating { get; set; }
    }
}
