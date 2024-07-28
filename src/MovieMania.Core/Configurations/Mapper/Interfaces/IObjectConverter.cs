namespace MovieMania.Core.Configurations.Mapper.Interfaces;

/// <summary>
/// Interface for object conversion and mapping services.
/// </summary>
public interface IObjectConverter
{
    /// <summary>
    /// Maps the source object to a new object of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the destination object.</typeparam>
    /// <param name="source">The source object to map from.</param>
    /// <returns>The mapped object of type <typeparamref name="T"/>.</returns>
    T Map<T>(object source);

    /// <summary>
    /// Maps the source object of type <typeparamref name="T"/> to the destination object of type <typeparamref name="D"/>.
    /// </summary>
    /// <typeparam name="T">The type of the source object.</typeparam>
    /// <typeparam name="D">The type of the destination object.</typeparam>
    /// <param name="source">The source object to map from.</param>
    /// <param name="destination">The destination object to map to.</param>
    /// <returns>The mapped object of type <typeparamref name="D"/>.</returns>
    D Map<T, D>(T source, D destination);
}
