using fakestrore_Net.Data;
using fakestrore_Net.DTOs.UserDTO;
using fakestrore_Net.Services.AuthService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService : IAuthService
{
    private readonly DataContext _context;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    private string GetSecretKey()
    {
        return _configuration["AppSettings:Token"];
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
            PasswordHash = passwordHash
        };
        if (!string.IsNullOrEmpty(request.Role))
        {
            newUser.Role = request.Role;
        }
        else
        {
            newUser.Role = "Customer"; // Nếu không truyền giá trị cho Role, mặc định sẽ là "customer"
        }


        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return newUser;
    }

    public async Task<ActionResult<string>> Login(UserLoginDTO request)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);

        if (existingUser == null)
        {
            return null;
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, existingUser.PasswordHash))
        {
            return null;
        }

        string token = CreateToken(existingUser);
        return token;
    }

    private string CreateToken(User user)
    {
        var secretKey = Encoding.UTF8.GetBytes(GetSecretKey()); // Get the secret key as a byte array
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Role, user.Role)
    };

        var tokenOptions = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(secretKey), // Use the secret key for signing
                SecurityAlgorithms.HmacSha512)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return tokenString;
    }
    public async Task<ActionResult<UserGetDTO>> Information()
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return new ActionResult<UserGetDTO>(new BadRequestResult()); // Người dùng chưa đăng nhập, trả về mã lỗi 401
        }
        var existingUser = await _context.Users.FindAsync(int.Parse(userId));

        if (existingUser == null)
        {
            return new ActionResult<UserGetDTO>(new NotFoundResult()); // Người dùng không tồn tại, trả về mã lỗi 404
        }

        var userDto = new UserGetDTO
        {
            Id = existingUser.Id,
            UserName = existingUser.UserName,
            UserEmail = existingUser.UserEmail,
            Role = existingUser.Role

            // Các thuộc tính khác của đối tượng UserGetDTO
        };

        return userDto;

    }
}
