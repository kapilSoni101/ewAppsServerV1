using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Data {

	public interface IBAVendorRepository: IBaseRepository<BAVendor> {
    Task<List<BusVendorSetUpAppDTO>> GetVendorListForBizSetupApp(Guid tenantId, bool isDeleted, CancellationToken token = default(CancellationToken));
    Task<BusVendorSetUpAppViewDTO> GetVendorDetailForBizSetupApp(Guid vendorId, CancellationToken token = default(CancellationToken));
    Task<BusVendorDTO> GetVendorDetailByIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken));
    Task<List<BusVendorDTO>> GetVendorListByStatusAndTenantIdAsync(Guid tenantId, int status, CancellationToken token = default(CancellationToken));
    Task<List<BusVendorDTO>> GetVendorListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken));
  }

}