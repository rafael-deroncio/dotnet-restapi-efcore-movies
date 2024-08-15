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

public class DepartmentServiceFixture
{
    private readonly Fixture _fixture;
    private readonly ILogger<IDepartmentService> _logger;
    private readonly IBaseRepository<DepartmentEntity> _repository;
    private readonly IPaginationService _paginationService;
    private readonly IObjectConverter _mapper;

    public DepartmentServiceFixture()
    {
        _fixture = GetFixture();
        _logger = Substitute.For<ILogger<IDepartmentService>>();
        _repository = Substitute.For<IBaseRepository<DepartmentEntity>>();
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

    public DepartmentService Instance() => new(_logger, _repository, _paginationService, _mapper);

    #region Calls
    public IEnumerable<ICall> BaseRepositoryCalls() => _repository.ReceivedCalls();
    public IEnumerable<ICall> LoggerCalls() => _logger.ReceivedCalls();
    public IEnumerable<ICall> PaginationServiceCalls() => _paginationService.ReceivedCalls();
    #endregion

    #region Fixtures
    public DepartmentServiceFixture WithGetAllDepartment(string name)
    {
        IEnumerable<DepartmentEntity> entities = _fixture.CreateMany<DepartmentEntity>();

        if (!string.IsNullOrEmpty(name))
            entities = entities.Select(x =>
            {
                x.Name = name;
                return x;
            });

        _repository.Get().Returns(entities);
        return this;
    }

    public DepartmentServiceFixture WithGetDepartment(int id, bool returnsNull = false)
    {
        DepartmentEntity entity = null;
        DepartmentEntity argument = Arg.Any<DepartmentEntity>();

        if (!returnsNull)
        {
            entity = _fixture.Create<DepartmentEntity>();
            entity.DepartmentId = id;
        }

        _repository.Get(argument).Returns(entity);
        return this;
    }

    public DepartmentServiceFixture WithCreateDepartment()
    {
        DepartmentEntity argument = Arg.Any<DepartmentEntity>();
        DepartmentEntity entity = _fixture.Create<DepartmentEntity>();
        _repository.Create(argument).Returns(entity);
        return this;
    }

    public DepartmentServiceFixture WithUpdateDepartment()
    {
        DepartmentEntity argument = Arg.Any<DepartmentEntity>();
        DepartmentEntity entity = _fixture.Create<DepartmentEntity>();
        _repository.Update(argument).Returns(entity);
        return this;
    }

    public DepartmentServiceFixture WithDeleteDepartment(bool success = true)
    {
        DepartmentEntity argument = Arg.Any<DepartmentEntity>();
        _repository.Delete(argument).Returns(success);
        return this;
    }

    public DepartmentServiceFixture WithGetPagination()
    {
        PaginationRequest<DepartmentResponse> request = Arg.Any<PaginationRequest<DepartmentResponse>>();
        PaginationResponse<DepartmentResponse> response = _fixture.Create<PaginationResponse<DepartmentResponse>>();
        _paginationService.GetPagination<DepartmentResponse>(request).Returns(response);
        return this;
    }
    #endregion

    #region Mocks
    public DepartmentRequest DepartmentRequestMock() => _fixture.Create<DepartmentRequest>();
    public PaginationRequest PaginationRequestMock() => _fixture.Create<PaginationRequest>();

    #endregion
}
