using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZadatakV2.Dto.Models;
using ZadatakV2.Service.Abstractions;

namespace ZadatakV2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class GradeController(IGradeService gradeService) : ControllerBase
    {       
        [HttpPost]   
        public async Task<IActionResult> Add(AddGradeRequest gradeRequest)
        {
            await gradeService.AddGradeAsync(gradeRequest);
            return Ok();
        }
    }
}
