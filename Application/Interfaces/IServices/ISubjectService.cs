using Application.DTOs.Common;
using Application.DTOs.Student;
using Application.DTOs.Subject;

namespace Application.Interfaces.IServices;

public interface ISubjectService
{
    /// <summary>
    /// Trae todas las materias registradas - Panel Admin y Estudiante
    /// </summary>
    /// <returns></returns>
    Task<ResultRequestDTO<IEnumerable<SubjectWithProfessorDTO>>> GetAllSubjectsAsync();

    /// <summary>
    /// Registra una nueva materia - Panel Admin
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ResultRequestDTO<string>> CreateSubjectAsync(CreateSubjectRequestDTO request);

    /// <summary>
    /// Elimina una materia por su ID - Panel Admin
    /// </summary>
    /// <param name="subjectId"></param>
    /// <returns></returns>
    Task<ResultRequestDTO<string>> DeleteSubjectAsync(Guid subjectId);
}
