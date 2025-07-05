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
    /// Trae todas las materias registradas
    /// </summary>
    [HttpGet]
    [Authorize]
    [Route(nameof(GetAllSubjects))]
    public async Task<IEnumerable<SubjectResponseDTO>> GetAllSubjects() => await _subjectService.GetAllSubjectsAsync();

    /// <summary>
    /// Trae una materia por su ID
    /// </summary>
    [HttpGet]
    [Authorize]
    [Route(nameof(GetSubjectById))]
    public async Task<SubjectResponseDTO?> GetSubjectById(Guid subjectId) => await _subjectService.GetSubjectByIdAsync(subjectId);

    /// <summary>
    /// Registra una nueva materia
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route(nameof(CreateSubject))]
    public async Task CreateSubject(CreateSubjectRequestDTO request) => await _subjectService.CreateSubjectAsync(request);

    /// <summary>
    /// Elimina una materia por su ID
    /// </summary>
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route(nameof(DeleteSubject))]
    public async Task DeleteSubject(Guid subjectId) => await _subjectService.DeleteSubjectAsync(subjectId);
}
