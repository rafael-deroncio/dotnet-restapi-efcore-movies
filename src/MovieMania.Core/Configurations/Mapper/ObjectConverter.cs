using AutoMapper;
using MovieMania.Core.Configurations.Mapper.Interfaces;

namespace MovieMania.Core;

public class ObjectConverter : IObjectConverter
{
    private readonly IMapper _mapper;

    public ObjectConverter()
    {
        
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
