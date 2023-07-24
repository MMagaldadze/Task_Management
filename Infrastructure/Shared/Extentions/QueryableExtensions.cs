using System.Linq.Expressions;

namespace Infrastructure.Shared.Extentions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TSource> Filter<TSource, TFilter>(this IQueryable<TSource> source, TFilter? filter, Expression<Func<TSource, bool>> predicate)
            where TFilter : struct
        {
            if (predicate == null)
            {
                return source;
            }
            if (!filter.HasValue)
            {
                return source;
            }
            return source.Where(predicate);
        }
    }
}
