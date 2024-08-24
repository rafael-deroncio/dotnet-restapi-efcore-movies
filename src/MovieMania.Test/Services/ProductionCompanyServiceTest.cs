using MovieMania.Core.Exceptions;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;
using MovieMania.Test.Fixtures;
using NSubstitute.Core;

namespace MovieMania.Test.Services;

public class ProductionCompanyServiceTest
{
    #region Get
    [Fact]
    public async Task GetProductionCompanyById_ValidId_ReturnsProductionCompanyResponse()
    {
        // Arrange
        int id = 1;
        int callsGet = 1;
        ProductionCompanyServiceFixture fixture = new();

        // Act
        IProductionCompanyService service = fixture.WithGetProductionCompany(id)
                                          .Instance();

        ProductionCompanyResponse response = await service.GetProductionCompanyById(id);

        IEnumerable<ICall> repositoryGetCalls = fixture.BaseRepositoryCalls()
            .Where(call => call.GetMethodInfo().Name == "Get");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(callsGet, repositoryGetCalls.Count());
    }

    [Fact]
    public async Task GetProductionCompanyById_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Production Company Not Found";
        string exceptionMessage = $"Production Company with id {id} not exists";
        int callsGet = 1;
        ProductionCompanyServiceFixture fixture = new();

        // Act
        IProductionCompanyService service = fixture.WithGetProductionCompany(id, true).Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            await service.GetProductionCompanyById(id));

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
    public async Task CreateProductionCompany_ValidRequest_ReturnsProductionCompanyResponse()
    {
        // Arrange
        string isoCode = string.Empty;
        string name = string.Empty;
        int callsGet = 1;
        int callsCreate = 1;
        ProductionCompanyServiceFixture fixture = new();

        // Act
        IProductionCompanyService service = fixture.WithGetAllProductionCompany(name)
                                          .WithCreateProductionCompany()
                                          .Instance();

        ProductionCompanyRequest request = fixture.ProductionCompanyRequestMock();
        ProductionCompanyResponse response = await service.CreateProductionCompany(request);

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
    public async Task CreateProductionCompany_InvalidRequest_ThrowsException()
    {
        // Arrange
        string company = "Male";
        string exceptionTitle = "Error on create production company entity";
        string exceptionMessage = "Production Company alredy registred";
        int callsGet = 1;
        int callsCreate = 0;
        ProductionCompanyServiceFixture fixture = new();

        // Act
        IProductionCompanyService service = fixture.WithGetAllProductionCompany(company)
                                          .Instance();

        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(
            async () =>
            {
                ProductionCompanyRequest request = fixture.ProductionCompanyRequestMock();
                request.Name = company;

                ProductionCompanyResponse response = await service.CreateProductionCompany(request);
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
    public async Task UpdateProductionCompany_ValidIdAndRequest_ReturnsProductionCompanyResponse()
    {
        // Arrange
        int id = 1;
        int callsGet = 2;
        int callsUpdate = 1;
        ProductionCompanyServiceFixture fixture = new();

        // Act
        IProductionCompanyService service = fixture.WithGetProductionCompany(id)
                                          .WithUpdateProductionCompany()
                                          .Instance();

        ProductionCompanyRequest request = fixture.ProductionCompanyRequestMock();
        ProductionCompanyResponse response = await service.UpdateProductionCompany(id, request);

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
    public async Task UpdateProductionCompany_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Production Company Not Found";
        string exceptionMessage = $"Production Company with id {id} not exists";
        int callsGet = 1;
        int callsUpdate = 0;
        ProductionCompanyServiceFixture fixture = new();

        // Act
        IProductionCompanyService service = fixture.WithGetProductionCompany(id, true)
                                          .Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            ProductionCompanyRequest request = fixture.ProductionCompanyRequestMock();
            ProductionCompanyResponse response = await service.UpdateProductionCompany(id, request);
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
    public async Task UpdateProductionCompany_InvalidRequest_ThrowsException()
    {
        // Arrange
        int id = 1;
        string company = "Male";
        string exceptionTitle = "Error on update production company entity";
        string exceptionMessage = $"Production Company alredy registred";
        int callsGet = 2;
        int callsUpdate = 0;
        ProductionCompanyServiceFixture fixture = new();

        // Act
        IProductionCompanyService service = fixture.WithGetProductionCompany(id)
                                         .WithGetAllProductionCompany(company)
                                         .Instance();

        EntityBadRequestException exception = await Assert.ThrowsAsync<EntityBadRequestException>(async () =>
        {
            ProductionCompanyRequest request = fixture.ProductionCompanyRequestMock();
            request.Name = company;

            ProductionCompanyResponse response = await service.UpdateProductionCompany(id, request);
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
    public async Task DeleteProductionCompany_ValidId_ReturnsTrue()
    {
        // Arrange
        int id = 1;
        int callsGet = 1;
        int callsDelete = 1;
        ProductionCompanyServiceFixture fixture = new();

        // Act
        IProductionCompanyService service = fixture.WithGetProductionCompany(id)
                                          .WithDeleteProductionCompany()
                                          .Instance();

        bool result = await service.DeleteProductionCompany(id);

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
    public async Task DeleteProductionCompany_InvalidId_ThrowsException()
    {
        // Arrange
        int id = 1;
        string exceptionTitle = "Production Company Not Found";
        string exceptionMessage = $"Production Company with id {id} not exists";
        int callsGet = 1;
        int callsDelete = 0;
        ProductionCompanyServiceFixture fixture = new();

        // Act
        IProductionCompanyService service = fixture.WithGetProductionCompany(id, true)
                                          .Instance();

        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(async ()
            => await service.DeleteProductionCompany(id));

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
    public async Task GetPagedCompanies_ValidRequest_ReturnsPaginatedProductionCompanyResponse()
    {
        // Arrange
        int callsGet = 2;
        int callsGetPagination = 1;
        ProductionCompanyServiceFixture fixture = new();

        // Act
        IProductionCompanyService service = fixture.WithGetAllProductionCompany(string.Empty)
                                          .WithGetPagination()
                                          .Instance();

        PaginationRequest pagination = fixture.PaginationRequestMock();
        PaginationResponse<ProductionCompanyResponse> response = await service.GetPagedProductionCompanys(pagination);

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