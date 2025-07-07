namespace Application.DTOs.Student
{
    public class StudentClassDetailsDTO
    {
        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string ProfessorName { get; set; } = string.Empty;
        public IEnumerable<ClassmateDTO> Classmates { get; set; } = Enumerable.Empty<ClassmateDTO>();
    }
}
