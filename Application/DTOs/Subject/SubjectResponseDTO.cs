namespace Application.DTOs.Subject
{
    public class SubjectResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Credits { get; set; }
        public Guid ProfessorId { get; set; }
    }
}
