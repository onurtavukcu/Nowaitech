using LanguageExt;

namespace NowaitechDomain.Repository.CustomQuery
{
    public static class CustomQuery
    {
        public static Option<TEntity> GetByCustomId<TEntity>(
            this IQueryable<TEntity> queryable,
            Func<TEntity, bool> propertyPredicate) where TEntity : class
        {
            return queryable.SingleOrDefault(propertyPredicate);
        }
    }
}
