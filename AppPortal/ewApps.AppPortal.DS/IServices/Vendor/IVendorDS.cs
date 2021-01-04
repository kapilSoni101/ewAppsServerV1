  using System; 
using System.Threading.Tasks;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.AppPortal.Data;
using ewApps.Core.UserSessionService;
using ewApps.AppPortal.DTO;
using System.Threading;
using System.Collections.Generic;

namespace ewApps.AppPortal.DS {

  /// <summary>

  /// </summary>
  public interface IVendorDS : IBaseDS<Vendor> {

    #region Get

    Task<VendorConfigurationDTO> GetVendorConfigurationDetailAsync(Guid buspartnertenantid, CancellationToken cancellationToken = default(CancellationToken));
   
    #endregion Get

    #region Add/Update/Delete

    #endregion Add/Update/Delete

  }

}