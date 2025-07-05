using Application.DTOs;
using Application.DTOs.Student;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Student")]
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
    /// Obtener información personal del estudiante.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetStudentProfile))]
    public async Task<StudentProfileDTO> GetStudentProfile() => await _studentService.GetStudentProfileAsync(_currentUser.UserId);

    /// <summary>
    /// Actualizar información personal del estudiante
    /// </summary>
    [HttpPut]
    [Route(nameof(UpdateStudentProfile))]
    public async Task UpdateStudentProfile([FromBody] UpdateStudentRequestDTO request) => await _studentService.UpdateStudentProfileAsync(_currentUser.UserId, request);

    /// <summary>
    /// Inscribir materias
    /// </summary>
    [HttpPost]
    [Route(nameof(EnrollInSubjects))]
    public async Task EnrollInSubjects([FromBody] SubjectEnrollmentRequestDTO request) => await _studentService.EnrollStudentInSubjectsAsync(_currentUser.UserId, request.SubjectIds);

    /// <summary>
    /// Obtener las materias inscritas del estudiante
    /// </summary>
    [HttpGet]
    [Route(nameof(GetSubjectsEnrolled))]
    public async Task<IEnumerable<SubjectWithProfessorDTO>> GetSubjectsEnrolled() => await _studentService.GetSubjectsEnrolledByStudentAsync(_currentUser.UserId);

    /// <summary>
    /// Obtener compañeros agrupados por cada materia compartida
    /// </summary>
    [HttpGet]
    [Route(nameof(GetClassmatesBySubject))]
    public async Task<IEnumerable<ClassmatesBySubjectDTO>> GetClassmatesBySubject() => await _studentService.GetClassmatesGroupedBySubjectAsync(_currentUser.UserId);
}
