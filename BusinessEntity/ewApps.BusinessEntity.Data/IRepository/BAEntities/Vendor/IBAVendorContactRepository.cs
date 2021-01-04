using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Data {

	public interface IBAVendorContactRepository: IBaseRepository<BAVendorContact> {

    Task<List<VendorContactDTO>> GetVendorContactListByIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken));

    Task<List<BAVendorContact>> GetVendorContactListByVendorIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken));

  }

}