using fakestrore_Net.Data;
using fakestrore_Net.DTOs.UserDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fakestrore_Net.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;

        public AuthService(DataContext context)
        {
            _context = context;
        }


        public async Task<ActionResult<User>> Register(UserCreateDTO request)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);

            if (existingUser != null)
            {
                return null;
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var newUser = new User
            {
                UserName = request.UserName,
                UserEmail = request.UserEmail,
                PasswordHash = passwordHash,
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;


        }
    }
}
