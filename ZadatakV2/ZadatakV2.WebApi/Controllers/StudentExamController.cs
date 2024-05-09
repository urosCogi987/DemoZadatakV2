using Microsoft.AspNetCore.Mvc;
using ZadatakV2.Dto.Models;
using ZadatakV2.Service.Abstractions;

namespace ZadatakV2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentExamController : ControllerBase
    {
        private readonly IStudentExamService _studentExamService;

        public StudentExamController(IStudentExamService studentExamService)
            => _studentExamService = studentExamService;

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentExamRequest studentExamRequest)
        {
            long id = await _studentExamService.AddStudentExamAsync(studentExamRequest);
            return Ok(id);
        }

    }
}
