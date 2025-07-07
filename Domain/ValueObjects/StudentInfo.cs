using Domain.Exceptions;
using System.Text.Json.Serialization;

namespace Domain.ValueObjects
{
    public sealed class StudentInfo
    {
        public string Email { get; }
        public string DocumentType { get; }
        public string DocumentNumber { get; }
        public string PhoneNumber { get; }
        public DateTime BirthDate { get; }
        public string Gender { get; }
        public string Address { get; }
        public DateTime EnrollmentDate { get; }
        public bool IsActive { get; }

        [JsonConstructor]
        public StudentInfo(string email, string documentType, string documentNumber, string phoneNumber, DateTime birthDate, string gender, string address, DateTime? enrollmentDate = null, bool isActive = true)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new DomainException("Invalid email format.");

            if (string.IsNullOrWhiteSpace(documentType))
                throw new DomainException("The type of document is mandatory.");

            if (string.IsNullOrWhiteSpace(documentNumber))
                throw new DomainException("The document number is mandatory.");

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new DomainException("The phone number is required.");

            if (birthDate > DateTime.UtcNow)
                throw new DomainException("The date of birth cannot be in the future.");

            if (string.IsNullOrWhiteSpace(gender))
                throw new DomainException("Gender is required.");

            if (string.IsNullOrWhiteSpace(address))
                throw new DomainException("Address is required.");

            Email = email.Trim();
            DocumentType = documentType.Trim();
            DocumentNumber = documentNumber.Trim();
            PhoneNumber = phoneNumber.Trim();
            BirthDate = birthDate;
            Gender = gender.Trim();
            Address = address.Trim();
            EnrollmentDate = enrollmentDate ?? DateTime.UtcNow;
            IsActive = isActive;
        }
    }

}
