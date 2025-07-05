using Application.DTOs.Student;
using Application.DTOs;
using Application.Interfaces.IServices;
using Domain.Exceptions;
using Domain.ValueObjects;
using Application.Interfaces.IRepository;
using Domain.Entities;

namespace Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly ISubjectRepository _subjectRepository;

    public StudentService(IStudentRepository studentRepository, ISubjectRepository subjectRepository)
    {
        _studentRepository = studentRepository;
        _subjectRepository = subjectRepository;
    }

    public async Task<StudentProfileDTO> GetStudentProfileAsync(Guid studentId)
    {
        try
        {
            var student = await _studentRepository.GetStudentByIdAsync(studentId) ?? throw new DomainException("Student not found.");

            return new StudentProfileDTO
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
            };
        }
        catch (DomainException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unexpected error occurred while retrieving student profile.", ex);
        }
    }

    public async Task UpdateStudentProfileAsync(Guid studentId, UpdateStudentRequestDTO request)
    {
        try
        {
            var student = await _studentRepository.GetStudentByIdAsync(studentId) ?? throw new DomainException("Student not found.");

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
        }
        catch (DomainException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unexpected error occurred while updating student profile.", ex);
        }
    }

    public async Task EnrollStudentInSubjectsAsync(Guid studentId, IEnumerable<Guid> subjectIds)
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

            var newSubjects = await _subjectRepository.GetSubjectsByIdsAsync(subjectIds);
            if (newSubjects.Count() != subjectIds.Count())
                throw new DomainException("Some selected subjects were not found.");

            student.EnrollInSubjects(newSubjects);

            await _studentRepository.SaveSubjectsForStudentAsync(studentId, subjectIds);
        }
        catch (DomainException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while enrolling in subjects.", ex);
        }
    }

    public async Task<IEnumerable<SubjectWithProfessorDTO>> GetSubjectsEnrolledByStudentAsync(Guid studentId)
    {
        try
        {
            var subjects = await _subjectRepository.GetSubjectsByStudentIdAsync(studentId);

            return subjects.Select(s => new SubjectWithProfessorDTO
            {
                Id = s.Id,
                Name = s.Name,
                Credits = s.Credits,
                ProfessorName = s.ProfessorName
            });
        }
        catch (DomainException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unexpected error occurred while retrieving enrolled subjects.", ex);
        }
    }

    public async Task<IEnumerable<ClassmatesBySubjectDTO>> GetClassmatesGroupedBySubjectAsync(Guid studentId)
    {
        try
        {
            var result = await _studentRepository.GetClassmatesGroupedBySubjectAsync(studentId);

            var grouped = result
                             .GroupBy(r => new
                             {
                                 SubjectId = (Guid)((IDictionary<string, object>)r)["SubjectId"],
                                 SubjectName = (string)((IDictionary<string, object>)r)["SubjectName"]
                             })
                             .Select(g => new ClassmatesBySubjectDTO
                             {
                                 SubjectId = g.Key.SubjectId,
                                 SubjectName = g.Key.SubjectName,
                                 Classmates = g.Select(x => new ClassmateDTO
                                 {
                                     Id = (Guid)((IDictionary<string, object>)x)["StudentId"],
                                     FullName = (string)((IDictionary<string, object>)x)["FullName"]
                                 }).ToList()
                             });

            return grouped;
        }
        catch (DomainException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unexpected error occurred while retrieving classmates.", ex);
        }
    }
}
