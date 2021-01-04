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
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ewApps.Core.BaseService {

    /// <summary>
    /// This is the base Repository class for all entities. It provides basic, common 
    /// database operations.
    /// </summary>
    /// <typeparam name="TEntity">The type of the target entity.</typeparam>
    /// <typeparam name="U">The type of DbContext.</typeparam>
    /// <seealso cref="ewApps.Core.Data.IBaseRepository{TEntity}" />
    public class BaseRepository<TEntity, U>:IBaseRepository<TEntity> where TEntity : BaseEntity where U : DbContext {

        #region Local Members

        /// <summary>
        /// Instance of DbContext to execute sql query.
        /// </summary>
        protected U _context;

        /// <summary>
        /// Instance of user session manager.
        /// </summary>
        protected IUserSessionManager _sessionManager;

        #endregion Local Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{TEntity, U}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="sessionManager">The session manager.</param>
        public BaseRepository(U context, IUserSessionManager sessionManager) {
            _context = context;
            _sessionManager = sessionManager;
        }

        #endregion Constructor

        #region New Methods

        ///// <summary>
        ///// Gets the all non deleted entites (of type TEntity) of logged-in tenant list order by updated date.
        ///// </summary>
        ///// <returns>
        ///// Returns list of TEntity.
        ///// </returns>
        //public virtual List<TEntity> GetEntityList() {
        //    return GetEntityList(false, true, ((TEntity e) => e.UpdatedOn));
        //}

        ///// <summary>
        ///// Gets the all non deleted entites (of type TEntity) of logged-in tenant list order by passed expression.
        ///// </summary>
        ///// <param name="orderBy">The order by expression to order entity list.</param>
        ///// <returns>
        ///// Returns entity list order by passed expression.
        ///// </returns>
        ///// <remarks>IF orderBy expression is null entity list is default sorted.</remarks>
        //public virtual List<TEntity> GetEntityList(Expression<Func<TEntity, object>> orderBy) {
        //    return GetEntityList(false, true, orderBy);
        //}

        ///// <summary>
        ///// Gets the entity list based on input parameter.
        ///// </summary>
        ///// <param name="skipTenantIdFilter">if set to <c>true</c> [skip tenant id filter].</param>
        ///// <param name="skipDeleted">if set to <c>true</c> [skip deleted] entity filter.</param>
        ///// <param name="orderBy">The order by expression to order entity list.</param>
        ///// <returns>
        ///// Returns entity list that matched input parameters and order by passed expression.
        ///// </returns>
        ///// <exception cref="EwpSystemException">Session is not initialized</exception>
        //public virtual List<TEntity> GetEntityList(bool skipTenantIdFilter, bool skipDeleted, Expression<Func<TEntity, object>> orderBy) {
        //    IQueryable<TEntity> querable = _context.Set<TEntity>();

        //    // If tenant id filter is requested and session manager instance is not available raise exception.
        //    if(skipTenantIdFilter && _sessionManager != null) {
        //        throw new EwpSystemException("Session is not initialized");
        //    }

        //    // If tenant id filter is requested apply tenant id filter by login tenant id.
        //    if(skipTenantIdFilter == false) {
        //        querable = querable.Where(e => e.TenantId == _sessionManager.GetSession().TenantId);
        //    }

        //    // If requested apply deleted record filter.
        //    if(skipDeleted) {
        //        querable = querable.Where(e => e.Deleted == false);
        //    }

        //    // Sort entity collectoin by input expression.
        //    if(orderBy != null) {
        //        querable = querable.OrderBy(orderBy);
        //    }

        //    return querable.ToList();
        //}


        ///// <summary>
        ///// Asynchronously gets the all non deleted entites (of type TEntity) of logged-in tenant list order by updated date.
        ///// </summary>
        ///// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        ///// <returns>
        ///// Returns list of TEntity.
        ///// </returns>
        //public async Task<List<TEntity>> GetEntityListAsync(CancellationToken token = default(CancellationToken)) {
        //    List<TEntity> entityList = await GetEntityListAsync(false, true, ((TEntity e) => e.UpdatedOn), token);
        //    return entityList;
        //}

        ///// <summary>
        ///// Asynchronously gets the all non deleted entites (of type TEntity) of logged-in tenant list order by passed expression.
        ///// </summary>
        ///// <param name="orderBy">The order by expression to order entity list.</param>
        ///// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        ///// <returns>
        ///// Returns entity list order by passed expression.
        ///// </returns>
        ///// <remarks>IF order by expression is null entity list is default sorted.</remarks>
        //public async Task<List<TEntity>> GetEntityListAsync(Expression<Func<TEntity, object>> orderBy, CancellationToken token = default(CancellationToken)) {
        //    List<TEntity> entityList = await GetEntityListAsync(false, true, orderBy, token);
        //    return entityList;
        //}

        ///// <summary>
        ///// Asynchronously gets the entity (of type TEntity) list based on input parameter.
        ///// </summary>
        ///// <param name="skipTenantIdFilter">if set to <c>true</c> [skip tenant identifier filter].</param>
        ///// <param name="skipDeleted">if set to <c>true</c> [skip deleted].</param>
        ///// <param name="orderBy">The order by expression to order entity list.</param>
        ///// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        ///// <returns>
        ///// Returns entity (of type TEntity)  list order by passed expression.
        ///// </returns>
        ///// <exception cref="EwpSystemException">Session is not initialized</exception>
        //public async Task<List<TEntity>> GetEntityListAsync(bool skipTenantIdFilter, bool skipDeleted, Expression<Func<TEntity, object>> orderBy, CancellationToken token = default(CancellationToken)) {
        //    IQueryable<TEntity> querable = _context.Set<TEntity>();

        //    if(skipTenantIdFilter && _sessionManager != null) {
        //        throw new EwpSystemException("Session is not initialized");
        //    }

        //    if(skipTenantIdFilter == false) {
        //        querable = querable.Where(e => e.TenantId == _sessionManager.GetSession().TenantId);
        //    }

        //    if(skipDeleted) {
        //        querable = querable.Where(e => e.Deleted == false);
        //    }

        //    if(orderBy != null) {
        //        querable = querable.OrderBy(orderBy);
        //    }

        //    return await querable.ToListAsync();
        //}

        /// <summary>
        /// Gets the entity list based on input SQL and parameters.
        /// </summary>
        /// <param name="sql">A valid SQL string that should contains all properties of TEntity entity.</param>
        /// <param name="parameters">The list of sql parameters (if null none of sql parameters applied).</param>
        /// <returns>Returns entity list based on input SQL and parameters.</returns>
        public virtual List<TEntity> GetEntityList(string sql, object[] parameters) {
            IQueryable<TEntity> entityList;

            if(parameters != null && parameters.Length > 0)
                entityList = _context.Set<TEntity>().FromSql(sql, parameters);
            else {
                entityList = _context.Set<TEntity>().FromSql(sql);
            }

            return entityList.ToList();
        }

        /// <summary>
        /// Asynchronously gets the entity list based on input SQL and parameters.
        /// </summary>
        /// <param name="sql">A valid SQL string that should contains all properties of TEntity entity.</param>
        /// <param name="parameters">The list of sql parameters (if null none of sql parameters applied).</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns>Returns entity list based on input SQL and parameters.</returns>
        public virtual async Task<List<TEntity>> GetEntityListAsync(string sql, object[] parameters, CancellationToken token = default(CancellationToken)) {
            IQueryable<TEntity> entityList;

            if(parameters != null && parameters.Length > 0)
                entityList = _context.Query<TEntity>().FromSql(sql, parameters);
            else {
                entityList = _context.Query<TEntity>().FromSql(sql);
            }

            return await entityList.ToListAsync();
        }


        /// <summary>
        /// Gets the entity (of type V) list based on input SQL and parameters.
        /// </summary>
        /// <typeparam name="V">The type of entity of result.</typeparam>
        /// <param name="sql">A valid SQL string that should contains all properties of TEntity entity.</param>
        /// <param name="parameters">The list of sql parameters (if null none of sql parameters applied).</param>
        /// <returns>Returns entity (of type V) list based on input SQL and parameters.</returns>
        public List<V> GetQueryEntityList<V>(string sql, object[] parameters) where V : class {
            IQueryable<V> querable;

            if(parameters != null && parameters.Length > 0) {
                querable = _context.Query<V>().FromSql(sql, parameters);
            }
            else {
                querable = _context.Query<V>().FromSql(sql);
            }

            return querable.ToList();
        }

        /// <summary>
        /// Asynchronously gets the entity (of type V) list based on input SQL and parameters.
        /// </summary>
        /// <typeparam name="V">The type of entity of result.</typeparam>
        /// <param name="sql">A valid SQL string that should contains all properties of TEntity entity.</param>
        /// <param name="parameters">The list of sql parameters (if null none of sql parameters applied).</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns>Returns entity (of type V) list based on input SQL and parameters.</returns>
        public async Task<List<V>> GetQueryEntityListAsync<V>(string sql, object[] parameters, CancellationToken token = default(CancellationToken)) where V : class {
            IQueryable<V> querable;

            if(parameters != null && parameters.Length > 0) {
                querable = _context.Query<V>().FromSql(sql, parameters);
            }
            else {
                querable = _context.Query<V>().FromSql(sql);
            }

            return await querable.ToListAsync();
        }

        #endregion

        #region Get

        /// <inheritdoc />  
        public virtual TEntity Get(Guid id) {

            return _context.Set<TEntity>().Find(id);
        }

        /// <inheritdoc />  
        public virtual async Task<TEntity> GetAsync(Guid id, CancellationToken token = default(CancellationToken)) {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        /// <inheritdoc />  
        public TEntity GetEntity(string sql, object[] parameters = null) {
            if(parameters != null && parameters.Length > 0)
                return _context.Set<TEntity>().FromSql(sql, parameters).FirstOrDefault<TEntity>();
            else {
                return _context.Set<TEntity>().FromSql(sql).FirstOrDefault<TEntity>();
            }

        }

        /// <inheritdoc />  
        public async Task<TEntity> GetEntityAsync(string sql, object[] parameters = null, CancellationToken token = default(CancellationToken)) {
            if(parameters != null && parameters.Length > 0)
                return await _context.Set<TEntity>().FromSql(sql, parameters).FirstOrDefaultAsync<TEntity>();
            else {
                return await _context.Set<TEntity>().FromSql(sql).FirstOrDefaultAsync<TEntity>();
            }
        }

        /// <inheritdoc />  
        public V GetQueryEntity<V>(string sql, object[] parameters = null) where V : class {
            if(parameters != null && parameters.Length > 0)
                return _context.Query<V>().FromSql(sql, parameters).FirstOrDefault<V>();
            else {
                return _context.Query<V>().FromSql(sql).FirstOrDefault<V>();
            }
        }

        /// <inheritdoc />  
        public async Task<V> GetQueryEntityAsync<V>(string sql, object[] parameters = null, CancellationToken token = default(CancellationToken)) where V : class {
            if(parameters != null && parameters.Length > 0)
                return await _context.Query<V>().FromSql(sql, parameters).FirstOrDefaultAsync<V>();
            else {
                return await _context.Query<V>().FromSql(sql).FirstOrDefaultAsync<V>();
            }
        }

        #endregion Get

        #region Get All

        //// Not Required
        /// <inheritdoc />  
        public IQueryable<TEntity> GetAll() {
            return _context.Set<TEntity>();
        }

        //// Not Required
        /// <inheritdoc />  
        //public virtual async Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, object>> orderBy, bool skipDeleted = false, CancellationToken token = default(CancellationToken)) {
        //  //if(skipDeleted) {
        //  //  if(orderBy == null)
        //  //    return await _context.Set<T>().Where(x => !x.Deleted).ToListAsync();
        //  //  else
        //  //    return await _context.Set<T>().Where(x => !x.Deleted).OrderBy(orderBy).ToListAsync();
        //  //}
        //  //else {
        //  //  if(orderBy == null)
        //  //    return await _context.Set<T>().ToListAsync();
        //  //  else
        //  //    return await _context.Set<T>().OrderBy(orderBy).ToListAsync();
        //  //}

        //  if (orderBy == null)
        //    return await _context.Set<TEntity>().ToListAsync();
        //  else
        //    return await _context.Set<TEntity>().OrderBy(orderBy).ToListAsync();

        //}

        //// Not Required
        ///// <inheritdoc />  
        //public virtual async Task<ICollection<TEntity>> GetAllAsync(CancellationToken token = default(CancellationToken)) {
        //    //return await GetAllAsync(null, true, token);
        //    return await GetEntityListAsync(token);
        //}

        //// Not Required
        ///// <inheritdoc />  
        //public List<TEntity> GetEntityList(string sql, object[] parameters = null) {
        //  if (parameters != null && parameters.Length > 0)
        //    return _context.Query<TEntity>().FromSql(sql, parameters).ToList();
        //  else {
        //    return _context.Query<TEntity>().FromSql(sql).ToList();
        //  }
        //}

        //// Not Required
        ///// <inheritdoc />  
        //public async Task<List<TEntity>> GetEntityListAsync(string sql, object[] parameters = null, CancellationToken token = default(CancellationToken)) {
        //  if (parameters != null && parameters.Length > 0)
        //    return await _context.Query<TEntity>().FromSql(sql, parameters).ToListAsync();
        //  else {
        //    return await _context.Query<TEntity>().FromSql(sql).ToListAsync();
        //  }
        //}

        //// Not Required
        ///// <inheritdoc />  
        //public async Task<List<V>> GetEntityListAsync<V>(string sql, object[] parameters = null, CancellationToken token = default(CancellationToken)) where V : class {
        //  if (parameters != null && parameters.Length > 0)
        //    return await _context.Query<V>().FromSql(sql, parameters).ToListAsync();
        //  else {
        //    return await _context.Query<V>().FromSql(sql).ToListAsync();
        //  }
        //}

        //// Not Required
        ///// <inheritdoc />  
        //public List<V> GetQueryEntityList<V>(string sql, object[] parameters = null) where V : class {
        //  IQueryable<V> querable;

        //  if (parameters != null && parameters.Length > 0) {
        //    querable = _context.Query<V>().FromSql(sql, parameters);
        //  }
        //  else {
        //    querable = _context.Query<V>().FromSql(sql);
        //  }

        //  return querable.ToList();
        //}

        //public List<V> GetQueryEntityList1<V>(string sql, object[] parameters = null) where V : BaseDTONew {
        //  IQueryable<V> querable;

        //  //sql = "Select AppUser.CreatedBy, AppUser.CreatedOn, AppUser.UpdatedBy, AppUser.UpdatedOn, AppUser.TenantId, AppUser.Deleted FROM AppUser INNER JOIN UserAppLinking ON AppUser.TenantId=UserAppLinking.TenantId Where AppUser.Deleted=1";
        //  //// Where TenantId = 'CA30D8E7-E82A-4241-BA61-8E50AEA498D2' AND Deleted = 0";

        //  if (parameters != null && parameters.Length > 0) {
        //    querable = _context.Query<V>().FromSql(sql, parameters);
        //  }
        //  else {
        //    querable = _context.Query<V>().FromSql(sql);
        //  }

        //  // querable = querable.Where(e => e.TenantId == new Guid("CA30D8E7-E82A-4241-BA61-8E50AEA498D2") && e.Deleted==false);
        //  querable = querable.OrderBy(e => e.UpdatedBy);

        //  return querable.ToList();
        //}

        //// Not Required
        ///// <inheritdoc />  
        //public async Task<List<V>> GetQueryEntityListAsync<V>(string sql, object[] parameters = null, CancellationToken token = default(CancellationToken)) where V : class {
        //  if (parameters != null && parameters.Length > 0)
        //    return await _context.Query<V>().FromSql(sql, parameters).ToListAsync();
        //  else {
        //    return await _context.Query<V>().FromSql(sql).ToListAsync();
        //  }
        //}

        #endregion Get All

        #region Count

        /// <inheritdoc />  
        public int CountAll() {
            return _context.Set<TEntity>().Count();
        }

        /// <inheritdoc />  
        public async Task<int> CountAllAsync(CancellationToken token = default(CancellationToken)) {
            //return await _context.Set<TEntity>().Where(x => !x.Deleted).CountAsync();
            return await _context.Set<TEntity>().CountAsync();

        }

        #endregion Count

        #region Find

        /// <inheritdoc />  
        public virtual TEntity Find(Expression<Func<TEntity, bool>> match) {
            return _context.Set<TEntity>().SingleOrDefault(match);
        }

        /// <inheritdoc />  
        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match, CancellationToken token = default(CancellationToken)) {
            return await FindAsync(match, true);
        }

        /// <inheritdoc />  
        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match, bool skipDeleted = true, CancellationToken token = default(CancellationToken)) {
            //if(skipDeleted)
            //  return await _context.Set<T>().Where(x => !x.Deleted).SingleOrDefaultAsync(match);
            //else
            //  return await _context.Set<T>().SingleOrDefaultAsync(match);

            return await _context.Set<TEntity>().SingleOrDefaultAsync(match);
        }

        /// <inheritdoc />  
        public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match) {
            return _context.Set<TEntity>().Where(match).ToList();
        }

        /// <inheritdoc />  
        public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match, CancellationToken token = default(CancellationToken)) {
            return await FindAllAsync(match, true);
        }

        /// <inheritdoc />  
        public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match, bool skipDeleted = true, CancellationToken token = default(CancellationToken)) {
            //if(skipDeleted)
            //  return await _context.Set<TEntity>().Where(match).Where(x => !x.Deleted).ToListAsync();
            //else
            //  return await _context.Set<TEntity>().Where(match).ToListAsync();

            return await _context.Set<TEntity>().Where(match).ToListAsync();
        }

        #endregion Find

        #region Add

        /// <inheritdoc />  
        public virtual TEntity Add(TEntity t) {
            _context.Set<TEntity>().Add(t);
            return t;
        }

        /// <inheritdoc />  
        public virtual async Task<TEntity> AddAsync(TEntity t, CancellationToken token = default(CancellationToken)) {
            await _context.Set<TEntity>().AddAsync(t);
            return t;
        }

        #endregion Add

        #region Update

        /// <inheritdoc />  
        public virtual TEntity Update(TEntity t, object key) {
            if(t == null)
                return null;
            TEntity exist = _context.Set<TEntity>().Find(key);
            if(exist != null) {
                _context.Entry(exist).CurrentValues.SetValues(t);
            }
            return exist;
        }

        /// <inheritdoc />  
        public virtual async Task<TEntity> UpdateAsync(TEntity t, object key, CancellationToken token = default(CancellationToken)) {
            if(t == null)
                return null;
            TEntity exist = await _context.Set<TEntity>().FindAsync(key);
            if(exist != null) {
                _context.Entry(exist).CurrentValues.SetValues(t);
            }
            return exist;
        }

        #endregion Update

        #region Delete

        /// <inheritdoc />  
        public virtual async Task DeleteAsync(Guid id, CancellationToken token = default(CancellationToken)) {
            // Get the entity
            TEntity e = await GetAsync(id);
            // Call Delete
            Delete(e);
        }


        /// <inheritdoc />  
        public virtual void Delete(TEntity entity) {
            _context.Set<TEntity>().Remove(entity);
            //_context.SaveChanges();
        }


        #endregion Delete

        #region Save

        /// <inheritdoc />  
        public virtual void Save() {
            _context.SaveChanges();
        }

        /// <inheritdoc />  
        public async virtual Task<int> SaveAsync(CancellationToken token = default(CancellationToken)) {
            return await _context.SaveChangesAsync();
        }

        #endregion Save

        #region System Field 

        /// <inheritdoc />  
        public void UpdateEntitySystemFields() {
            // Get list of all entities that are either Add or Modified or Deleted from current dbcontext.
            List<EntityEntry> trackedEntityEntryList = _context.ChangeTracker.Entries()
                                                       .Where(trackedEntity => (trackedEntity.State == EntityState.Added ||
                                                       trackedEntity.State == EntityState.Modified || trackedEntity.State == EntityState.Deleted))
                                                       .ToList();

            // Loop on all changed entity list to update system fields as per current entity state.
            for(int i = 0; i < trackedEntityEntryList.Count; i++) {
                // Check whether changed entity implemented ISystemEntityField interface. If yes, make a call to update system fields in entity.
                if(trackedEntityEntryList[i] is ISystemEntityField sysEntityFields) {
                    // If entity state is Added, make a call to update all system fields that are defined (in SystemFieldMask.AddOpSystemFields enum) to be update in any Add operation.
                    if(trackedEntityEntryList[i].State == EntityState.Added) {
                        UpdateSystemFields(trackedEntityEntryList[i].Entity as TEntity, SystemFieldMask.AddOpSystemFields);
                    }
                    // If entity state is Modified, make a call to update all system fields that are defined (in SystemFieldMask.UpdateOpSystemFields enum) to be update in any Add operation.
                    else if(trackedEntityEntryList[i].State == EntityState.Modified) {
                        UpdateSystemFields(trackedEntityEntryList[i].Entity as TEntity, SystemFieldMask.UpdateOpSystemFields);
                    }
                    // If entity state is Deleted, make a call to update all system fields that are defined (in SystemFieldMask.DeleteOpSystemFields enum) to be update in any Add operation.
                    else if(trackedEntityEntryList[i].State == EntityState.Deleted) {
                        UpdateSystemFields(trackedEntityEntryList[i].Entity as TEntity, SystemFieldMask.DeleteOpSystemFields);
                    }
                }
            }
        }

        /// <inheritdoc />  
        public virtual void UpdateSystemFields(TEntity entity, SystemFieldMask includeOptions) {
            // Gets current user session.
            UserSession session = _sessionManager.GetSession();

            // Check whether entity is derived from ISystemEntityField interface.
            if(entity is ISystemEntityField sysEntityFields) {
                // If input masked value contain SystemFieldMask.Id, generate new value for ID system field. 
                if(includeOptions.HasFlag(SystemFieldMask.Id)) {
                    sysEntityFields.ID = Guid.NewGuid();
                }

                // If input masked value contain SystemFieldMask.CreatedBy, updates CreatedBy with current login user from session. 
                if(includeOptions.HasFlag(SystemFieldMask.CreatedBy)) {
                    sysEntityFields.CreatedBy = session.TenantUserId;
                }

                // If input masked value contain SystemFieldMask.CreatedOn, updates CreatedOn with current UTC date and time.
                if(includeOptions.HasFlag(SystemFieldMask.CreatedOn)) {
                    sysEntityFields.CreatedOn = DateTime.UtcNow;
                }

                // If input masked value contain SystemFieldMask.UpdatedBy, updates UpdatedBy with current login user from session. 
                if(includeOptions.HasFlag(SystemFieldMask.UpdatedBy)) {
                    sysEntityFields.UpdatedBy = session.TenantUserId;
                }

                // If input masked value contain SystemFieldMask.UpdatedOn, updates UpdatedOn with current UTC date and time.
                if(includeOptions.HasFlag(SystemFieldMask.UpdatedOn)) {
                    sysEntityFields.UpdatedOn = DateTime.UtcNow;
                }

                // If input masked value contain SystemFieldMask.UpdatedOn, updates UpdatedOn with current UTC date and time.
                if(includeOptions.HasFlag(SystemFieldMask.TenantId)) {
                    sysEntityFields.TenantId = _sessionManager.GetSession().TenantId;
                }
            }
        }

        /// <inheritdoc />  
        public virtual void UpdateSystemFieldMask(TEntity entity, OperationType opType) {

            // Check whether entity is derived from ISystemEntityField interface.
            if(entity is ISystemEntityField) {
                // Gets system field mask value that are defined corresponding to input operation type.
                SystemFieldMask systemFieldBitMaskValue = GenerateSystemFieldMaskValue(opType);

                // Updates all system fields (masked in systemFieldBitMaskValue) of entity. 
                UpdateSystemFields(entity, systemFieldBitMaskValue);
            }
        }

        // This method returns system field mask value defined as updatable for passed operation type.
        private SystemFieldMask GenerateSystemFieldMaskValue(OperationType opType) {
            SystemFieldMask systemFieldBitMaskValue = SystemFieldMask.None;
            switch(opType) {
                case OperationType.Add:
                    systemFieldBitMaskValue = SystemFieldMask.AddOpSystemFields;
                    break;
                case OperationType.Update:
                    systemFieldBitMaskValue = SystemFieldMask.UpdateOpSystemFields;
                    break;
                case OperationType.Delete:
                    systemFieldBitMaskValue = SystemFieldMask.DeleteOpSystemFields;
                    break;
                //default:
                //    throw new EwpInvalidOperationException("Invalid operation type.");
            }

            return systemFieldBitMaskValue;
        }

        #endregion System Filed

        #region Dispose

        //private bool disposed = false;

        ///// <summary>
        ///// Standard Dispose method
        ///// </summary>
        ///// <param name="disposing">Whether call from user code directly or indirectly</param>
        //protected virtual void Dispose(bool disposing) {
        //  if(!this.disposed) {
        //    if(disposing) {
        //      _context.Dispose();
        //    }
        //    this.disposed = true;
        //  }
        //}

        ///// <summary>
        ///// Implements IDisposable.
        ///// Do not make this method virtual.
        ///// A derived class should not be able to override this method.
        ///// </summary>
        //public void Dispose() {
        //  Dispose(true);
        //  GC.SuppressFinalize(this);
        //}



        #endregion Dispose

        public bool PropertyChanged(Guid id, string propertyName) {
            EntityEntry entityEntry = _context.ChangeTracker.Entries().FirstOrDefault(i => i.Entity.GetType().Name == typeof(TEntity).Name && Guid.Parse(i.OriginalValues["ID"].ToString()).Equals(id));

            if(entityEntry != null) {
                return entityEntry.OriginalValues[propertyName] != entityEntry.CurrentValues[propertyName];
            }
            else {
                return false;
            }
        }

        public PropType GetOrignalValue<PropType>(Guid id, string propertyName) {
            EntityEntry entityEntry = _context.ChangeTracker.Entries().FirstOrDefault(i => i.Entity.GetType().Name == typeof(TEntity).Name && Guid.Parse(i.OriginalValues["ID"].ToString()).Equals(id));

            if(entityEntry != null) {
                return (PropType)entityEntry.OriginalValues[propertyName];
            }
            else {
                return default(PropType);
            }
        }

    }
}
