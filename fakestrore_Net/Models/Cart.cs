namespace fakestrore_Net.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime CartDate { get; set; }

        // 1 - n: Một user có thể có nhiều cart
        public int UserId { get; set; }
        public User User { get; set; }

        // n - n: Một cart có thể có nhiều product, 1 product cũng có thể có trong nhiều cart
        public List<CartProduct> CartProducts { get; set; }

    }
}
