using System.Text.Json.Serialization;

namespace webApp_CAEPlatform.model;

public class Parameters
{
    [JsonPropertyName("h1")]
    public int H1 { get; set; }
    [JsonPropertyName("h3")]
    public int H3 { get; set; }
    [JsonPropertyName("l2")]
    public int L2 { get; set; }
    [JsonPropertyName("l3")]
    public int L3 { get; set; }
}