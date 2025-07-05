using Application.DTOs.Professor;

namespace Application.Interfaces.IRepository
{
    public interface IProfessorRepository
    {
        Task<IEnumerable<ProfessorResponseDTO>> GetAllProfessorsAsync();
        Task<ProfessorResponseDTO?> GetProfessorByIdAsync(Guid professorId);
        Task CreateProfessorAsync(CreateProfessorRequestDTO request);
        Task DeleteProfessorAsync(Guid professorId);
    }
}
