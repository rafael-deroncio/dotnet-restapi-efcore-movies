using Microsoft.EntityFrameworkCore;
using MovieMania.Core.Contexts;
using MovieMania.Core.Contexts.Entities;
using MovieMania.Core.Repositories.Interfaces;

namespace MovieMania.Core.Repositories;

public class DatabaseMemory : IDatabaseMemory
{
    private readonly MovieManiaContext _context;
    private List<CountryEntity> _countries;
    private List<DepartmentEntity> _departments;
    private List<GenderEntity> _genders;
    private List<GenreEntity> _genres;
    private List<KeywordEntity> _keywords;
    private List<LanguageEntity> _languages;
    private List<LanguageRoleEntity> _languageRoles;
    private List<ProductionCompanyEntity> _productionCompanies;

    public DatabaseMemory(MovieManiaContext context)
    {
        this._context = context;
        Start().GetAwaiter().GetResult();
    }

    public IEnumerable<CountryEntity> Countries { get { return this._countries; } }
    public IEnumerable<DepartmentEntity> Departments { get { return this._departments; } }
    public IEnumerable<GenderEntity> Genders { get { return this._genders; } }
    public IEnumerable<GenreEntity> Genres { get { return this._genres; } }
    public IEnumerable<KeywordEntity> Keywords { get { return this._keywords; } }
    public IEnumerable<LanguageEntity> Languages { get { return this._languages; } }
    public IEnumerable<LanguageRoleEntity> LanguageRoles { get { return this._languageRoles; } }
    public IEnumerable<ProductionCompanyEntity> ProductionCompanies { get { return this._productionCompanies; } }


    private async Task Start()
    {
        await UpdateCountries();
        await UpdateDepartments();
        await UpdateGenders();
        await UpdateGenres();
        await UpdateKeywords();
        await UpdateLanguages();
        await UpdateLanguageRoles();
        await UpdateProductionCompanies();
    }

    public async Task UpdateCountries()
    {
        _countries = await _context.Countries.ToListAsync();
    }

    public async Task UpdateDepartments()
    {
        _departments = await _context.Departments.ToListAsync();
    }

    public async Task UpdateGenders()
    {
        _genders = await _context.Genders.ToListAsync();
    }

    public async Task UpdateGenres()
    {
        _genres = await _context.Genres.ToListAsync();
    }

    public async Task UpdateKeywords()
    {
        _keywords = await _context.Keywords.ToListAsync();
    }

    public async Task UpdateLanguages()
    {
        _languages = await _context.Languages.ToListAsync();
    }

    public async Task UpdateLanguageRoles()
    {
        _languageRoles = await _context.LanguageRoles.ToListAsync();
    }

    public async Task UpdateProductionCompanies()
    {
        _productionCompanies = await _context.ProductionCompanies.ToListAsync();
    }
}
