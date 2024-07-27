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
    }
}
