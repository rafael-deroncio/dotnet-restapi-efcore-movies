using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieMania.Domain.Requests;

public record MovieCrewRequest
{
    public Person Person { get; set; }
    public Department Department { get; set; }

    [StringLength(255)]
    [JsonPropertyName("job")]
    public string Job { get; set; }
}

public record Department
{
    public int Id { get; set; }
}