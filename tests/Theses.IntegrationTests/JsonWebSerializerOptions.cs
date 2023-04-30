using System.Text.Json;

namespace Theses.IntegrationTests;

public static class JsonWebSerializerOptions
{
    public readonly static JsonSerializerOptions Instance = new(JsonSerializerDefaults.Web);
}
