using MovieMania.Core.Exceptions;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;
using MovieMania.Test.Fixtures;
using NSubstitute.Core;

namespace MovieMania.Test.Services;

public class PersonServiceTest
{
    #region Get
    [Fact]
    public async Task GetPersonById_ValidId_ReturnsPersonResponse()
    {
        // Arrange
        int id = 1;
        int callsGet = 1;
        PersonServiceFixture fixture = new();

        // Act
        IPersonService service = fixture.WithGetPerson(id)
                                          .Instance();

        PersonResponse response = await service.GetPersonById(id);

        IEnumerable<ICall> repositoryGetCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
    }

    [Fact]
    public async Task GetPersonById_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Person Not Found";
        string exceptionMessage = $"Person with id {id} not exists";
        int callsGet = 1;
        PersonServiceFixture fixture = new();

        // Act
        IPersonService service = fixture.WithGetPerson(id, true).Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            await service.GetPersonById(id));

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
    public async Task CreatePerson_ValidRequest_ReturnsPersonResponse()
    {
        // Arrange
        string isoCode = string.Empty;
        string name = string.Empty;
        int callsGet = 1;
        int callsCreate = 1;
        PersonServiceFixture fixture = new();

        // Act
        IPersonService service = fixture.WithGetAllPerson(name)
                                          .WithCreatePerson()
                                          .Instance();

        PersonRequest request = fixture.PersonRequestMock();
        PersonResponse response = await service.CreatePerson(request);

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
    public async Task CreatePerson_InvalidRequest_ThrowsException()
    {
        // Arrange
        string person = "Male";
        string exceptionTitle = "Error on create person entity";
        string exceptionMessage = "Person alredy registred";
        int callsGet = 1;
        int callsCreate = 0;
        PersonServiceFixture fixture = new();

        // Act
        IPersonService service = fixture.WithGetAllPerson(person)
                                          .Instance();

        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(
            async () =>
            {
                PersonRequest request = fixture.PersonRequestMock();
                request.Name = person;

                PersonResponse response = await service.CreatePerson(request);
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
    public async Task UpdatePerson_ValidIdAndRequest_ReturnsPersonResponse()
    {
        // Arrange
        int id = 1;
        int callsGet = 2;
        int callsUpdate = 1;
        PersonServiceFixture fixture = new();

        // Act
        IPersonService service = fixture.WithGetPerson(id)
                                          .WithUpdatePerson()
                                          .Instance();

        PersonRequest request = fixture.PersonRequestMock();
        PersonResponse response = await service.UpdatePerson(id, request);

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
    public async Task UpdatePerson_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Person Not Found";
        string exceptionMessage = $"Person with id {id} not exists";
        int callsGet = 1;
        int callsUpdate = 0;
        PersonServiceFixture fixture = new();

        // Act
        IPersonService service = fixture.WithGetPerson(id, true)
                                          .Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            PersonRequest request = fixture.PersonRequestMock();
            PersonResponse response = await service.UpdatePerson(id, request);
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
    public async Task UpdatePerson_InvalidRequest_ThrowsException()
    {
        // Arrange
        int id = 1;
        string person = "Male";
        string exceptionTitle = "Error on update person entity";
        string exceptionMessage = $"Person alredy registred";
        int callsGet = 2;
        int callsUpdate = 0;
        PersonServiceFixture fixture = new();

        // Act
        IPersonService service = fixture.WithGetPerson(id)
                                         .WithGetAllPerson(person)
                                         .Instance();

        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            PersonRequest request = fixture.PersonRequestMock();
            request.Name = person;

            PersonResponse response = await service.UpdatePerson(id, request);
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
    public async Task DeletePerson_ValidId_ReturnsTrue()
    {
        // Arrange
        int id = 1;
        int callsGet = 1;
        int callsDelete = 1;
        PersonServiceFixture fixture = new();

        // Act
        IPersonService service = fixture.WithGetPerson(id)
                                          .WithDeletePerson()
                                          .Instance();

        bool result = await service.DeletePerson(id);

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
    public async Task DeletePerson_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Person Not Found";
        string exceptionMessage = $"Person with id {id} not exists";
        int callsGet = 1;
        int callsDelete = 0;
        PersonServiceFixture fixture = new();

        // Act
        IPersonService service = fixture.WithGetPerson(id, true)
                                          .Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async ()
            => await service.DeletePerson(id));

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
    public async Task GetPagedPersons_ValidRequest_ReturnsPaginatedPersonResponse()
    {
        // Arrange
        int callsGet = 2;
        int callsGetPagination = 1;
        PersonServiceFixture fixture = new();

        // Act
        IPersonService service = fixture.WithGetAllPerson(string.Empty)
                                          .WithGetPagination()
                                          .Instance();

        PaginationRequest pagination = fixture.PaginationRequestMock();
        PaginationResponse<PersonResponse> response = await service.GetPagedPersons(pagination);

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