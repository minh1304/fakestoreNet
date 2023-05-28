using fakestrore_Net.DTOs.OrderDTO;
using fakestrore_Net.DTOs.UserDTO;
using Microsoft.AspNetCore.Mvc;

namespace fakestrore_Net.Services.UserService
{
    public interface IUserService
    {
        Task<ActionResult<List<Order>>> AddOrder(OrderCreateDTO request);
        Task<ActionResult<List<UserGetDTO>>> GetUser();

    }
}
