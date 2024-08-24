using MovieMania.Core.Exceptions;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;
using MovieMania.Test.Fixtures;
using NSubstitute.Core;

namespace MovieMania.Test.Services;

public class GenderServiceTest
{
    #region Get
    [Fact]
    public async Task GetGenderById_ValidId_ReturnsGenderResponse()
    {
        // Arrange
        int id = 1;
        int callsGet = 1;
        GenderServiceFixture fixture = new();

        // Act
        IGenderService service = fixture.WithGetGender(id)
                                          .Instance();

        GenderResponse response = await service.GetGenderById(id);

        IEnumerable<ICall> repositoryGetCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
    }

    [Fact]
    public async Task GetGenderById_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Gender Not Found";
        string exceptionMessage = $"Gender with id {id} not exists";
        int callsGet = 1;
        GenderServiceFixture fixture = new();

        // Act
        IGenderService service = fixture.WithGetGender(id, true).Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            await service.GetGenderById(id));

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
    public async Task CreateGender_ValidRequest_ReturnsGenderResponse()
    {
        // Arrange
        string isoCode = string.Empty;
        string name = string.Empty;
        int callsGet = 1;
        int callsCreate = 1;
        GenderServiceFixture fixture = new();

        // Act
        IGenderService service = fixture.WithGetAllGender(name)
                                          .WithCreateGender()
                                          .Instance();

        GenderRequest request = fixture.GenderRequestMock();
        GenderResponse response = await service.CreateGender(request);

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
    public async Task CreateGender_InvalidRequest_ThrowsException()
    {
        // Arrange
        string gender = "Male";
        string exceptionTitle = "Error on create gender entity";
        string exceptionMessage = "Gender alredy registred with gender";
        int callsGet = 1;
        int callsCreate = 0;
        GenderServiceFixture fixture = new();

        // Act
        IGenderService service = fixture.WithGetAllGender(gender)
                                          .Instance();

        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(
            async () =>
            {
                GenderRequest request = fixture.GenderRequestMock();
                request.Gender = gender;

                GenderResponse response = await service.CreateGender(request);
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
    public async Task UpdateGender_ValidIdAndRequest_ReturnsGenderResponse()
    {
        // Arrange
        int id = 1;
        int callsGet = 2;
        int callsUpdate = 1;
        GenderServiceFixture fixture = new();

        // Act
        IGenderService service = fixture.WithGetGender(id)
                                          .WithUpdateGender()
                                          .Instance();

        GenderRequest request = fixture.GenderRequestMock();
        GenderResponse response = await service.UpdateGender(id, request);

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
    public async Task UpdateGender_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Gender Not Found";
        string exceptionMessage = $"Gender with id {id} not exists";
        int callsGet = 1;
        int callsUpdate = 0;
        GenderServiceFixture fixture = new();

        // Act
        IGenderService service = fixture.WithGetGender(id, true)
                                          .Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            GenderRequest request = fixture.GenderRequestMock();
            GenderResponse response = await service.UpdateGender(id, request);
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
    public async Task UpdateGender_InvalidRequest_ThrowsException()
    {
        // Arrange
        int id = 1;
        string gender = "Male";
        string exceptionTitle = "Error on update gender entity";
        string exceptionMessage = $"Gender alredy registred with gender";
        int callsGet = 2;
        int callsUpdate = 0;
        GenderServiceFixture fixture = new();

        // Act
        IGenderService service = fixture.WithGetGender(id)
                                         .WithGetAllGender(gender)
                                         .Instance();

        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            GenderRequest request = fixture.GenderRequestMock();
            request.Gender = gender;

            GenderResponse response = await service.UpdateGender(id, request);
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
    public async Task DeleteGender_ValidId_ReturnsTrue()
    {
        // Arrange
        int id = 1;
        int callsGet = 1;
        int callsDelete = 1;
        GenderServiceFixture fixture = new();

        // Act
        IGenderService service = fixture.WithGetGender(id)
                                          .WithDeleteGender()
                                          .Instance();

        bool result = await service.DeleteGender(id);

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
    public async Task DeleteGender_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Gender Not Found";
        string exceptionMessage = $"Gender with id {id} not exists";
        int callsGet = 1;
        int callsDelete = 0;
        GenderServiceFixture fixture = new();

        // Act
        IGenderService service = fixture.WithGetGender(id, true)
                                          .Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async ()
            => await service.DeleteGender(id));

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
    public async Task GetPagedGenders_ValidRequest_ReturnsPaginatedGenderResponse()
    {
        // Arrange
        int callsGet = 2;
        int callsGetPagination = 1;
        GenderServiceFixture fixture = new();

        // Act
        IGenderService service = fixture.WithGetAllGender(string.Empty)
                                          .WithGetPagination()
                                          .Instance();

        PaginationRequest pagination = fixture.PaginationRequestMock();
        PaginationResponse<GenderResponse> response = await service.GetPagedGenders(pagination);

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