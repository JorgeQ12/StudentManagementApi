
namespace Domain.ValueObjects
{
    public sealed class FullName
    {
        public string FirstName { get; }
        public string? MiddleName { get; }
        public string LastName { get; }
        public string? SecondLastName { get; }

        public FullName(string firstName, string? middleName, string lastName, string? secondLastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name is required.", nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("The first surname is required", nameof(lastName));

            FirstName = firstName.Trim();
            MiddleName = middleName?.Trim();
            LastName = lastName.Trim();
            SecondLastName = secondLastName?.Trim();
        }

        public override string ToString()
        {
            return $"{FirstName} {(MiddleName ?? "")} {LastName} {(SecondLastName ?? "")}".Replace("  ", " ").Trim();
        }
    }

}
