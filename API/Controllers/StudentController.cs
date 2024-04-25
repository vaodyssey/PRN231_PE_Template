using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Constants;
using Repository.Payload.Request.StudentService;
using Repository.Services;


namespace API.Controllers
{

    public class StudentController : Controller
    {
        private IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpGet("/student")]
        [Authorize(Policy = "StaffOnly")]
        public IActionResult Get([FromQuery] int groupId,
            [FromQuery] DateTime minBirthYear,
            [FromQuery] DateTime maxBirthYear,
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize)
        {
            var result = _studentService.Get(new GetStudentListRequest()
            {
                GroupId = groupId,
                MinBirthYear = minBirthYear,
                MaxBirthYear = maxBirthYear,
                PageIndex = pageIndex,
                PageSize = pageSize
            });
            if (result.StatusCode == 401)
            {
                return Unauthorized(result);
            }
            return StatusCode(result.StatusCode,result);

        }
        [HttpGet("/student/{id}")]
        [Authorize(Policy = "StaffOnly")]
        public IActionResult GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _studentService.GetById(id);
            if (result.StatusCode == 401)
            {
                return Unauthorized(result);
            }
            return StatusCode(result.StatusCode, result);

        }
        [HttpPost("/student")]
        [Authorize(Policy = "StaffOnly")]
        public IActionResult Create([FromBody] NewStudentRequest studentRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _studentService.Create(studentRequest);
            if (result.StatusCode == 401)
            {
                return Unauthorized(result);
            }
            return StatusCode(result.StatusCode,result);

        }
        [HttpDelete("/student/{id}")]
        [Authorize(Policy = "StaffOnly")]
        public IActionResult Delete([FromRoute] int id)
        {
            var result = _studentService.Delete(id);
            if (result.StatusCode == 401)
            {
                return Unauthorized(result);
            }
            return StatusCode(result.StatusCode, result);

        }
        [HttpPut("/student")]
        [Authorize(Policy = "StaffOnly")]
        public IActionResult Update([FromBody] UpdateStudentRequest updateStudentRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _studentService.Update(updateStudentRequest);

            if (result.StatusCode == 401)
            {
                return Unauthorized(result);
            }
            return StatusCode(result.StatusCode, result);

        }
    } 
}
