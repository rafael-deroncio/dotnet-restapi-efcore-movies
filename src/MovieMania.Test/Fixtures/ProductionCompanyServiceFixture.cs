using AutoFixture;
using Microsoft.Extensions.Logging;
using MovieMania.Core.Configurations.Mapper;
using MovieMania.Core.Configurations.Mapper.Interfaces;
using MovieMania.Core.Contexts.Entities;
using MovieMania.Core.Repositories.Interfaces;
using MovieMania.Core.Requests;
using MovieMania.Core.Services;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;
using NSubstitute;
using NSubstitute.Core;

namespace MovieMania.Test.Fixtures;

public class ProductionCompanyServiceFixture
{
    private readonly Fixture _fixture;
    private readonly ILogger<IProductionCompanyService> _logger;
    private readonly IBaseRepository<ProductionCompanyEntity> _repository;
    private readonly IPaginationService _paginationService;
    private readonly IObjectConverter _mapper;

    public ProductionCompanyServiceFixture()
    {
        _fixture = GetFixture();
        _logger = Substitute.For<ILogger<IProductionCompanyService>>();
        _repository = Substitute.For<IBaseRepository<ProductionCompanyEntity>>();
        _paginationService = Substitute.For<IPaginationService>();
        _mapper = new ObjectConverter();
    }

    private Fixture GetFixture()
    {
        Fixture fixture = new();
        fixture.Behaviors
            .OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));

        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        return fixture;
    }

    public ProductionCompanyService Instance() => new(_logger, _repository, _paginationService, _mapper);

    #region Calls
    public IEnumerable<ICall> BaseRepositoryCalls() => _repository.ReceivedCalls();
    public IEnumerable<ICall> LoggerCalls() => _logger.ReceivedCalls();
    public IEnumerable<ICall> PaginationServiceCalls() => _paginationService.ReceivedCalls();
    #endregion

    #region Fixtures
    public ProductionCompanyServiceFixture WithGetAllProductionCompany(string person)
    {
        IEnumerable<ProductionCompanyEntity> entities = _fixture.CreateMany<ProductionCompanyEntity>();

        if (!string.IsNullOrEmpty(person))
            entities = entities.Select(x =>
            {
                x.Name = person;
                return x;
            });

        _repository.Get().Returns(entities);
        return this;
    }

    public ProductionCompanyServiceFixture WithGetProductionCompany(int id, bool returnsNull = false)
    {
        ProductionCompanyEntity entity = null;
        ProductionCompanyEntity argument = Arg.Any<ProductionCompanyEntity>();

        if (!returnsNull)
        {
            entity = _fixture.Create<ProductionCompanyEntity>();
            entity.CompanyId = id;
        }

        _repository.Get(argument).Returns(entity);
        return this;
    }

    public ProductionCompanyServiceFixture WithCreateProductionCompany()
    {
        ProductionCompanyEntity argument = Arg.Any<ProductionCompanyEntity>();
        ProductionCompanyEntity entity = _fixture.Create<ProductionCompanyEntity>();
        _repository.Create(argument).Returns(entity);
        return this;
    }

    public ProductionCompanyServiceFixture WithUpdateProductionCompany()
    {
        ProductionCompanyEntity argument = Arg.Any<ProductionCompanyEntity>();
        ProductionCompanyEntity entity = _fixture.Create<ProductionCompanyEntity>();
        _repository.Update(argument).Returns(entity);
        return this;
    }

    public ProductionCompanyServiceFixture WithDeleteProductionCompany(bool success = true)
    {
        ProductionCompanyEntity argument = Arg.Any<ProductionCompanyEntity>();
        _repository.Delete(argument).Returns(success);
        return this;
    }

    public ProductionCompanyServiceFixture WithGetPagination()
    {
        PaginationRequest<ProductionCompanyResponse> request = Arg.Any<PaginationRequest<ProductionCompanyResponse>>();
        PaginationResponse<ProductionCompanyResponse> response = _fixture.Create<PaginationResponse<ProductionCompanyResponse>>();
        _paginationService.GetPagination<ProductionCompanyResponse>(request).Returns(response);
        return this;
    }
    #endregion

    #region Mocks
    public ProductionCompanyRequest ProductionCompanyRequestMock() => _fixture.Create<ProductionCompanyRequest>();
    public PaginationRequest PaginationRequestMock() => _fixture.Create<PaginationRequest>();

    #endregion
}
