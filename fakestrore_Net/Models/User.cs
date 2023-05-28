namespace fakestrore_Net.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        // 1 - n 
        public List<Order>? Orders { get; set; }


    }
}
