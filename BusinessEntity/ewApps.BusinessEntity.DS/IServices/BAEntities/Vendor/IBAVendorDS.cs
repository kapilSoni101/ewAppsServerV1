  using System; 
using System.Threading.Tasks;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.AppPortal.Data;
using ewApps.Core.UserSessionService;
using System.Threading;
using ewApps.BusinessEntity.DTO;
using System.Collections.Generic;

namespace ewApps.BusinessEntity.DS {

  /// <summary>

  /// </summary>
  public interface IBAVendorDS : IBaseDS<BAVendor> {

    #region Get

    Task<List<BusVendorSetUpAppDTO>> GetVendorListForBizSetupApp(Guid tenantId, bool isDeleted, CancellationToken token = default(CancellationToken));

    Task<BusVendorSetUpAppViewDTO> GetVendorDetailForBizSetupApp(Guid vendorId, CancellationToken token = default(CancellationToken));

    Task<BusVendorDetailDTO> GetVendorDetailByIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken));
    Task<List<BusVendorDTO>> GetVendorListByStatusAndTenantIdAsync(Guid tenantId, int status, CancellationToken token = default(CancellationToken));
    Task<List<BusVendorDTO>> GetVendorListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken));


    #endregion Get

    #region Add/Update/Delete

    Task<bool> AddVendorListAsync(List<BAVendorSyncDTO> vendorDetailList, Guid tenantId, Guid tenantUserId, bool isBulkInsert, CancellationToken token = default(CancellationToken));

    Task<bool> UpdateVendorDetailForBizSetupApp(BusVendorUpdateDTO vendDetailDTO, CancellationToken token = default(CancellationToken));
   
 #endregion Add/Update/Delete

  }

}