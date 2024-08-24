using MovieMania.Core.Exceptions;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;
using MovieMania.Test.Fixtures;
using NSubstitute.Core;

namespace MovieMania.Test.Services;

public class GenreServiceTest
{
    #region Get
    [Fact]
    public async Task GetGenreById_ValidId_ReturnsGenreResponse()
    {
        // Arrange
        int id = 1;
        int callsGet = 1;
        GenreServiceFixture fixture = new();

        // Act
        IGenreService service = fixture.WithGetGenre(id)
                                          .Instance();

        GenreResponse response = await service.GetGenreById(id);

        IEnumerable<ICall> repositoryGetCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
    }

    [Fact]
    public async Task GetGenreById_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Genre Not Found";
        string exceptionMessage = $"Genre with id {id} not exists";
        int callsGet = 1;
        GenreServiceFixture fixture = new();

        // Act
        IGenreService service = fixture.WithGetGenre(id, true).Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            await service.GetGenreById(id));

        IEnumerable<ICall> repositoryGetCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.Equal(exceptionMessage, exception.Message);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
    }
    #endregion

    #region Create
    [Fact]
    public async Task CreateGenre_ValidRequest_ReturnsGenreResponse()
    {
        // Arrange
        string isoCode = string.Empty;
        string name = string.Empty;
        int callsGet = 1;
        int callsCreate = 1;
        GenreServiceFixture fixture = new();

        // Act
        IGenreService service = fixture.WithGetAllGenre(name)
                                          .WithCreateGenre()
                                          .Instance();

        GenreRequest request = fixture.GenreRequestMock();
        GenreResponse response = await service.CreateGenre(request);

        IEnumerable<ICall> repositoryGetCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> repositoryCreateCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Create");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
        Assert.Equal(callsCreate, repositoryCreateCalls.Count());
    }

    [Fact]
    public async Task CreateGenre_InvalidRequest_ThrowsException()
    {
        // Arrange
        string genre = "Male";
        string exceptionTitle = "Error on create genre entity";
        string exceptionMessage = "Genre alredy registred";
        int callsGet = 1;
        int callsCreate = 0;
        GenreServiceFixture fixture = new();

        // Act
        IGenreService service = fixture.WithGetAllGenre(genre)
                                          .Instance();

        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(
            async () =>
            {
                GenreRequest request = fixture.GenreRequestMock();
                request.Name = genre;

                GenreResponse response = await service.CreateGenre(request);
            });

        IEnumerable<ICall> repositoryGetCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> repositoryCreateCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Create");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.Equal(exceptionMessage, exception.Message);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
        Assert.Equal(callsCreate, repositoryCreateCalls.Count());
    }
    #endregion

    #region Update
    [Fact]
    public async Task UpdateGenre_ValidIdAndRequest_ReturnsGenreResponse()
    {
        // Arrange
        int id = 1;
        int callsGet = 2;
        int callsUpdate = 1;
        GenreServiceFixture fixture = new();

        // Act
        IGenreService service = fixture.WithGetGenre(id)
                                          .WithUpdateGenre()
                                          .Instance();

        GenreRequest request = fixture.GenreRequestMock();
        GenreResponse response = await service.UpdateGenre(id, request);

        IEnumerable<ICall> repositoryGetCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> repositoryUpdateCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Update");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
        Assert.Equal(callsUpdate, repositoryUpdateCalls.Count());
    }

    [Fact]
    public async Task UpdateGenre_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Genre Not Found";
        string exceptionMessage = $"Genre with id {id} not exists";
        int callsGet = 1;
        int callsUpdate = 0;
        GenreServiceFixture fixture = new();

        // Act
        IGenreService service = fixture.WithGetGenre(id, true)
                                          .Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            GenreRequest request = fixture.GenreRequestMock();
            GenreResponse response = await service.UpdateGenre(id, request);
        });

        IEnumerable<ICall> repositoryGetCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> repositoryUpdateCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Update");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.Equal(exceptionMessage, exception.Message);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
        Assert.Equal(callsUpdate, repositoryUpdateCalls.Count());
    }

    [Fact]
    public async Task UpdateGenre_InvalidRequest_ThrowsException()
    {
        // Arrange
        int id = 1;
        string genre = "Male";
        string exceptionTitle = "Error on update genre entity";
        string exceptionMessage = $"Genre alredy registred";
        int callsGet = 2;
        int callsUpdate = 0;
        GenreServiceFixture fixture = new();

        // Act
        IGenreService service = fixture.WithGetGenre(id)
                                         .WithGetAllGenre(genre)
                                         .Instance();

        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            GenreRequest request = fixture.GenreRequestMock();
            request.Name = genre;

            GenreResponse response = await service.UpdateGenre(id, request);
        });

        IEnumerable<ICall> repositoryGetCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> repositoryUpdateCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Update");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.Equal(exceptionMessage, exception.Message);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
        Assert.Equal(callsUpdate, repositoryUpdateCalls.Count());
    }
    #endregion

    #region Delete
    [Fact]
    public async Task DeleteGenre_ValidId_ReturnsTrue()
    {
        // Arrange
        int id = 1;
        int callsGet = 1;
        int callsDelete = 1;
        GenreServiceFixture fixture = new();

        // Act
        IGenreService service = fixture.WithGetGenre(id)
                                          .WithDeleteGenre()
                                          .Instance();

        bool result = await service.DeleteGenre(id);

        IEnumerable<ICall> repositoryGetCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> repositoryDeleteCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Delete");

        // Assert
        Assert.True(result);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
        Assert.Equal(callsDelete, repositoryDeleteCalls.Count());
    }

    [Fact]
    public async Task DeleteGenre_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Genre Not Found";
        string exceptionMessage = $"Genre with id {id} not exists";
        int callsGet = 1;
        int callsDelete = 0;
        GenreServiceFixture fixture = new();

        // Act
        IGenreService service = fixture.WithGetGenre(id, true)
                                          .Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async ()
            => await service.DeleteGenre(id));

        IEnumerable<ICall> repositoryGetCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> repositoryDeleteCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Delete");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.Equal(exceptionMessage, exception.Message);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
        Assert.Equal(callsDelete, repositoryDeleteCalls.Count());
    }
    #endregion

    #region Paged
    [Fact]
    public async Task GetPagedGenres_ValidRequest_ReturnsPaginatedGenreResponse()
    {
        // Arrange
        int callsGet = 2;
        int callsGetPagination = 1;
        GenreServiceFixture fixture = new();

        // Act
        IGenreService service = fixture.WithGetAllGenre(string.Empty)
                                          .WithGetPagination()
                                          .Instance();

        PaginationRequest pagination = fixture.PaginationRequestMock();
        PaginationResponse<GenreResponse> response = await service.GetPagedGenres(pagination);

        IEnumerable<ICall> repositoryGetCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> getPaginationCalls = fixture.PaginationServiceCalls()
            .Where(call => call.GetMethodInfo().Name == "GetPagination");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
        Assert.Equal(callsGetPagination, getPaginationCalls.Count());
    }
    #endregion
}