using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZadatakV2.Dto.Models;
using ZadatakV2.Service.Abstractions;

namespace ZadatakV2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService gradeService)        
            => _gradeService = gradeService;

        [HttpPost]   
        public async Task<IActionResult> Add(AddGradeRequest gradeRequest)
        {
            await _gradeService.AddGradeAsync(gradeRequest);
            return Ok();
        }
    }
}
