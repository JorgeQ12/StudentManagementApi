using Application.DTOs.Common;
using Application.DTOs.Professor;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfessorController : ControllerBase
{
    private readonly IProfessorService _professorService;

    public ProfessorController(IProfessorService professorService)
    {
        _professorService = professorService;
    }

    /// <summary>
    /// Lista todos los profesores registrados - Panel Admin
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route(nameof(GetAllProfessors))]
    public async Task<ResultRequestDTO<IEnumerable<ProfessorResponseDTO>>> GetAllProfessors() => await _professorService.GetAllProfessorsAsync();

    /// <summary>
    /// Crea un nuevo profesor - Panel Admin
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Admin")] 
    [Route(nameof(CreateProfessor))]
    public async Task<ResultRequestDTO<string>> CreateProfessor(CreateProfessorRequestDTO request) => await _professorService.CreateProfessorAsync(request);

    /// <summary>
    /// Elimina un profesor por su ID - Panel Admin
    /// </summary>
    /// <param name="professorId"></param>
    /// <returns></returns>
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route(nameof(DeleteProfessor))]
    public async Task<ResultRequestDTO<string>> DeleteProfessor(Guid professorId) => await _professorService.DeleteProfessorAsync(professorId);
}
