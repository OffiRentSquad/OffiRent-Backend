using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OffiRent.Domain.Repository
{
    /* Extract of ABP library */
    
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Used to get a IQueryable that is used to retrieve entities from entire table.
        /// </summary>
        /// <returns>IQueryable to be used to select entities from database</returns>
        IQueryable<T> GetAll();
        
        /// <summary>Gets an entity with given primary key.</summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity</returns>
        T Get(int id);

        /// <summary>Gets an entity with given primary key.</summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity</returns>
        Task<T> GetAsync(int id);
        
        /// <summary>
        /// Gets exactly one entity with given predicate.
        /// Throws exception if no entity or more than one entity.
        /// </summary>
        /// <param name="predicate">Entity</param>
        T Single(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets exactly one entity with given predicate.
        /// Throws exception if no entity or more than one entity.
        /// </summary>
        /// <param name="predicate">Entity</param>
        Task<T> SingleAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets an entity with given given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities</param>
        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets an entity with given given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities</param>
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        
        /// <summary>Inserts a new entity.</summary>
        /// <param name="entity">Inserted entity</param>
        T Insert(T entity);

        Task SimpleInsertAsync(T entity);

        /// <summary>Inserts a new entity.</summary>
        /// <param name="entity">Inserted entity</param>
        Task<T> InsertAsync(T entity);

        /// <summary>
        /// Inserts or updates given entity depending on Id's value.
        /// </summary>
        /// <param name="entity">Entity</param>
        Task<T> InsertOrUpdateAsync(T entity);

        /// <summary>Updates an existing entity.</summary>
        /// <param name="entity">Entity</param>
        Task<T> UpdateAsync(T entity);

        /// <summary>Deletes an entity.</summary>
        /// <param name="entity">Entity to be deleted</param>
        void Delete(T entity);

        Task DeleteAsync(T entity);
        /// <summary>Deletes an entity by primary key.</summary>
        /// <param name="id">Primary key of the entity</param>
        void Delete(int id);

        /// <summary>Deletes an entity by primary key.</summary>
        /// <param name="id">Primary key of the entity</param>
        Task DeleteAsync(int id);
        
        /// <summary>
        /// Deletes many entities by function.
        /// Notice that: All entities fits to given predicate are retrieved and deleted.
        /// This may cause major performance problems if there are too many entities with
        /// given predicate.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        void Delete(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Deletes many entities by function.
        /// Notice that: All entities fits to given predicate are retrieved and deleted.
        /// This may cause major performance problems if there are too many entities with
        /// given predicate.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        Task DeleteAsync(Expression<Func<T, bool>> predicate);
        
        /// <summary>Gets count of all entities in this repository.</summary>
        /// <returns>Count of entities</returns>
        int Count();

        /// <summary>Gets count of all entities in this repository.</summary>
        /// <returns>Count of entities</returns>
        Task<int> CountAsync();

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate" />.
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        int Count(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate" />.
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        Task<bool> Contains(T entity);
    }
}