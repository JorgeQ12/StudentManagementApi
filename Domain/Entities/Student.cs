using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Student
{
    public Guid Id { get; private set; }
    public FullName FullName { get; private set; }
    public StudentInfo Info { get; private set; }

    private IEnumerable<Subject> _subjects = Enumerable.Empty<Subject>();
    public IEnumerable<Subject> Subjects => _subjects;

    public Student(Guid id, FullName fullName, StudentInfo info)
    {
        Id = id;
        FullName = fullName;
        Info = info;
    }

    public void SetInfo(StudentInfo newInfo)
    {
        Info = newInfo ?? throw new DomainException("Student info cannot be null.");
    }

    public void EnrollInSubjects(IEnumerable<Subject> subjects)
    {
        if (subjects.Count() != 3)
            throw new DomainException("Student must be enrolled in exactly 3 subjects.");

        var distinctProfessors = subjects.Select(s => s.ProfessorId).Distinct().Count();
        if (distinctProfessors != 3)
            throw new DomainException("Subjects must be taught by different professors.");

        _subjects = subjects;
    }
}
