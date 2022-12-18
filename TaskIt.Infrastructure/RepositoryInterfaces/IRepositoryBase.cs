using System;
using System.Linq;
using System.Linq.Expressions;

namespace TaskIt.Domain.RepositoryInterfaces
{
    public interface IRepositoryBase<T>
    {
        /// <summary>
        /// Retrieve all objects
        /// </summary>
        IQueryable<T> FindAll();
        /// <summary>
        /// Retrieve all ibjects by condition
        /// </summary>
        /// <param name="expression">Given link expression</param>
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        /// <summary>
        /// Create a database object of entity
        /// </summary>
        void Create(T entity);
        /// <summary>
        /// Update a database object of entity
        /// </summary>
        void Update(T entity);
        /// <summary>
        /// Delete a database object of entity
        /// </summary>
        void Delete(T entity);
    }
}
