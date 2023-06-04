using fakestrore_Net.DTOs.OrderDTO;
using Microsoft.AspNetCore.Mvc;

namespace fakestrore_Net.Services.UserService
{
    public interface IUserService
    {
        Task<ActionResult<List<Cart>>> AddOrder(CartCreateDTO request);


    }
}
