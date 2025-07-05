using Application.DTOs.Student;
using Application.DTOs.Subject;
using Domain.Entities;

namespace Application.Interfaces.IRepository;

public interface ISubjectRepository
{
    /// <summary>
    /// Trae todas las materias registradas
    /// </summary>
    Task<IEnumerable<SubjectResponseDTO>> GetAllSubjectsAsync();

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
    /// Obtiene todas las materias inscritas por un estudiante
    /// </summary>
    Task<IEnumerable<SubjectWithProfessorDTO>> GetSubjectsByStudentIdAsync(Guid studentId);

    /// <summary>
    /// Registra una nueva materia
    /// </summary>
    Task CreateSubjectAsync(Subject subject);

    /// <summary>
    /// Elimina una materia por su ID
    /// </summary>
    Task DeleteSubjectAsync(Guid subjectId);
}
