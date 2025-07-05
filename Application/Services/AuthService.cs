using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using Domain.Entities;
using Domain.Exceptions;
using Application.DTOs.Auth;
using Domain.Interface.IAuth;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(IAuthRepository authRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
    {
        _authRepository = authRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task RegisterStudentAsync(RegisterRequestDTO request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                throw new DomainException("Username and password are required.");

            var student = new Student(Guid.NewGuid(), request.FullName, request.Info);

            var hashedPassword = _passwordHasher.Hash(request.Password);

            var user = new User(
                id: Guid.NewGuid(),
                username: request.Username,
                passwordHash: hashedPassword,
                role: "Student",
                studentId: student.Id
            );

            await _authRepository.RegisterNewStudentAsync(user, student);
        }
        catch (DomainException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unexpected error occurred during student registration.", ex);
        }
    }

    public async Task<string> AuthenticateAsync(string username, string password)
    {
        try
        {
            var user = await _authRepository.FindUserByUsernameAsync(username) ?? throw new DomainException("Invalid username or password.");

            if (!_passwordHasher.Verify(user.PasswordHash, password))
                throw new DomainException("Invalid username or password.");

            return _jwtTokenGenerator.GenerateToken(user);
        }
        catch (DomainException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unexpected error occurred during authentication.", ex);
        }
    }
}
