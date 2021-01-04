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
    /// This class implements standard business logic and operations for BAVendorAddress entity.
    /// </summary>
    public class BAVendorAddressDS: BaseDS<BAVendorAddress>, IBAVendorAddressDS {

        #region Local Member 

        IBAVendorAddressRepository _repository;
        IUserSessionManager _sessionmanager;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sessionmanager"></param>
        public BAVendorAddressDS(IBAVendorAddressRepository repository, IUserSessionManager sessionmanager) : base(repository) {          
            _repository = repository;
            _sessionmanager = sessionmanager;
        }

    #endregion Constructor

    #region Get

    /// <summary>
    /// Get vendorId list.
    /// </summary>
    /// <param name="vendorId">Tenant id.</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<List<VendorAddressDTO>> GetVendorAddressListByIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken))
    {
      return await _repository.GetVendorAddressListByIdAsync(vendorId, token);
    }

    /// <summary>
    /// Get vendorId address entity list.
    /// </summary>
    /// <param name="vendorId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<List<BAVendorAddress>> GetVendorAddressEntityListByIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken))
    {
      return await _repository.GetVendorAddressEntityListByIdAsync(vendorId, token);
    }

    #endregion Get

    #region Add/Update/Delete



    #endregion Add/Update/Delete

  }

}