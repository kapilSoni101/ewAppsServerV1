using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS
{
  public interface IBusVendorDS
  {
    #region Get



    Task<List<BusVendorSetUpAppDTO>> GetVendorListForBizSetupApp(Guid tenantId, bool isDeleted, CancellationToken token = default(CancellationToken));
    Task<BusVendorSetUpAppViewDTO> GetVendorDetailForBizSetupApp(Guid customerId, CancellationToken token = default(CancellationToken));
    Task<BusVendorDetailDTO> GetVendorDetailByIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken));
    Task<List<BusVendorDTO>> GetVendorListByStatusAndTenantIdAsync(Guid tenantId, int status, CancellationToken token = default(CancellationToken));
    Task<List<BusVendorDTO>> GetVendorListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken));



    #endregion Get 



    Task<bool> UpdateVendorDetailForBizSetupApp(BusVendorUpdateDTO customerUpdateDTO, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get vendor detail.
    /// </summary>
    /// <param name="vendorId">Vendor Id</param>
    /// <param name="tenantId">Tenant Id</param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<BusinessVendorResponse> GetBusinessVendorDetail(Guid vendorId, Guid tenantId, CancellationToken token = default(CancellationToken));


  }
}