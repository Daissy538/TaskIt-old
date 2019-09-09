using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using TaskItApi.Models;
using TaskItApi.Repositories.Interfaces;

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
            this.TaskItDbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {            
            this.TaskItDbContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return this.TaskItDbContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.TaskItDbContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            this.TaskItDbContext.Set<T>().Update(entity);
        }
    }
}
