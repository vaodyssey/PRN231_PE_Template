using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Constants;
using Repository.Payload.Request.StudentService;
using Repository.Services;
using System.Security.Claims;

namespace API.Controllers
{
    public class StudentController : Controller
    {
        private IStudentService _studentService;
        
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;        
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
            return Ok(result);

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
            return Ok(result);

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
            return Ok(result);

        }
    }
}
