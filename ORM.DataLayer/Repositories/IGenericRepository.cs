using System.Linq.Expressions;
using Microsoft.Data.SqlClient;

namespace ORM.DataLayer.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<T> GetByIdAsync(int id);

        Task<List<T>> GetAllAsync();

        Task AddAsync(T entry);

        T Update(T entry);

        void Delete(T entry);

        void SaveChanges();

        Task<List<T>> Find(Expression<Func<T, bool>> predicate);

        Task<List<T>> GetWithIncludeAsync(params Expression<Func<T, object>>[] includeProperties);

        Task<List<T>> GetWithIncludeAndTrackingAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties);

        Task<List<T>> GetWithIncludeAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties);

        Task<List<T>> ExecuteSqlWithReturningModel(string sql, params SqlParameter[] parameters);

        Task ExecuteSql(string sql, params SqlParameter[] parameters);

        Task SaveChangesAsync();
    }
}
