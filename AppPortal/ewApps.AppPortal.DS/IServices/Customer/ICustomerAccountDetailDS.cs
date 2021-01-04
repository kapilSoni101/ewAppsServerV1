using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DS{
    public interface ICustomerAccountDetailDS : IBaseDS<CustomerAccountDetail> {
        Task<CustomeAccDetailDTO> GetCustomerAccDetailCustomerIdAsync(Guid customerId, CancellationToken token = default(CancellationToken));
        Task AddUpdateCustomerAcctDetail(Guid customerId, CustomeAccDetailDTO customeAccDetail, CancellationToken token = default(CancellationToken));

}
}
