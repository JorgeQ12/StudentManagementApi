using Domain.Entities;

namespace Domain.Interface.IAuth;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
