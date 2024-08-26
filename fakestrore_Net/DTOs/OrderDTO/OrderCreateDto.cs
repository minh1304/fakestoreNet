namespace fakestrore_Net.DTOs.OrderDTO
{
    public class OrderCreateDto
    {
        public List<OrderProductCreateDto> Products { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
    }
}
