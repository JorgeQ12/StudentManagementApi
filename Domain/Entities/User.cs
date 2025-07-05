using Domain.Interface.IAuth;

namespace Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string PasswordHash { get; private set; }
    public string Role { get; private set; }
    public Guid? StudentId { get; private set; }


    public User(Guid id, string username, string passwordHash, string role, Guid? studentId = null)
    {
        Id = id;
        Username = username;
        PasswordHash = passwordHash;
        Role = role;
        StudentId = studentId;
    }

    public bool VerifyPassword(string plainPassword, IPasswordHasher hasher)
    {
        return hasher.Verify(PasswordHash, plainPassword);
    }
}
