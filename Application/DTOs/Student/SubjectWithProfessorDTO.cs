namespace Application.DTOs.Student
{
    public class SubjectWithProfessorDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Credits { get; set; }
        public Guid ProfessorId { get; set; }
        public string ProfessorName { get; set; } = string.Empty;
    }
}
