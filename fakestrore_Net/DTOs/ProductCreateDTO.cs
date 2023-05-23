using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace fakestrore_Net.DTOs
{
    public class ProductCreateDTO
    {
        public string Title { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        //n-1
        // Để vì có ràng buộc thuộc category nào
        /*public int CategoryID { get; set; }*/
        public string Image { get; set; }
        //1-1
        public RatingCreateDTO Rating { get; set; }
    }
}
