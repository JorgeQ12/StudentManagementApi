using Application.DTOs.Student;
using Application.Interfaces.IRepository;
using Application.Interfaces.IRepository.Common;
using Domain.Entities;
using Domain.ValueObjects;

namespace Infrastructure.Repository;

public class StudentRepository : IStudentRepository
{
    private readonly IGenericRepository _genericRepository;

    public StudentRepository(IGenericRepository generic)
    {
        _genericRepository = generic;
    }

    /// <summary>
    /// Obtener todos los estudiantes - Panel Admin y Estudiante
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<StudentProfileDTO>> GetAllStudents()
    {
        return await _genericRepository.GetProcedureAsync<StudentProfileDTO>("SP_GetAllStudents", new {});
    }

    /// <summary>
    /// Obtener información personal del estudiante - Panel Estudiante
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    public async Task<Student?> GetStudentByIdAsync(Guid studentId)
    {
        var dbResult = await _genericRepository.GetProcedureSingleAsync<dynamic>(
            "SP_GetStudentById",
            new { StudentId = studentId }
        );

        if (dbResult == null) return null;

        var fullName = new FullName(
            dbResult.FirstName,
            dbResult.MiddleName,
            dbResult.LastName,
            dbResult.SecondLastName
        );

        var info = new StudentInfo(
            dbResult.Email,
            dbResult.DocumentType,
            dbResult.DocumentNumber,
            dbResult.PhoneNumber,
            dbResult.BirthDate,
            dbResult.Gender,
            dbResult.Address,
            dbResult.EnrollmentDate,
            dbResult.IsActive
        );

        return new Student(dbResult.Id, fullName, info);
    }

    /// <summary>
    /// lista de estudiantes junto con las materias en las que están inscritos - Panel Estudiante
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<StudentsWithSubjectsDTO>> GetAllStudentsWithSubjectsAsync()
    {
        var result = await _genericRepository.GetProcedureAsync<dynamic>("SP_GetAllStudentsWithSubjects", new { });

        return result.Select(x => new StudentsWithSubjectsDTO
        {
            FullName = x.FullName,
            Subjects = x.Subjects.ToString().Split(", ", StringSplitOptions.TrimEntries)
        });
    }

    /// <summary>
    /// Obtener detalle de cada clase registrada por el estudiante - Panel Estudiante
    ///</summary>
    /// <param name="studentId">ID del estudiante actual</param>
    public async Task<IEnumerable<StudentClassDetailsDTO>> GetStudentClassDetailsAsync(Guid studentId)
    {
        var result = await _genericRepository.GetProcedureAsync<dynamic>("SP_GetStudentClassDetails", new { StudentId = studentId });
        var grouped = result
                             .GroupBy(r => new
                             {
                                 SubjectId = (Guid)((IDictionary<string, object>)r)["SubjectId"],
                                 SubjectName = (string)((IDictionary<string, object>)r)["SubjectName"],
                                 ProfessorName = (string)((IDictionary<string, object>)r)["ProfessorName"]
                             })
                             .Select(g => new StudentClassDetailsDTO
                             {
                                 SubjectId = g.Key.SubjectId,
                                 SubjectName = g.Key.SubjectName,
                                 ProfessorName = g.Key.ProfessorName,
                                 Classmates = g.Select(x => new ClassmateDTO
                                 {
                                     Id = (Guid)((IDictionary<string, object>)x)["StudentId"],
                                     FullName = (string)((IDictionary<string, object>)x)["FullName"]
                                 }).ToList()
                             });
        return grouped;
    }

    /// <summary>
    /// Inscribir materias - Panel Estudiante
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="subjectIds"></param>
    /// <returns></returns>
    public async Task EnrollStudentInSubjectsAsync(Guid studentId, IEnumerable<Guid> subjectIds)
    {
        var idsString = string.Join(",", subjectIds);

        await _genericRepository.ExecuteProcedureAsync("SP_EnrollStudentSubjects", new
        {
            StudentId = studentId,
            Ids = idsString
        });
    }

    /// <summary>
    /// Actualizar información personal del estudiante - Panel Estudiante
    /// </summary>
    /// <param name="student"></param>
    /// <returns></returns>
    public async Task UpdateStudentAsync(Student student)
    {
        await _genericRepository.ExecuteProcedureAsync("SP_UpdateStudentInfo", new
        {
            StudentId = student.Id,
            Email = student.Info.Email,
            DocumentType = student.Info.DocumentType,
            DocumentNumber = student.Info.DocumentNumber,
            PhoneNumber = student.Info.PhoneNumber,
            BirthDate = student.Info.BirthDate,
            Gender = student.Info.Gender,
            Address = student.Info.Address
        });
    }

    /// <summary>
    /// Eliminar materia registrada por usuario - Panel Estudiante
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="subjectId"></param>
    /// <returns></returns>
    public async Task DeleteEnrollInSubjects(Guid studentId, Guid subjectId)
    {
        await _genericRepository.ExecuteProcedureAsync("SP_DeleteEnrollInSubjects", new
        {
            StudentId = studentId,
            SubjectId = subjectId
        });
    }

    /// <summary>
    /// Eliminar el perfil del estudiante registrado - Panel Admin
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    public async Task DeleteStudentProfile(Guid studentId)
    {
        await _genericRepository.ExecuteProcedureAsync("SP_DeleteStudentProfile", new
        {
            StudentId = studentId,
        });
    }
}
