using System.Text.Json;
using System.Text.Json.Serialization;

namespace PersonalFinanceManager.API.Infrastructure;

public class DoubleInfinityConverter : JsonConverter<double>
{
    public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var str = reader.GetString();
            if (str == "$$PositiveInfinity$$")
                return double.PositiveInfinity;
            if (str == "$$NegativeInfinity$$")
                return double.NegativeInfinity;
            if (str == "$$NaN$$")
                return double.NaN;
        }
        return reader.GetDouble();
    }

    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
    {
        if (double.IsPositiveInfinity(value))
            writer.WriteStringValue("$$PositiveInfinity$$");
        else if (double.IsNegativeInfinity(value))
            writer.WriteStringValue("$$NegativeInfinity$$");
        else if (double.IsNaN(value))
            writer.WriteStringValue("$$NaN$$");
        else
            writer.WriteNumberValue(value);
    }
}
