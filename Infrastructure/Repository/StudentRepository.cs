using Application.Interfaces.IRepository;
using Application.Interfaces.IRepository.Common;
using Domain.Entities;

namespace Infrastructure.Repository;

public class StudentRepository : IStudentRepository
{
    private readonly IGenericRepository _genericRepository;

    public StudentRepository(IGenericRepository generic)
    {
        _genericRepository = generic;
    }

    public async Task<Student?> GetStudentByIdAsync(Guid studentId)
    {
        return await _genericRepository.GetProcedureSingleAsync<Student>("SP_GetStudentById", new { StudentId = studentId });
    }

    public async Task UpdateStudentAsync(Student student)
    {
        await _genericRepository.ExecuteProcedureAsync("SP_UpdateStudentInfo", new
        {
            StudentId = student.Id,
            Email = student.Info.Email,
            DocumentType = student.Info.DocumentType,
            DocumentNumber = student.Info.DocumentNumber,
            PhoneNumber = student.Info.PhoneNumber,
            BirthDate = student.Info.BirthDate,
            Gender = student.Info.Gender,
            Address = student.Info.Address
        });
    }

    public async Task SaveSubjectsForStudentAsync(Guid studentId, IEnumerable<Guid> subjectIds)
    {
        await _genericRepository.ExecuteProcedureAsync("SP_EnrollStudentSubjects", new
        {
            StudentId = studentId,
            SubjectId1 = subjectIds.ElementAtOrDefault(0),
            SubjectId2 = subjectIds.ElementAtOrDefault(1),
            SubjectId3 = subjectIds.ElementAtOrDefault(2)
        });
    }

    public async Task<IEnumerable<dynamic>> GetClassmatesGroupedBySubjectAsync(Guid studentId)
    {
        return await _genericRepository.GetProcedureAsync<dynamic>("SP_GetClassmatesBySubject", new { StudentId = studentId });
    }

}
