using System.Text.Json.Serialization;

namespace MovieMania.Domain.Responses;

public record CrewResponse
{
    [JsonPropertyName("job")]
    public string Job { get; set; }

    [JsonPropertyName("person")]
    public PersonResponse Person { get; set; }

    [JsonPropertyName("department")]
    public DepartmentResponse Department { get; set; }
}