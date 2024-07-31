using MovieMania.Core.Configurations.Mapper.Interfaces;
using MovieMania.Core.Contexts.Entities;
using MovieMania.Core.Exceptions;
using MovieMania.Core.Repositories.Interfaces;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services;

public class LanguageService(
    ILogger<ILanguageService> logger,
    IDatabaseMemory memory,
    IBaseRepository<LanguageEntity> languageRepository,
    IBaseRepository<LanguageRoleEntity> languageRoleRepository,
    IPaginationService paginationService,
    IObjectConverter mapper
) : ILanguageService
{
    private readonly ILogger<ILanguageService> _logger = logger;
    private readonly IDatabaseMemory _databaseMemory = memory;
    private readonly IBaseRepository<LanguageEntity> _languageRepository = languageRepository;
    private readonly IBaseRepository<LanguageRoleEntity> _languageRoleRepository = languageRoleRepository;
    private readonly IPaginationService _paginationService = paginationService;
    private readonly IObjectConverter _mapper = mapper;

    public async Task<LanguageResponse> CreateLanguage(LanguageRequest request)
    {
        try
        {
            if (_databaseMemory.Languages.Where(x => x.Language == request.Language).Any())
                throw new EntityBadRequestException("Error on create language entity", "Language alredy registred with name or iso code");

            LanguageEntity entity = _mapper.Map<LanguageEntity>(request);

            entity = await _languageRepository.Create(entity);
            if (entity is not null) await _databaseMemory.UpdateLanguages();

            return _mapper.Map<LanguageResponse>(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on create language with requet {Request}. Error: {Exception}", request, exception);
            throw new EntityUnprocessableException(
                title: "Language Entity Error",
                message: $"Unable to create a new record for language at this time. Please try again.");
        }
    }

    public async Task<bool> DeleteLanguage(int id)
    {
        try
        {
            LanguageEntity entity = _databaseMemory.Languages.FirstOrDefault(x => x.LanguageId == id);
            entity ??= await _languageRepository.Get(new() { LanguageId = id }) ??
                    throw new EntityNotFoundException("Language Not Found", $"Language with id {id} not exists.");

            bool result = await _languageRepository.Delete(entity);
            if (result) await _databaseMemory.UpdateLanguages();

            return result;
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on delete language with id {Identifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Language Entity Error",
                message: $"Unable to delete language with id {id} at this time. Please try again.");
        }
    }

    public async Task<LanguageResponse> GetLanguageById(int id)
    {
        try
        {
            LanguageEntity entity = _databaseMemory.Languages.FirstOrDefault(x => x.LanguageId == id);
            if (entity is not null)
                return _mapper.Map<LanguageResponse>(entity);

            entity = await _languageRepository.Get(new() { LanguageId = id });
            if (entity is not null)
                return _mapper.Map<LanguageResponse>(entity);

            throw new EntityNotFoundException("Language Not Found", $"Language with id {id} not exists.");
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get Language with id {Identifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Language Entity Error",
                message: $"Unable to get language wit id {id} at this time. Please try again.");
        }
    }

    public async Task<PaginationResponse<LanguageResponse>> GetPagedLanguages(PaginationRequest request)
    {
        try
        {
            IEnumerable<LanguageEntity> entities = _databaseMemory.Languages
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size);

            return await _paginationService.GetPagination<LanguageResponse>(new()
            {
                Content = _mapper.Map<IEnumerable<LanguageResponse>>(entities),
                Page = request.Page,
                Size = request.Size,
                Total = _databaseMemory.Languages.Count()
            });
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get paged languages with request {Request}. Error: {Exception}", request, exception);
            throw new EntityUnprocessableException(
                title: "Language Entity Error",
                message: "Unable get paged records for languages at this time. Please try again.");
        }
    }

    public async Task<LanguageResponse> UpdateLanguage(int id, LanguageRequest request)
    {
        try
        {
            LanguageEntity entity = _databaseMemory.Languages.FirstOrDefault(x => x.LanguageId == id);
            entity ??= await _languageRepository.Get(new() { LanguageId = id }) ??
                    throw new EntityNotFoundException("Language Not Found", $"Language with id {id} not exists.");

            entity.Language = request.Language.Trim();

            entity = await _languageRepository.Update(entity);
            if (entity is not null) await _databaseMemory.UpdateLanguages();

            return _mapper.Map<LanguageResponse>(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on update language with id {Identifier} from request {Request}. Error: {Exception}", id, request, exception);
            throw new EntityUnprocessableException(
                title: "Language Entity Error",
                message: $"Unable to update language with id {id} at this time. Please try again.");
        }
    }

    public async Task<LanguageRoleResponse> CreateLanguageRole(LanguageRoleRequest request)
    {
        try
        {
            if (_databaseMemory.LanguageRoles.Where(x => x.Role == request.Role).Any())
                throw new EntityBadRequestException("Error on create language role entity", "Language role alredy registred with name or iso code");

            LanguageRoleEntity entity = _mapper.Map<LanguageRoleEntity>(request);

            entity = await _languageRoleRepository.Create(entity);
            if (entity is not null) await _databaseMemory.UpdateLanguageRoles();

            return _mapper.Map<LanguageRoleResponse>(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on create language role with requet {Request}. Error: {Exception}", request, exception);
            throw new EntityUnprocessableException(
                title: "Language Entity Error",
                message: $"Unable to create a new record for language role at this time. Please try again.");
        }
    }

    public async Task<bool> DeleteLanguageRole(int id)
    {
        try
        {
            LanguageRoleEntity entity = _databaseMemory.LanguageRoles.FirstOrDefault(x => x.LanguageRoleId == id);
            entity ??= await _languageRoleRepository.Get(new() { LanguageRoleId = id }) ??
                    throw new EntityNotFoundException("Language Role Not Found", $"Language Role with id {id} not exists.");

            bool result = await _languageRoleRepository.Delete(entity);
            if (result) await _databaseMemory.UpdateLanguageRoles();

            return result;
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on delete language role with id {Identifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Language Role Entity Error",
                message: $"Unable to delete language role with id {id} at this time. Please try again.");
        }
    }

    public async Task<LanguageRoleResponse> GetLanguageRoleById(int id)
    {
        try
        {
            LanguageRoleEntity entity = _databaseMemory.LanguageRoles.FirstOrDefault(x => x.LanguageRoleId == id);
            if (entity is not null)
                return _mapper.Map<LanguageRoleResponse>(entity);

            entity = await _languageRoleRepository.Get(new() { LanguageRoleId = id });
            if (entity is not null)
                return _mapper.Map<LanguageRoleResponse>(entity);

            throw new EntityNotFoundException("Language Role Not Found", $"Language with id {id} not exists.");
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get Language Role with id {Identifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Language Entity Error",
                message: $"Unable to get language role wit id {id} at this time. Please try again.");
        }
    }

    public async Task<PaginationResponse<LanguageRoleResponse>> GetPagedLanguageRoles(PaginationRequest request)
    {
        try
        {
            IEnumerable<LanguageRoleEntity> entities = _databaseMemory.LanguageRoles
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size);

            return await _paginationService.GetPagination<LanguageRoleResponse>(new()
            {
                Content = _mapper.Map<IEnumerable<LanguageRoleResponse>>(entities),
                Page = request.Page,
                Size = request.Size,
                Total = _databaseMemory.Languages.Count()
            });
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get paged language roles with request {Request}. Error: {Exception}", request, exception);
            throw new EntityUnprocessableException(
                title: "Language Entity Error",
                message: "Unable get paged records for language roles at this time. Please try again.");
        }
    }

    public async Task<LanguageRoleResponse> UpdateLanguageRole(int id, LanguageRoleRequest request)
    {
        try
        {
            LanguageRoleEntity entity = _databaseMemory.LanguageRoles.FirstOrDefault(x => x.LanguageRoleId == id);
            entity ??= await _languageRoleRepository.Get(new() { LanguageRoleId = id }) ??
                    throw new EntityNotFoundException("Language Role Not Found", $"Language Role with id {id} not exists.");

            entity.Role = request.Role.Trim();

            entity = await _languageRoleRepository.Update(entity);
            if (entity is not null) await _databaseMemory.UpdateLanguageRoles();

            return _mapper.Map<LanguageRoleResponse>(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on update language role with id {Identifier} from request {Request}. Error: {Exception}", id, request, exception);
            throw new EntityUnprocessableException(
                title: "Language Role Entity Error",
                message: $"Unable to update language role with id {id} at this time. Please try again.");
        }
    }

}