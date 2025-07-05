namespace Application.DTOs.Student
{
    public class ClassmatesBySubjectDTO
    {
        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public IEnumerable<ClassmateDTO> Classmates { get; set; } = Enumerable.Empty<ClassmateDTO>();
    }
}
