using Domain.ValueObjects;

namespace Application.DTOs.Auth;

public class RegisterRequestDTO
{
    public string Username { get; set; }
    public string Password { get; set; }

    public FullName FullName { get; set; }
    public StudentInfo Info { get; set; }
}
