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

public class PersonServiceFixture
{
    private readonly Fixture _fixture;
    private readonly ILogger<IPersonService> _logger;
    private readonly IBaseRepository<PersonEntity> _repository;
    private readonly IPaginationService _paginationService;
    private readonly IObjectConverter _mapper;

    public PersonServiceFixture()
    {
        _fixture = GetFixture();
        _logger = Substitute.For<ILogger<IPersonService>>();
        _repository = Substitute.For<IBaseRepository<PersonEntity>>();
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

    public PersonService Instance() => new(_logger, _repository, _paginationService, _mapper);

    #region Calls
    public IEnumerable<ICall> BaseRepositoryCalls() => _repository.ReceivedCalls();
    public IEnumerable<ICall> LoggerCalls() => _logger.ReceivedCalls();
    public IEnumerable<ICall> PaginationServiceCalls() => _paginationService.ReceivedCalls();
    #endregion

    #region Fixtures
    public PersonServiceFixture WithGetAllPerson(string person)
    {
        IEnumerable<PersonEntity> entities = _fixture.CreateMany<PersonEntity>();

        if (!string.IsNullOrEmpty(person))
            entities = entities.Select(x =>
            {
                x.Name = person;
                return x;
            });

        _repository.Get().Returns(entities);
        return this;
    }

    public PersonServiceFixture WithGetPerson(int id, bool returnsNull = false)
    {
        PersonEntity entity = null;
        PersonEntity argument = Arg.Any<PersonEntity>();

        if (!returnsNull)
        {
            entity = _fixture.Create<PersonEntity>();
            entity.PersonId = id;
        }

        _repository.Get(argument).Returns(entity);
        return this;
    }

    public PersonServiceFixture WithCreatePerson()
    {
        PersonEntity argument = Arg.Any<PersonEntity>();
        PersonEntity entity = _fixture.Create<PersonEntity>();
        _repository.Create(argument).Returns(entity);
        return this;
    }

    public PersonServiceFixture WithUpdatePerson()
    {
        PersonEntity argument = Arg.Any<PersonEntity>();
        PersonEntity entity = _fixture.Create<PersonEntity>();
        _repository.Update(argument).Returns(entity);
        return this;
    }

    public PersonServiceFixture WithDeletePerson(bool success = true)
    {
        PersonEntity argument = Arg.Any<PersonEntity>();
        _repository.Delete(argument).Returns(success);
        return this;
    }

    public PersonServiceFixture WithGetPagination()
    {
        PaginationRequest<PersonResponse> request = Arg.Any<PaginationRequest<PersonResponse>>();
        PaginationResponse<PersonResponse> response = _fixture.Create<PaginationResponse<PersonResponse>>();
        _paginationService.GetPagination<PersonResponse>(request).Returns(response);
        return this;
    }
    #endregion

    #region Mocks
    public PersonRequest PersonRequestMock() => _fixture.Create<PersonRequest>();
    public PaginationRequest PaginationRequestMock() => _fixture.Create<PaginationRequest>();

    #endregion
}
