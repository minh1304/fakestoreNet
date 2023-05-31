using fakestrore_Net.DTOs.UserDTO;
using Microsoft.AspNetCore.Mvc;

namespace fakestrore_Net.Services.AuthService
{
    public interface IAuthService
    {
        Task<ActionResult<User>> Register(UserCreateDTO request);
        Task<ActionResult<string>> Login(UserLoginDTO request);
    }
}
