namespace fakestrore_Net.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        // 1 - n: Một order thuộc về một user
        public int UserId { get; set; }
        public User User { get; set; }

        // 1 - n: Một order có thể có nhiều cart
        public List<Cart> Carts { get; set; }

    }
}
