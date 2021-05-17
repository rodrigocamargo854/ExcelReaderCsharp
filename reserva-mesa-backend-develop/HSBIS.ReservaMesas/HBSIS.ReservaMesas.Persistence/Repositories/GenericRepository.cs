using HBSIS.ReservaMesas.Domain.Entities;
using HBSIS.ReservaMesas.Domain.Interfaces.Repositories;
using HBSIS.ReservaMesas.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HBSIS.ReservaMesas.Persistence.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly MainContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        protected GenericRepository(MainContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public virtual async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task<T> GetById(int id)
        {
            return await Query().SingleOrDefaultAsync(entity => entity.Id == id);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task Save() => await _dbContext.SaveChangesAsync();

        protected IQueryable<T> Query() => _dbSet.AsNoTracking().Where(entity => !entity.Deleted);
    }
}
