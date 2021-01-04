/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar<abadgujar@batchmaster>
 * Date: 22 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 22 August 2019
 */


using ewApps.Core.BaseService;

namespace ewApps.Core.DMService {

    public class DMFileStorageDS : BaseDS<DMFileStorage>, IDMFileStorageDS {

    #region Local Member  

    IDMFileStorageRepository _dmFileStorageRepository;

    #endregion Local Member

    #region Constructor

    /// <summary>
    /// Initialinzing local variables
    /// </summary>
    /// <param name="dmDocumentRepository"></param>
    /// <param name="cacheService"></param>    
    public DMFileStorageDS(IDMFileStorageRepository dmFileStorageRepository) : base(dmFileStorageRepository) {
      _dmFileStorageRepository = dmFileStorageRepository;
    }

    #endregion Constructor

    //public FileStorage Add(FileStorage t) {
    //  // return Add(t);
    //  throw new NotImplementedException();
    //}

    //public Task<FileStorage> AddAsync(FileStorage t, CancellationToken token = default) {
    //  throw new NotImplementedException();
    //}

    //public int CountAll() {
    //  throw new NotImplementedException();
    //}

    //public Task<int> CountAllAsync(CancellationToken token = default) {
    //  throw new NotImplementedException();
    //}

    //public void Delete(FileStorage entity) {
    //  throw new NotImplementedException();
    //}

    //public Task DeleteAsync(Guid id, CancellationToken token = default) {
    //  throw new NotImplementedException();
    //}

    //public FileStorage Find(Expression<Func<FileStorage, bool>> match) {
    //  throw new NotImplementedException();
    //}

    //public ICollection<FileStorage> FindAll(Expression<Func<FileStorage, bool>> match) {
    //  throw new NotImplementedException();
    //}

    //public Task<ICollection<FileStorage>> FindAllAsync(Expression<Func<FileStorage, bool>> match, CancellationToken token = default) {
    //  throw new NotImplementedException();
    //}

    //public Task<FileStorage> FindAsync(Expression<Func<FileStorage, bool>> match, CancellationToken token = default) {
    //  throw new NotImplementedException();
    //}

    //public FileStorage Get(Guid id) {
    //  throw new NotImplementedException();
    //}

    //public IQueryable<FileStorage> GetAll() {
    //  throw new NotImplementedException();
    //}

    //public Task<ICollection<FileStorage>> GetAllAsync(CancellationToken token = default) {
    //  throw new NotImplementedException();
    //}

    //public Task<FileStorage> GetAsync(Guid id, CancellationToken token = default) {
    //  throw new NotImplementedException();
    //}

    //public PropType GetOrignalValue<PropType>(Guid id, string propertyName) {
    //  throw new NotImplementedException();
    //}

    //public bool PropertyChanged(Guid id, string propertyName) {
    //  throw new NotImplementedException();
    //}

    //public void Save() {
    //  throw new NotImplementedException();
    //}

    //public Task<int> SaveAsync(CancellationToken token = default) {
    //  throw new NotImplementedException();
    //}

    //public FileStorage Update(FileStorage t, object key) {
    //  throw new NotImplementedException();
    //}

    //public Task<FileStorage> UpdateAsync(FileStorage t, object key, CancellationToken token = default) {
    //  throw new NotImplementedException();
    //}

    //public void UpdateEntitySystemFields() {
    //  throw new NotImplementedException();
    //}

    //public void UpdateSystemFields(FileStorage entity, SystemFieldMask excludeOptions = SystemFieldMask.None) {
    //  throw new NotImplementedException();
    //}

    //public void UpdateSystemFieldsByOpType(FileStorage entity, OperationType opType) {
    //  throw new NotImplementedException();
    //}
  }
}
