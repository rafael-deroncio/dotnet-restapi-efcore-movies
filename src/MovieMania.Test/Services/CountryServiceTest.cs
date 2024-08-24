using MovieMania.Core.Exceptions;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;
using MovieMania.Test.Fixtures;
using NSubstitute.Core;

namespace MovieMania.Test.Services;

public class CountryServiceTest
{
    #region Get
    [Fact]
    public async Task GetCountryById_ValidId_ReturnsCountryResponse()
    {
        // Arrange
        int id = 1;
        int callsGet = 1;
        CountryServiceFixture fixture = new();

        // Act
        ICountryService service = fixture.WithGetCountry(id)
                                          .Instance();

        CountryResponse response = await service.GetCountryById(id);

        IEnumerable<ICall> repositoryGetCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
    }

    [Fact]
    public async Task GetCountryById_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Country Not Found";
        string exceptionMessage = $"Country with id {id} not exists";
        int callsGet = 1;
        CountryServiceFixture fixture = new();

        // Act
        ICountryService service = fixture.WithGetCountry(id, true).Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            await service.GetCountryById(id));

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
    public async Task CreateCountry_ValidRequest_ReturnsCountryResponse()
    {
        // Arrange
        string isoCode = string.Empty;
        string name = string.Empty;
        int callsGet = 1;
        int callsCreate = 1;
        CountryServiceFixture fixture = new();

        // Act
        ICountryService service = fixture.WithGetAllCountry(isoCode, name)
                                          .WithCreateCountry()
                                          .Instance();

        CountryRequest request = fixture.CountryRequestMock();
        CountryResponse response = await service.CreateCountry(request);

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
    public async Task CreateCountry_InvalidRequest_ThrowsException()
    {
        // Arrange
        string isoCode = "BR";
        string name = "Brazil";
        string exceptionTitle = "Error on create country entity";
        string exceptionMessage = "Country alredy registred with name or iso code";
        int callsGet = 1;
        int callsCreate = 0;
        CountryServiceFixture fixture = new();

        // Act
        ICountryService service = fixture.WithGetAllCountry(isoCode, name)
                                          .Instance();

        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(
            async () =>
            {
                CountryRequest request = fixture.CountryRequestMock();
                request.IsoCode = isoCode;
                request.Name = name;

                CountryResponse response = await service.CreateCountry(request);
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
    public async Task UpdateCountry_ValidIdAndRequest_ReturnsCountryResponse()
    {
        // Arrange
        int id = 1;
        int callsGet = 2;
        int callsUpdate = 1;
        CountryServiceFixture fixture = new();

        // Act
        ICountryService service = fixture.WithGetCountry(id)
                                          .WithUpdateCountry()
                                          .Instance();

        CountryRequest request = fixture.CountryRequestMock();
        CountryResponse response = await service.UpdateCountry(id, request);

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
    public async Task UpdateCountry_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Country Not Found";
        string exceptionMessage = $"Country with id {id} not exists";
        int callsGet = 1;
        int callsUpdate = 0;
        CountryServiceFixture fixture = new();

        // Act
        ICountryService service = fixture.WithGetCountry(id, true)
                                          .Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            CountryRequest request = fixture.CountryRequestMock();
            CountryResponse response = await service.UpdateCountry(id, request);
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
    public async Task UpdateCountry_InvalidRequest_ThrowsException()
    {
        // Arrange
        int id = 1;
        string isoCode = "BR";
        string name = "Brazil";
        string exceptionTitle = "Error on update country entity";
        string exceptionMessage = $"Country alredy registred with name or iso code";
        int callsGet = 2;
        int callsUpdate = 0;
        CountryServiceFixture fixture = new();

        // Act
        ICountryService service = fixture.WithGetCountry(id)
                                         .WithGetAllCountry(isoCode, name)
                                         .Instance();

        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            CountryRequest request = fixture.CountryRequestMock();
            request.IsoCode = isoCode;
            request.Name = name;

            CountryResponse response = await service.UpdateCountry(id, request);
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
    public async Task DeleteCountry_ValidId_ReturnsTrue()
    {
        // Arrange
        int id = 1;
        int callsGet = 1;
        int callsDelete = 1;
        CountryServiceFixture fixture = new();

        // Act
        ICountryService service = fixture.WithGetCountry(id)
                                          .WithDeleteCountry()
                                          .Instance();

        bool result = await service.DeleteCountry(id);

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
    public async Task DeleteCountry_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Country Not Found";
        string exceptionMessage = $"Country with id {id} not exists";
        int callsGet = 1;
        int callsDelete = 0;
        CountryServiceFixture fixture = new();

        // Act
        ICountryService service = fixture.WithGetCountry(id, true)
                                          .Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async ()
            => await service.DeleteCountry(id));

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
    public async Task GetPagedCountries_ValidRequest_ReturnsPaginatedCountryResponse()
    {
        // Arrange
        int callsGet = 2;
        int callsGetPagination = 1;
        CountryServiceFixture fixture = new();

        // Act
        ICountryService service = fixture.WithGetAllCountry(string.Empty, string.Empty)
                                          .WithGetPagination()
                                          .Instance();

        PaginationRequest pagination = fixture.PaginationRequestMock();
        PaginationResponse<CountryResponse> response = await service.GetPagedCountries(pagination);

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