namespace MovieMania.Domain.DTOs;

public record MovieLanguage
{
    public Language Language { get; set; }
    public LanguageRole LanguageRole { get; set; }
}
