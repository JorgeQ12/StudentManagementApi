using Application.DTOs.Student;
using Application.DTOs.Subject;
using Domain.Entities;

namespace Application.Interfaces.IRepository;

public interface ISubjectRepository
{
    /// <summary>
    /// Trae todas las materias registradas - Panel Admin y Estudiante
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<SubjectWithProfessorDTO>> GetAllSubjectsAsync();

    /// <summary>
    /// Trae una materia por su ID
    /// </summary>
    Task<SubjectResponseDTO?> GetSubjectByIdAsync(Guid subjectId);

    /// <summary>
    /// Obtiene los IDs de materias inscritas por un estudiante
    /// </summary>
    Task<IEnumerable<Guid>> GetSubjectIdsByStudentAsync(Guid studentId);

    /// <summary>
    /// Obtiene las materias completas a partir de una lista de IDs 
    /// </summary>
    Task<IEnumerable<Subject>> GetSubjectsByIdsAsync(IEnumerable<Guid> subjectIds);

    /// <summary>
    /// Registra una nueva materia - Panel Admin
    /// </summary>
    /// <param name="subject"></param>
    /// <returns></returns>
    Task CreateSubjectAsync(Subject subject);

    /// <summary>
    /// Elimina una materia por su ID - Panel Admin
    /// </summary>
    /// <param name="subjectId"></param>
    /// <returns></returns>
    Task DeleteSubjectAsync(Guid subjectId);
}
