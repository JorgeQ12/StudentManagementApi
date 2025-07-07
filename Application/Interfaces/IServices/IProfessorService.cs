using Application.DTOs.Common;
using Application.DTOs.Professor;

namespace Application.Interfaces.IServices
{
    public interface IProfessorService
    {
        /// <summary>
        /// Lista todos los profesores registrados - Panel Admin
        /// </summary>
        Task<ResultRequestDTO<IEnumerable<ProfessorResponseDTO>>> GetAllProfessorsAsync();

        /// <summary>
        /// Crea un nuevo profesor - Panel Admin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResultRequestDTO<string>> CreateProfessorAsync(CreateProfessorRequestDTO request);

        /// <summary>
        /// Elimina un profesor por su ID - Panel Admin
        /// </summary>
        /// <param name="professorId"></param>
        /// <returns></returns>
        Task<ResultRequestDTO<string>> DeleteProfessorAsync(Guid professorId);
    }
}
