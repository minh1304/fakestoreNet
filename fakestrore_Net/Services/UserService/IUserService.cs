using fakestrore_Net.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace fakestrore_Net.Services.UserService
{
    public interface IUserService
    {
        Task<ActionResult<List<Order>>> AddOrder(OrderCreateDTO request);
    }
}
