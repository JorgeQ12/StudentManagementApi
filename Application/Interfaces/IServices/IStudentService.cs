using Application.DTOs.Student;
using Application.DTOs;
using Application.DTOs.Common;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces.IServices;

public interface IStudentService
{
    /// <summary>
    /// Obtener todos los estudiantes - Panel Admin y Estudiante
    /// </summary>
    /// <returns></returns>
    Task<ResultRequestDTO<IEnumerable<StudentProfileDTO>>> GetAllStudents();

    /// <summary>
    /// Obtener información personal del estudiante - Panel Estudiante
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    Task<ResultRequestDTO<StudentProfileDTO>> GetStudentProfileAsync(Guid studentId);

    /// <summary>
    /// lista de estudiantes junto con las materias en las que están inscritos - Panel Estudiante
    /// </summary>
    /// <returns></returns>
    Task<ResultRequestDTO<IEnumerable<StudentsWithSubjectsDTO>>> GetAllStudentsWithSubjectsAsync();

    /// <summary>
    /// Obtener detalle de cada clase registrada por el estudiante - Panel Estudiante
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    Task<ResultRequestDTO<IEnumerable<StudentClassDetailsDTO>>> GetStudentClassDetailsAsync(Guid studentId);

    /// <summary>
    /// Inscribir materias - Panel Estudiante
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="subjectIds"></param>
    /// <returns></returns>
    Task<ResultRequestDTO<string>> EnrollStudentInSubjectsAsync(Guid studentId, IEnumerable<Guid> subjectIds);

    /// <summary>
    /// Actualizar información personal del estudiante - Panel Estudiante
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ResultRequestDTO<string>> UpdateStudentProfileAsync(UpdateStudentRequestDTO request);

    /// <summary>
    /// Eliminar materia registrada por usuario - Panel Estudiante
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="subjectId"></param>
    /// <returns></returns>
    Task<ResultRequestDTO<string>> DeleteEnrollInSubjects(Guid studentId, Guid subjectId);

    /// <summary>
    /// Eliminar el perfil del estudiante registrado - Panel Admin
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    Task<ResultRequestDTO<string>> DeleteStudentProfile(Guid studentId);
}
