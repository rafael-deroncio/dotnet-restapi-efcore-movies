using AutoMapper;
using MovieMania.Core.Configurations.Mapper.Profiles;

namespace MovieMania.Core.Configurations.Mapper;

public class ObjectConverterConfiguration
{
    public MapperConfiguration RegisterMappings()
        => new(configuration =>
        {
            configuration.AddProfile(new EntityToResponseProfile());
            configuration.AddProfile(new EntityToRequestProfile());
        });

}