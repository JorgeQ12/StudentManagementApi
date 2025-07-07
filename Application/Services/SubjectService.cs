using Application.DTOs.Common;
using Application.DTOs.Student;
using Application.DTOs.Subject;
using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Application.Services;

public class SubjectService : ISubjectService
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IProfessorRepository _professorRepository;
    private readonly ILogger<SubjectService> _logger;

    public SubjectService(ISubjectRepository subjectRepository, IProfessorRepository professorRepository, ILogger<SubjectService> logger)
    {
        _subjectRepository = subjectRepository;
        _professorRepository = professorRepository;
        _logger = logger;
    }

    /// <summary>
    /// Trae todas las materias registradas - Panel Admin y Estudiante
    /// </summary>
    /// <returns></returns>
    public async Task<ResultRequestDTO<IEnumerable<SubjectWithProfessorDTO>>> GetAllSubjectsAsync()
    {
        try
        {
            return ResultRequestDTO<IEnumerable<SubjectWithProfessorDTO>>.Success(await _subjectRepository.GetAllSubjectsAsync());
        }
        catch (DomainException exDomain)
        {
            _logger.LogError(exDomain, "Error occurred while retrieving subjects.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving subjects.");
            throw;
        }
    }

    /// <summary>
    /// Registra una nueva materia - Panel Admin
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ResultRequestDTO<string>> CreateSubjectAsync(CreateSubjectRequestDTO request)
    {
        try
        {
            var professor = await _professorRepository.GetProfessorByIdAsync(request.ProfessorId);
            if (professor is null)
                throw new DomainException("The associated professor does not exist.");

            var subject = new Subject(Guid.NewGuid(), request.Name, request.ProfessorId);
            await _subjectRepository.CreateSubjectAsync(subject);
            return ResultRequestDTO<string>.Success("Subject created successfully");
        }
        catch (DomainException exDomain)
        {
            _logger.LogError(exDomain, "Error occurred while creating the subject.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating the subject.");
            throw;
        }
    }

    /// <summary>
    /// Elimina una materia por su ID - Panel Admin
    /// </summary>
    /// <param name="subjectId"></param>
    /// <returns></returns>
    public async Task<ResultRequestDTO<string>> DeleteSubjectAsync(Guid subjectId)
    {
        try
        {
            var subject = await _subjectRepository.GetSubjectByIdAsync(subjectId);
            if (subject is null)
                throw new DomainException("The subject you are trying to delete does not exist.");

            await _subjectRepository.DeleteSubjectAsync(subjectId);
            return ResultRequestDTO<string>.Success("Subject deleted successfully");
        }
        catch (DomainException exDomain)
        {
            _logger.LogError(exDomain, "Error occurred while deleting the subject.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting the subject.");
            throw;
        }
    }
}
