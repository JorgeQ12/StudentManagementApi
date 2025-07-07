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
        var subjectList = subjects.ToList();

        if (subjectList.Count is < 1 or > 3)
            throw new DomainException("Student must be enrolled in 1 to 3 subjects.");

        var distinctProfessors = subjectList.Select(s => s.ProfessorId).Distinct().Count();
        if (distinctProfessors != subjectList.Count)
            throw new DomainException("Each subject must have a different professor.");

        _subjects = subjectList;
    }
}
