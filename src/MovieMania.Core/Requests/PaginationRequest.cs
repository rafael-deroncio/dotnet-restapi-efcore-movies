namespace MovieMania.Core.Requests;

public record PaginationRequest<T>
{
    public int Page { get; set; }

    public int Size { get; set; }

    public int Total { get; set; }
    
    public IEnumerable<T> Content { get; set; }
}
