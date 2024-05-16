using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZadatakV2.Service.Abstractions;

namespace ZadatakV2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)        
            => _userService = userService;

        [HttpPut("block/{id}")]
        public async Task<IActionResult> BlockUser(long id)
        {
            await _userService.BlockUserAsync(id);
            return Ok();
        }
    }
}
