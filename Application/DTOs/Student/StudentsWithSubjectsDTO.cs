namespace Application.DTOs.Student
{
    public class StudentsWithSubjectsDTO
    {
        public string FullName { get; set; } = string.Empty;
        public IEnumerable<string> Subjects { get; set; } = Enumerable.Empty<string>();
    }
}
