using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.BusinessEntity.DS {

	/// <summary>
    /// This class implements standard business logic and operations for BAVendorContact entity.
    /// </summary>
    public class BAVendorContactDS: BaseDS<BAVendorContact>, IBAVendorContactDS {

        #region Local Member 

        IBAVendorContactRepository _repository;
        IUserSessionManager _sessionmanager;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sessionmanager"></param>
        public BAVendorContactDS(IBAVendorContactRepository repository, IUserSessionManager sessionmanager) : base(repository) {          
            _repository = repository;
            _sessionmanager = sessionmanager;
        }

    #endregion Constructor

    #region Get

    /// <summary>
    /// Get vendorId list.
    /// </summary>
    /// <param name="customerId">Tenant id.</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<List<VendorContactDTO>> GetVendorContactListByIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken))
    {
      return await _repository.GetVendorContactListByIdAsync(vendorId, token);
    }

    ///<inheritdoc/>
    public async Task<List<BAVendorContact>> GetVendorContactListByVendorIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken))
    {
      return await _repository.GetVendorContactListByVendorIdAsync(vendorId, token);
    }

    #endregion Get

    #region Add/Update/Delete



    #endregion Add/Update/Delete

  }

}