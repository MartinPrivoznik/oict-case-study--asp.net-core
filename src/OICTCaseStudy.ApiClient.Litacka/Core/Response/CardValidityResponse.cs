using System.Text.Json.Serialization;

namespace OICTCaseStudy.ApiClient.Litacka.Core.Response;

public class CardValidityResponse
{
    [JsonPropertyName("validity_start")]
    public DateTime ValidityStart { get; init; }

    [JsonPropertyName("validity_end")]
    public DateTime ValidityEnd { get; init; }
}