using AutoFixture;
using Microsoft.Extensions.Logging;
using MovieMania.Core.Configurations.Mapper.Interfaces;
using MovieMania.Core.Contexts.Entities;
using MovieMania.Core.Repositories.Interfaces;
using MovieMania.Core.Services;
using MovieMania.Core.Services.Interfaces;
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
        _fixture = new Fixture();
        _logger = Substitute.For<ILogger<ICountryService>>();
        _repository = Substitute.For<IBaseRepository<CountryEntity>>();
        _paginationService = Substitute.For<IPaginationService>();
        _mapper = Substitute.For<IObjectConverter>();
    }

    public CountryService Instance() => new(_logger, _repository, _paginationService, _mapper);

    #region Calls
    public IEnumerable<ICall> BaseRepositoryCalls() => _repository.ReceivedCalls();
    public IEnumerable<ICall> LoggerCalls() => _logger.ReceivedCalls();
    public IEnumerable<ICall> PaginationServiceCalls() => _paginationService.ReceivedCalls();
    #endregion

    #region Fixtures
    #endregion
}
