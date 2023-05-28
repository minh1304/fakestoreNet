namespace fakestrore_Net.DTOs
{
    public class OrderCreateDTO
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
