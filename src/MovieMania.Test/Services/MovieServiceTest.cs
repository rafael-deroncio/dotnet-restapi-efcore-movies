using MovieMania.Core.Exceptions;
using MovieMania.Core.Services;
using MovieMania.Core.Services.Interfaces;
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
        string exceptionMesage = $"Movie with id {movieId} not exists.";
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

    // [Fact]
    public async Task CreateMovie_InvalidRequest_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
    }

    public async Task CreateMovie_InvalidProductionCountries_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
    }

    public async Task CreateMovie_InvalidLanguages_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
    }

    public async Task CreateMovie_InvalidGenres_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
    }

    public async Task CreateMovie_InvalidKeywords_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
    }

    public async Task CreateMovie_InvalidCompanies_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
    }

    public async Task CreateMovie_InvalidCasts_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
    }

    public async Task CreateMovie_InvalidCrews_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
    }
    #endregion

    #region Update
    // [Fact]
    public async Task UpdateMovie_ValidIdAndRequest_ReturnsMovieResponse()
    {
        // Arrange

        // Act

        // Assert
    }

    // [Fact]
    public async Task UpdateMovie_InvalidId_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
    }

    // [Fact]
    public async Task UpdateMovie_InvalidRequest_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
    }

    public async Task UpdateMovie_InvalidProductionCountries_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
    }

    public async Task UpdateMovie_InvalidLanguages_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
    }

    public async Task UpdateMovie_InvalidGenres_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
    }

    public async Task UpdateMovie_InvalidKeywords_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
    }

    public async Task UpdateMovie_InvalidCompanies_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
    }

    public async Task UpdateMovie_InvalidCasts_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
    }

    public async Task UpdateMovie_InvalidCrews_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
    }
    #endregion

    #region Delete
    // [Fact]
    public async Task DeleteMovie_ValidId_ReturnsTrue()
    {
        // Arrange

        // Act

        // Assert
    }

    // [Fact]
    public async Task DeleteMovie_InvalidId_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
    }
    #endregion

    #region Paged
    // [Fact]
    public async Task GetPagedMovies_ValidRequest_ReturnsPaginatedMovieResponse()
    {
        // Arrange

        // Act

        // Assert
    }
    #endregion
}