/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.Core.BaseService {

    /// <summary>
    /// This interface provides generic methods to execute Get/Find/Add/Update/Delete query agianst respective entity type <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of domain entity.</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity {
    // List<V> GetQueryEntityList1<V>(string sql, object[] parameters = null) where V : BaseDTONew;

    ///// <summary>
    ///// Gets the entity list.
    ///// </summary>
    ///// <returns>Returns list of TEntity.</returns>
    //List<TEntity> GetEntityList();

    ///// <summary>
    ///// Gets the entity list order by passed expression.
    ///// </summary>
    ///// <param name="orderBy">The order by expression to order entity list.</param>
    ///// <returns>Returns entity list order by passed expression.</returns>
    //List<TEntity> GetEntityList(Expression<Func<TEntity, object>> orderBy);

    ///// <summary>
    ///// Gets the entity list based on input parameter.
    ///// </summary>
    ///// <param name="skipTenantIdFilter">if set to <c>true</c> [skip tenant id filter].</param>
    ///// <param name="skipDeleted">if set to <c>true</c> [skip deleted] entities.</param>
    ///// <param name="orderBy">The order by expression to order entity list.</param>
    ///// <returns>Returns entity list that matched input parameters and order by passed expression.</returns>
    //List<TEntity> GetEntityList(bool skipTenantIdFilter, bool skipDeleted, Expression<Func<TEntity, object>> orderBy);


    //Task<List<TEntity>> GetEntityListAsync(CancellationToken token = default(CancellationToken));
    //Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, object>> orderBy, CancellationToken token = default(CancellationToken));
    //Task<List<TEntity>> GetEntityListAsync(bool skipTenantIdFilter, bool skipDeleted, Expression<Func<TEntity, object>> orderBy, CancellationToken token = default(CancellationToken));
 
    List<TEntity> GetEntityList (string sql, object[] parameters);
    Task<List<TEntity>> GetEntityListAsync(string sql, object[] parameters, CancellationToken token = default(CancellationToken));
    List<V> GetQueryEntityList<V>(string sql, object[] parameters) where V : class;
    Task<List<V>> GetQueryEntityListAsync<V>(string sql, object[] parameters, CancellationToken token = default(CancellationToken)) where V : class;

    ///// <summary>
    ///// Create a executable raw SQL query to get all records of <typeparamref name="TEntity" />.
    ///// </summary>
    ///// <returns>
    ///// Returns An System.Linq.IQueryable`1 representing the raw SQL query.
    ///// </returns>
    //IQueryable<TEntity> GetAll();


    ///// <summary>Get all entities ordered by the given expression. No sorting if the OrderBy expression is null.
    ///// Optionally deleted entities are skipped.</summary>
    ///// <param name="orderBy">Sort order expression</param>
    ///// <param name="skipDeleted">Whether to skip deleted entities</param>
    ///// <param name="token">For cancellation</param>
    ///// <returns>Collection of entities</returns>
    //Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, object>> orderBy, bool skipDeleted = false, CancellationToken token = default(CancellationToken));

    ///// <summary>
    ///// An entity type may have a default ordering expression. For such type, get all entities ordered by the default expression. 
    ///// This method is implemented and the default expression is applied. Here it returns unordered values.
    ///// It skips deleted entities.
    ///// </summary>
    ///// <param name="token">For cancellation</param>
    ///// <returns>Collection of entities</returns>
    //Task<ICollection<TEntity>> GetAllAsync(CancellationToken token = default(CancellationToken));

    ///// <summary>
    ///// Creates a System.Collections.Generic.List`1 by executing input raw SQL query and parameters.
    ///// </summary>
    ///// <param name="sql">The raw SQL query to be execute to get required System.Collections.Generic.List`1.</param>
    ///// <param name="parameters">The values to be assigned to parameters in input raw sql.</param>
    ///// <returns>Returns System.Collections.Generic.List`1 that mapped with raw sql execution result.</returns>
    //List<TEntity> GetEntityList(string sql, object[] parameters = null);

    ///// <summary>
    ///// Given the SQL query string, get the list of entities.
    ///// The result of the query is materialized into the entity list.
    ///// The result entity type is the same as the subclasses entity.
    ///// </summary>
    ///// <param name="sql">SQL query string</param>
    ///// <param name="parameters">SQL query parameters, if any</param>
    ///// <param name="token"></param>
    ///// <returns>Resulting entity list</returns>
    //Task<List<TEntity>> GetEntityListAsync(string sql, object[] parameters = null, CancellationToken token = default(CancellationToken));

    ///// <summary>
    ///// Given the SQL query string, get the list of entities.
    ///// The result of the query is materialized into the entity list.
    ///// The result entity type can be any type (V).
    ///// </summary>
    ///// <param name="sql">SQL query string</param>
    ///// <param name="parameters">SQL query parameters, if any</param>
    ///// <param name="token"></param>
    ///// <returns>Resulting entity list</returns>
    //Task<List<V>> GetEntityListAsync<V>(string sql, object[] parameters = null, CancellationToken token = default(CancellationToken)) where V : BaseEntity;

    ///// <summary>
    ///// Creates a System.Collections.Generic.List`1 by executing input raw SQL query and parameters.
    ///// </summary>
    ///// <typeparam name="V">The type parameter (other than domain entity) to be map with raw sql result.</typeparam>
    ///// <param name="sql">The raw SQL query to be execute to get required System.Collections.Generic.List`1.</param>
    ///// <param name="parameters">The values to be assigned to parameters in input raw sql.</param>
    ///// <returns>Returns System.Collections.Generic.List`1 that mapped with raw sql execution result.</returns>
    //List<V> GetQueryEntityList<V>(string sql, object[] parameters = null) where V : BaseEntity;

    ///// <summary>
    ///// Asynchronously creates a System.Collections.Generic.List`1 by executing input raw SQL query and parameters.
    ///// </summary>
    ///// <typeparam name="V">The type parameter (other than domain entity) to be map with raw sql result.</typeparam>
    ///// <param name="sql">The raw SQL query to be execute to get required System.Collections.Generic.List`1.</param>
    ///// <param name="parameters">The values to be assigned to parameters in input raw sql.</param>
    ///// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
    ///// <returns>Returns a task that represents the asynchronous operation. The task result contains a System.Collections.Generic.List`1.</returns>
    //Task<List<V>> GetQueryEntityListAsync<V>(string sql, object[] parameters = null, CancellationToken token = default(CancellationToken)) where V : class;

    /// <summary>
    /// Asynchronously creates a entity (of type V) by executing input raw SQL query and parameters.
    /// </summary>
    /// <typeparam name="V">The type parameter (other than domain entity) to be map with raw sql result.</typeparam>
    /// <param name="sql">The raw SQL query to be execute to get required System.Collections.Generic.List`1.</param>
    /// <param name="parameters">The values to be assigned to parameters in input raw sql.</param>
    /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
    /// <returns>Returns a task that represents the asynchronous operation. The task result contains a entity or null if no record found.</returns>
    Task<V> GetQueryEntityAsync<V>(string sql, object[] parameters = null, CancellationToken token = default(CancellationToken)) where V : class;

    /// <summary>
    /// Gets the count of all records of type <typeparamref name="TEntity"/>.
    /// </summary>
    /// <returns>Returns all record count of type <typeparamref name="TEntity"/>.</returns>
    int CountAll();

    /// <summary>
    /// Count all entities in the database. Skip deleted items in the count.
    /// </summary>
    /// <param name="token">For cancellation</param>
    /// <returns>Number of entities in the database</returns>
    Task<int> CountAllAsync(CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Gets TEntity that matches the specified identifier.
    /// </summary>
    /// <param name="id">The identifier value to find TEntity.</param>
    /// <returns>Return TEntity that matches the specified identifier.</returns>
    TEntity Get(Guid id);

    /// <summary>
    /// Get the entity given its primarky key, Guid id.
    /// </summary>
    /// <param name="id">Primary key</param>
    /// <param name="token">For cancellation</param>
    /// <returns>Selected entity</returns>
    Task<TEntity> GetAsync(Guid id, CancellationToken token = default(CancellationToken));

    TEntity GetEntity(string sql, object[] parameters = null);

    /// <summary>
    /// Given the SQL query string, get the entity, which is 
    /// is materialized from the query result
    /// The result entity type is the same as the subclasses entity.
    /// </summary>
    /// <param name="sql">SQL query string</param>
    /// <param name="parameters">SQL query parameters, if any</param>
    /// <param name="token"></param>
    /// <returns>Resulting entity</returns>
    Task<TEntity> GetEntityAsync(string sql, object[] parameters = null, CancellationToken token = default(CancellationToken));

    V GetQueryEntity<V>(string sql, object[] parameters = null) where V : class;


    TEntity Find(Expression<Func<TEntity, bool>> match);

    /// <summary>
    /// Find the entity that matches the given expression. Deleted entities are skipped. 
    /// If there are more than one matches, raise an exception.
    /// </summary>
    /// <param name="match">Expression to match</param>
    /// <param name="token">For cancellation</param>
    /// <returns>Matched entity</returns>
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Find the entity that matches the given expression. Skip deleted entities optionally. 
    /// If there are more than one matches, raise an exception.
    /// </summary>
    /// <param name="match">Expression to match</param>
    /// <param name="skipDeleted">Whether to skip deleted entities</param>
    /// <param name="token">For cancellation</param>
    /// <returns>Matched entity</returns>
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match, bool skipDeleted = true, CancellationToken token = default(CancellationToken));

    ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match);

    /// <summary>
    /// Find all entities that match the given expression. Deleted entities are skipped.
    /// </summary>
    /// <param name="match">Expression to match</param>
    /// <param name="token">For cancellation</param>
    /// <returns>Matched entities</returns>
    Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Find all entities that match the given expression, optionally skipping deleted entities.
    /// </summary>
    /// <param name="match">Expression to match</param>
    /// <param name="skipDeleted">Whether to skip deleted entities</param>
    /// <param name="token">For cancellation</param>
    /// <returns>Matched entities</returns>
    Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match, bool skipDeleted = true, CancellationToken token = default(CancellationToken));

    TEntity Add(TEntity t);

    /// <summary>
    /// Add a new entity. It is added to the entity set but not saved.
    /// </summary>
    /// <param name="t">Entity to add</param>
    /// <returns>Added entity</returns>
    Task<TEntity> AddAsync(TEntity t, CancellationToken token = default(CancellationToken));

    TEntity Update(TEntity t, object key);

    /// <summary>
    /// Update an existing entity. It first gets the EF tracked entity copy, and 
    /// then updates its property values. It does not save the entity.
    /// </summary>
    /// <param name="t">Entity to update</param>
    /// <param name="key">Entity primary key value</param>
    /// <param name="token">For cancellation</param>
    /// <returns>Updated entity</returns>
    Task<TEntity> UpdateAsync(TEntity t, object key, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Delete an entity. Notethat this not an async method.
    /// </summary>
    /// <param name="entity">Entity to delete</param>
    void Delete(TEntity entity);

    /// <summary>
    /// Delete an entity.
    /// </summary>
    /// <param name="id">Entity Id to delete</param>
    /// <param name="token">For cancellation</param>
    /// <returns></returns>
    Task DeleteAsync(Guid id, CancellationToken token = default(CancellationToken));

    void Save();

    /// <summary>
    /// Save all pending chnges
    /// </summary>
    /// <param name="token">For cancellation</param>
    /// <returns>Number of state changes made to the database</returns>
    Task<int> SaveAsync(CancellationToken token = default(CancellationToken));


    /// <summary>
    /// Updates system fields (ID, CreatedBy, CreatedOn, UpdatedBy and UpdatedOn) of all changed entities by accessing operations state (like Add, Modified etc.) tracked by current database context.
    /// </summary>
    /// <remarks>This method update system fields according to current state tracked by DbContext.</remarks>
    void UpdateEntitySystemFields();

    /// <summary>
    /// Updates all system field(s) (of entity) that are masked in provided includeOptions parameter.
    /// </summary>
    /// <param name="entity">The entity to update system field(s).</param>
    /// <param name="includeOptions">The mask value of SystemFieldMask enum that get updated.</param>
    void UpdateSystemFields(TEntity entity, SystemFieldMask includeOptions);

    /// <summary>
    /// Updates the system fields of entity as per provided operation type.
    /// </summary>
    /// <param name="entity">The entity to be update as per provided operation type.</param>
    /// <param name="opType">Type of the operation to get corresponding system fields to be update.</param>
    void UpdateSystemFieldMask(TEntity entity, OperationType opType);

    ///// <summary>
    ///// Releases unmanaged and - optionally - managed resources.
    ///// </summary>
    //void Dispose();

    bool PropertyChanged(Guid id, string propertyName);

    PropType GetOrignalValue<PropType>(Guid id, string propertyName);
  }
}
