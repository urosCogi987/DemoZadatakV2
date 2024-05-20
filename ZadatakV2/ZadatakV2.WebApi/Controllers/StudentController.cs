using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZadatakV2.Dto.Models;
using ZadatakV2.Service.Abstractions;

namespace ZadatakV2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class StudentController(IStudentService studentService) : ControllerBase
    {        
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentRequest studentRequest)
        {            
            await studentService.AddStudentAsync(studentRequest);
            return Ok();
        }
    }
}
