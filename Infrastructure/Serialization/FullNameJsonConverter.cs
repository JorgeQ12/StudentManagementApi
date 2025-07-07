using Domain.ValueObjects;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Serialization;

public sealed class FullNameJsonConverter : JsonConverter<FullName>
{
    public override FullName? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var root = jsonDoc.RootElement;

        var firstName = root.GetProperty("firstName").GetString();
        var middleName = root.TryGetProperty("middleName", out var m) ? m.GetString() : null;
        var lastName = root.GetProperty("lastName").GetString();
        var secondLastName = root.TryGetProperty("secondLastName", out var s) ? s.GetString() : null;

        return new FullName(firstName!, middleName, lastName!, secondLastName);
    }

    public override void Write(Utf8JsonWriter writer, FullName value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("firstName", value.FirstName);
        writer.WriteString("middleName", value.MiddleName);
        writer.WriteString("lastName", value.LastName);
        writer.WriteString("secondLastName", value.SecondLastName);
        writer.WriteEndObject();
    }
}
