using Application.DTOs.Student;
using Application.DTOs.Subject;
using Application.Interfaces.IRepository;
using Application.Interfaces.IRepository.Common;
using Domain.Entities;

namespace Infrastructure.Repository;

public class SubjectRepository : ISubjectRepository
{
    private readonly IGenericRepository _generic;

    public SubjectRepository(IGenericRepository generic)
    {
        _generic = generic;
    }

    public async Task<IEnumerable<SubjectResponseDTO>> GetAllSubjectsAsync()
    {
        return await _generic.GetProcedureAsync<SubjectResponseDTO>("SP_GetAllSubjects", new { });
    }

    public async Task<SubjectResponseDTO?> GetSubjectByIdAsync(Guid subjectId)
    {
        return await _generic.GetProcedureSingleAsync<SubjectResponseDTO>("SP_GetSubjectById", new
        {
            SubjectId = subjectId
        });
    }

    public async Task<IEnumerable<Guid>> GetSubjectIdsByStudentAsync(Guid studentId)
    {
        var result = await _generic.GetProcedureAsync<object>( "SP_GetSubjectIdsByStudent", new { StudentId = studentId });
        return result.Select(x => (Guid)x.GetType().GetProperty("SubjectId")!.GetValue(x)!);
    }

    public async Task<IEnumerable<Subject>> GetSubjectsByIdsAsync(IEnumerable<Guid> subjectIds)
    {
        var ids = string.Join(",", subjectIds);
        return await _generic.GetProcedureAsync<Subject>("SP_GetSubjectsByIds", new { Ids = ids });
    }

    public async Task<IEnumerable<SubjectWithProfessorDTO>> GetSubjectsByStudentIdAsync(Guid studentId)
    {
        return await _generic.GetProcedureAsync<SubjectWithProfessorDTO>("SP_GetSubjectsByStudentId", new { StudentId = studentId });
    }

    public async Task CreateSubjectAsync(Subject subject)
    {
        await _generic.ExecuteProcedureAsync("SP_CreateSubject", new
        {
            subject.Id,
            subject.Name,
            subject.ProfessorId
        });
    }

    public async Task DeleteSubjectAsync(Guid subjectId)
    {
        await _generic.ExecuteProcedureAsync("SP_DeleteSubject", new { SubjectId = subjectId });
    }
}
