using AutoMapper;
using MovieMania.Core.Contexts.Entities;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Configurations.Mapper.Profiles;

public class EntityToResponseProfile : Profile
{
    public EntityToResponseProfile()
    {
        CreateMap<CountryEntity, CountryResponse>()
            .ForMember(dest => dest.Id,
                opts => opts.MapFrom(src => src.CountryId))
            .ReverseMap();

        CreateMap<DepartmentEntity, DepartmentResponse>()
            .ForMember(dest => dest.Id,
                opts => opts.MapFrom(src => src.DepartmentId))
            .ReverseMap();

        CreateMap<GenderEntity, GenderResponse>()
            .ForMember(dest => dest.Id,
                opts => opts.MapFrom(src => src.GenderId))
            .ReverseMap();

        CreateMap<GenreEntity, GenreResponse>()
            .ForMember(dest => dest.Id,
                opts => opts.MapFrom(src => src.GenreId))
            .ReverseMap();

        CreateMap<KeywordEntity, KeywordResponse>()
            .ForMember(dest => dest.Id,
                opts => opts.MapFrom(src => src.KeywordId))
            .ReverseMap();

        CreateMap<LanguageEntity, LanguageResponse>()
            .ForMember(dest => dest.Id,
                opts => opts.MapFrom(src => src.LanguageId))
            .ReverseMap();

        CreateMap<LanguageRoleEntity, LanguageRoleResponse>()
            .ForMember(dest => dest.Id,
                opts => opts.MapFrom(src => src.LanguageRoleId))
            .ReverseMap();

        CreateMap<ProductionCompanyEntity, ProductionCompanyResponse>()
            .ForMember(dest => dest.Id,
                opts => opts.MapFrom(src => src.CompanyId))
            .ReverseMap();

        CreateMap<PersonEntity, PersonResponse>()
            .ForMember(dest => dest.Id,
                opts => opts.MapFrom(src => src.PersonId))
            .ReverseMap();

        CreateMap<MovieEntity, MovieResponse>()
            .ForMember(dest => dest.Id,
                opts => opts.MapFrom(src => src.MovieId))
            .ReverseMap();

        CreateMap<ProductionCountryEntity, ProductionCountryResponse>()
            .ForMember(dest => dest.Country,
                opts => opts.MapFrom(src => src.Country))
            .ReverseMap();

        CreateMap<MovieLanguageEntity, MovieLanguageResponse>()
            .ForMember(dest => dest.Language,
                opts => opts.MapFrom(src => src.Language))
            .ForMember(dest => dest.LanguageRole,
                opts => opts.MapFrom(src => src.LanguageRole))
            .ReverseMap();

        CreateMap<MovieGenreEntity, GenreResponse>()
            .ForMember(dest => dest.Name,
                opts => opts.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.Id,
                opts => opts.MapFrom(src => src.Genre.GenreId))
            .ReverseMap();

        CreateMap<MovieKeywordEntity, KeywordResponse>()
            .ForPath(dest => dest.Id,
                opts => opts.MapFrom(src => src.Keyword.KeywordId))
            .ForPath(dest => dest.Keyword,
                opts => opts.MapFrom(src => src.Keyword.Keyword))
            .ForPath(dest => dest.CreatedAt,
                opts => opts.MapFrom(src => src.Keyword.CreatedAt))
            .ReverseMap();

        CreateMap<MovieCompanyEntity, ProductionCompanyResponse>()
            .ForPath(dest => dest.Name,
                opts => opts.MapFrom(src => src.ProductionCompany.Name))
            .ForPath(dest => dest.Id,
                opts => opts.MapFrom(src => src.ProductionCompany.CompanyId))
            .ForPath(dest => dest.CreatedAt,
                opts => opts.MapFrom(src => src.ProductionCompany.CreatedAt))
            .ReverseMap();

        CreateMap<MovieCastEntity, CastResponse>()
            .ForMember(dest => dest.Gender,
                opts => opts.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Person,
                opts => opts.MapFrom(src => src.Person))
            .ReverseMap();

        CreateMap<MovieCrewEntity, CrewResponse>()
            .ForMember(dest => dest.Person,
                opts => opts.MapFrom(src => src.Person))
            .ForMember(dest => dest.Department,
                opts => opts.MapFrom(src => src.Department))
            .ReverseMap();
    }
}
