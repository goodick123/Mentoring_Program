using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ORM.DataLayer.DbContexts;
using ORM.DataLayer.Models;

namespace ORM.DataLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly OrmDbContext _dbContext;

        public GenericRepository(OrmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(entry => entry.Id == id);
        }

        public async Task AddAsync(T entry)
        {
            if (entry is null)
            {
                throw new ArgumentNullException($"Argument with type {typeof(T).Name} null");
            }

            await _dbContext.Set<T>().AddAsync(entry);
        }

        public T Update(T entry)
        {
            if (entry is null)
            {
                throw new ArgumentNullException($"Argument with type {typeof(T).Name} null");
            }

            return _dbContext.Set<T>().Update(entry).Entity;
        }

        public void Delete(T entry)
        {
            if (entry is null)
            {
                throw new ArgumentNullException($"Argument with type {typeof(T).Name} null");
            }

            _dbContext.Set<T>().Remove(entry);
        }

        public async Task<List<T>> GetWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<List<T>> GetWithIncludeAndTrackingAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = IncludeWithTracking(includeProperties);
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<List<T>> GetWithIncludeAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            return await Include(includeProperties).ToListAsync();
        }

        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = _dbContext.Set<T>().AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty).AsNoTracking()).AsNoTracking();
        }

        private IQueryable<T> IncludeWithTracking(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public async Task<List<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<List<T>> ExecuteSqlWithReturningModel(string sql, params SqlParameter[] parameters)
        {
            var result = await _dbContext.Set<T>().FromSqlRaw(sql, parameters).ToListAsync();

            return result;
        }

        public async Task ExecuteSql(string sql, params SqlParameter[] parameters)
        {
            await _dbContext.Set<T>().FromSqlRaw(sql, parameters).ToListAsync();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges() ;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
