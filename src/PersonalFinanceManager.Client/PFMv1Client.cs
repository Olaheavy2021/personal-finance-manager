using System.Text.Json;
using System.Text.Json.Serialization;

namespace PersonalFinanceManager.Client;

public partial class PFMv1Client
{
    static partial void UpdateJsonSerializerSettings(JsonSerializerOptions settings)
    {
        settings.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        settings.Converters.Add(new JsonStringEnumConverter());
    }
}
