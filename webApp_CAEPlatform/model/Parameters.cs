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

    public override string? ToString()
    {
        var str = $"H1:{H1.ToString()}; H3:{H3.ToString()}; L2:{L2.ToString()}; L3:{L3.ToString()}";
        return str;
    }
}