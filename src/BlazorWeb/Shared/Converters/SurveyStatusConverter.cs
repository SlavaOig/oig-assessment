using System.Text.Json.Serialization;
using System.Text.Json;
using oig_assessment.Domain.ValueObjects;

public class SurveyStatusConverter : JsonConverter<SurveyStatus>
{
    public override SurveyStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            var root = doc.RootElement;
            var value = root.GetProperty("value").GetInt32(); 
            var name = root.GetProperty("name").GetString();  

            return SurveyStatus.FromValue(value);
        }
    }

    public override void Write(Utf8JsonWriter writer, SurveyStatus value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("Value", value.Value);
        writer.WriteString("Name", value.Name);
        writer.WriteEndObject();
    }
}
