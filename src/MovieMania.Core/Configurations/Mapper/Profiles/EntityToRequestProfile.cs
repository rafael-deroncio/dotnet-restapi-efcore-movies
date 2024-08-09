using AutoMapper;
using MovieMania.Core.Contexts.Entities;
using MovieMania.Domain.DTOs;
using MovieMania.Domain.Requests;

namespace MovieMania.Core.Configurations.Mapper.Profiles;

public class EntityToRequestProfile : Profile
{
    public EntityToRequestProfile()
    {
        CreateMap<CountryEntity, CountryRequest>().ReverseMap();
        CreateMap<DepartmentEntity, DepartmentRequest>().ReverseMap();
        CreateMap<GenderEntity, GenderRequest>().ReverseMap();
        CreateMap<GenreEntity, GenreRequest>().ReverseMap();
        CreateMap<KeywordEntity, KeywordRequest>().ReverseMap();
        CreateMap<LanguageEntity, LanguageRequest>().ReverseMap();
        CreateMap<LanguageRoleEntity, LanguageRoleRequest>().ReverseMap();
        CreateMap<ProductionCompanyEntity, ProductionCompanyRequest>().ReverseMap();
        CreateMap<PersonEntity, PersonRequest>().ReverseMap();

        CreateMap<MovieEntity, MovieRequest>().ReverseMap();

        CreateMap<CountryEntity, MovieCountryRequest>().ReverseMap();
        CreateMap<ProductionCountryEntity, MovieProductionCountryRequest>()
            .ForPath(dest => dest.Country.Id,
                opts => opts.MapFrom(src => src.CountryId))
            .ReverseMap();

        CreateMap<LanguageEntity, Language>().ReverseMap();
        CreateMap<LanguageRoleEntity, LanguageRole>().ReverseMap();
        CreateMap<MovieLanguageEntity, MovieLanguageRequest>()
            .ForPath(dest => dest.Language.Id,
                opts => opts.MapFrom(src => src.LanguageId))
            .ForPath(dest => dest.LanguageRole.Id,
                opts => opts.MapFrom(src => src.LanguageRoleId))
            .ReverseMap();

        CreateMap<MovieGenreEntity, MovieGenreRequest>()
            .ForMember(dest => dest.Id,
                opts => opts.MapFrom(src => src.GenreId))
            .ReverseMap();

        CreateMap<MovieKeywordEntity, MovieKeywordRequest>()
            .ForMember(dest => dest.Id,
                opts => opts.MapFrom(src => src.KeywordId))
            .ReverseMap();

        CreateMap<MovieCompanyEntity, MovieCompanyRequest>()
            .ForMember(dest => dest.Id,
                opts => opts.MapFrom(src => src.CompanyId))
            .ReverseMap();

        CreateMap<GenderEntity, Gender>().ReverseMap();
        CreateMap<MovieCastEntity, MovieCastRequest>()
            .ForPath(dest => dest.Person.Id,
                opts => opts.MapFrom(src => src.PersonId))
            .ForPath(dest => dest.Gender.Id,
                opts => opts.MapFrom(src => src.Gender.GenderId))
            .ReverseMap();

        CreateMap<PersonEntity, Person>().ReverseMap();
        CreateMap<DepartmentEntity, Department>().ReverseMap();
        CreateMap<MovieCrewEntity, MovieCrewRequest>()
            .ForPath(dest => dest.Person.Id,
                opts => opts.MapFrom(src => src.PersonId))
            .ForPath(dest => dest.Department.Id,
                opts => opts.MapFrom(src => src.DepartmentId))
            .ReverseMap();
    }
}