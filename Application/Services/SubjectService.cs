using Application.DTOs.Subject;
using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services;

public class SubjectService : ISubjectService
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IProfessorRepository _professorRepository;

    public SubjectService(ISubjectRepository subjectRepository, IProfessorRepository professorRepository)
    {
        _subjectRepository = subjectRepository;
        _professorRepository = professorRepository;
    }

    public async Task<IEnumerable<SubjectResponseDTO>> GetAllSubjectsAsync()
    {
        try
        {
            return await _subjectRepository.GetAllSubjectsAsync();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving subjects.", ex);
        }
    }

    public async Task<SubjectResponseDTO?> GetSubjectByIdAsync(Guid subjectId)
    {
        try
        {
            return await _subjectRepository.GetSubjectByIdAsync(subjectId);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the subject.", ex);
        }
    }

    public async Task CreateSubjectAsync(CreateSubjectRequestDTO request)
    {
        try
        {
            var professor = await _professorRepository.GetProfessorByIdAsync(request.ProfessorId);
            if (professor is null)
                throw new DomainException("The associated professor does not exist.");

            var subject = new Subject(Guid.NewGuid(), request.Name, request.ProfessorId);
            await _subjectRepository.CreateSubjectAsync(subject);
        }
        catch (DomainException) { throw; }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the subject.", ex);
        }
    }

    public async Task DeleteSubjectAsync(Guid subjectId)
    {
        try
        {
            var subject = await _subjectRepository.GetSubjectByIdAsync(subjectId);
            if (subject is null)
                throw new DomainException("The subject you are trying to delete does not exist.");

            await _subjectRepository.DeleteSubjectAsync(subjectId);
        }
        catch (DomainException) { throw; }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the subject.", ex);
        }
    }
}
