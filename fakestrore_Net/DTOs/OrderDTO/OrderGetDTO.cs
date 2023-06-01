using fakestrore_Net.DTOs.ProductDTO;

namespace fakestrore_Net.DTOs.OrderDTO
{
    public class OrderGetDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }

        public List<ProductGetDTO>? Products { get; set; }
    }
}
