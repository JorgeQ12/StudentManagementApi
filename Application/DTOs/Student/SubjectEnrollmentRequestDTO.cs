namespace Application.DTOs.Student
{
    public class SubjectEnrollmentRequestDTO
    {
        public IEnumerable<Guid> SubjectIds { get; set; } = Enumerable.Empty<Guid>();
    }
}
