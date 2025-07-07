using Application.DTOs.Common;
using Application.DTOs.Student;
using Application.DTOs.Subject;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectController : ControllerBase
{
    private readonly ISubjectService _subjectService;

    public SubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    /// <summary>
    /// Trae todas las materias registradas - Panel Admin y Estudiante
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = "Admin, Student")]
    [Route(nameof(GetAllSubjects))]
    public async Task<ResultRequestDTO<IEnumerable<SubjectWithProfessorDTO>>> GetAllSubjects() => await _subjectService.GetAllSubjectsAsync();

    /// <summary>
    /// Registra una nueva materia - Panel Admin
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route(nameof(CreateSubject))]
    public async Task<ResultRequestDTO<string>> CreateSubject(CreateSubjectRequestDTO request) => await _subjectService.CreateSubjectAsync(request);

    /// <summary>
    /// Elimina una materia por su ID - Panel Admin
    /// </summary>
    /// <param name="subjectId"></param>
    /// <returns></returns>
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route(nameof(DeleteSubject))]
    public async Task<ResultRequestDTO<string>> DeleteSubject(Guid subjectId) => await _subjectService.DeleteSubjectAsync(subjectId);
}
