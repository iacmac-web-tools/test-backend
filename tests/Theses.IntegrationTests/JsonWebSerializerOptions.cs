using System.Text.Json;

namespace Theses.IntegrationTests;

public static class JsonWebSerializerOptions
{
    public static JsonSerializerOptions Instance = new(JsonSerializerDefaults.Web);
}
