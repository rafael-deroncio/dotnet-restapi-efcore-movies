using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieMania.Domain.Requests;

public record MovieCastRequest
{
    [StringLength(255)]
    [JsonPropertyName("character_name")]
    public string CharacterName { get; set; }

    [JsonPropertyName("cast_order")]
    public int CastOrder { get; set; }
    
    public Person Person { get; set; }
    public Gender Gender { get; set; }
}

public record Person
{
    public int Id { get; set; }
}

public record Gender
{
    public int Id { get; set; }
}
