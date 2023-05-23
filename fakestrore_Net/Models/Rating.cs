using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace fakestrore_Net.Models
{
    public class Rating
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Rate { get; set; }
        public int Count { get; set; }
        public int ProductID { get; set; }
        [JsonIgnore]
        public Product? Product { get; set; }

    }
}
