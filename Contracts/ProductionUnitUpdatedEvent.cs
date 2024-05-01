using System.Text.Json.Serialization;

namespace Contracts;

public class ProductionUnitUpdatedEvent
{
    [JsonPropertyName("ProductionUnitNumber")]
    public int ProductionUnitNumber { get; set; }
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    /*[JsonPropertyName("PhoneNumber")]
    public string PhoneNumber { get; set; }*/
    /*[JsonPropertyName("Name")]
    public string Email { get; set; }*/
    [JsonPropertyName("City")]
    public string City { get; set; }
    [JsonPropertyName("Address")]
    public string Address { get; set; }
    [JsonPropertyName("ZipCode")]
    public int ZipCode { get; set; }
}