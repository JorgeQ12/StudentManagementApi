using Application.DTOs.Auth;
using Application.DTOs.Common;
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

        /// <summary>
        /// Registrar estudiante y usuario login - General
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(AuthController.RegisterStudentAsync))]
        public async Task<ResultRequestDTO<string>> RegisterStudentAsync([FromBody] RegisterRequestDTO registerRequest) => await _authService.RegisterStudentAsync(registerRequest);

        /// <summary>
        /// Autenticacion - General
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>

        [HttpPost]
        [Route(nameof(AuthController.AuthenticateAsync))]
        public async Task<ResultRequestDTO<string>> AuthenticateAsync([FromBody] LoginRequestDTO loginRequest) => await _authService.AuthenticateAsync(loginRequest);
    }
}
