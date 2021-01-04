using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Data {

	public interface IBAVendorAddressRepository: IBaseRepository<BAVendorAddress> {
    Task<List<VendorAddressDTO>> GetVendorAddressListByIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken));
    Task<List<BAVendorAddress>> GetVendorAddressEntityListByIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken));
  }

}