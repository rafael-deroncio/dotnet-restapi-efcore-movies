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

public class GenreServiceFixture
{
    private readonly Fixture _fixture;
    private readonly ILogger<IGenreService> _logger;
    private readonly IBaseRepository<GenreEntity> _repository;
    private readonly IPaginationService _paginationService;
    private readonly IObjectConverter _mapper;

    public GenreServiceFixture()
    {
        _fixture = GetFixture();
        _logger = Substitute.For<ILogger<IGenreService>>();
        _repository = Substitute.For<IBaseRepository<GenreEntity>>();
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

    public GenreService Instance() => new(_logger, _repository, _paginationService, _mapper);

    #region Calls
    public IEnumerable<ICall> BaseRepositoryCalls() => _repository.ReceivedCalls();
    public IEnumerable<ICall> LoggerCalls() => _logger.ReceivedCalls();
    public IEnumerable<ICall> PaginationServiceCalls() => _paginationService.ReceivedCalls();
    #endregion

    #region Fixtures
    public GenreServiceFixture WithGetAllGenre(string genre)
    {
        IEnumerable<GenreEntity> entities = _fixture.CreateMany<GenreEntity>();

        if (!string.IsNullOrEmpty(genre))
            entities = entities.Select(x =>
            {
                x.Name = genre;
                return x;
            });

        _repository.Get().Returns(entities);
        return this;
    }

    public GenreServiceFixture WithGetGenre(int id, bool returnsNull = false)
    {
        GenreEntity entity = null;
        GenreEntity argument = Arg.Any<GenreEntity>();

        if (!returnsNull)
        {
            entity = _fixture.Create<GenreEntity>();
            entity.GenreId = id;
        }

        _repository.Get(argument).Returns(entity);
        return this;
    }

    public GenreServiceFixture WithCreateGenre()
    {
        GenreEntity argument = Arg.Any<GenreEntity>();
        GenreEntity entity = _fixture.Create<GenreEntity>();
        _repository.Create(argument).Returns(entity);
        return this;
    }

    public GenreServiceFixture WithUpdateGenre()
    {
        GenreEntity argument = Arg.Any<GenreEntity>();
        GenreEntity entity = _fixture.Create<GenreEntity>();
        _repository.Update(argument).Returns(entity);
        return this;
    }

    public GenreServiceFixture WithDeleteGenre(bool success = true)
    {
        GenreEntity argument = Arg.Any<GenreEntity>();
        _repository.Delete(argument).Returns(success);
        return this;
    }

    public GenreServiceFixture WithGetPagination()
    {
        PaginationRequest<GenreResponse> request = Arg.Any<PaginationRequest<GenreResponse>>();
        PaginationResponse<GenreResponse> response = _fixture.Create<PaginationResponse<GenreResponse>>();
        _paginationService.GetPagination<GenreResponse>(request).Returns(response);
        return this;
    }
    #endregion

    #region Mocks
    public GenreRequest GenreRequestMock() => _fixture.Create<GenreRequest>();
    public PaginationRequest PaginationRequestMock() => _fixture.Create<PaginationRequest>();

    #endregion
}
