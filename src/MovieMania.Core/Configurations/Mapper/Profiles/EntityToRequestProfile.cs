using AutoMapper;
using MovieMania.Core.Contexts.Entities;
using MovieMania.Domain.Requests;

namespace MovieMania.Core.Configurations.Mapper.Profiles;

public class EntityToRequestProfile : Profile
{
    public EntityToRequestProfile()
    {
        CreateMap<CountryEntity, CountryRequest>().ReverseMap();
    }
}