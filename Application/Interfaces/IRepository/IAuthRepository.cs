using Domain.Entities;

namespace Application.Interfaces.IRepository;

public interface IAuthRepository
{
    /// <summary>
    /// Registrar un nuevo estudiante.
    /// </summary>
    Task RegisterNewStudentAsync(User user, Student student);

    /// <summary>
    /// Obtiene un usuario autenticable a partir de su nombre de usuario.
    /// </summary>
    Task<User?> FindUserByUsernameAsync(string username);
}
