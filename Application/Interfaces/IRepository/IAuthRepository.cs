using Domain.Entities;

namespace Application.Interfaces.IRepository;

public interface IAuthRepository
{
    /// <summary>
    /// Registrar estudiante y usuario login - General
    /// </summary>
    /// <param name="user"></param>
    /// <param name="student"></param>
    /// <returns></returns>
    Task RegisterNewStudentAsync(User user, Student student);


    /// <summary>
    /// Autenticacion - General
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    Task<User?> FindUserByUsernameAsync(string username);
}
