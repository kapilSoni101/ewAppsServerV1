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

        public abstract class BaseDS<TEntity>:IBaseDS<TEntity> where TEntity : BaseEntity {

        #region Local Members

        protected IBaseRepository<TEntity> _data;

        #endregion Local Members

        #region Constructor

        /// <inheritdoc />  
        public BaseDS(IBaseRepository<TEntity> context) {
            _data = context;
        }

        #endregion

        #region Get All

        ///// <inheritdoc />  
        //public IQueryable<TEntity> GetAll() {
        //    return _data.GetAll();
        //}

        ///// <inheritdoc />  
        //public virtual async Task<ICollection<TEntity>> GetAllAsync(CancellationToken token = default(CancellationToken)) {
        //    return await _data.GetAllAsync();
        //}

        #endregion Get All

        #region Count

        /// <inheritdoc />  
        public int CountAll() {
            return _data.CountAll();
        }

        /// <inheritdoc />  
        public async Task<int> CountAllAsync(CancellationToken token = default(CancellationToken)) {
            return await _data.CountAllAsync();
        }

        #endregion Count

        #region Get

        /// <inheritdoc />  
        public virtual TEntity Get(Guid id) {
            return _data.Get(id);
        }

        /// <inheritdoc />  
        public virtual async Task<TEntity> GetAsync(Guid id, CancellationToken token = default(CancellationToken)) {
            return await _data.GetAsync(id);
        }

        #endregion Get

        #region Find

        /// <inheritdoc />  
        public virtual TEntity Find(Expression<Func<TEntity, bool>> match) {
            return _data.Find(match);
        }

        /// <inheritdoc />  
        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match, CancellationToken token = default(CancellationToken)) {
            return await _data.FindAsync(match, token);
        }

        /// <inheritdoc />  
        public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match) {
            return _data.FindAll(match);
        }

        /// <inheritdoc />  
        public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match, CancellationToken token = default(CancellationToken)) {
            return await _data.FindAllAsync(match, token);
        }

        #endregion Find

        #region Add

        /// <inheritdoc />  
        public virtual TEntity Add(TEntity entity) {
            return _data.Add(entity);
        }

        /// <inheritdoc />  
        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken token = default(CancellationToken)) {
            await _data.AddAsync(entity);
            return entity;
        }

        #endregion  Add

        #region Update

        /// <inheritdoc />  
        public virtual TEntity Update(TEntity entity, object key) {
            TEntity updatedEntity = _data.Update(entity, key);
            //UpdateInCache<Guid>(updatedEntity.ID, updatedEntity);
            return updatedEntity;
        }

        /// <inheritdoc />  
        public virtual async Task<TEntity> UpdateAsync(TEntity entity, object key, CancellationToken token = default(CancellationToken)) {
            return await _data.UpdateAsync(entity, key);
        }

        #endregion Update

        #region Delete

        /// <inheritdoc />  
        public virtual void Delete(TEntity entity) {
            //RemoveFromCache<Guid>(entity.ID);
            _data.Delete(entity);
        }

        /// <inheritdoc />  
        public virtual async Task DeleteAsync(Guid id, CancellationToken token = default(CancellationToken)) {
            await _data.DeleteAsync(id, token);
        }

        #endregion Delete

        #region Save

        /// <inheritdoc />  
        public virtual void Save() {
            _data.Save();
        }

        /// <inheritdoc />  
        public async virtual Task<int> SaveAsync(CancellationToken token = default(CancellationToken)) {
            return await _data.SaveAsync();
        }

        #endregion Save

        #region System Field

        /// <inheritdoc />  
        public void UpdateEntitySystemFields() {
            _data.UpdateEntitySystemFields();
        }

        /// <inheritdoc />  
        public void UpdateSystemFields(TEntity entity, SystemFieldMask includeOptions = SystemFieldMask.None) {
            _data.UpdateSystemFields(entity, includeOptions);
        }

        /// <inheritdoc />  
        public void UpdateSystemFieldsByOpType(TEntity entity, OperationType opType) {
            _data.UpdateSystemFieldMask(entity, opType);
        }

        /// <inheritdoc />  
        public void UpdateSystemFields() {
            throw new NotImplementedException();
        }

        #endregion System Field

        public bool PropertyChanged(Guid id, string propertyName) {
            return _data.PropertyChanged(id, propertyName);
        }

        public PropType GetOrignalValue<PropType>(Guid id, string propertyName) {
            return _data.GetOrignalValue<PropType>(id, propertyName);
        }

        #region Dispose

        ///// <inheritdoc /> 
        //public void Dispose() {
        //  Dispose(true);
        //  GC.SuppressFinalize(this);
        //}

        //private bool disposed = false;
        //protected virtual void Dispose(bool disposing) {
        //  if (!this.disposed) {
        //    if (disposing) {
        //      _data.Dispose();
        //    }
        //    this.disposed = true;
        //  }
        //}

        #endregion Dispose
    }
}
