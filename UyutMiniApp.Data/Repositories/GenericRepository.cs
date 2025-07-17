using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UyutMiniApp.Data.Contexts;
using UyutMiniApp.Data.IRepositories;

namespace UyutMiniApp.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly UyutMiniAppDbContext dbContext;
        private readonly DbSet<T> dbSet;

        public GenericRepository(UyutMiniAppDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }

        public async ValueTask<T> CreateAsync(T entity) =>
            (await dbContext.AddAsync(entity)).Entity;

        public async ValueTask<bool> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            var entity = await GetAsync(expression);

            if (entity == null)
                return false;

            dbSet.Remove(entity);

            return true;
        }

        public IQueryable<T> GetAll(bool isTracking = true,
            Expression<Func<T, bool>> expression = null,
            string[] includes = null)
        {
            IQueryable<T> query = expression is null ? dbSet : dbSet.Where(expression);

            if (includes != null)
                foreach (var include in includes)
                    if (!string.IsNullOrEmpty(include))
                        query = query.Include(include);

            if (!isTracking)
                query = query.AsNoTracking();

            return query;
        }

        public async ValueTask<T> GetAsync(Expression<Func<T, bool>> expression, string[] includes = null, bool isTracking = true) =>
            await GetAll(true, expression, includes).FirstOrDefaultAsync();

        public T Update(T entity) =>
            dbSet.Update(entity).Entity;

        public async Task SaveChangesAsync() =>
            await dbContext.SaveChangesAsync();
    }
}
