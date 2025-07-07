using Application.DTOs.Professor;

namespace Application.Interfaces.IRepository
{
    public interface IProfessorRepository
    {
        /// <summary>
        /// Lista todos los profesores registrados - Panel Admin
        /// </summary>
        Task<IEnumerable<ProfessorResponseDTO>> GetAllProfessorsAsync();

        /// <summary>
        /// Obtener el profesor por Id
        /// </summary>
        /// <param name="professorId"></param>
        /// <returns></returns>
        Task<ProfessorResponseDTO?> GetProfessorByIdAsync(Guid professorId);

        /// <summary>
        /// Crea un nuevo profesor - Panel Admin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task CreateProfessorAsync(CreateProfessorRequestDTO request);

        /// <summary>
        /// Elimina un profesor por su ID - Panel Admin
        /// </summary>
        /// <param name="professorId"></param>
        /// <returns></returns>
        Task DeleteProfessorAsync(Guid professorId);
    }
}
