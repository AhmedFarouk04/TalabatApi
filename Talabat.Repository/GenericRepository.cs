using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext dbContext;

        public GenericRepository(StoreContext dbContext) {
        
        this.dbContext=dbContext;
        }
        #region Static Query
        public async Task<IReadOnlyList <T>> GetAllAsync()
        {     //not best solution to get brand and type on product sson we write the best (Specification Pattern  product >>brand type  if exist another 
            //if (typeof(T) == typeof(Product))
            //    return (IEnumerable<T>)await dbContext.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync();

            return await dbContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            //if (typeof(T) == typeof(Product))
            //   return   await dbContext.Products.Where(p => p.Id==id).Include(p => p.ProductBrand).Include(p => p.ProductType).FirstOrDefaultAsync();
            return await dbContext.Set<T>().FindAsync(id);

        } 
        #endregion




        public async Task<IReadOnlyList<T>> GetAllWitSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync(); ;

        }

        public  async Task<T> GetByIdWitSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync(); ;
        }

      

        public Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
        {
            return ApplySpecification(spec).CountAsync(); 
        }


        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvalutor<T>.GetQuery(dbContext.Set<T>(), spec);
        }

        public async Task Add(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }

       
    }
}

// when or why dont create productrepo,productbrandrepo,producttype repo ?
//when need create function specifics to such that product only or ... only
