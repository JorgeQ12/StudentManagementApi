using Application.DTOs.Student;
using Application.DTOs;

namespace Application.Interfaces.IServices;

public interface IStudentService
{
    /// <summary>
    /// Obtener el perfil del estudiante
    /// </summary>
    Task<StudentProfileDTO> GetStudentProfileAsync(Guid studentId);

    /// <summary>
    /// Actualizar los datos personales del estudiante
    /// </summary>
    Task UpdateStudentProfileAsync(Guid studentId, UpdateStudentRequestDTO request);

    /// <summary>
    /// Inscribir al estudiante en exactamente 3 materias con profesores distintos
    /// </summary>
    Task EnrollStudentInSubjectsAsync(Guid studentId, IEnumerable<Guid> subjectIds);

    /// <summary>
    /// Obtener las materias en las que el estudiante está inscrito
    /// </summary>
    Task<IEnumerable<SubjectWithProfessorDTO>> GetSubjectsEnrolledByStudentAsync(Guid studentId);

    /// <summary>
    /// Obtener los compañeros agrupados por cada materia compartida
    /// </summary>
    Task<IEnumerable<ClassmatesBySubjectDTO>> GetClassmatesGroupedBySubjectAsync(Guid studentId);
}
