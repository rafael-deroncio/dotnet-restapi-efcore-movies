using MovieMania.Core.Exceptions;
using MovieMania.Core.Services;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;
using MovieMania.Test.Fixtures;
using NSubstitute.Core;

namespace MovieMania.Test.Services;

public class MovieServiceTest
{
    #region Get
    [Fact]
    public async Task GetMovieById_ValidId_ReturnsMovieResponseWithFullObject()
    {
        // Arrange
        int movieId = 1;
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity(movieId);
        MovieService service = fixture.Instance();

        // Act
        MovieFilterRequest filter = fixture.MovieFilterRequestMock(full: true);
        MovieResponse response = await service.GetMovieById(movieId, filter);

        // AssertWithMovieEntity
        Assert.NotNull(response);
        Assert.NotEmpty(response.ProductionCountries);
        Assert.NotEmpty(response.Companies);
        Assert.NotEmpty(response.Languages);
        Assert.NotEmpty(response.Genres);
        Assert.NotEmpty(response.Keywords);
        Assert.NotEmpty(response.Casts);
        Assert.NotEmpty(response.Crews);
    }

    [Fact]
    public async Task GetMovieById_ValidId_ReturnsMovieResponseWithProductionCountries()
    {
        // Arrange
        int movieId = 1;
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity(movieId);
        MovieService service = fixture.Instance();

        // Act
        MovieFilterRequest filter = fixture.MovieFilterRequestMock();
        filter.AddProductionCountries = true;
        MovieResponse response = await service.GetMovieById(movieId, filter);

        // Assert
        Assert.NotNull(response);
        Assert.NotEmpty(response.ProductionCountries);
        Assert.Empty(response.Companies);
        Assert.Empty(response.Languages);
        Assert.Empty(response.Genres);
        Assert.Empty(response.Keywords);
        Assert.Empty(response.Casts);
        Assert.Empty(response.Crews);
    }

    [Fact]
    public async Task GetMovieById_ValidId_ReturnsMovieResponseWithoutRelatedObjects()
    {
        // Arrange
        int movieId = 1;
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity(movieId);
        MovieService service = fixture.Instance();

        // Act
        MovieFilterRequest filter = fixture.MovieFilterRequestMock();
        MovieResponse response = await service.GetMovieById(movieId, filter);

        // Assert
        Assert.NotNull(response);
        Assert.Empty(response.ProductionCountries);
        Assert.Empty(response.Companies);
        Assert.Empty(response.Languages);
        Assert.Empty(response.Genres);
        Assert.Empty(response.Keywords);
        Assert.Empty(response.Casts);
        Assert.Empty(response.Crews);
    }

