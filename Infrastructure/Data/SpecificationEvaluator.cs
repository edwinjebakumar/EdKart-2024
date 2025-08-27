using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> ApplySpecification(IQueryable<T> query, ISpecification<T> specification)
        {
            if (specification.Criteria != null)
                query = query.Where(specification.Criteria); // x => x.Brand == brand
            if (specification.OrderBy != null)
                query = query.OrderBy(specification.OrderBy);
            if (specification.OrderByDesc != null)
                query = query.OrderByDescending(specification.OrderByDesc);
            if (specification.IsDistinct)
                query = query.Distinct();
            return query;
        }

        public static IQueryable<TResult> ApplySpecification<TSpec, TResult>(IQueryable<T> query,
             ISpecification<T, TResult> specification)
        {
            if (specification.Criteria != null)
                query = query.Where(specification.Criteria); // x => x.Brand == brand
            if (specification.OrderBy != null)
                query = query.OrderBy(specification.OrderBy);
            if (specification.OrderByDesc != null)
                query = query.OrderByDescending(specification.OrderByDesc);

            var selectQuery = query as IQueryable<TResult>;
            if (specification.Select != null)
                selectQuery = query.Select(specification.Select);
            if (specification.IsDistinct)
                selectQuery = selectQuery?.Distinct();
            return selectQuery ?? query.Cast<TResult>();
        }
    }
}