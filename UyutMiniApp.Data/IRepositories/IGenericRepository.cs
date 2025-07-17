using System.Linq.Expressions;

namespace UyutMiniApp.Data.IRepositories
{
    public interface IGenericRepository<T>
    {
        ValueTask<T> CreateAsync(T entity);
        T Update(T entity);
        ValueTask<bool> DeleteAsync(Expression<Func<T, bool>> expression);
        ValueTask<T> GetAsync(Expression<Func<T, bool>> expression, string[] includes = null, bool isTracking = true);
        IQueryable<T> GetAll(bool isTracking = true,Expression < Func<T, bool>> expression = null, string[] includes = null);
        Task SaveChangesAsync();
    }
}