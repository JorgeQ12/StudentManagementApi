using Application.Interfaces.IServices;
using Domain.Exceptions;
using Application.Interfaces.IRepository;
using Application.DTOs.Professor;

namespace Application.Services;

public class ProfessorService : IProfessorService
{
    private readonly IProfessorRepository _professorRepository;

    public ProfessorService(IProfessorRepository professorRepository)
    {
        _professorRepository = professorRepository;
    }

    public async Task<IEnumerable<ProfessorResponseDTO>> GetAllProfessorsAsync()
    {
        try
        {
            return await _professorRepository.GetAllProfessorsAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error retrieving professors.", ex);
        }
    }

    public async Task<ProfessorResponseDTO?> GetProfessorByIdAsync(Guid professorId)
    {
        try
        {
            return await _professorRepository.GetProfessorByIdAsync(professorId);
        }
        catch (Exception ex)
        {
            throw new Exception("Error retrieving professor by ID.", ex);
        }
    }

    public async Task CreateProfessorAsync(CreateProfessorRequestDTO request)
    {
        try
        {
            await _professorRepository.CreateProfessorAsync(request);
        }
        catch (DomainException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception("Error creating professor.", ex);
        }
    }

    public async Task DeleteProfessorAsync(Guid professorId)
    {
        try
        {
            await _professorRepository.DeleteProfessorAsync(professorId);
        }
        catch (DomainException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception("Error deleting professor.", ex);
        }
    }
}
