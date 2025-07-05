namespace Domain.Entities;

public class Subject
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public int Credits { get; private set; }
    public Guid ProfessorId { get; private set; }

    public Subject(Guid id, string name, Guid professorId)
    {
        Id = id;
        Name = name;
        ProfessorId = professorId;
        Credits = 3;
    }
}
