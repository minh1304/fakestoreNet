using fakestrore_Net.DTOs.CartDTO;

namespace fakestrore_Net.DTOs.UserDTO
{
    public class UserGetDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        // 1 - n 
        public List<CartGetDTO>? Carts { get; set; }

    }
}
