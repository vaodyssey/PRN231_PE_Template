
using Microsoft.AspNetCore.Mvc;
using Repository.Payload.Request.Login;
using Repository.Payload.Response;
using Repository.Services;


namespace Client.Controllers
{
    public class AuthenticationController : Controller
    {
        private IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService) { 
            _authenticationService = authenticationService; 
        }
        [HttpPost("/login")]
        public IActionResult Login([FromBody]LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _authenticationService.Login(loginRequest);
            if(result.StatusCode==401)
            {
                return Unauthorized(result); 
            }
            return Ok(result);
           
        }
        
    }
}
