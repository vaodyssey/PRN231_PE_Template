using Repository.Constants;
using Repository.Entities;
using Repository.Payload.Request.Login;
using Repository.Payload.Response.Login;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services.Implementation
{

    public class AuthenticationService : IAuthenticationService
    {
        private LoginRequest _loginRequest;
        private UserRole _userRole;
        private IUnitOfWork _unitOfWork;
        private string _token;
        public AuthenticationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public LoginResponse Login(LoginRequest loginRequest)
        {
            ResetLoginRequest(loginRequest);
            if (!AreCredentialsValid()) return InvalidLoginCredentialsResponse();
            GenerateToken();
            return SuccessfulLoginResponse();
        }
        private void ResetLoginRequest(LoginRequest loginRequest)
        {
            _loginRequest = null;
            _loginRequest = loginRequest;
        }
        private bool AreCredentialsValid()
        {
            _userRole = _unitOfWork.UserRoleRepository
                .Get(userRole => userRole.Username == _loginRequest.Username)
                .FirstOrDefault()!;

            if (_userRole == null ||
                !_userRole.Passphrase!.Equals(_loginRequest.Password)) return false;
            return true;
        }
        private void GenerateToken()
        {
            _token = JWTService.GenerateToken(new ClaimsParameters()
            {
                Id = _userRole.Id.ToString(),
                UserRole = _userRole.UserRole1.ToString()
            });
        }

        private LoginResponse InvalidLoginCredentialsResponse()
        {
            return new LoginResponse
            {
                Result = "Failure",
                StatusCode = AuthenticationServiceStatusCode.INVALID_CREDENTIALS,
                Message = "Either the username or password is incorrect. Please try again."
            };
        }
        private LoginResponse SuccessfulLoginResponse()
        {
            return new LoginResponse
            {
                Result = "Success",
                StatusCode = AuthenticationServiceStatusCode.SUCCESSFUL_LOGIN,
                Message = "Successfully Logged in.",
                ReturnData = _token
            };
        }
    }

}
