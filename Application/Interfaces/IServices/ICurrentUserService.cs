namespace Application.Interfaces.IServices;

public interface ICurrentUserService
{
    Guid UserId { get; }
    Guid StudentId { get; }
}