    [Fact]
    public async Task GetMovieById_InvalidId_ThrowsException()
    {
        // Arrange
        int movieId = 1;
        string exceptionTitle = "Movie Not Found";
        string exceptionMesage = $"Movie with id {movieId} not exists";
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity();
        MovieService service = fixture.Instance();

        // Act
        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            MovieFilterRequest filter = fixture.MovieFilterRequestMock();
            MovieResponse response = await service.GetMovieById(movieId, filter);
        });

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.Equal(exceptionMesage, exception.Message);
    }
    #endregion

    #region Create
    [Fact]
    public async Task CreateMovie_ValidRequest_ReturnsMovieResponse()
    {
        // Arrange
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity()
                                                               .WithGetCountryById()
                                                               .WithCountryEntity()
                                                               .WithGetLanguageById()
                                                               .WithLanguageEntity()
                                                               .WithGetLanguageRoleById()
                                                               .WithLanguageRoleEntity()
                                                               .WithGetGenreById()
                                                               .WithGenreEntity()
                                                               .WithGetKeywordById()
                                                               .WithKeywordEntity()
                                                               .WithGetProductionCompanyById()
                                                               .WithProductionCompanyEntity()
                                                               .WithGetGenderById()
                                                               .WithGenderEntity()
                                                               .WithGetPersonById()
                                                               .WithPersonEntity()
                                                               .WithGetDepartmentById()
                                                               .WithDepartmentEntity();

        // Act
        MovieRequest request = fixture.MovieRequestMock();
        MovieResponse response = await fixture.Instance().CreateMovie(request);

        IEnumerable<ICall> callsGetCountryById = fixture.CountryServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetCountryById");

        IEnumerable<ICall> callsGetLanguageById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageById");

        IEnumerable<ICall> callsGetLanguageRoleById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageRoleById");

        IEnumerable<ICall> callsGetGenreById = fixture.GenreServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenreById");

        IEnumerable<ICall> callsGetKeywordById = fixture.KeywordServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetKeywordById");

        IEnumerable<ICall> callsGetProductionCompanyById = fixture.ProductionCompanyServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetProductionCompanyById");

        IEnumerable<ICall> callsGenderServiceCalls = fixture.GenderServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenderById");

        IEnumerable<ICall> callsGetPersonById = fixture.PersonServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetPersonById");

        IEnumerable<ICall> callsGetDepartmentById = fixture.DepartmentServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetDepartmentById");

        // Assert
        Assert.NotNull(response);
        Assert.NotEmpty(callsGetPersonById);
        Assert.NotEmpty(callsGetCountryById);
        Assert.NotEmpty(callsGetLanguageRoleById);
        Assert.NotEmpty(callsGetGenreById);
        Assert.NotEmpty(callsGetKeywordById);
        Assert.NotEmpty(callsGetProductionCompanyById);
        Assert.NotEmpty(callsGenderServiceCalls);
        Assert.NotEmpty(callsGetDepartmentById);
    }

    [Fact]
    public async Task CreateMovie_InvalidRequest_ThrowsException()
    {
        // Arrange
        string title = "xpto";
        string exceptionTitle = "Error on create movie entity";
        string exceptionMessage = "Movie already registered with the same title.";
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity(title: title);

        // Act
        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            MovieRequest request = fixture.MovieRequestMock();
            request.Title = title;
            MovieResponse response = await fixture.Instance().CreateMovie(request);
        });

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.Equal(exceptionMessage, exception.Message);
    }

    [Fact]
    public async Task CreateMovie_InvalidProductionCountries_ThrowsException()
    {
        // Arrange
        string exceptionTitle = "Movie Entity Error";
        string exceptionMessage = "Error: Country with id ";
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity()
                                                               .WithGetCountryById(returnsNull: true)
                                                               .WithCountryEntity()
                                                               .WithGetLanguageById()
                                                               .WithLanguageEntity()
                                                               .WithGetLanguageRoleById()
                                                               .WithLanguageRoleEntity()
                                                               .WithGetGenreById()
                                                               .WithGenreEntity()
                                                               .WithGetKeywordById()
                                                               .WithKeywordEntity()
                                                               .WithGetProductionCompanyById()
                                                               .WithProductionCompanyEntity()
                                                               .WithGetGenderById()
                                                               .WithGenderEntity()
                                                               .WithGetPersonById()
                                                               .WithPersonEntity()
                                                               .WithGetDepartmentById()
                                                               .WithDepartmentEntity();

        // Act
        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            MovieRequest request = fixture.MovieRequestMock();
            MovieResponse response = await fixture.Instance().CreateMovie(request);
        });

        IEnumerable<ICall> callsGetCountryById = fixture.CountryServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetCountryById");

        IEnumerable<ICall> callsGetLanguageById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageById");

        IEnumerable<ICall> callsGetLanguageRoleById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageRoleById");

        IEnumerable<ICall> callsGetGenreById = fixture.GenreServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenreById");

        IEnumerable<ICall> callsGetKeywordById = fixture.KeywordServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetKeywordById");

        IEnumerable<ICall> callsGetProductionCompanyById = fixture.ProductionCompanyServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetProductionCompanyById");

        IEnumerable<ICall> callsGenderServiceCalls = fixture.GenderServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenderById");

        IEnumerable<ICall> callsGetPersonById = fixture.PersonServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetPersonById");

        IEnumerable<ICall> callsGetDepartmentById = fixture.DepartmentServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetDepartmentById");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.StartsWith(exceptionMessage, exception.Message);
        Assert.NotEmpty(callsGetPersonById);
        Assert.NotEmpty(callsGetCountryById);
        Assert.NotEmpty(callsGetLanguageRoleById);
        Assert.NotEmpty(callsGetGenreById);
        Assert.NotEmpty(callsGetKeywordById);
        Assert.NotEmpty(callsGetProductionCompanyById);
        Assert.NotEmpty(callsGenderServiceCalls);
        Assert.NotEmpty(callsGetDepartmentById);
    }

    [Fact]
    public async Task CreateMovie_InvalidLanguages_ThrowsException()
    {
        // Arrange
        string exceptionTitle = "Movie Entity Error";
        string exceptionMessage = "Error: Language with id ";
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity()
                                                               .WithGetCountryById()
                                                               .WithCountryEntity()
                                                               .WithGetLanguageById(returnsNull: true)
                                                               .WithLanguageEntity()
                                                               .WithGetLanguageRoleById()
                                                               .WithLanguageRoleEntity()
                                                               .WithGetGenreById()
                                                               .WithGenreEntity()
                                                               .WithGetKeywordById()
                                                               .WithKeywordEntity()
                                                               .WithGetProductionCompanyById()
                                                               .WithProductionCompanyEntity()
                                                               .WithGetGenderById()
                                                               .WithGenderEntity()
                                                               .WithGetPersonById()
                                                               .WithPersonEntity()
                                                               .WithGetDepartmentById()
                                                               .WithDepartmentEntity();

        // Act
        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            MovieRequest request = fixture.MovieRequestMock();
            MovieResponse response = await fixture.Instance().CreateMovie(request);
        });

        IEnumerable<ICall> callsGetCountryById = fixture.CountryServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetCountryById");

        IEnumerable<ICall> callsGetLanguageById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageById");

        IEnumerable<ICall> callsGetLanguageRoleById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageRoleById");

        IEnumerable<ICall> callsGetGenreById = fixture.GenreServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenreById");

        IEnumerable<ICall> callsGetKeywordById = fixture.KeywordServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetKeywordById");

        IEnumerable<ICall> callsGetProductionCompanyById = fixture.ProductionCompanyServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetProductionCompanyById");

        IEnumerable<ICall> callsGenderServiceCalls = fixture.GenderServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenderById");

        IEnumerable<ICall> callsGetPersonById = fixture.PersonServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetPersonById");

        IEnumerable<ICall> callsGetDepartmentById = fixture.DepartmentServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetDepartmentById");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.StartsWith(exceptionMessage, exception.Message);
        Assert.NotEmpty(callsGetPersonById);
        Assert.NotEmpty(callsGetCountryById);
        Assert.NotEmpty(callsGetLanguageRoleById);
        Assert.NotEmpty(callsGetGenreById);
        Assert.NotEmpty(callsGetKeywordById);
        Assert.NotEmpty(callsGetProductionCompanyById);
        Assert.NotEmpty(callsGenderServiceCalls);
        Assert.NotEmpty(callsGetDepartmentById);
    }

    [Fact]
    public async Task CreateMovie_InvalidLanguageRoles_ThrowsException()
    {
        // Arrange
        string exceptionTitle = "Movie Entity Error";
        string exceptionMessage = "Error: Language Role with id ";
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity()
                                                               .WithGetCountryById()
                                                               .WithCountryEntity()
                                                               .WithGetLanguageById()
                                                               .WithLanguageEntity()
                                                               .WithGetLanguageRoleById(returnsNull: true)
                                                               .WithLanguageRoleEntity()
                                                               .WithGetGenreById()
                                                               .WithGenreEntity()
                                                               .WithGetKeywordById()
                                                               .WithKeywordEntity()
                                                               .WithGetProductionCompanyById()
                                                               .WithProductionCompanyEntity()
                                                               .WithGetGenderById()
                                                               .WithGenderEntity()
                                                               .WithGetPersonById()
                                                               .WithPersonEntity()
                                                               .WithGetDepartmentById()
                                                               .WithDepartmentEntity();

        // Act
        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            MovieRequest request = fixture.MovieRequestMock();
            MovieResponse response = await fixture.Instance().CreateMovie(request);
        });

        IEnumerable<ICall> callsGetCountryById = fixture.CountryServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetCountryById");

        IEnumerable<ICall> callsGetLanguageById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageById");

        IEnumerable<ICall> callsGetLanguageRoleById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageRoleById");

        IEnumerable<ICall> callsGetGenreById = fixture.GenreServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenreById");

        IEnumerable<ICall> callsGetKeywordById = fixture.KeywordServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetKeywordById");

        IEnumerable<ICall> callsGetProductionCompanyById = fixture.ProductionCompanyServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetProductionCompanyById");

        IEnumerable<ICall> callsGenderServiceCalls = fixture.GenderServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenderById");

        IEnumerable<ICall> callsGetPersonById = fixture.PersonServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetPersonById");

        IEnumerable<ICall> callsGetDepartmentById = fixture.DepartmentServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetDepartmentById");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.StartsWith(exceptionMessage, exception.Message);
        Assert.NotEmpty(callsGetPersonById);
        Assert.NotEmpty(callsGetCountryById);
        Assert.NotEmpty(callsGetLanguageRoleById);
        Assert.NotEmpty(callsGetGenreById);
        Assert.NotEmpty(callsGetKeywordById);
        Assert.NotEmpty(callsGetProductionCompanyById);
        Assert.NotEmpty(callsGenderServiceCalls);
        Assert.NotEmpty(callsGetDepartmentById);
    }

    [Fact]
    public async Task CreateMovie_InvalidGenres_ThrowsException()
    {
        // Arrange
        string exceptionTitle = "Movie Entity Error";
        string exceptionMessage = "Error: Genre with id ";
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity()
                                                               .WithGetCountryById()
                                                               .WithCountryEntity()
                                                               .WithGetLanguageById()
                                                               .WithLanguageEntity()
                                                               .WithGetLanguageRoleById()
                                                               .WithLanguageRoleEntity()
                                                               .WithGetGenreById(returnsNull: true)
                                                               .WithGenreEntity()
                                                               .WithGetKeywordById()
                                                               .WithKeywordEntity()
                                                               .WithGetProductionCompanyById()
                                                               .WithProductionCompanyEntity()
                                                               .WithGetGenderById()
                                                               .WithGenderEntity()
                                                               .WithGetPersonById()
                                                               .WithPersonEntity()
                                                               .WithGetDepartmentById()
                                                               .WithDepartmentEntity();

        // Act
        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            MovieRequest request = fixture.MovieRequestMock();
            MovieResponse response = await fixture.Instance().CreateMovie(request);
        });

        IEnumerable<ICall> callsGetCountryById = fixture.CountryServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetCountryById");

        IEnumerable<ICall> callsGetLanguageById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageById");

        IEnumerable<ICall> callsGetLanguageRoleById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageRoleById");

        IEnumerable<ICall> callsGetGenreById = fixture.GenreServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenreById");

        IEnumerable<ICall> callsGetKeywordById = fixture.KeywordServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetKeywordById");

        IEnumerable<ICall> callsGetProductionCompanyById = fixture.ProductionCompanyServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetProductionCompanyById");

        IEnumerable<ICall> callsGenderServiceCalls = fixture.GenderServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenderById");

        IEnumerable<ICall> callsGetPersonById = fixture.PersonServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetPersonById");

        IEnumerable<ICall> callsGetDepartmentById = fixture.DepartmentServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetDepartmentById");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.StartsWith(exceptionMessage, exception.Message);
        Assert.NotEmpty(callsGetPersonById);
        Assert.NotEmpty(callsGetCountryById);
        Assert.NotEmpty(callsGetLanguageRoleById);
        Assert.NotEmpty(callsGetGenreById);
        Assert.NotEmpty(callsGetKeywordById);
        Assert.NotEmpty(callsGetProductionCompanyById);
        Assert.NotEmpty(callsGenderServiceCalls);
        Assert.NotEmpty(callsGetDepartmentById);
    }

    [Fact]
    public async Task CreateMovie_InvalidKeywords_ThrowsException()
    {
        // Arrange
        string exceptionTitle = "Movie Entity Error";
        string exceptionMessage = "Error: Keyword with id ";
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity()
                                                               .WithGetCountryById()
                                                               .WithCountryEntity()
                                                               .WithGetLanguageById()
                                                               .WithLanguageEntity()
                                                               .WithGetLanguageRoleById()
                                                               .WithLanguageRoleEntity()
                                                               .WithGetGenreById()
                                                               .WithGenreEntity()
                                                               .WithGetKeywordById(returnsNull: true)
                                                               .WithKeywordEntity()
                                                               .WithGetProductionCompanyById()
                                                               .WithProductionCompanyEntity()
                                                               .WithGetGenderById()
                                                               .WithGenderEntity()
                                                               .WithGetPersonById()
                                                               .WithPersonEntity()
                                                               .WithGetDepartmentById()
                                                               .WithDepartmentEntity();

        // Act
        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            MovieRequest request = fixture.MovieRequestMock();
            MovieResponse response = await fixture.Instance().CreateMovie(request);
        });

        IEnumerable<ICall> callsGetCountryById = fixture.CountryServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetCountryById");

        IEnumerable<ICall> callsGetLanguageById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageById");

        IEnumerable<ICall> callsGetLanguageRoleById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageRoleById");

        IEnumerable<ICall> callsGetGenreById = fixture.GenreServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenreById");

        IEnumerable<ICall> callsGetKeywordById = fixture.KeywordServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetKeywordById");

        IEnumerable<ICall> callsGetProductionCompanyById = fixture.ProductionCompanyServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetProductionCompanyById");

        IEnumerable<ICall> callsGenderServiceCalls = fixture.GenderServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenderById");

        IEnumerable<ICall> callsGetPersonById = fixture.PersonServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetPersonById");

        IEnumerable<ICall> callsGetDepartmentById = fixture.DepartmentServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetDepartmentById");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.StartsWith(exceptionMessage, exception.Message);
        Assert.NotEmpty(callsGetPersonById);
        Assert.NotEmpty(callsGetCountryById);
        Assert.NotEmpty(callsGetLanguageRoleById);
        Assert.NotEmpty(callsGetGenreById);
        Assert.NotEmpty(callsGetKeywordById);
        Assert.NotEmpty(callsGetProductionCompanyById);
        Assert.NotEmpty(callsGenderServiceCalls);
        Assert.NotEmpty(callsGetDepartmentById);
    }

    [Fact]
    public async Task CreateMovie_InvalidCompanies_ThrowsException()
    {
        // Arrange
        string exceptionTitle = "Movie Entity Error";
        string exceptionMessage = "Error: Production Company with id ";
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity()
                                                               .WithGetCountryById()
                                                               .WithCountryEntity()
                                                               .WithGetLanguageById()
                                                               .WithLanguageEntity()
                                                               .WithGetLanguageRoleById()
                                                               .WithLanguageRoleEntity()
                                                               .WithGetGenreById()
                                                               .WithGenreEntity()
                                                               .WithGetKeywordById()
                                                               .WithKeywordEntity()
                                                               .WithGetProductionCompanyById(returnsNull: true)
                                                               .WithProductionCompanyEntity()
                                                               .WithGetGenderById()
                                                               .WithGenderEntity()
                                                               .WithGetPersonById()
                                                               .WithPersonEntity()
                                                               .WithGetDepartmentById()
                                                               .WithDepartmentEntity();

        // Act
        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            MovieRequest request = fixture.MovieRequestMock();
            MovieResponse response = await fixture.Instance().CreateMovie(request);
        });

        IEnumerable<ICall> callsGetCountryById = fixture.CountryServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetCountryById");

        IEnumerable<ICall> callsGetLanguageById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageById");

        IEnumerable<ICall> callsGetLanguageRoleById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageRoleById");

        IEnumerable<ICall> callsGetGenreById = fixture.GenreServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenreById");

        IEnumerable<ICall> callsGetKeywordById = fixture.KeywordServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetKeywordById");

        IEnumerable<ICall> callsGetProductionCompanyById = fixture.ProductionCompanyServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetProductionCompanyById");

        IEnumerable<ICall> callsGenderServiceCalls = fixture.GenderServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenderById");

        IEnumerable<ICall> callsGetPersonById = fixture.PersonServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetPersonById");

        IEnumerable<ICall> callsGetDepartmentById = fixture.DepartmentServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetDepartmentById");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.StartsWith(exceptionMessage, exception.Message);
        Assert.NotEmpty(callsGetPersonById);
        Assert.NotEmpty(callsGetCountryById);
        Assert.NotEmpty(callsGetLanguageRoleById);
        Assert.NotEmpty(callsGetGenreById);
        Assert.NotEmpty(callsGetKeywordById);
        Assert.NotEmpty(callsGetProductionCompanyById);
        Assert.NotEmpty(callsGenderServiceCalls);
        Assert.NotEmpty(callsGetDepartmentById);
    }

    [Fact]
    public async Task CreateMovie_InvalidCasts_ThrowsException()
    {
        // Arrange
        string exceptionTitle = "Movie Entity Error";
        string exceptionMessage = "Error: Gender with id, Error: Person with id";
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity()
                                                               .WithGetCountryById()
                                                               .WithCountryEntity()
                                                               .WithGetLanguageById()
                                                               .WithLanguageEntity()
                                                               .WithGetLanguageRoleById()
                                                               .WithLanguageRoleEntity()
                                                               .WithGetGenreById()
                                                               .WithGenreEntity()
                                                               .WithGetKeywordById()
                                                               .WithKeywordEntity()
                                                               .WithGetProductionCompanyById()
                                                               .WithProductionCompanyEntity()
                                                               .WithGetGenderById(returnsNull: true)
                                                               .WithGenderEntity()
                                                               .WithGetPersonById(returnsNull: true)
                                                               .WithPersonEntity()
                                                               .WithGetDepartmentById()
                                                               .WithDepartmentEntity();

        // Act
        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            MovieRequest request = fixture.MovieRequestMock();
            MovieResponse response = await fixture.Instance().CreateMovie(request);
        });

        IEnumerable<ICall> callsGetCountryById = fixture.CountryServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetCountryById");

        IEnumerable<ICall> callsGetLanguageById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageById");

        IEnumerable<ICall> callsGetLanguageRoleById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageRoleById");

        IEnumerable<ICall> callsGetGenreById = fixture.GenreServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenreById");

        IEnumerable<ICall> callsGetKeywordById = fixture.KeywordServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetKeywordById");

        IEnumerable<ICall> callsGetProductionCompanyById = fixture.ProductionCompanyServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetProductionCompanyById");

        IEnumerable<ICall> callsGenderServiceCalls = fixture.GenderServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenderById");

        IEnumerable<ICall> callsGetPersonById = fixture.PersonServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetPersonById");

        IEnumerable<ICall> callsGetDepartmentById = fixture.DepartmentServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetDepartmentById");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.True(exception.Message.Split(",").Where(x => x.StartsWith(exceptionMessage.Split(",")[0])).Any());
        Assert.True(exception.Message.Split(",").Where(x => x.StartsWith(exceptionMessage.Split(",")[1])).Any());
        Assert.NotEmpty(callsGetPersonById);
        Assert.NotEmpty(callsGetCountryById);
        Assert.NotEmpty(callsGetLanguageRoleById);
        Assert.NotEmpty(callsGetGenreById);
        Assert.NotEmpty(callsGetKeywordById);
        Assert.NotEmpty(callsGetProductionCompanyById);
        Assert.NotEmpty(callsGenderServiceCalls);
        Assert.NotEmpty(callsGetDepartmentById);
    }

    [Fact]
    public async Task CreateMovie_InvalidCrews_ThrowsException()
    {
        // Arrange
        string exceptionTitle = "Movie Entity Error";
        string exceptionMessage = "Error: Person with id, Error: Department with id";
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity()
                                                               .WithGetCountryById()
                                                               .WithCountryEntity()
                                                               .WithGetLanguageById()
                                                               .WithLanguageEntity()
                                                               .WithGetLanguageRoleById()
                                                               .WithLanguageRoleEntity()
                                                               .WithGetGenreById()
                                                               .WithGenreEntity()
                                                               .WithGetKeywordById()
                                                               .WithKeywordEntity()
                                                               .WithGetProductionCompanyById()
                                                               .WithProductionCompanyEntity()
                                                               .WithGetGenderById()
                                                               .WithGenderEntity()
                                                               .WithGetPersonById(returnsNull: true)
                                                               .WithPersonEntity()
                                                               .WithGetDepartmentById(returnsNull: true)
                                                               .WithDepartmentEntity();

        // Act
        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            MovieRequest request = fixture.MovieRequestMock();
            MovieResponse response = await fixture.Instance().CreateMovie(request);
        });

        IEnumerable<ICall> callsGetCountryById = fixture.CountryServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetCountryById");

        IEnumerable<ICall> callsGetLanguageById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageById");

        IEnumerable<ICall> callsGetLanguageRoleById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageRoleById");

        IEnumerable<ICall> callsGetGenreById = fixture.GenreServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenreById");

        IEnumerable<ICall> callsGetKeywordById = fixture.KeywordServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetKeywordById");

        IEnumerable<ICall> callsGetProductionCompanyById = fixture.ProductionCompanyServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetProductionCompanyById");

        IEnumerable<ICall> callsGenderServiceCalls = fixture.GenderServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenderById");

        IEnumerable<ICall> callsGetPersonById = fixture.PersonServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetPersonById");

        IEnumerable<ICall> callsGetDepartmentById = fixture.DepartmentServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetDepartmentById");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.True(exception.Message.Split(",").Where(x => x.StartsWith(exceptionMessage.Split(",")[0])).Any());
        Assert.True(exception.Message.Split(",").Where(x => x.StartsWith(exceptionMessage.Split(",")[1])).Any());
        Assert.NotEmpty(callsGetPersonById);
        Assert.NotEmpty(callsGetCountryById);
        Assert.NotEmpty(callsGetLanguageRoleById);
        Assert.NotEmpty(callsGetGenreById);
        Assert.NotEmpty(callsGetKeywordById);
        Assert.NotEmpty(callsGetProductionCompanyById);
        Assert.NotEmpty(callsGenderServiceCalls);
        Assert.NotEmpty(callsGetDepartmentById);
    }
    #endregion

    #region Update
    [Fact]
    public async Task UpdateMovie_ValidIdAndRequest_ReturnsMovieResponse()
    {
        // Arrange
        int id = 1;
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity(id)
                                                               .WithGetCountryById()
                                                               .WithCountryEntity()
                                                               .WithGetLanguageById()
                                                               .WithLanguageEntity()
                                                               .WithGetLanguageRoleById()
                                                               .WithLanguageRoleEntity()
                                                               .WithGetGenreById()
                                                               .WithGenreEntity()
                                                               .WithGetKeywordById()
                                                               .WithKeywordEntity()
                                                               .WithGetProductionCompanyById()
                                                               .WithProductionCompanyEntity()
                                                               .WithGetGenderById()
                                                               .WithGenderEntity()
                                                               .WithGetPersonById()
                                                               .WithPersonEntity()
                                                               .WithGetDepartmentById()
                                                               .WithDepartmentEntity();

        // Act
        MovieRequest request = fixture.MovieRequestMock();
        MovieResponse response = await fixture.Instance().UpdateMovie(id, request);

        IEnumerable<ICall> callsGetCountryById = fixture.CountryServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetCountryById");

        IEnumerable<ICall> callsGetLanguageById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageById");

        IEnumerable<ICall> callsGetLanguageRoleById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageRoleById");

        IEnumerable<ICall> callsGetGenreById = fixture.GenreServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenreById");

        IEnumerable<ICall> callsGetKeywordById = fixture.KeywordServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetKeywordById");

        IEnumerable<ICall> callsGetProductionCompanyById = fixture.ProductionCompanyServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetProductionCompanyById");

        IEnumerable<ICall> callsGenderServiceCalls = fixture.GenderServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenderById");

        IEnumerable<ICall> callsGetPersonById = fixture.PersonServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetPersonById");

        IEnumerable<ICall> callsGetDepartmentById = fixture.DepartmentServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetDepartmentById");

        // Assert
        Assert.NotNull(response);
        Assert.NotEmpty(callsGetPersonById);
        Assert.NotEmpty(callsGetCountryById);
        Assert.NotEmpty(callsGetLanguageRoleById);
        Assert.NotEmpty(callsGetGenreById);
        Assert.NotEmpty(callsGetKeywordById);
        Assert.NotEmpty(callsGetProductionCompanyById);
        Assert.NotEmpty(callsGenderServiceCalls);
        Assert.NotEmpty(callsGetDepartmentById);
    }

    [Fact]
    public async Task UpdateMovie_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Movie Not Found";
        string exceptionMesage = $"Movie with id {id} not exists";
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity();

        // Act
        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            MovieRequest request = fixture.MovieRequestMock();
            MovieResponse response = await fixture.Instance().UpdateMovie(id, request);
        });

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.Equal(exceptionMesage, exception.Message);
    }

    [Fact]
    public async Task UpdateMovie_InvalidRequest_ThrowsException()
    {
        // Arrange
        int id = 1;
        string title = "XPTO";
        string exceptionTitle = "Error on update movie entity";
        string exceptionMesage = "Movie already registered with the same title.";
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity(id: id, title: title);

        // Act
        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            MovieRequest request = fixture.MovieRequestMock();
            request.Title = title;
            MovieResponse response = await fixture.Instance().UpdateMovie(id, request);
        });

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.Equal(exceptionMesage, exception.Message);
    }

    [Fact]
    public async Task UpdateMovie_InvalidProductionCountries_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Movie Entity Error";
        string exceptionMessage = "Error: Country with id ";
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity(id)
                                                               .WithGetCountryById(returnsNull: true)
                                                               .WithCountryEntity();

        // Act
        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            MovieRequest request = fixture.MovieRequestMock();
            MovieResponse response = await fixture.Instance().UpdateMovie(id, request);
        });

        IEnumerable<ICall> callsGetCountryById = fixture.CountryServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetCountryById");

        IEnumerable<ICall> callsGetLanguageById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageById");

        IEnumerable<ICall> callsGetLanguageRoleById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageRoleById");

        IEnumerable<ICall> callsGetGenreById = fixture.GenreServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenreById");

        IEnumerable<ICall> callsGetKeywordById = fixture.KeywordServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetKeywordById");

        IEnumerable<ICall> callsGetProductionCompanyById = fixture.ProductionCompanyServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetProductionCompanyById");

        IEnumerable<ICall> callsGenderServiceCalls = fixture.GenderServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenderById");

        IEnumerable<ICall> callsGetPersonById = fixture.PersonServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetPersonById");

        IEnumerable<ICall> callsGetDepartmentById = fixture.DepartmentServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetDepartmentById");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.StartsWith(exceptionMessage, exception.Message);
        Assert.NotEmpty(callsGetCountryById);
        Assert.Empty(callsGetPersonById);
        Assert.Empty(callsGetLanguageRoleById);
        Assert.Empty(callsGetGenreById);
        Assert.Empty(callsGetKeywordById);
        Assert.Empty(callsGetProductionCompanyById);
        Assert.Empty(callsGenderServiceCalls);
        Assert.Empty(callsGetDepartmentById);
    }

    [Fact]
    public async Task UpdateMovie_InvalidLanguages_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Movie Entity Error";
        string exceptionMessage = "Error: Language with id ";
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity(id)
                                                               .WithGetCountryById()
                                                               .WithCountryEntity()
                                                               .WithLanguageEntity()
                                                               .WithGetLanguageById(returnsNull: true)
                                                               .WithLanguageRoleEntity()
                                                               .WithGetLanguageRoleById();

        // Act
        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            MovieRequest request = fixture.MovieRequestMock();
            MovieResponse response = await fixture.Instance().UpdateMovie(id, request);
        });

        IEnumerable<ICall> callsGetCountryById = fixture.CountryServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetCountryById");

        IEnumerable<ICall> callsGetLanguageById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageById");

        IEnumerable<ICall> callsGetLanguageRoleById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageRoleById");

        IEnumerable<ICall> callsGetGenreById = fixture.GenreServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenreById");

        IEnumerable<ICall> callsGetKeywordById = fixture.KeywordServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetKeywordById");

        IEnumerable<ICall> callsGetProductionCompanyById = fixture.ProductionCompanyServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetProductionCompanyById");

        IEnumerable<ICall> callsGenderServiceCalls = fixture.GenderServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenderById");

        IEnumerable<ICall> callsGetPersonById = fixture.PersonServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetPersonById");

        IEnumerable<ICall> callsGetDepartmentById = fixture.DepartmentServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetDepartmentById");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.StartsWith(exceptionMessage, exception.Message);
        Assert.NotEmpty(callsGetCountryById);
        Assert.NotEmpty(callsGetLanguageById);
        Assert.NotEmpty(callsGetLanguageRoleById);
        Assert.Empty(callsGetGenreById);
        Assert.Empty(callsGetPersonById);
        Assert.Empty(callsGetKeywordById);
        Assert.Empty(callsGetProductionCompanyById);
        Assert.Empty(callsGenderServiceCalls);
        Assert.Empty(callsGetDepartmentById);
    }

    [Fact]
    public async Task UpdateMovie_InvalidLanguageRoles_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Movie Entity Error";
        string exceptionMessage = "Error: Language Role with id ";
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity(id)
                                                               .WithGetCountryById()
                                                               .WithCountryEntity()
                                                               .WithLanguageEntity()
                                                               .WithGetLanguageById()
                                                               .WithLanguageRoleEntity()
                                                               .WithGetLanguageRoleById(returnsNull: true);

        // Act
        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            MovieRequest request = fixture.MovieRequestMock();
            MovieResponse response = await fixture.Instance().UpdateMovie(id, request);
        });

        IEnumerable<ICall> callsGetCountryById = fixture.CountryServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetCountryById");

        IEnumerable<ICall> callsGetLanguageById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageById");

        IEnumerable<ICall> callsGetLanguageRoleById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageRoleById");

        IEnumerable<ICall> callsGetGenreById = fixture.GenreServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenreById");

        IEnumerable<ICall> callsGetKeywordById = fixture.KeywordServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetKeywordById");

        IEnumerable<ICall> callsGetProductionCompanyById = fixture.ProductionCompanyServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetProductionCompanyById");

        IEnumerable<ICall> callsGenderServiceCalls = fixture.GenderServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenderById");

        IEnumerable<ICall> callsGetPersonById = fixture.PersonServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetPersonById");

        IEnumerable<ICall> callsGetDepartmentById = fixture.DepartmentServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetDepartmentById");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.StartsWith(exceptionMessage, exception.Message);
        Assert.NotEmpty(callsGetCountryById);
        Assert.NotEmpty(callsGetLanguageById);
        Assert.NotEmpty(callsGetLanguageRoleById);
        Assert.Empty(callsGetGenreById);
        Assert.Empty(callsGetPersonById);
        Assert.Empty(callsGetKeywordById);
        Assert.Empty(callsGetProductionCompanyById);
        Assert.Empty(callsGenderServiceCalls);
        Assert.Empty(callsGetDepartmentById);
    }

    [Fact]
    public async Task UpdateMovie_InvalidGenres_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Movie Entity Error";
        string exceptionMessage = "Error: Genre with id ";
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity(id)
                                                               .WithGetCountryById()
                                                               .WithCountryEntity()
                                                               .WithLanguageEntity()
                                                               .WithGetLanguageById()
                                                               .WithLanguageRoleEntity()
                                                               .WithGetLanguageRoleById()
                                                               .WithGenreEntity()
                                                               .WithGetGenreById(returnsNull: true);

        // Act
        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            MovieRequest request = fixture.MovieRequestMock();
            MovieResponse response = await fixture.Instance().UpdateMovie(id, request);
        });

        IEnumerable<ICall> callsGetCountryById = fixture.CountryServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetCountryById");

        IEnumerable<ICall> callsGetLanguageById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageById");

        IEnumerable<ICall> callsGetLanguageRoleById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageRoleById");

        IEnumerable<ICall> callsGetGenreById = fixture.GenreServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenreById");

        IEnumerable<ICall> callsGetKeywordById = fixture.KeywordServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetKeywordById");

        IEnumerable<ICall> callsGetProductionCompanyById = fixture.ProductionCompanyServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetProductionCompanyById");

        IEnumerable<ICall> callsGenderServiceCalls = fixture.GenderServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenderById");

        IEnumerable<ICall> callsGetPersonById = fixture.PersonServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetPersonById");

        IEnumerable<ICall> callsGetDepartmentById = fixture.DepartmentServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetDepartmentById");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.StartsWith(exceptionMessage, exception.Message);
        Assert.NotEmpty(callsGetCountryById);
        Assert.NotEmpty(callsGetLanguageById);
        Assert.NotEmpty(callsGetLanguageRoleById);
        Assert.NotEmpty(callsGetGenreById);
        Assert.Empty(callsGetPersonById);
        Assert.Empty(callsGetKeywordById);
        Assert.Empty(callsGetProductionCompanyById);
        Assert.Empty(callsGenderServiceCalls);
        Assert.Empty(callsGetDepartmentById);
    }

    [Fact]
    public async Task UpdateMovie_InvalidKeywords_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Movie Entity Error";
        string exceptionMessage = "Error: Keyword with id ";
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity(id)
                                                               .WithGetCountryById()
                                                               .WithCountryEntity()
                                                               .WithLanguageEntity()
                                                               .WithGetLanguageById()
                                                               .WithLanguageRoleEntity()
                                                               .WithGetLanguageRoleById()
                                                               .WithGenreEntity()
                                                               .WithGetGenreById()
                                                               .WithKeywordEntity()
                                                               .WithGetKeywordById(returnsNull: true);

        // Act
        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            MovieRequest request = fixture.MovieRequestMock();
            MovieResponse response = await fixture.Instance().UpdateMovie(id, request);
        });

        IEnumerable<ICall> callsGetCountryById = fixture.CountryServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetCountryById");

        IEnumerable<ICall> callsGetLanguageById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageById");

        IEnumerable<ICall> callsGetLanguageRoleById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageRoleById");

        IEnumerable<ICall> callsGetGenreById = fixture.GenreServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenreById");

        IEnumerable<ICall> callsGetKeywordById = fixture.KeywordServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetKeywordById");

        IEnumerable<ICall> callsGetProductionCompanyById = fixture.ProductionCompanyServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetProductionCompanyById");

        IEnumerable<ICall> callsGenderServiceCalls = fixture.GenderServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenderById");

        IEnumerable<ICall> callsGetPersonById = fixture.PersonServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetPersonById");

        IEnumerable<ICall> callsGetDepartmentById = fixture.DepartmentServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetDepartmentById");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.StartsWith(exceptionMessage, exception.Message);
        Assert.NotEmpty(callsGetCountryById);
        Assert.NotEmpty(callsGetLanguageById);
        Assert.NotEmpty(callsGetLanguageRoleById);
        Assert.NotEmpty(callsGetGenreById);
        Assert.NotEmpty(callsGetKeywordById);
        Assert.Empty(callsGetProductionCompanyById);
        Assert.Empty(callsGenderServiceCalls);
        Assert.Empty(callsGetPersonById);
        Assert.Empty(callsGetDepartmentById);
    }

    [Fact]
    public async Task UpdateMovie_InvalidCompanies_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Movie Entity Error";
        string exceptionMessage = "Error: Production Company with id ";
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity(id)
                                                               .WithGetCountryById()
                                                               .WithCountryEntity()
                                                               .WithLanguageEntity()
                                                               .WithGetLanguageById()
                                                               .WithLanguageRoleEntity()
                                                               .WithGetLanguageRoleById()
                                                               .WithGenreEntity()
                                                               .WithGetGenreById()
                                                               .WithKeywordEntity()
                                                               .WithGetKeywordById()
                                                               .WithProductionCompanyEntity()
                                                               .WithGetProductionCompanyById(returnsNull: true);

        // Act
        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            MovieRequest request = fixture.MovieRequestMock();
            MovieResponse response = await fixture.Instance().UpdateMovie(id, request);
        });

        IEnumerable<ICall> callsGetCountryById = fixture.CountryServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetCountryById");

        IEnumerable<ICall> callsGetLanguageById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageById");

        IEnumerable<ICall> callsGetLanguageRoleById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageRoleById");

        IEnumerable<ICall> callsGetGenreById = fixture.GenreServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenreById");

        IEnumerable<ICall> callsGetKeywordById = fixture.KeywordServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetKeywordById");

        IEnumerable<ICall> callsGetProductionCompanyById = fixture.ProductionCompanyServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetProductionCompanyById");

        IEnumerable<ICall> callsGenderServiceCalls = fixture.GenderServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenderById");

        IEnumerable<ICall> callsGetPersonById = fixture.PersonServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetPersonById");

        IEnumerable<ICall> callsGetDepartmentById = fixture.DepartmentServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetDepartmentById");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.StartsWith(exceptionMessage, exception.Message);
        Assert.NotEmpty(callsGetCountryById);
        Assert.NotEmpty(callsGetLanguageById);
        Assert.NotEmpty(callsGetLanguageRoleById);
        Assert.NotEmpty(callsGetGenreById);
        Assert.NotEmpty(callsGetKeywordById);
        Assert.NotEmpty(callsGetProductionCompanyById);
        Assert.Empty(callsGenderServiceCalls);
        Assert.Empty(callsGetPersonById);
        Assert.Empty(callsGetDepartmentById);
    }

    [Fact]
    public async Task UpdateMovie_InvalidCasts_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Movie Entity Error";
        string exceptionMessage = "Error: Gender with id, Error: Person with id";
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity(id)
                                                               .WithGetCountryById()
                                                               .WithCountryEntity()
                                                               .WithLanguageEntity()
                                                               .WithGetLanguageById()
                                                               .WithLanguageRoleEntity()
                                                               .WithGetLanguageRoleById()
                                                               .WithGenreEntity()
                                                               .WithGetGenreById()
                                                               .WithKeywordEntity()
                                                               .WithGetKeywordById()
                                                               .WithProductionCompanyEntity()
                                                               .WithGetProductionCompanyById()
                                                               .WithGenderEntity()
                                                               .WithGetGenderById(returnsNull: true)
                                                               .WithPersonEntity()
                                                               .WithGetPersonById(returnsNull: true);

        // Act
        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            MovieRequest request = fixture.MovieRequestMock();
            MovieResponse response = await fixture.Instance().UpdateMovie(id, request);
        });

        IEnumerable<ICall> callsGetCountryById = fixture.CountryServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetCountryById");

        IEnumerable<ICall> callsGetLanguageById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageById");

        IEnumerable<ICall> callsGetLanguageRoleById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageRoleById");

        IEnumerable<ICall> callsGetGenreById = fixture.GenreServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenreById");

        IEnumerable<ICall> callsGetKeywordById = fixture.KeywordServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetKeywordById");

        IEnumerable<ICall> callsGetProductionCompanyById = fixture.ProductionCompanyServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetProductionCompanyById");

        IEnumerable<ICall> callsGenderServiceCalls = fixture.GenderServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenderById");

        IEnumerable<ICall> callsGetPersonById = fixture.PersonServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetPersonById");

        IEnumerable<ICall> callsGetDepartmentById = fixture.DepartmentServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetDepartmentById");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.True(exception.Message.Split(",").Where(x => x.StartsWith(exceptionMessage.Split(",")[0])).Any());
        Assert.True(exception.Message.Split(",").Where(x => x.StartsWith(exceptionMessage.Split(",")[1])).Any());
        Assert.NotEmpty(callsGetCountryById);
        Assert.NotEmpty(callsGetLanguageById);
        Assert.NotEmpty(callsGetLanguageRoleById);
        Assert.NotEmpty(callsGetGenreById);
        Assert.NotEmpty(callsGetKeywordById);
        Assert.NotEmpty(callsGetProductionCompanyById);
        Assert.NotEmpty(callsGenderServiceCalls);
        Assert.NotEmpty(callsGetPersonById);
        Assert.Empty(callsGetDepartmentById);
    }

    [Fact]
    public async Task UpdateMovie_InvalidCrews_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Movie Entity Error";
        string exceptionMessage = "Error: Person with id";
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity(id)
                                                               .WithGetCountryById()
                                                               .WithCountryEntity()
                                                               .WithLanguageEntity()
                                                               .WithGetLanguageById()
                                                               .WithLanguageRoleEntity()
                                                               .WithGetLanguageRoleById()
                                                               .WithGenreEntity()
                                                               .WithGetGenreById()
                                                               .WithKeywordEntity()
                                                               .WithGetKeywordById()
                                                               .WithProductionCompanyEntity()
                                                               .WithGetProductionCompanyById()
                                                               .WithGenderEntity()
                                                               .WithGetGenderById()
                                                               .WithPersonEntity()
                                                               .WithGetPersonById(returnsNull: true)
                                                               .WithDepartmentEntity()
                                                               .WithGetDepartmentById(returnsNull: true);

        // Act
        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            MovieRequest request = fixture.MovieRequestMock();
            MovieResponse response = await fixture.Instance().UpdateMovie(id, request);
        });

        IEnumerable<ICall> callsGetCountryById = fixture.CountryServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetCountryById");

        IEnumerable<ICall> callsGetLanguageById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageById");

        IEnumerable<ICall> callsGetLanguageRoleById = fixture.LanguageServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetLanguageRoleById");

        IEnumerable<ICall> callsGetGenreById = fixture.GenreServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenreById");

        IEnumerable<ICall> callsGetKeywordById = fixture.KeywordServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetKeywordById");

        IEnumerable<ICall> callsGetProductionCompanyById = fixture.ProductionCompanyServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetProductionCompanyById");

        IEnumerable<ICall> callsGenderServiceCalls = fixture.GenderServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetGenderById");

        IEnumerable<ICall> callsGetPersonById = fixture.PersonServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetPersonById");

        IEnumerable<ICall> callsGetDepartmentById = fixture.DepartmentServiceCalls()
            .Where(x => x.GetMethodInfo().Name == "GetDepartmentById");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.True(exception.Message.Split(",").Where(x => x.StartsWith(exceptionMessage.Split(",")[0])).Any());
        Assert.NotEmpty(callsGetCountryById);
        Assert.NotEmpty(callsGetLanguageById);
        Assert.NotEmpty(callsGetLanguageRoleById);
        Assert.NotEmpty(callsGetGenreById);
        Assert.NotEmpty(callsGetKeywordById);
        Assert.NotEmpty(callsGetProductionCompanyById);
        Assert.NotEmpty(callsGenderServiceCalls);
        Assert.NotEmpty(callsGetPersonById);
        Assert.Empty(callsGetDepartmentById);
    }
    #endregion

    #region Delete
    [Fact]
    public async Task DeleteMovie_ValidId_ReturnsTrue()
    {
        // Arrange
        int id = 1;
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity(id);

        // Act
        bool result = await fixture.Instance().DeleteMovie(id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteMovie_InvalidId_ThrowsException()
    {
        // Arrange
        // Arrange
        int id = 1;
        string exceptionTitle = "Movie Not Found";
        string exceptionMesage = $"Movie with id {id} not exists";
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity();

        // Act
        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            await fixture.Instance().DeleteMovie(id);
        });

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.Equal(exceptionMesage, exception.Message);
    }
    #endregion

    #region Paged
    [Fact]
    public async Task GetPagedMovies_ValidRequest_ReturnsPaginatedMovieResponse()
    {
        // Arrange
        int id = 1;
        MovieServiceFixture fixture = new MovieServiceFixture().WithMovieEntity(id)
                                                               .WithGetPagination();
        MovieService service = fixture.Instance();

        // Act
        MovieFilterRequest filter = fixture.MovieFilterRequestMock(full: true);
        PaginationRequest request = fixture.PaginationRequestMock();
        PaginationResponse<MovieResponse> response = await service.GetPagedMovies(request, filter);

        IEnumerable<ICall> callsGetPagination = fixture.PaginationServiceCalls().Where(x => 
            x.GetMethodInfo(). Name == "GetPagination");

        // Assert
        Assert.NotNull(response);
        Assert.Single(callsGetPagination);
    }
    #endregion
}