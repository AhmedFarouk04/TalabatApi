using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories
{
    public interface IGenericRepository<T> where T: BaseEntity

    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> GetAllWitSpecAsync(ISpecification<T> spec);

        Task<T> GetByIdWitSpecAsync(ISpecification<T> spec);

        Task<int> GetCountWithSpecAsync(ISpecification<T> spec );
         
        Task Add(T entity);
        void Update(T entity);

        void Delete(T entity);
    }
}
