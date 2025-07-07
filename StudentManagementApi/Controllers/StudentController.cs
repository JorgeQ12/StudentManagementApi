using Application.DTOs;
using Application.DTOs.Common;
using Application.DTOs.Student;
using Application.Interfaces.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.VisualStudio.Services.Graph.GraphResourceIds;

namespace StudentManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly ICurrentUserService _currentUser;

    public StudentController(IStudentService studentService, ICurrentUserService currentUser)
    {
        _studentService = studentService;
        _currentUser = currentUser;
    }

    /// <summary>
    /// Obtener todos los estudiantes - Panel Admin y Estudiante
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = "Admin, Student")]
    [Route(nameof(GetAllStudents))]
    public async Task<ResultRequestDTO<IEnumerable<StudentProfileDTO>>> GetAllStudents() => await _studentService.GetAllStudents();

    /// <summary>
    /// Obtener información personal del estudiante - Panel Estudiante
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = "Student")]
    [Route(nameof(GetStudentProfile))]
    public async Task<ResultRequestDTO<StudentProfileDTO>> GetStudentProfile() => await _studentService.GetStudentProfileAsync(_currentUser.StudentId);

    /// <summary>
    /// lista de estudiantes junto con las materias en las que están inscritos - Panel Estudiante
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = "Student")]
    [Route(nameof(GetAllStudentsWithSubjects))]
    public async Task<ResultRequestDTO<IEnumerable<StudentsWithSubjectsDTO>>> GetAllStudentsWithSubjects() => await _studentService.GetAllStudentsWithSubjectsAsync();

    /// <summary>
    /// Obtener detalle de cada clase registrada por el estudiante - Panel Estudiante
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = "Student")]
    [Route(nameof(GetStudentClassDetails))]
    public async Task<ResultRequestDTO<IEnumerable<StudentClassDetailsDTO>>> GetStudentClassDetails() => await _studentService.GetStudentClassDetailsAsync(_currentUser.StudentId);

    /// <summary>
    /// Inscribir materias - Panel Estudiante
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Student")]
    [Route(nameof(EnrollInSubjects))]
    public async Task<ResultRequestDTO<string>> EnrollInSubjects([FromBody] SubjectEnrollmentRequestDTO request) => await _studentService.EnrollStudentInSubjectsAsync(_currentUser.StudentId, request.SubjectIds);

    /// <summary>
    /// Actualizar información personal del estudiante - Panel Admin y Estudiante
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize(Roles = "Admin, Student")]
    [Route(nameof(UpdateStudentProfile))]
    public async Task<ResultRequestDTO<string>> UpdateStudentProfile([FromBody] UpdateStudentRequestDTO request) => await _studentService.UpdateStudentProfileAsync(request);

    /// <summary>
    /// Eliminar materia registrada por usuario - Panel Estudiante
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpDelete]
    [Authorize(Roles = "Student")]
    [Route(nameof(DeleteEnrollInSubjects))]
    public async Task<ResultRequestDTO<string>> DeleteEnrollInSubjects([FromBody] DeleteEnrollmentRequestDTO request) => await _studentService.DeleteEnrollInSubjects(_currentUser.StudentId, request.SubjectId);

    /// <summary>
    /// Eliminar el perfil del estudiante registrado - Panel Admin
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route(nameof(DeleteStudentProfile))]
    public async Task<ResultRequestDTO<string>> DeleteStudentProfile([FromBody] DeleteStudentProfileDTO request) => await _studentService.DeleteStudentProfile(request.Id);
}
