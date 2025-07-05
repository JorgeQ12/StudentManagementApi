namespace Application.DTOs.Subject
{
    public class CreateSubjectRequestDTO
    {
        public string Name { get; set; } = string.Empty;
        public Guid ProfessorId { get; set; }
    }
}
