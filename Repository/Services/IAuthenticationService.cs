using Repository.Payload.Request.Login;
using Repository.Payload.Response.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public interface IAuthenticationService
    {
        public LoginResponse Login(LoginRequest loginRequest);
    }
}
