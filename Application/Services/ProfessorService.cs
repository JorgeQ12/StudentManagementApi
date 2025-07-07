using Application.Interfaces.IServices;
using Domain.Exceptions;
using Application.Interfaces.IRepository;
using Application.DTOs.Professor;
using Application.DTOs.Common;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProfessorService : IProfessorService
{
    private readonly IProfessorRepository _professorRepository;
    private readonly ILogger<ProfessorService> _logger;

    public ProfessorService(IProfessorRepository professorRepository, ILogger<ProfessorService> logger )
    {
        _professorRepository = professorRepository;
        _logger = logger;
    }

    /// <summary>
    /// Lista todos los profesores registrados - Panel Admin
    /// </summary>
    public async Task<ResultRequestDTO<IEnumerable<ProfessorResponseDTO>>> GetAllProfessorsAsync()
    {
        try
        {
            return ResultRequestDTO<IEnumerable<ProfessorResponseDTO>>.Success(await _professorRepository.GetAllProfessorsAsync());
        }
        catch (DomainException exDomain)
        {
            _logger.LogError(exDomain, "Error retrieving professors.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving professors.");
            throw;
        }
    }

    /// <summary>
    /// Crea un nuevo profesor - Panel Admin
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ResultRequestDTO<string>> CreateProfessorAsync(CreateProfessorRequestDTO request)
    {
        try
        {
            await _professorRepository.CreateProfessorAsync(request);
            return ResultRequestDTO<string>.Success("Professor created successfully");
        }
        catch (DomainException exDomain)
        {
            _logger.LogError(exDomain, "Error creating professor.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating professor.");
            throw;
        }
    }

    /// <summary>
    /// Elimina un profesor por su ID - Panel Admin
    /// </summary>
    /// <param name="professorId"></param>
    /// <returns></returns>
    public async Task<ResultRequestDTO<string>> DeleteProfessorAsync(Guid professorId)
    {
        try
        {
            await _professorRepository.DeleteProfessorAsync(professorId);
            return ResultRequestDTO<string>.Success("Professor delete successfully");
        }
        catch (DomainException exDomain)
        {
            _logger.LogError(exDomain, "Error deleting professor.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting professor.");
            throw;
        }
    }
}
