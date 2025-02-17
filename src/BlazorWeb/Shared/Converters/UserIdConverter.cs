using System.Text.Json;
using System.Text.Json.Serialization;
using oig_assessment.Domain.ValueObjects;

namespace BlazorWeb.Shared.AuthHelpers;

public class UserIdJsonConverter: JsonConverter<UserId>
{
    public override UserId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var guid = reader.GetGuid();
        return new UserId(guid);
    }

    public override void Write(Utf8JsonWriter writer, UserId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value.ToString());
    }
}
