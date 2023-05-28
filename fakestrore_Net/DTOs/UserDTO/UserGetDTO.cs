using fakestrore_Net.DTOs.OrderDTO;

namespace fakestrore_Net.DTOs.UserDTO
{
    public class UserGetDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        // 1 - n 
        public List<OrderGetDTO>? Orders { get; set; }

    }
}
