using Application.DTOs.Professor;

namespace Application.Interfaces.IServices
{
    public interface IProfessorService
    {
        /// <summary>
        /// Lista todos los profesores
        /// </summary>
        Task<IEnumerable<ProfessorResponseDTO>> GetAllProfessorsAsync();

        /// <summary>
        /// Obtiene un profesor por ID
        /// </summary>
        Task<ProfessorResponseDTO?> GetProfessorByIdAsync(Guid professorId);

        /// <summary>
        /// Crea un nuevo profesor
        /// </summary>
        Task CreateProfessorAsync(CreateProfessorRequestDTO request);

        /// <summary>
        /// Elimina un profesor por ID
        /// </summary>
        Task DeleteProfessorAsync(Guid professorId);
    }
}
