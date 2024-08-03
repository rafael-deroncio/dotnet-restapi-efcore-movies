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
    IBaseRepository<LanguageEntity> languageRepository,
    IBaseRepository<LanguageRoleEntity> languageRoleRepository,
    IPaginationService paginationService,
    IObjectConverter mapper
) : ILanguageService
{
    private readonly ILogger<ILanguageService> _logger = logger;
    private readonly IBaseRepository<LanguageEntity> _languageRepository = languageRepository;
    private readonly IBaseRepository<LanguageRoleEntity> _languageRoleRepository = languageRoleRepository;
    private readonly IPaginationService _paginationService = paginationService;
    private readonly IObjectConverter _mapper = mapper;

    public async Task<LanguageResponse> CreateLanguage(LanguageRequest request)
    {
        try
        {
            if ((await _languageRepository.Get()).Where(x => x.Language == request.Language).Any())
                throw new EntityBadRequestException("Error on create language entity", "Language alredy registred with name or iso code");

            LanguageEntity entity = _mapper.Map<LanguageEntity>(request);

            return _mapper.Map<LanguageResponse>(await _languageRepository.Create(entity));
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
            LanguageRoleEntity entity = await _languageRoleRepository.Get(new() { LanguageRoleId = id })
                ?? throw new EntityNotFoundException("Language Role Not Found", $"Language Role with id {id} not exists.");

            return await _languageRoleRepository.Delete(entity);
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
            LanguageEntity entity = await _languageRepository.Get(new() { LanguageId = id })
                ?? throw new EntityNotFoundException("Language Not Found", $"Language with id {id} not exists.");

            return _mapper.Map<LanguageResponse>(entity);
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
            IEnumerable<LanguageEntity> entities = (await _languageRepository.Get())
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size);

            return await _paginationService.GetPagination<LanguageResponse>(new()
            {
                Content = _mapper.Map<IEnumerable<LanguageResponse>>(entities),
                Page = request.Page,
                Size = request.Size,
                Total = (await _languageRepository.Get()).Count()
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
            LanguageEntity entity = await _languageRepository.Get(new() { LanguageId = id })
                ?? throw new EntityNotFoundException("Language Not Found", $"Language with id {id} not exists.");

            entity.Language = request.Language.Trim();

            return _mapper.Map<LanguageResponse>(await _languageRepository.Update(entity));
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
            if ((await _languageRoleRepository.Get()).Where(x => x.Role == request.Role).Any())
                throw new EntityBadRequestException("Error on create language role entity", "Language Role alredy registred with name or iso code");

            LanguageRoleEntity entity = _mapper.Map<LanguageRoleEntity>(request);

            return _mapper.Map<LanguageRoleResponse>(await _languageRoleRepository.Create(entity));
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
            LanguageRoleEntity entity = await _languageRoleRepository.Get(new() { LanguageRoleId = id })
                ?? throw new EntityNotFoundException("Language Role Not Found", $"Language Role with id {id} not exists.");

            return await _languageRoleRepository.Delete(entity);
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
            LanguageRoleEntity entity = await _languageRoleRepository.Get(new() { LanguageRoleId = id })
                ?? throw new EntityNotFoundException("Language Role Not Found", $"Language Role with id {id} not exists.");

            return _mapper.Map<LanguageRoleResponse>(entity);
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
            IEnumerable<LanguageRoleEntity> entities = (await _languageRoleRepository.Get())
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size);

            return await _paginationService.GetPagination<LanguageRoleResponse>(new()
            {
                Content = _mapper.Map<IEnumerable<LanguageRoleResponse>>(entities),
                Page = request.Page,
                Size = request.Size,
                Total = (await _languageRoleRepository.Get()).Count()
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
            LanguageRoleEntity entity = await _languageRoleRepository.Get(new() { LanguageRoleId = id })
                ?? throw new EntityNotFoundException("Language Role Not Found", $"Language Role with id {id} not exists.");

            entity.Role = request.Role.Trim();

            return _mapper.Map<LanguageRoleResponse>(await _languageRoleRepository.Update(entity));
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