using MovieMania.Core.Exceptions;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;
using MovieMania.Test.Fixtures;
using NSubstitute.Core;

namespace MovieMania.Test.Services;

public class DepartmentServiceTest
{
    #region Get
    [Fact]
    public async Task GetDepartmentById_ValidId_ReturnsDepartmentResponse()
    {
        // Arrange
        int id = 1;
        int callsGet = 1;
        DepartmentServiceFixture fixture = new();

        // Act
        IDepartmentService service = fixture.WithGetDepartment(id)
                                          .Instance();

        DepartmentResponse response = await service.GetDepartmentById(id);

        IEnumerable<ICall> repositoryGetCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
    }

    [Fact]
    public async Task GetDepartmentById_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Department Not Found";
        string exceptionMessage = $"Department with id {id} not exists.";
        int callsGet = 1;
        DepartmentServiceFixture fixture = new();

        // Act
        IDepartmentService service = fixture.WithGetDepartment(id, true).Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            await service.GetDepartmentById(id));

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
    public async Task CreateDepartment_ValidRequest_ReturnsDepartmentResponse()
    {
        // Arrange
        string isoCode = string.Empty;
        string name = string.Empty;
        int callsGet = 1;
        int callsCreate = 1;
        DepartmentServiceFixture fixture = new();

        // Act
        IDepartmentService service = fixture.WithGetAllDepartment(name)
                                          .WithCreateDepartment()
                                          .Instance();

        DepartmentRequest request = fixture.DepartmentRequestMock();
        DepartmentResponse response = await service.CreateDepartment(request);

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
    public async Task CreateDepartment_InvalidRequest_ThrowsException()
    {
        // Arrange
        string name = "Production";
        string exceptionTitle = "Error on create department entity";
        string exceptionMessage = "Department alredy registred";
        int callsGet = 1;
        int callsCreate = 0;
        DepartmentServiceFixture fixture = new();

        // Act
        IDepartmentService service = fixture.WithGetAllDepartment(name)
                                          .Instance();

        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(
            async () =>
            {
                DepartmentRequest request = fixture.DepartmentRequestMock();
                request.Name = name;

                DepartmentResponse response = await service.CreateDepartment(request);
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
    public async Task UpdateDepartment_ValidIdAndRequest_ReturnsDepartmentResponse()
    {
        // Arrange
        int id = 1;
        int callsGet = 2;
        int callsUpdate = 1;
        DepartmentServiceFixture fixture = new();

        // Act
        IDepartmentService service = fixture.WithGetDepartment(id)
                                          .WithUpdateDepartment()
                                          .Instance();

        DepartmentRequest request = fixture.DepartmentRequestMock();
        DepartmentResponse response = await service.UpdateDepartment(id, request);

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
    public async Task UpdateDepartment_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Department Not Found";
        string exceptionMessage = $"Department with id {id} not exists.";
        int callsGet = 1;
        int callsUpdate = 0;
        DepartmentServiceFixture fixture = new();

        // Act
        IDepartmentService service = fixture.WithGetDepartment(id, true)
                                          .Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            DepartmentRequest request = fixture.DepartmentRequestMock();
            DepartmentResponse response = await service.UpdateDepartment(id, request);
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
    public async Task UpdateDepartment_InvalidRequest_ThrowsException()
    {
        // Arrange
        int id = 1;
        string name = "Production";
        string exceptionTitle = "Error on update department entity";
        string exceptionMessage = $"Department alredy registred";
        int callsGet = 2;
        int callsUpdate = 0;
        DepartmentServiceFixture fixture = new();

        // Act
        IDepartmentService service = fixture.WithGetDepartment(id)
                                         .WithGetAllDepartment(name)
                                         .Instance();

        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            DepartmentRequest request = fixture.DepartmentRequestMock();
            request.Name = name;

            DepartmentResponse response = await service.UpdateDepartment(id, request);
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
    public async Task DeleteDepartment_ValidId_ReturnsTrue()
    {
        // Arrange
        int id = 1;
        int callsGet = 1;
        int callsDelete = 1;
        DepartmentServiceFixture fixture = new();

        // Act
        IDepartmentService service = fixture.WithGetDepartment(id)
                                          .WithDeleteDepartment()
                                          .Instance();

        bool result = await service.DeleteDepartment(id);

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
    public async Task DeleteDepartment_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Department Not Found";
        string exceptionMessage = $"Department with id {id} not exists.";
        int callsGet = 1;
        int callsDelete = 0;
        DepartmentServiceFixture fixture = new();

        // Act
        IDepartmentService service = fixture.WithGetDepartment(id, true)
                                          .Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async ()
            => await service.DeleteDepartment(id));

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
    public async Task GetPagedDepartments_ValidRequest_ReturnsPaginatedDepartmentResponse()
    {
        // Arrange
        int callsGet = 2;
        int callsGetPagination = 1;
        DepartmentServiceFixture fixture = new();

        // Act
        IDepartmentService service = fixture.WithGetAllDepartment(string.Empty)
                                          .WithGetPagination()
                                          .Instance();

        PaginationRequest pagination = fixture.PaginationRequestMock();
        PaginationResponse<DepartmentResponse> response = await service.GetPagedDepartments(pagination);

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