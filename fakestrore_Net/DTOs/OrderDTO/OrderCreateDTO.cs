using fakestrore_Net.DTOs.CartDTO;

namespace fakestrore_Net.DTOs.OrderDTO
{
    public class OrderCreateDTO
    {
        public string CustomerName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<CartCreateDTO> Carts { get; set; }
    }
}
