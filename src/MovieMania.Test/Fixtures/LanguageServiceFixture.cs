using AutoFixture;
using Microsoft.Extensions.Logging;
using MovieMania.Core.Configurations.DTOs;
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

public class LanguageServiceFixture
{
    private readonly Fixture _fixture;
    private readonly ILogger<ILanguageService> _logger;
    private readonly IBaseRepository<LanguageEntity> _languageRepository;
    private readonly IBaseRepository<LanguageRoleEntity> _languageRoleRepository;
    private readonly IPaginationService _paginationService;
    private readonly IObjectConverter _mapper;

    public LanguageServiceFixture()
    {
        _fixture = GetFixture();
        _logger = Substitute.For<ILogger<ILanguageService>>();
        _languageRepository = Substitute.For<IBaseRepository<LanguageEntity>>();
        _languageRoleRepository = Substitute.For<IBaseRepository<LanguageRoleEntity>>();
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

    public LanguageService Instance() => new(_logger, _languageRepository, _languageRoleRepository, _paginationService, _mapper);

    #region Calls
    public IEnumerable<ICall> LanguageRepositoryCalls() => _languageRepository.ReceivedCalls();
    public IEnumerable<ICall> LanguageRoleRepositoryCalls() => _languageRoleRepository.ReceivedCalls();
    public IEnumerable<ICall> LoggerCalls() => _logger.ReceivedCalls();
    public IEnumerable<ICall> PaginationServiceCalls() => _paginationService.ReceivedCalls();
    #endregion

    #region Fixtures
    public LanguageServiceFixture WithGetAllLanguage(string language)
    {
        IEnumerable<LanguageEntity> entities = _fixture.CreateMany<LanguageEntity>();

        if (!string.IsNullOrEmpty(language))
            entities = entities.Select(x =>
            {
                x.Language = language;
                return x;
            });

        _languageRepository.Get().Returns(entities);
        return this;
    }

    public LanguageServiceFixture WithGetLanguage(int id, bool returnsNull = false)
    {
        LanguageEntity entity = null;
        LanguageEntity argument = Arg.Any<LanguageEntity>();

        if (!returnsNull)
        {
            entity = _fixture.Create<LanguageEntity>();
            entity.LanguageId = id;
        }

        _languageRepository.Get(argument).Returns(entity);
        return this;
    }

    public LanguageServiceFixture WithCreateLanguage()
    {
        LanguageEntity argument = Arg.Any<LanguageEntity>();
        LanguageEntity entity = _fixture.Create<LanguageEntity>();
        _languageRepository.Create(argument).Returns(entity);
        return this;
    }

    public LanguageServiceFixture WithUpdateLanguage()
    {
        LanguageEntity argument = Arg.Any<LanguageEntity>();
        LanguageEntity entity = _fixture.Create<LanguageEntity>();
        _languageRepository.Update(argument).Returns(entity);
        return this;
    }

    public LanguageServiceFixture WithDeleteLanguage(bool success = true)
    {
        LanguageEntity argument = Arg.Any<LanguageEntity>();
        _languageRepository.Delete(argument).Returns(success);
        return this;
    }

    public LanguageServiceFixture WithGetPagination<T>()
    {
        PaginationRequest<T> request = Arg.Any<PaginationRequest<T>>();
        PaginationResponse<T> response = _fixture.Create<PaginationResponse<T>>();
        _paginationService.GetPagination<T>(request).Returns(response);
        return this;
    }
    #endregion

    #region Fixtures
    public LanguageServiceFixture WithGetAllLanguageRole(string role)
    {
        IEnumerable<LanguageRoleEntity> entities = _fixture.CreateMany<LanguageRoleEntity>();

        if (!string.IsNullOrEmpty(role))
            entities = entities.Select(x =>
            {
                x.Role = role;
                return x;
            });

        _languageRoleRepository.Get().Returns(entities);
        return this;
    }

    public LanguageServiceFixture WithGetLanguageRole(int id, bool returnsNull = false)
    {
        LanguageRoleEntity entity = null;
        LanguageRoleEntity argument = Arg.Any<LanguageRoleEntity>();

        if (!returnsNull)
        {
            entity = _fixture.Create<LanguageRoleEntity>();
            entity.LanguageRoleId = id;
        }

        _languageRoleRepository.Get(argument).Returns(entity);
        return this;
    }

    public LanguageServiceFixture WithCreateLanguageRole()
    {
        LanguageRoleEntity argument = Arg.Any<LanguageRoleEntity>();
        LanguageRoleEntity entity = _fixture.Create<LanguageRoleEntity>();
        _languageRoleRepository.Create(argument).Returns(entity);
        return this;
    }

    public LanguageServiceFixture WithUpdateLanguageRole()
    {
        LanguageRoleEntity argument = Arg.Any<LanguageRoleEntity>();
        LanguageRoleEntity entity = _fixture.Create<LanguageRoleEntity>();
        _languageRoleRepository.Update(argument).Returns(entity);
        return this;
    }

    public LanguageServiceFixture WithDeleteLanguageRole(bool success = true)
    {
        LanguageRoleEntity argument = Arg.Any<LanguageRoleEntity>();
        _languageRoleRepository.Delete(argument).Returns(success);
        return this;
    }
    #endregion

    #region Mocks
    public LanguageRequest LanguageRequestMock() => _fixture.Create<LanguageRequest>();
    public LanguageRoleRequest LanguageRoleRequestMock() => _fixture.Create<LanguageRoleRequest>();
    public PaginationRequest PaginationRequestMock() => _fixture.Create<PaginationRequest>();

    #endregion
}
