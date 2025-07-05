using Application.Interfaces.IRepository;
using Application.Interfaces.IRepository.Common;
using Domain.Entities;

namespace Infrastructure.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly IGenericRepository _generic;

    public AuthRepository(IGenericRepository generic)
    {
        _generic = generic;
    }

    public async Task RegisterNewStudentAsync(User user, Student student)
    {
        await _generic.ExecuteProcedureAsync("SP_RegisterStudentAndUser", new
        {
            StudentId = student.Id,
            FirstName = student.FullName.FirstName,
            MiddleName = student.FullName.MiddleName,
            LastName = student.FullName.LastName,
            SecondLastName = student.FullName.SecondLastName,
            Email = student.Info.Email,
            DocumentType = student.Info.DocumentType,
            DocumentNumber = student.Info.DocumentNumber,
            PhoneNumber = student.Info.PhoneNumber,
            BirthDate = student.Info.BirthDate,
            Gender = student.Info.Gender,
            Address = student.Info.Address,
            EnrollmentDate = student.Info.EnrollmentDate,
            Username = user.Username,
            PasswordHash = user.PasswordHash,
            Role = user.Role,
            UserId = user.Id
        });
    }

    public async Task<User?> FindUserByUsernameAsync(string username)
    {
        return await _generic.GetProcedureSingleAsync<User>("SP_GetUserByUsername", new
        {
            Username = username
        });
    }
}
