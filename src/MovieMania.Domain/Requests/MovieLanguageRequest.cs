namespace MovieMania.Domain.Requests;

public record MovieLanguageRequest
{
    public Language Language { get; set; }
    public LanguageRole LanguageRole { get; set; }
}

public record Language
{
    public int Id { get; set; }
}

public record LanguageRole
{
    public int Id { get; set; }
}