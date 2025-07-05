namespace Application.Interfaces.IServices;

public interface ICurrentUserService
{
    Guid UserId { get; }
    string? Role { get; }
    bool IsInRole(string role);
}
