using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZadatakV2.Dto.Models;
using ZadatakV2.Service.Abstractions;

namespace ZadatakV2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]    
    public class SubjectController(ISubjectService subjectService) : ControllerBase
    {       
        [HttpPost]        
        public async Task<IActionResult> Add(AddSubjectRequest studentRequest)
        {
            await subjectService.AddSubjectAsync(studentRequest);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            await subjectService.DeleteSubjectAsync(id);
            return Ok();
        }
    }
}
