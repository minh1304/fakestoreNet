namespace fakestrore_Net.DTOs.OrderDTO
{
    public class OrderCreateDTO
    {
        public int UserId { get; set; }
        public required List<int> ProductIds { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
