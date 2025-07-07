using Application.DTOs.Common;
using Application.DTOs.Student;
using Domain.Entities;

namespace Application.Interfaces.IRepository
{
    public interface IStudentRepository
    {
        /// <summary>
        /// Obtener todos los estudiantes - Panel Admin y Estudiante
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<StudentProfileDTO>> GetAllStudents();

        /// <summary>
        /// Obtener información personal del estudiante - Panel Estudiante
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task<Student?> GetStudentByIdAsync(Guid studentId);

        /// <summary>
        /// lista de estudiantes junto con las materias en las que están inscritos - Panel Estudiante
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<StudentsWithSubjectsDTO>> GetAllStudentsWithSubjectsAsync();

        /// <summary>
        /// Obtener detalle de cada clase registrada por el estudiante - Panel Estudiante
        ///</summary>
        /// <param name="studentId">ID del estudiante actual</param>
        Task<IEnumerable<StudentClassDetailsDTO>> GetStudentClassDetailsAsync(Guid studentId);

        /// <summary>
        /// Inscribir materias - Panel Estudiante
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="subjectIds"></param>
        /// <returns></returns>
        Task EnrollStudentInSubjectsAsync(Guid studentId, IEnumerable<Guid> subjectIds);

        /// <summary>
        /// Actualizar información personal del estudiante - Panel Estudiante
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        Task UpdateStudentAsync(Student student);

        /// <summary>
        /// Eliminar materia registrada por usuario - Panel Estudiante
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        Task DeleteEnrollInSubjects(Guid studentId, Guid subjectId);

        /// <summary>
        /// Eliminar el perfil del estudiante registrado - Panel Admin
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task DeleteStudentProfile(Guid studentId);
    }
}
