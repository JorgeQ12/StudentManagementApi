
using Domain.Exceptions;
using System.Text.Json.Serialization;

namespace Domain.ValueObjects
{
    public sealed class FullName
    {
        public string FirstName { get; }
        public string? MiddleName { get; }
        public string LastName { get; }
        public string? SecondLastName { get; }

        [JsonConstructor]
        public FullName(string firstName, string? middleName, string lastName, string? secondLastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("First name is required.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("The first surname is required");

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
