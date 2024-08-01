﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieMania.Domain.Requests;

public record PersonRequest
{
    [Required]
    [StringLength(100)]
    [JsonPropertyName("name")]
    public string Name { get; set; }
}