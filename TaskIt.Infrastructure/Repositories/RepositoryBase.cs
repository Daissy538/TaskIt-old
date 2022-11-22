using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskIt.Domain.RepositoryInterfaces;
using TaskItApi.Models;
namespace TaskItApi.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected TaskItDbContext TaskItDbContext { get; set; }

        public RepositoryBase(TaskItDbContext taskItDbContext)
        {
            TaskItDbContext = taskItDbContext;
        }

        public void Create(T entity)
        {
            TaskItDbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {            
            TaskItDbContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return TaskItDbContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return TaskItDbContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            TaskItDbContext.Set<T>().Update(entity);
        }
    }
}
