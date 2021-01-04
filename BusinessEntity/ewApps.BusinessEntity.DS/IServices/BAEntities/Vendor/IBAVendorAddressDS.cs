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
  public interface IBAVendorAddressDS:IBaseDS<BAVendorAddress> {

    #region Get
    Task<List<VendorAddressDTO>> GetVendorAddressListByIdAsync(Guid customerId, CancellationToken token = default(CancellationToken));

    Task<List<BAVendorAddress>> GetVendorAddressEntityListByIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken));
    #endregion Get

    #region Add/Update/Delete

    #endregion Add/Update/Delete

  }

}