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

public class GenderServiceFixture
{
    private readonly Fixture _fixture;
    private readonly ILogger<IGenderService> _logger;
    private readonly IBaseRepository<GenderEntity> _repository;
    private readonly IPaginationService _paginationService;
    private readonly IObjectConverter _mapper;

    public GenderServiceFixture()
    {
        _fixture = GetFixture();
        _logger = Substitute.For<ILogger<IGenderService>>();
        _repository = Substitute.For<IBaseRepository<GenderEntity>>();
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

    public GenderService Instance() => new(_logger, _repository, _paginationService, _mapper);

    #region Calls
    public IEnumerable<ICall> BaseRepositoryCalls() => _repository.ReceivedCalls();
    public IEnumerable<ICall> LoggerCalls() => _logger.ReceivedCalls();
    public IEnumerable<ICall> PaginationServiceCalls() => _paginationService.ReceivedCalls();
    #endregion

    #region Fixtures
    public GenderServiceFixture WithGetAllGender(string gender)
    {
        IEnumerable<GenderEntity> entities = _fixture.CreateMany<GenderEntity>();

        if (!string.IsNullOrEmpty(gender))
            entities = entities.Select(x =>
            {
                x.Gender = gender;
                return x;
            });

        _repository.Get().Returns(entities);
        return this;
    }

    public GenderServiceFixture WithGetGender(int id, bool returnsNull = false)
    {
        GenderEntity entity = null;
        GenderEntity argument = Arg.Any<GenderEntity>();

        if (!returnsNull)
        {
            entity = _fixture.Create<GenderEntity>();
            entity.GenderId = id;
        }

        _repository.Get(argument).Returns(entity);
        return this;
    }

    public GenderServiceFixture WithCreateGender()
    {
        GenderEntity argument = Arg.Any<GenderEntity>();
        GenderEntity entity = _fixture.Create<GenderEntity>();
        _repository.Create(argument).Returns(entity);
        return this;
    }

    public GenderServiceFixture WithUpdateGender()
    {
        GenderEntity argument = Arg.Any<GenderEntity>();
        GenderEntity entity = _fixture.Create<GenderEntity>();
        _repository.Update(argument).Returns(entity);
        return this;
    }

    public GenderServiceFixture WithDeleteGender(bool success = true)
    {
        GenderEntity argument = Arg.Any<GenderEntity>();
        _repository.Delete(argument).Returns(success);
        return this;
    }

    public GenderServiceFixture WithGetPagination()
    {
        PaginationRequest<GenderResponse> request = Arg.Any<PaginationRequest<GenderResponse>>();
        PaginationResponse<GenderResponse> response = _fixture.Create<PaginationResponse<GenderResponse>>();
        _paginationService.GetPagination<GenderResponse>(request).Returns(response);
        return this;
    }
    #endregion

    #region Mocks
    public GenderRequest GenderRequestMock() => _fixture.Create<GenderRequest>();
    public PaginationRequest PaginationRequestMock() => _fixture.Create<PaginationRequest>();

    #endregion
}
