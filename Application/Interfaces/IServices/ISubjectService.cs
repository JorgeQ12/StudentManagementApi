using Application.DTOs.Subject;

namespace Application.Interfaces.IServices;

public interface ISubjectService
{
    Task<IEnumerable<SubjectResponseDTO>> GetAllSubjectsAsync();
    Task<SubjectResponseDTO?> GetSubjectByIdAsync(Guid subjectId);
    Task CreateSubjectAsync(CreateSubjectRequestDTO request);
    Task DeleteSubjectAsync(Guid subjectId);
}
