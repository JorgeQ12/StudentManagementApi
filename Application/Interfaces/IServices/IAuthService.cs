using Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(string username, string password);
        Task RegisterStudentAsync(RegisterRequestDTO request);
    }
}
