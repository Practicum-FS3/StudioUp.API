using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class TimeOnlyConverter : JsonConverter<TimeOnly>
{
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException($"Expected StartObject but got {reader.TokenType}.");
        }

        int hour = 0, minute = 0;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return new TimeOnly(hour, minute);
            }

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                string propertyName = reader.GetString();
                reader.Read();

                switch (propertyName)
                {
                    case "hour":
                        hour = reader.GetInt32();
                        break;
                    case "minute":
                        minute = reader.GetInt32();
                        break;
                }
            }
        }

        throw new JsonException("Invalid JSON format for TimeOnly.");
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("hour", value.Hour);
        writer.WriteNumber("minute", value.Minute);
        writer.WriteEndObject();
    }
}
