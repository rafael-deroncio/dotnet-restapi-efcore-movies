using MovieMania.Core.Exceptions;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;
using MovieMania.Test.Fixtures;
using NSubstitute.Core;

namespace MovieMania.Test.Services;

public class LanguageServiceTest
{
    #region Get
    [Fact]
    public async Task GetLanguageById_ValidId_ReturnsLanguageResponse()
    {
        // Arrange
        int id = 1;
        int callsGet = 1;
        LanguageServiceFixture fixture = new();

        // Act
        ILanguageService service = fixture.WithGetLanguage(id)
                                          .Instance();

        LanguageResponse response = await service.GetLanguageById(id);

        IEnumerable<ICall> repositoryGetCalls = fixture.LanguageRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
    }

    [Fact]
    public async Task GetLanguageById_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Language Not Found";
        string exceptionMessage = $"Language with id {id} not exists.";
        int callsGet = 1;
        LanguageServiceFixture fixture = new();

        // Act
        ILanguageService service = fixture.WithGetLanguage(id, true).Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            await service.GetLanguageById(id));

        IEnumerable<ICall> repositoryGetCalls = fixture.LanguageRepositoryCalls()
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
    public async Task CreateLanguage_ValidRequest_ReturnsLanguageResponse()
    {
        // Arrange
        string isoCode = string.Empty;
        string name = string.Empty;
        int callsGet = 1;
        int callsCreate = 1;
        LanguageServiceFixture fixture = new();

        // Act
        ILanguageService service = fixture.WithGetAllLanguage(name)
                                          .WithCreateLanguage()
                                          .Instance();

        LanguageRequest request = fixture.LanguageRequestMock();
        LanguageResponse response = await service.CreateLanguage(request);

        IEnumerable<ICall> repositoryGetCalls = fixture.LanguageRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> repositoryCreateCalls = fixture.LanguageRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Create");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
        Assert.Equal(callsCreate, repositoryCreateCalls.Count());
    }

    [Fact]
    public async Task CreateLanguage_InvalidRequest_ThrowsException()
    {
        // Arrange
        string language = "Male";
        string exceptionTitle = "Error on create language entity";
        string exceptionMessage = "Language alredy registred";
        int callsGet = 1;
        int callsCreate = 0;
        LanguageServiceFixture fixture = new();

        // Act
        ILanguageService service = fixture.WithGetAllLanguage(language)
                                          .Instance();

        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(
            async () =>
            {
                LanguageRequest request = fixture.LanguageRequestMock();
                request.Language = language;

                LanguageResponse response = await service.CreateLanguage(request);
            });

        IEnumerable<ICall> repositoryGetCalls = fixture.LanguageRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> repositoryCreateCalls = fixture.LanguageRepositoryCalls()
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
    public async Task UpdateLanguage_ValidIdAndRequest_ReturnsLanguageResponse()
    {
        // Arrange
        int id = 1;
        int callsGet = 2;
        int callsUpdate = 1;
        LanguageServiceFixture fixture = new();

        // Act
        ILanguageService service = fixture.WithGetLanguage(id)
                                          .WithUpdateLanguage()
                                          .Instance();

        LanguageRequest request = fixture.LanguageRequestMock();
        LanguageResponse response = await service.UpdateLanguage(id, request);

        IEnumerable<ICall> repositoryGetCalls = fixture.LanguageRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> repositoryUpdateCalls = fixture.LanguageRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Update");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
        Assert.Equal(callsUpdate, repositoryUpdateCalls.Count());
    }

    [Fact]
    public async Task UpdateLanguage_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Language Not Found";
        string exceptionMessage = $"Language with id {id} not exists.";
        int callsGet = 1;
        int callsUpdate = 0;
        LanguageServiceFixture fixture = new();

        // Act
        ILanguageService service = fixture.WithGetLanguage(id, true)
                                          .Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            LanguageRequest request = fixture.LanguageRequestMock();
            LanguageResponse response = await service.UpdateLanguage(id, request);
        });

        IEnumerable<ICall> repositoryGetCalls = fixture.LanguageRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> repositoryUpdateCalls = fixture.LanguageRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Update");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.Equal(exceptionMessage, exception.Message);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
        Assert.Equal(callsUpdate, repositoryUpdateCalls.Count());
    }

    [Fact]
    public async Task UpdateLanguage_InvalidRequest_ThrowsException()
    {
        // Arrange
        int id = 1;
        string language = "Male";
        string exceptionTitle = "Error on update language entity";
        string exceptionMessage = $"Language alredy registred";
        int callsGet = 2;
        int callsUpdate = 0;
        LanguageServiceFixture fixture = new();

        // Act
        ILanguageService service = fixture.WithGetLanguage(id)
                                         .WithGetAllLanguage(language)
                                         .Instance();

        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            LanguageRequest request = fixture.LanguageRequestMock();
            request.Language = language;

            LanguageResponse response = await service.UpdateLanguage(id, request);
        });

        IEnumerable<ICall> repositoryGetCalls = fixture.LanguageRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> repositoryUpdateCalls = fixture.LanguageRepositoryCalls()
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
    public async Task DeleteLanguage_ValidId_ReturnsTrue()
    {
        // Arrange
        int id = 1;
        int callsGet = 1;
        int callsDelete = 1;
        LanguageServiceFixture fixture = new();

        // Act
        ILanguageService service = fixture.WithGetLanguage(id)
                                          .WithDeleteLanguage()
                                          .Instance();

        bool result = await service.DeleteLanguage(id);

        IEnumerable<ICall> repositoryGetCalls = fixture.LanguageRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> repositoryDeleteCalls = fixture.LanguageRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Delete");

        // Assert
        Assert.True(result);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
        Assert.Equal(callsDelete, repositoryDeleteCalls.Count());
    }

    [Fact]
    public async Task DeleteLanguage_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Language Not Found";
        string exceptionMessage = $"Language with id {id} not exists.";
        int callsGet = 1;
        int callsDelete = 0;
        LanguageServiceFixture fixture = new();

        // Act
        ILanguageService service = fixture.WithGetLanguage(id, true)
                                          .Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async ()
            => await service.DeleteLanguage(id));

        IEnumerable<ICall> repositoryGetCalls = fixture.LanguageRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> repositoryDeleteCalls = fixture.LanguageRepositoryCalls()
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
    public async Task GetPagedLanguages_ValidRequest_ReturnsPaginatedLanguageResponse()
    {
        // Arrange
        int callsGet = 2;
        int callsGetPagination = 1;
        LanguageServiceFixture fixture = new();

        // Act
        ILanguageService service = fixture.WithGetAllLanguage(string.Empty)
                                          .WithGetPagination<LanguageResponse>()
                                          .Instance();

        PaginationRequest pagination = fixture.PaginationRequestMock();
        PaginationResponse<LanguageResponse> response = await service.GetPagedLanguages(pagination);

        IEnumerable<ICall> repositoryGetCalls = fixture.LanguageRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> getPaginationCalls = fixture.PaginationServiceCalls()
            .Where(call => call.GetMethodInfo().Name == "GetPagination");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
        Assert.Equal(callsGetPagination, getPaginationCalls.Count());
    }
    #endregion

    #region Get
    [Fact]
    public async Task GetLanguageRoleById_ValidId_ReturnsLanguageRoleResponse()
    {
        // Arrange
        int id = 1;
        int callsGet = 1;
        LanguageServiceFixture fixture = new();

        // Act
        ILanguageService service = fixture.WithGetLanguageRole(id)
                                          .Instance();

        LanguageRoleResponse response = await service.GetLanguageRoleById(id);

        IEnumerable<ICall> repositoryGetCalls = fixture.LanguageRoleRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
    }

    [Fact]
    public async Task GetLanguageRoleById_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Language Role Not Found";
        string exceptionMessage = $"Language Role with id {id} not exists.";
        int callsGet = 1;
        LanguageServiceFixture fixture = new();

        // Act
        ILanguageService service = fixture.WithGetLanguageRole(id, true).Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            await service.GetLanguageRoleById(id));

        IEnumerable<ICall> repositoryGetCalls = fixture.LanguageRoleRepositoryCalls()
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
    public async Task CreateLanguageRole_ValidRequest_ReturnsLanguageRoleResponse()
    {
        // Arrange
        string isoCode = string.Empty;
        string name = string.Empty;
        int callsGet = 1;
        int callsCreate = 1;
        LanguageServiceFixture fixture = new();

        // Act
        ILanguageService service = fixture.WithGetAllLanguageRole(name)
                                          .WithCreateLanguageRole()
                                          .Instance();

        LanguageRoleRequest request = fixture.LanguageRoleRequestMock();
        LanguageRoleResponse response = await service.CreateLanguageRole(request);

        IEnumerable<ICall> repositoryGetCalls = fixture.LanguageRoleRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> repositoryCreateCalls = fixture.LanguageRoleRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Create");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
        Assert.Equal(callsCreate, repositoryCreateCalls.Count());
    }

    [Fact]
    public async Task CreateLanguageRole_InvalidRequest_ThrowsException()
    {
        // Arrange
        string role = "Male";
        string exceptionTitle = "Error on create language role entity";
        string exceptionMessage = "Language Role alredy registred";
        int callsGet = 1;
        int callsCreate = 0;
        LanguageServiceFixture fixture = new();

        // Act
        ILanguageService service = fixture.WithGetAllLanguageRole(role)
                                          .Instance();

        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(
            async () =>
            {
                LanguageRoleRequest request = fixture.LanguageRoleRequestMock();
                request.Role = role;

                LanguageRoleResponse response = await service.CreateLanguageRole(request);
            });

        IEnumerable<ICall> repositoryGetCalls = fixture.LanguageRoleRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> repositoryCreateCalls = fixture.LanguageRoleRepositoryCalls()
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
    public async Task UpdateLanguageRole_ValidIdAndRequest_ReturnsLanguageRoleResponse()
    {
        // Arrange
        int id = 1;
        int callsGet = 2;
        int callsUpdate = 1;
        LanguageServiceFixture fixture = new();

        // Act
        ILanguageService service = fixture.WithGetLanguageRole(id)
                                          .WithUpdateLanguageRole()
                                          .Instance();

        LanguageRoleRequest request = fixture.LanguageRoleRequestMock();
        LanguageRoleResponse response = await service.UpdateLanguageRole(id, request);

        IEnumerable<ICall> repositoryGetCalls = fixture.LanguageRoleRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> repositoryUpdateCalls = fixture.LanguageRoleRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Update");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
        Assert.Equal(callsUpdate, repositoryUpdateCalls.Count());
    }

    [Fact]
    public async Task UpdateLanguageRole_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Language Role Not Found";
        string exceptionMessage = $"Language Role with id {id} not exists.";
        int callsGet = 1;
        int callsUpdate = 0;
        LanguageServiceFixture fixture = new();

        // Act
        ILanguageService service = fixture.WithGetLanguageRole(id, true)
                                          .Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            LanguageRoleRequest request = fixture.LanguageRoleRequestMock();
            LanguageRoleResponse response = await service.UpdateLanguageRole(id, request);
        });

        IEnumerable<ICall> repositoryGetCalls = fixture.LanguageRoleRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> repositoryUpdateCalls = fixture.LanguageRoleRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Update");

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(exceptionTitle, exception.Title);
        Assert.Equal(exceptionMessage, exception.Message);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
        Assert.Equal(callsUpdate, repositoryUpdateCalls.Count());
    }

    [Fact]
    public async Task UpdateLanguageRole_InvalidRequest_ThrowsException()
    {
        // Arrange
        int id = 1;
        string role = "Male";
        string exceptionTitle = "Error on update language role entity";
        string exceptionMessage = $"Language Role alredy registred";
        int callsGet = 2;
        int callsUpdate = 0;
        LanguageServiceFixture fixture = new();

        // Act
        ILanguageService service = fixture.WithGetLanguageRole(id)
                                         .WithGetAllLanguageRole(role)
                                         .Instance();

        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            LanguageRoleRequest request = fixture.LanguageRoleRequestMock();
            request.Role = role;

            LanguageRoleResponse response = await service.UpdateLanguageRole(id, request);
        });

        IEnumerable<ICall> repositoryGetCalls = fixture.LanguageRoleRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> repositoryUpdateCalls = fixture.LanguageRoleRepositoryCalls()
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
    public async Task DeleteLanguageRole_ValidId_ReturnsTrue()
    {
        // Arrange
        int id = 1;
        int callsGet = 1;
        int callsDelete = 1;
        LanguageServiceFixture fixture = new();

        // Act
        ILanguageService service = fixture.WithGetLanguageRole(id)
                                          .WithDeleteLanguageRole()
                                          .Instance();

        bool result = await service.DeleteLanguageRole(id);

        IEnumerable<ICall> repositoryGetCalls = fixture.LanguageRoleRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> repositoryDeleteCalls = fixture.LanguageRoleRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Delete");

        // Assert
        Assert.True(result);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
        Assert.Equal(callsDelete, repositoryDeleteCalls.Count());
    }

    [Fact]
    public async Task DeleteLanguageRole_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Language Role Not Found";
        string exceptionMessage = $"Language Role with id {id} not exists.";
        int callsGet = 1;
        int callsDelete = 0;
        LanguageServiceFixture fixture = new();

        // Act
        ILanguageService service = fixture.WithGetLanguageRole(id, true)
                                          .Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async ()
            => await service.DeleteLanguageRole(id));

        IEnumerable<ICall> repositoryGetCalls = fixture.LanguageRoleRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        IEnumerable<ICall> repositoryDeleteCalls = fixture.LanguageRoleRepositoryCalls()
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
    public async Task GetPagedLanguageRoles_ValidRequest_ReturnsPaginatedLanguageRoleResponse()
    {
        // Arrange
        int callsGet = 2;
        int callsGetPagination = 1;
        LanguageServiceFixture fixture = new();

        // Act
        ILanguageService service = fixture.WithGetAllLanguageRole(string.Empty)
                                          .WithGetPagination<LanguageRoleResponse>()
                                          .Instance();

        PaginationRequest pagination = fixture.PaginationRequestMock();
        PaginationResponse<LanguageRoleResponse> response = await service.GetPagedLanguageRoles(pagination);

        IEnumerable<ICall> repositoryGetCalls = fixture.LanguageRoleRepositoryCalls()
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