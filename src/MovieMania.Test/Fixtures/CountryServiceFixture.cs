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

public class CountryServiceFixture
{
    private readonly Fixture _fixture;
    private readonly ILogger<ICountryService> _logger;
    private readonly IBaseRepository<CountryEntity> _repository;
    private readonly IPaginationService _paginationService;
    private readonly IObjectConverter _mapper;

    public CountryServiceFixture()
    {
        _fixture = GetFixture();
        _logger = Substitute.For<ILogger<ICountryService>>();
        _repository = Substitute.For<IBaseRepository<CountryEntity>>();
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

    public CountryService Instance() => new(_logger, _repository, _paginationService, _mapper);

    #region Calls
    public IEnumerable<ICall> BaseRepositoryCalls() => _repository.ReceivedCalls();
    public IEnumerable<ICall> LoggerCalls() => _logger.ReceivedCalls();
    public IEnumerable<ICall> PaginationServiceCalls() => _paginationService.ReceivedCalls();
    #endregion

    #region Fixtures
    public CountryServiceFixture WithGetAllCountry(string isoCode, string name)
    {
        IEnumerable<CountryEntity> entities = _fixture.CreateMany<CountryEntity>();

        if (!string.IsNullOrEmpty(isoCode) || !string.IsNullOrEmpty(name))
            entities = entities.Select(x =>
            {
                x.IsoCode = isoCode;
                x.Name = name;
                return x;
            });

        _repository.Get().Returns(entities);
        return this;
    }

    public CountryServiceFixture WithGetCountry(int id, bool returnsNull = false)
    {
        CountryEntity entity = null;
        CountryEntity argument = Arg.Any<CountryEntity>();

        if (!returnsNull)
        {
            entity = _fixture.Create<CountryEntity>();
            entity.CountryId = id;
        }

        _repository.Get(argument).Returns(entity);
        return this;
    }

    public CountryServiceFixture WithCreateCountry()
    {
        CountryEntity argument = Arg.Any<CountryEntity>();
        CountryEntity entity = _fixture.Create<CountryEntity>();
        _repository.Create(argument).Returns(entity);
        return this;
    }

    public CountryServiceFixture WithUpdateCountry()
    {
        CountryEntity argument = Arg.Any<CountryEntity>();
        CountryEntity entity = _fixture.Create<CountryEntity>();
        _repository.Update(argument).Returns(entity);
        return this;
    }

    public CountryServiceFixture WithDeleteCountry(bool success = true)
    {
        CountryEntity argument = Arg.Any<CountryEntity>();
        _repository.Delete(argument).Returns(success);
        return this;
    }

    public CountryServiceFixture WithGetPagination()
    {
        PaginationRequest<CountryResponse> request = Arg.Any<PaginationRequest<CountryResponse>>();
        PaginationResponse<CountryResponse> response = _fixture.Create<PaginationResponse<CountryResponse>>();
        _paginationService.GetPagination<CountryResponse>(request).Returns(response);
        return this;
    }
    #endregion

    #region Mocks
    public CountryRequest CountryRequestMock() => _fixture.Create<CountryRequest>();
    public PaginationRequest PaginationRequestMock() => _fixture.Create<PaginationRequest>();

    #endregion
}
