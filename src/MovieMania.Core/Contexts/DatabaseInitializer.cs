using Microsoft.EntityFrameworkCore;
using MovieMania.Core.Contexts.Entities;

namespace MovieMania.Core.Contexts;

public static class DatabaseInitializer
{
    public static async Task Initialize(MovieManiaContext context)
    {
        await StartCountries(context);
        await StartDepartments(context);
        await StartGenders(context);
        await StartGenres(context);
        await StartKeywords(context);
        await StartLanguages(context);
        await StartLanguageRoles(context);
        await StartPersons(context);
        await StartProductionCompanies(context);
    }

    private static async Task StartCountries(MovieManiaContext context)
    {
        if (!await context.Countries.AnyAsync())
        {
            await context.Countries.AddRangeAsync(
                new List<CountryEntity>
                {
                new() { IsoCode = "US", Name = "United States", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { IsoCode = "CA", Name = "Canada", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { IsoCode = "GB", Name = "United Kingdom", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { IsoCode = "DE", Name = "Germany", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { IsoCode = "FR", Name = "France", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { IsoCode = "JP", Name = "Japan", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { IsoCode = "CN", Name = "China", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { IsoCode = "IN", Name = "India", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { IsoCode = "BR", Name = "Brazil", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { IsoCode = "AU", Name = "Australia", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { IsoCode = "IT", Name = "Italy", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { IsoCode = "RU", Name = "Russia", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { IsoCode = "ZA", Name = "South Africa", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { IsoCode = "MX", Name = "Mexico", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { IsoCode = "KR", Name = "South Korea", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                });

            await context.SaveChangesAsync();
        }
    }

    private static async Task StartDepartments(MovieManiaContext context)
    {
        if (!await context.Departments.AnyAsync())
        {
            await context.Departments.AddRangeAsync(
                new List<DepartmentEntity>
                {
                new() { Name = "Production", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { Name = "Art", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { Name = "Costume & Make-Up", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { Name = "Sound", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { Name = "Camera", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { Name = "Lighting", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { Name = "Visual Effects", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { Name = "Editing", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { Name = "Direction", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { Name = "Writing", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                });

            await context.SaveChangesAsync();
        }
    }

    private static async Task StartGenders(MovieManiaContext context)
    {
        if (!await context.Genders.AnyAsync())
        {
            await context.Genders.AddRangeAsync(
                new List<GenderEntity>
                {
                    new() { Gender = "Male", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Gender = "Female", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Gender = "Non-Binary", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Gender = "Other", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Gender = "Agender", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Gender = "Bigender", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Gender = "Genderfluid", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Gender = "Genderqueer", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Gender = "Two-Spirit", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                });

            await context.SaveChangesAsync();
        }
    }

    private static async Task StartGenres(MovieManiaContext context)
    {
        if (!await context.Genres.AnyAsync())
        {
            await context.Genres.AddRangeAsync(
                new List<GenreEntity>
                {
                    new() { Name = "Action", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Comedy", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Drama", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Horror", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Sci-Fi", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Romance", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Thriller", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Fantasy", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Adventure", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Mystery", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Animation", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Documentary", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Family", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Musical", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "War", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Western", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                });

            await context.SaveChangesAsync();
        }
    }

    private static async Task StartKeywords(MovieManiaContext context)
    {
        if (!await context.Keywords.AnyAsync())
            await context.Keywords.AddRangeAsync(
                new List<KeywordEntity>
                {
                new() { Keyword = "Action-packed", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { Keyword = "Romantic", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { Keyword = "Thriller", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { Keyword = "Epic", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { Keyword = "Heartwarming", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                });

        await context.SaveChangesAsync();
    }

    private static async Task StartLanguages(MovieManiaContext context)
    {
        if (!await context.Languages.AnyAsync())
        {
            await context.Languages.AddRangeAsync(
                new List<LanguageEntity>
                {
                    new() { Code = "EN", Language = "English", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Code = "ES", Language = "Spanish", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Code = "FR", Language = "French", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Code = "DE", Language = "German", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Code = "ZH", Language = "Chinese", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Code = "JA", Language = "Japanese", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Code = "RU", Language = "Russian", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Code = "PT", Language = "Portuguese", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Code = "IT", Language = "Italian", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Code = "HI", Language = "Hindi", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Code = "AR", Language = "Arabic", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Code = "KO", Language = "Korean", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Code = "NL", Language = "Dutch", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Code = "SV", Language = "Swedish", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Code = "DA", Language = "Danish", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Code = "NO", Language = "Norwegian", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Code = "FI", Language = "Finnish", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Code = "PL", Language = "Polish", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Code = "TR", Language = "Turkish", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Code = "HE", Language = "Hebrew", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                });

            await context.SaveChangesAsync();
        }
    }

    private static async Task StartLanguageRoles(MovieManiaContext context)
    {
        if (!await context.LanguageRoles.AnyAsync())
        {
            await context.LanguageRoles.AddRangeAsync(
                new List<LanguageRoleEntity>
                {
                    new() { Role = "Lead", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Role = "Supporting", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Role = "Cameo", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Role = "Narrator", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Role = "Voice Actor", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Role = "Director", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Role = "Producer", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Role = "Screenwriter", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Role = "Composer", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Role = "Editor", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                });

            await context.SaveChangesAsync();
        }
    }

    private static async Task StartPersons(MovieManiaContext context)
    {
        if (!await context.Persons.AnyAsync())
        {
            await context.Persons.AddRangeAsync(
                new List<PersonEntity>
                {
                    new() { Name = "John Doe", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Jane Smith", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Michael Johnson", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Emily Davis", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Robert De Niro", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Meryl Streep", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Steven Spielberg", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Martin Scorsese", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Christopher Nolan", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Quentin Tarantino", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Leonardo DiCaprio", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Tom Hanks", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Scarlett Johansson", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Brad Pitt", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Jennifer Lawrence", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Natalie Portman", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Christian Bale", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "George Clooney", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Emma Watson", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Harrison Ford", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Johnny Depp", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Julia Roberts", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Denzel Washington", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Morgan Freeman", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Matt Damon", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Anne Hathaway", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Ryan Gosling", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Natalie Dormer", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Idris Elba", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Gal Gadot", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Chris Hemsworth", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Robert Downey Jr.", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Chris Evans", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Halle Berry", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Mark Ruffalo", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Margot Robbie", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Tom Hardy", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Benedict Cumberbatch", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Rami Malek", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Zoe Saldana", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Amy Adams", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Viola Davis", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Joaquin Phoenix", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Eddie Redmayne", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Saoirse Ronan", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Jake Gyllenhaal", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Chadwick Boseman", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Mahershala Ali", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Tessa Thompson", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Timothée Chalamet", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Florence Pugh", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Zendaya", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                });

            await context.SaveChangesAsync();
        }
    }

    private static async Task StartProductionCompanies(MovieManiaContext context)
    {
        if (!await context.ProductionCompanies.AnyAsync())
        {
            await context.ProductionCompanies.AddRangeAsync(
                new List<ProductionCompanyEntity>
                {
                    new() { Name = "Warner Bros.", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Universal Pictures", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "20th Century Studios", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Sony Pictures", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Paramount Pictures", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Walt Disney Pictures", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Columbia Pictures", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "Lionsgate", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "New Line Cinema", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new() { Name = "MGM", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                });

            await context.SaveChangesAsync();
        }
    }

}
