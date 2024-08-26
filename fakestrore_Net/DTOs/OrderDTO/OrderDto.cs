namespace fakestrore_Net.DTOs.OrderDTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderUpdatedDate { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public ICollection<OrderProductDto> OrderProducts { get; set; }
    }
}
