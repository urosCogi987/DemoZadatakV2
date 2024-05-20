using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZadatakV2.Service.Abstractions;

namespace ZadatakV2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {       
        [HttpPut("block/{id}")]
        public async Task<IActionResult> BlockUser(long id)
        {
            await userService.BlockUserAsync(id);
            return Ok();
        }
    }
}
