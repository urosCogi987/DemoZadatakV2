using Microsoft.AspNetCore.Mvc;
using ZadatakV2.Dto.Models;
using ZadatakV2.Service.Abstractions;

namespace ZadatakV2.WebApi.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]    
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
            => _subjectService = subjectService;

        [HttpPost]        
        public async Task<IActionResult> Add(AddSubjectRequest studentRequest)
        {
            long id = await _subjectService.AddStudentAsync(studentRequest);
            return Ok(id);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            await _subjectService.DeleteSubjectAsync(id);
            return Ok();
        }
    }
}
