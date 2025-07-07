using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using Domain.Entities;
using Domain.Exceptions;
using Application.DTOs.Auth;
using Domain.Interface.IAuth;
using Application.DTOs.Common;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.Data;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IAuthRepository authRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator, ILogger<AuthService> logger)
    {
        _authRepository = authRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _logger = logger;
    }

    /// <summary>
    /// Registrar estudiante y usuario login - General
    /// </summary>
    /// <param name="registerRequest"></param>
    /// <returns></returns>
    public async Task<ResultRequestDTO<string>> RegisterStudentAsync(RegisterRequestDTO registerRequest)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(registerRequest.Username) || string.IsNullOrWhiteSpace(registerRequest.Password))
                throw new DomainException("Username and password are required.");

            var student = new Student(Guid.NewGuid(), registerRequest.FullName, registerRequest.Info);

            var hashedPassword = _passwordHasher.Hash(registerRequest.Password);

            var user = new User(
                id: Guid.NewGuid(),
                username: registerRequest.Username,
                passwordHash: hashedPassword,
                role: "Student",
                studentId: student.Id
            );

            await _authRepository.RegisterNewStudentAsync(user, student);

            return ResultRequestDTO<string>.Success("Student created successfully");
        }
        catch (DomainException exDomain)
        {
            _logger.LogError(exDomain, "Error occurred during student registration.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during student registration.");
            throw;
        }
    }

    /// <summary>
    /// Autenticacion - General
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    public async Task<ResultRequestDTO<string>> AuthenticateAsync(LoginRequestDTO loginRequest)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(loginRequest.Username) || string.IsNullOrWhiteSpace(loginRequest.Password))
                return ResultRequestDTO<string>.Failure("Username and password are required.");

            var user = await _authRepository.FindUserByUsernameAsync(loginRequest.Username);
            if (user is null || !_passwordHasher.Verify(user.PasswordHash, loginRequest.Password))
                return ResultRequestDTO<string>.Failure("Invalid username or password.");

            if (!_passwordHasher.Verify(user.PasswordHash, loginRequest.Password))
                return ResultRequestDTO<string>.Failure("Invalid username or password.");

            return ResultRequestDTO<string>.Success(_jwtTokenGenerator.GenerateToken(user));
        }
        catch (DomainException exDomain)
        {
            _logger.LogError(exDomain, "Error authenticating user: {Username}", loginRequest.Username);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error authenticating user: {Username}", loginRequest.Username);
            throw;
        }
    }
}
