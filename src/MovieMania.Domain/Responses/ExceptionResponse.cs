namespace MovieMania.Domain.Responses;

public record ExceptionResponse
{
    public string Title { get; set; }
    public string Type { get; set; }
    public string[] Messages { get; set; }
}