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

    /// <summary>
    /// Trae todas las materias registradas - Panel Admin y Estudiante
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<SubjectWithProfessorDTO>> GetAllSubjectsAsync()
    {
        return await _generic.GetProcedureAsync<SubjectWithProfessorDTO>("SP_GetAllSubjects", new { });
    }

    /// <summary>
    /// Trae una materia por su ID
    /// </summary>
    public async Task<SubjectResponseDTO?> GetSubjectByIdAsync(Guid subjectId)
    {
        return await _generic.GetProcedureSingleAsync<SubjectResponseDTO>("SP_GetSubjectById", new
        {
            SubjectId = subjectId
        });
    }

    /// <summary>
    /// Obtiene los IDs de materias inscritas por un estudiante
    /// </summary>
    public async Task<IEnumerable<Guid>> GetSubjectIdsByStudentAsync(Guid studentId)
    {
        var result = await _generic.GetProcedureAsync<dynamic>(
            "SP_GetSubjectIdsByStudent",
            new { StudentId = studentId }
        );

        return result.Select(x => (Guid)x.SubjectId);
    }

    /// <summary>
    /// Obtiene las materias completas a partir de una lista de IDs 
    /// </summary>
    public async Task<IEnumerable<Subject>> GetSubjectsByIdsAsync(IEnumerable<Guid> subjectIds)
    {
        var ids = string.Join(",", subjectIds);
        return await _generic.GetProcedureAsync<Subject>("SP_GetSubjectsByIds", new { Ids = ids });
    }
   
    /// <summary>
    /// Registra una nueva materia - Panel Admin
    /// </summary>
    /// <param name="subject"></param>
    /// <returns></returns>
    public async Task CreateSubjectAsync(Subject subject)
    {
        await _generic.ExecuteProcedureAsync("SP_CreateSubject", new
        {
            subject.Id,
            subject.Name,
            subject.ProfessorId
        });
    }

    /// <summary>
    /// Elimina una materia por su ID - Panel Admin
    /// </summary>
    /// <param name="subjectId"></param>
    /// <returns></returns>
    public async Task DeleteSubjectAsync(Guid subjectId)
    {
        await _generic.ExecuteProcedureAsync("SP_DeleteSubject", new { SubjectId = subjectId });
    }
}
