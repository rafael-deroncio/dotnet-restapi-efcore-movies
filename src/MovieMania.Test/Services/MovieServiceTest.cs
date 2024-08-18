using MovieMania.Core.Exceptions;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;
using MovieMania.Test.Fixtures;
using NSubstitute.Core;

namespace MovieMania.Test.Services;

public class MovieServiceTest
{
    #region Get
    // [Fact]
    public async Task GetMovieById_ValidId_ReturnsMovieResponseWithFullObject()
    {
        // Arrange
        

        // Act

        // Assert
    }

    // [Fact]
    public async Task GetMovieById_InvalidId_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
    }
    #endregion

    #region Create
    // [Fact]
    public async Task CreateMovie_ValidRequest_ReturnsMovieResponse()
    {
        // Arrange

        // Act

        // Assert
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