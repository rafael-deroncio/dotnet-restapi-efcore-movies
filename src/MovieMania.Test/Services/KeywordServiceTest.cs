using MovieMania.Core.Exceptions;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;
using MovieMania.Test.Fixtures;
using NSubstitute.Core;

namespace MovieMania.Test.Services;

public class KeywordServiceTest
{
    #region Get
    [Fact]
    public async Task GetKeywordById_ValidId_ReturnsKeywordResponse()
    {
        // Arrange
        int id = 1;
        int callsGet = 1;
        KeywordServiceFixture fixture = new();

        // Act
        IKeywordService service = fixture.WithGetKeyword(id)
                                          .Instance();

        KeywordResponse response = await service.GetKeywordById(id);

        IEnumerable<ICall> repositoryGetCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
    }

    [Fact]
    public async Task GetKeywordById_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Keyword Not Found";
        string exceptionMessage = $"Keyword with id {id} not exists.";
        int callsGet = 1;
        KeywordServiceFixture fixture = new();

        // Act
        IKeywordService service = fixture.WithGetKeyword(id, true).Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            await service.GetKeywordById(id));

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
    public async Task CreateKeyword_ValidRequest_ReturnsKeywordResponse()
    {
        // Arrange
        string isoCode = string.Empty;
        string name = string.Empty;
        int callsGet = 1;
        int callsCreate = 1;
        KeywordServiceFixture fixture = new();

        // Act
        IKeywordService service = fixture.WithGetAllKeyword(name)
                                          .WithCreateKeyword()
                                          .Instance();

        KeywordRequest request = fixture.KeywordRequestMock();
        KeywordResponse response = await service.CreateKeyword(request);

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
    public async Task CreateKeyword_InvalidRequest_ThrowsException()
    {
        // Arrange
        string keyword = "Male";
        string exceptionTitle = "Error on create keyword entity";
        string exceptionMessage = "Keyword alredy registred";
        int callsGet = 1;
        int callsCreate = 0;
        KeywordServiceFixture fixture = new();

        // Act
        IKeywordService service = fixture.WithGetAllKeyword(keyword)
                                          .Instance();

        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(
            async () =>
            {
                KeywordRequest request = fixture.KeywordRequestMock();
                request.Keyword = keyword;

                KeywordResponse response = await service.CreateKeyword(request);
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
    public async Task UpdateKeyword_ValidIdAndRequest_ReturnsKeywordResponse()
    {
        // Arrange
        int id = 1;
        int callsGet = 2;
        int callsUpdate = 1;
        KeywordServiceFixture fixture = new();

        // Act
        IKeywordService service = fixture.WithGetKeyword(id)
                                          .WithUpdateKeyword()
                                          .Instance();

        KeywordRequest request = fixture.KeywordRequestMock();
        KeywordResponse response = await service.UpdateKeyword(id, request);

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
    public async Task UpdateKeyword_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Keyword Not Found";
        string exceptionMessage = $"Keyword with id {id} not exists.";
        int callsGet = 1;
        int callsUpdate = 0;
        KeywordServiceFixture fixture = new();

        // Act
        IKeywordService service = fixture.WithGetKeyword(id, true)
                                          .Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            KeywordRequest request = fixture.KeywordRequestMock();
            KeywordResponse response = await service.UpdateKeyword(id, request);
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
    public async Task UpdateKeyword_InvalidRequest_ThrowsException()
    {
        // Arrange
        int id = 1;
        string keyword = "Male";
        string exceptionTitle = "Error on update keyword entity";
        string exceptionMessage = $"Keyword alredy registred";
        int callsGet = 2;
        int callsUpdate = 0;
        KeywordServiceFixture fixture = new();

        // Act
        IKeywordService service = fixture.WithGetKeyword(id)
                                         .WithGetAllKeyword(keyword)
                                         .Instance();

        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            KeywordRequest request = fixture.KeywordRequestMock();
            request.Keyword = keyword;

            KeywordResponse response = await service.UpdateKeyword(id, request);
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
    public async Task DeleteKeyword_ValidId_ReturnsTrue()
    {
        // Arrange
        int id = 1;
        int callsGet = 1;
        int callsDelete = 1;
        KeywordServiceFixture fixture = new();

        // Act
        IKeywordService service = fixture.WithGetKeyword(id)
                                          .WithDeleteKeyword()
                                          .Instance();

        bool result = await service.DeleteKeyword(id);

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
    public async Task DeleteKeyword_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Keyword Not Found";
        string exceptionMessage = $"Keyword with id {id} not exists.";
        int callsGet = 1;
        int callsDelete = 0;
        KeywordServiceFixture fixture = new();

        // Act
        IKeywordService service = fixture.WithGetKeyword(id, true)
                                          .Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async ()
            => await service.DeleteKeyword(id));

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
    public async Task GetPagedKeywords_ValidRequest_ReturnsPaginatedKeywordResponse()
    {
        // Arrange
        int callsGet = 2;
        int callsGetPagination = 1;
        KeywordServiceFixture fixture = new();

        // Act
        IKeywordService service = fixture.WithGetAllKeyword(string.Empty)
                                          .WithGetPagination()
                                          .Instance();

        PaginationRequest pagination = fixture.PaginationRequestMock();
        PaginationResponse<KeywordResponse> response = await service.GetPagedKeywords(pagination);

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