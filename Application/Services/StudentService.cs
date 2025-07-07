using Application.DTOs.Student;
using Application.Interfaces.IServices;
using Domain.Exceptions;
using Domain.ValueObjects;
using Application.Interfaces.IRepository;
using Application.DTOs.Common;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Domain.Entities;

namespace Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly ILogger<StudentService> _logger;

    public StudentService(IStudentRepository studentRepository, ISubjectRepository subjectRepository, ILogger<StudentService> logger)
    {
        _studentRepository = studentRepository;
        _subjectRepository = subjectRepository;
        _logger = logger;
    }

    /// <summary>
    /// Obtener todos los estudiantes - Panel Admin y Estudiante
    /// </summary>
    /// <returns></returns>
    public async Task<ResultRequestDTO<IEnumerable<StudentProfileDTO>>> GetAllStudents()
    {
        try
        {
            return ResultRequestDTO<IEnumerable<StudentProfileDTO>>.Success(await _studentRepository.GetAllStudents());
        }
        catch (DomainException exDomain)
        {
            _logger.LogError(exDomain, "An unexpected error occurred while retrieving students.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while retrieving students.");
            throw;
        }
    }

    /// <summary>
    /// Obtener información personal del estudiante - Panel Estudiante
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    public async Task<ResultRequestDTO<StudentProfileDTO>> GetStudentProfileAsync(Guid studentId)
    {
        try
        {
            var student = await _studentRepository.GetStudentByIdAsync(studentId) ?? throw new DomainException("Student not found.");

            return ResultRequestDTO<StudentProfileDTO>.Success(new StudentProfileDTO
            {
                Id = student.Id,
                FullName = student.FullName.ToString(),
                Email = student.Info.Email,
                DocumentType = student.Info.DocumentType,
                DocumentNumber = student.Info.DocumentNumber,
                PhoneNumber = student.Info.PhoneNumber,
                BirthDate = student.Info.BirthDate,
                Gender = student.Info.Gender,
                Address = student.Info.Address,
                EnrollmentDate = student.Info.EnrollmentDate,
                IsActive = student.Info.IsActive
            });
        }
        catch (DomainException exDomain)
        {
            _logger.LogError(exDomain, "An unexpected error occurred while retrieving student profile.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while retrieving student profile.");
            throw;
        }
    }

    /// <summary>
    /// lista de estudiantes junto con las materias en las que están inscritos - Panel Estudiante
    /// </summary>
    /// <returns></returns>
    public async Task<ResultRequestDTO<IEnumerable<StudentsWithSubjectsDTO>>> GetAllStudentsWithSubjectsAsync()
    {
        try
        {
            return ResultRequestDTO<IEnumerable<StudentsWithSubjectsDTO>>.Success(await _studentRepository.GetAllStudentsWithSubjectsAsync());
        }
        catch (DomainException exDomain)
        {
            _logger.LogError(exDomain, "An unexpected error occurred while retrieving all students with subjects.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while retrieving all students with subjects.");
            throw;
        }
    }

    /// <summary>
    /// Obtener detalle de cada clase registrada por el estudiante - Panel Estudiante
    ///</summary>
    /// <param name="studentId">ID del estudiante actual</param>
    public async Task<ResultRequestDTO<IEnumerable<StudentClassDetailsDTO>>> GetStudentClassDetailsAsync(Guid studentId)
    {
        try
        {
            return ResultRequestDTO<IEnumerable<StudentClassDetailsDTO>>.Success(await _studentRepository.GetStudentClassDetailsAsync(studentId));
        }
        catch (DomainException exDomain)
        {
            _logger.LogError(exDomain, "An unexpected error occurred while retrieving classmates.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while retrieving classmates.");
            throw;
        }
    }

    /// <summary>
    /// Inscribir materias - Panel Estudiante
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="subjectIds"></param>
    /// <returns></returns>
    public async Task<ResultRequestDTO<string>> EnrollStudentInSubjectsAsync(Guid studentId, IEnumerable<Guid> subjectIds)
    {
        try
        {
            if (subjectIds is null || !subjectIds.Any())
                throw new DomainException("At least one subject must be selected.");

            var student = await _studentRepository.GetStudentByIdAsync(studentId) ?? throw new DomainException("Student not found.");

            var existingSubjectIds = await _subjectRepository.GetSubjectIdsByStudentAsync(studentId);

            var totalSubjects = existingSubjectIds.Union(subjectIds).ToList();
            if (totalSubjects.Count != 3)
                throw new DomainException("You must be enrolled in exactly 3 subjects in total.");

            var newSubjects = await _subjectRepository.GetSubjectsByIdsAsync(totalSubjects);
            if (!subjectIds.All(id => newSubjects.Any(s => s.Id == id)))
                throw new DomainException("Some selected subjects were not found.");

            student.EnrollInSubjects(newSubjects);

            await _studentRepository.EnrollStudentInSubjectsAsync(studentId, subjectIds);
            return ResultRequestDTO<string>.Success("EnrollStudent create successfully");
        }
        catch (DomainException exDomain)
        {
            _logger.LogError(exDomain, "An error occurred while enrolling in subjects.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while enrolling in subjects.");
            throw;
        }
    }

    /// <summary>
    /// Actualizar información personal del estudiante - Panel Estudiante
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ResultRequestDTO<string>> UpdateStudentProfileAsync(UpdateStudentRequestDTO request)
    {
        try
        {
            var student = await _studentRepository.GetStudentByIdAsync(request.Id) ?? throw new DomainException("Student not found.");

            student.SetInfo(new StudentInfo(
                email: request.Email,
                documentType: request.DocumentType,
                documentNumber: request.DocumentNumber,
                phoneNumber: request.PhoneNumber,
                birthDate: request.BirthDate,
                gender: request.Gender,
                address: request.Address,
                enrollmentDate: student.Info.EnrollmentDate,
                isActive: student.Info.IsActive
            ));

            await _studentRepository.UpdateStudentAsync(student);
            return ResultRequestDTO<string>.Success("Student update successfully");
        }
        catch (DomainException exDomain)
        {
            _logger.LogError(exDomain, "An unexpected error occurred while updating student profile.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while updating student profile.");
            throw;
        }
    }

    /// <summary>
    /// Eliminar materia registrada por usuario - Panel Estudiante
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="subjectId"></param>
    /// <returns></returns>
    public async Task<ResultRequestDTO<string>> DeleteEnrollInSubjects(Guid studentId, Guid subjectId)
    {
        try
        {
            await _studentRepository.DeleteEnrollInSubjects(studentId, subjectId);
            return ResultRequestDTO<string>.Success("Delete enrolling in subjects successfully");
        }
        catch (DomainException exDomain)
        {
            _logger.LogError(exDomain, "An error occurred while delete enrolling in subjects.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while delete enrolling in subjects.");
            throw;
        }
    }

    /// <summary>
    /// Eliminar el perfil del estudiante registrado - Panel Admin
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    public async Task<ResultRequestDTO<string>> DeleteStudentProfile(Guid studentId)
    {
        try
        {
            await _studentRepository.DeleteStudentProfile(studentId);
            return ResultRequestDTO<string>.Success("Delete student profile successfully");
        }
        catch (DomainException exDomain)
        {
            _logger.LogError(exDomain, "An error occurred while delete student profile.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while delete student profile.");
            throw;
        }
    }
}
