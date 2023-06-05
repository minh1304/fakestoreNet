using fakestrore_Net.DTOs.CartDTO;
using Microsoft.AspNetCore.Mvc;

namespace fakestrore_Net.Services.UserService
{
    public interface IUserService
    {
        Task<ActionResult<List<Cart>>> AddCart(CartCreateDTO request);
        Task<ActionResult<List<CartGetDTO>>> GetCart();



    }
}
