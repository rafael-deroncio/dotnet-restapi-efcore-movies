using Microsoft.EntityFrameworkCore;

namespace MovieMania.Core.Extensions;

public static class DbContextExtension
{
    public static DbSet<TEntity> GetEntity<TEntity>(this DbContext context) where TEntity : class
    {
        return context.Set<TEntity>();
    }
}