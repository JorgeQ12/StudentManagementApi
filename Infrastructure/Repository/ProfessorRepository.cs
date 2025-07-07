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

    /// <summary>
    /// Lista todos los profesores registrados - Panel Admin
    /// </summary>
    public async Task<IEnumerable<ProfessorResponseDTO>> GetAllProfessorsAsync()
    {
        return await _genericRepository.GetProcedureAsync<ProfessorResponseDTO>("SP_GetAllProfessors", new { });
    }

    /// <summary>
    /// Obtener el profesor por Id
    /// </summary>
    /// <param name="professorId"></param>
    /// <returns></returns>
    public async Task<ProfessorResponseDTO?> GetProfessorByIdAsync(Guid professorId)
    {
        return await _genericRepository.GetProcedureSingleAsync<ProfessorResponseDTO>("SP_GetProfessorById", new { ProfessorId = professorId });
    }

    /// <summary>
    /// Crea un nuevo profesor - Panel Admin
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task CreateProfessorAsync(CreateProfessorRequestDTO request)
    {
        await _genericRepository.ExecuteProcedureAsync("SP_CreateProfessor", new { Name = request.Name });
    }

    /// <summary>
    /// Elimina un profesor por su ID - Panel Admin
    /// </summary>
    /// <param name="professorId"></param>
    /// <returns></returns>
    public async Task DeleteProfessorAsync(Guid professorId)
    {
        await _genericRepository.ExecuteProcedureAsync("SP_DeleteProfessor", new { ProfessorId = professorId });
    }
}
