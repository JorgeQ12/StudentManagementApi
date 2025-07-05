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
    /// Lista todos los profesores registrados
    /// </summary>
    [HttpGet]
    [Authorize]
    [Route(nameof(GetAllProfessors))]
    public async Task<IEnumerable<ProfessorResponseDTO>> GetAllProfessors() => await _professorService.GetAllProfessorsAsync();

    /// <summary>
    /// Obtiene un profesor por su ID
    /// </summary>
    [HttpGet]
    [Authorize]
    [Route(nameof(GetProfessorById))]
    public async Task<ProfessorResponseDTO?> GetProfessorById(Guid professorId) => await _professorService.GetProfessorByIdAsync(professorId);

    /// <summary>
    /// Crea un nuevo profesor
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")] 
    [Route(nameof(CreateProfessor))]
    public async Task CreateProfessor(CreateProfessorRequestDTO request) => await _professorService.CreateProfessorAsync(request);

    /// <summary>
    /// Elimina un profesor por su ID
    /// </summary>
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route(nameof(DeleteProfessor))]
    public async Task DeleteProfessor(Guid professorId) => await _professorService.DeleteProfessorAsync(professorId);
}
