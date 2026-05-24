using System.Text.Json.Serialization;

namespace OICTCaseStudy.ApiClient.Litacka.Core.Response;

public class CardStateResponse
{
    [JsonPropertyName("state_id")]
    public ulong StateId { get; init; }

    [JsonPropertyName("state_description")]
    public string StateDescription { get; init; } = string.Empty;
}