using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;
using MovieMania.Test.Fixtures;
using NSubstitute.Core;

namespace MovieMania.Test.Services;

public class CountryServiceTest
{
    #region Get
    // [Fact]
    public async Task GetCountryById_ValidId_ReturnsCountryResponse()
    {
        // Arrange

        // Act

        // Assert
    }

    // [Fact]
    public async Task GetCountryById_InvalidId_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
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
                                          .WithGetCreateCountry()
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

    // [Fact]
    public async Task CreateCountry_InvalidRequest_ThrowsException()
    {

    }
    #endregion

    #region Update
    // [Fact]
    public async Task UpdateCountry_ValidIdAndRequest_ReturnsCountryResponse()
    {
        // Arrange

        // Act

        // Assert
    }

    // [Fact]
    public async Task UpdateCountry_InvalidId_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
    }

    // [Fact]
    public async Task UpdateCountry_InvalidRequest_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
    }
    #endregion

    #region Delete
    // [Fact]
    public async Task DeleteCountry_ValidId_ReturnsTrue()
    {
        // Arrange

        // Act

        // Assert
    }

    // [Fact]
    public async Task DeleteCountry_InvalidId_ReturnsFalse()
    {
        // Arrange

        // Act

        // Assert
    }
    #endregion

    #region Paged
    // [Fact]
    public async Task GetPagedCountries_ValidRequest_ReturnsPaginatedCountryResponse()
    {
        // Arrange

        // Act

        // Assert
    }
    #endregion
}