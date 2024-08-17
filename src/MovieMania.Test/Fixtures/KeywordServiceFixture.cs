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

public class KeywordServiceFixture
{
    private readonly Fixture _fixture;
    private readonly ILogger<IKeywordService> _logger;
    private readonly IBaseRepository<KeywordEntity> _repository;
    private readonly IPaginationService _paginationService;
    private readonly IObjectConverter _mapper;

    public KeywordServiceFixture()
    {
        _fixture = GetFixture();
        _logger = Substitute.For<ILogger<IKeywordService>>();
        _repository = Substitute.For<IBaseRepository<KeywordEntity>>();
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

    public KeywordService Instance() => new(_logger, _repository, _paginationService, _mapper);

    #region Calls
    public IEnumerable<ICall> BaseRepositoryCalls() => _repository.ReceivedCalls();
    public IEnumerable<ICall> LoggerCalls() => _logger.ReceivedCalls();
    public IEnumerable<ICall> PaginationServiceCalls() => _paginationService.ReceivedCalls();
    #endregion

    #region Fixtures
    public KeywordServiceFixture WithGetAllKeyword(string keyword)
    {
        IEnumerable<KeywordEntity> entities = _fixture.CreateMany<KeywordEntity>();

        if (!string.IsNullOrEmpty(keyword))
            entities = entities.Select(x =>
            {
                x.Keyword = keyword;
                return x;
            });

        _repository.Get().Returns(entities);
        return this;
    }

    public KeywordServiceFixture WithGetKeyword(int id, bool returnsNull = false)
    {
        KeywordEntity entity = null;
        KeywordEntity argument = Arg.Any<KeywordEntity>();

        if (!returnsNull)
        {
            entity = _fixture.Create<KeywordEntity>();
            entity.KeywordId = id;
        }

        _repository.Get(argument).Returns(entity);
        return this;
    }

    public KeywordServiceFixture WithCreateKeyword()
    {
        KeywordEntity argument = Arg.Any<KeywordEntity>();
        KeywordEntity entity = _fixture.Create<KeywordEntity>();
        _repository.Create(argument).Returns(entity);
        return this;
    }

    public KeywordServiceFixture WithUpdateKeyword()
    {
        KeywordEntity argument = Arg.Any<KeywordEntity>();
        KeywordEntity entity = _fixture.Create<KeywordEntity>();
        _repository.Update(argument).Returns(entity);
        return this;
    }

    public KeywordServiceFixture WithDeleteKeyword(bool success = true)
    {
        KeywordEntity argument = Arg.Any<KeywordEntity>();
        _repository.Delete(argument).Returns(success);
        return this;
    }

    public KeywordServiceFixture WithGetPagination()
    {
        PaginationRequest<KeywordResponse> request = Arg.Any<PaginationRequest<KeywordResponse>>();
        PaginationResponse<KeywordResponse> response = _fixture.Create<PaginationResponse<KeywordResponse>>();
        _paginationService.GetPagination<KeywordResponse>(request).Returns(response);
        return this;
    }
    #endregion

    #region Mocks
    public KeywordRequest KeywordRequestMock() => _fixture.Create<KeywordRequest>();
    public PaginationRequest PaginationRequestMock() => _fixture.Create<PaginationRequest>();

    #endregion
}
