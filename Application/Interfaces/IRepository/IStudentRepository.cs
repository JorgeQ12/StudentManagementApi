using Application.DTOs.Student;
using Domain.Entities;

namespace Application.Interfaces.IRepository
{
    public interface IStudentRepository
    {
        /// <summary>
        /// Obtiene un estudiante por su ID.
        /// </summary>
        /// <param name="studentId">ID del estudiante</param>
        Task<Student?> GetStudentByIdAsync(Guid studentId);

        /// <summary>
        /// Actualiza los datos personales de un estudiante.
        /// </summary>
        /// <param name="student">Entidad Student con los nuevos valores</param>
        Task UpdateStudentAsync(Student student);

        /// <summary>
        /// Asocia tres materias a un estudiante.
        /// </summary>
        /// <param name="studentId">ID del estudiante</param>
        /// <param name="subjectIds">Lista de exactamente tres IDs de materias</param>
        Task SaveSubjectsForStudentAsync(Guid studentId, IEnumerable<Guid> subjectIds);

        /// <summary>
        /// Obtiene los compañeros por cada materia inscrita por el estudiante.
        /// </summary>
        /// <param name="studentId">ID del estudiante actual</param>
        Task<IEnumerable<dynamic>> GetClassmatesGroupedBySubjectAsync(Guid studentId);
    }
}
