using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static  class SpecificationEvalutor<TEntity>where TEntity : BaseEntity
    {
        public  static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria); //p=>p.id

            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            if (spec.OrderByDescending is not null)
                query = query.OrderByDescending(spec.OrderByDescending);

            if (spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            // filter then pagination 

            //Context.Products.where(P=>P.id==id);
            query = spec.Includes.Aggregate(query, (currentQuery, IncludeExpression) => currentQuery.Include(IncludeExpression));
            //context.Products.where(P=>id==id).Include(P=P.ProductBrand).Include(P=>P.ProfuctType);
            //context.product.OrderBy(P=>P.Name).Include(P=P.ProductBrand).Include(P=>P.ProfuctType);


            return query; 
        }
    }
}
