using Application.DTOs.Professor;
using Application.Interfaces.IRepository.Common;
using Application.Interfaces.IRepository;

namespace Infrastructure.Repository;

public class ProfessorRepository : IProfessorRepository
{
    private readonly IGenericRepository _genericRepository;

    public ProfessorRepository(IGenericRepository generic)
    {
        _genericRepository = generic;
    }

    public async Task<IEnumerable<ProfessorResponseDTO>> GetAllProfessorsAsync()
    {
        return await _genericRepository.GetProcedureAsync<ProfessorResponseDTO>("SP_GetAllProfessors", new { });
    }

    public async Task<ProfessorResponseDTO?> GetProfessorByIdAsync(Guid professorId)
    {
        return await _genericRepository.GetProcedureSingleAsync<ProfessorResponseDTO>("SP_GetProfessorById", new { ProfessorId = professorId });
    }

    public async Task CreateProfessorAsync(CreateProfessorRequestDTO request)
    {
        await _genericRepository.ExecuteProcedureAsync("SP_CreateProfessor", new { Name = request.Name });
    }

    public async Task DeleteProfessorAsync(Guid professorId)
    {
        await _genericRepository.ExecuteProcedureAsync("SP_DeleteProfessor", new { ProfessorId = professorId });
    }
}
