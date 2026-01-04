using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork:IUnitOfWork
    {

        private readonly StoreContext context;
        private Hashtable _repositories;

       

        public UnitOfWork(StoreContext context)
        {
            this.context = context;
            _repositories=new Hashtable();
        }
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name; // Product, Order, etc.

            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(context);
                _repositories.Add(type, repository);
            }

            return _repositories[type] as IGenericRepository<TEntity>;
        }


        public async Task<int> Complete()
    => await context.SaveChangesAsync();

        public async ValueTask DisposeAsync()
    => await context.DisposeAsync();



    }
}
