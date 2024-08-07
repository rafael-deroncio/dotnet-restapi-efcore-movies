﻿using AutoMapper;
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

            
    }
}
