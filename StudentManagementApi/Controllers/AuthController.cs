using Application.DTOs.Auth;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route(nameof(AuthController.RegisterStudentAsync))]
        public async Task RegisterStudentAsync([FromBody] RegisterRequestDTO registerStudent) => await _authService.RegisterStudentAsync(registerStudent);

        [HttpPost]
        [Route(nameof(AuthController.AuthenticateAsync))]
        public async Task<string> AuthenticateAsync([FromBody] LoginRequestDTO request) => await _authService.AuthenticateAsync(request.Username, request.Password);
    }
}
