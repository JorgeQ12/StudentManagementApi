namespace Domain.Entities;

public class Professor
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public Professor(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
