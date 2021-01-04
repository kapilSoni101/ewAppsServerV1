using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;


namespace ewApps.BusinessEntity.DS {
  
  /// <summary>
  
  /// </summary>
  public interface IBAVendorContactDS:IBaseDS<BAVendorContact> {

    #region Get
    /// <summary>
    /// Get Customer List.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<List<VendorContactDTO>> GetVendorContactListByIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get Customer contact List by customerid.
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<List<BAVendorContact>> GetVendorContactListByVendorIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken));
    #endregion Get

    #region Add/Update/Delete

    #endregion Add/Update/Delete

  }

}