using MovieMania.Core.Contexts.Entities;

namespace MovieMania.Core.Repositories.Interfaces;

public interface IDatabaseMemory
{
    /// <summary>
    /// Interface for managing in-memory database operations.
    /// </summary>
    public interface IDatabaseMemory
    {
        /// <summary>
        /// Gets the list of countries.
        /// </summary>
        IEnumerable<CountryEntity> Countries { get; }

        /// <summary>
        /// Gets the list of departments.
        /// </summary>
        IEnumerable<DepartmentEntity> Departments { get; }

        /// <summary>
        /// Gets the list of genders.
        /// </summary>
        IEnumerable<GenderEntity> Genders { get; }

        /// <summary>
        /// Gets the list of genres.
        /// </summary>
        IEnumerable<GenreEntity> Genres { get; }

        /// <summary>
        /// Gets the list of keywords.
        /// </summary>
        IEnumerable<KeywordEntity> Keywords { get; }

        /// <summary>
        /// Gets the list of languages.
        /// </summary>
        IEnumerable<LanguageEntity> Languages { get; }

        /// <summary>
        /// Gets the list of language roles.
        /// </summary>
        IEnumerable<LanguageRoleEntity> LanguageRoles { get; }

        /// <summary>
        /// Gets the list of production companies.
        /// </summary>
        IEnumerable<ProductionCompanyEntity> ProductionCompanies { get; }

        /// <summary>
        /// Starts the process of updating the in-memory database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task Start();

        /// <summary>
        /// Updates the list of countries.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateCountries();

        /// <summary>
        /// Updates the list of departments.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateDepartments();

        /// <summary>
        /// Updates the list of genders.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateGenders();

        /// <summary>
        /// Updates the list of genres.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateGenres();

        /// <summary>
        /// Updates the list of keywords.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateKeywords();

        /// <summary>
        /// Updates the list of languages.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateLanguages();

        /// <summary>
        /// Updates the list of language roles.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateLanguageRoles();

        /// <summary>
        /// Updates the list of production companies.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateProductionCompanies();
    }
}
