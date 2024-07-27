using AutoMapper;
using MovieMania.Core.Configurations.Mapper.Interfaces;
using MovieMania.Core.Configurations.Mapper.Profiles;

namespace MovieMania.Core.Configurations.Mapper;

public class ObjectConverter : IObjectConverter
{
    private readonly IMapper _mapper;

    public ObjectConverter()
    {
        _mapper = new ObjectConverterConfiguration()
            .RegisterMappings()
            .CreateMapper();
    }

    public T Map<T>(object source)
    {
        return _mapper.Map<T>(source);
    }

    public D Map<T, D>(T source, D destination)
    {
        return source is null ? destination : _mapper.Map<T, D>(source, destination);
    }
}
