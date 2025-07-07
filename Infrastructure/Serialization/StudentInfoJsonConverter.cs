using Domain.ValueObjects;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Serialization;

public sealed class StudentInfoJsonConverter : JsonConverter<StudentInfo>
{
    public override StudentInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var root = jsonDoc.RootElement;

        string email = root.GetProperty("email").GetString()!;
        string documentType = root.GetProperty("documentType").GetString()!;
        string documentNumber = root.GetProperty("documentNumber").GetString()!;
        string phoneNumber = root.GetProperty("phoneNumber").GetString()!;
        DateTime birthDate = root.GetProperty("birthDate").GetDateTime();
        string gender = root.GetProperty("gender").GetString()!;
        string address = root.GetProperty("address").GetString()!;
        bool isActive = root.TryGetProperty("isActive", out var isActiveProp) && isActiveProp.GetBoolean();
        DateTime? enrollmentDate = root.TryGetProperty("enrollmentDate", out var ed) ? ed.GetDateTime() : null;

        return new StudentInfo(email, documentType, documentNumber, phoneNumber, birthDate, gender, address, enrollmentDate, isActive);
    }

    public override void Write(Utf8JsonWriter writer, StudentInfo value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("email", value.Email);
        writer.WriteString("documentType", value.DocumentType);
        writer.WriteString("documentNumber", value.DocumentNumber);
        writer.WriteString("phoneNumber", value.PhoneNumber);
        writer.WriteString("birthDate", value.BirthDate.ToString("yyyy-MM-dd"));
        writer.WriteString("gender", value.Gender);
        writer.WriteString("address", value.Address);
        writer.WriteString("enrollmentDate", value.EnrollmentDate.ToString("yyyy-MM-dd"));
        writer.WriteBoolean("isActive", value.IsActive);
        writer.WriteEndObject();
    }
}
