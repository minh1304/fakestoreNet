namespace fakestrore_Net.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        // 1 - n: Một user có thể có nhiều order
        public int UserId { get; set; }
        public User? User { get; set; }

        // n - n: Một order có thể có nhiều product, 1 product cũng có thể có trong nhiều order
        public ICollection<CartProduct> CartProducts { get; set; }
    }
}
