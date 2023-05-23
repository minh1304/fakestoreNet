using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public Category? Category { get; set; }
        public string Image { get; set; } = string.Empty;
        //1-1
        public Rating? Rating { get; set; }

    }
}
