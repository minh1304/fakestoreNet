namespace fakestrore_Net.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        // 1 - n: Một user có thể có nhiều cart
        public List<Cart> Carts { get; set; }

    }
}
