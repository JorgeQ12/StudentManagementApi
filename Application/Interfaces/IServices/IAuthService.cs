using Application.DTOs.Auth;
using Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IAuthService
    {
        /// <summary>
        /// Registrar estudiante y usuario login - General
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns></returns>
        Task<ResultRequestDTO<string>> RegisterStudentAsync(RegisterRequestDTO registerRequest);

        /// <summary>
        /// Autenticacion - General
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        Task<ResultRequestDTO<string>> AuthenticateAsync(LoginRequestDTO loginRequest);
    }
}
