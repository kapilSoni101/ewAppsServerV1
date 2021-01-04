using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Data {
   public  interface ICustomerAccountDetailRepository:IBaseRepository<CustomerAccountDetail> {
        Task<List<CustomerAccountDTO>> GetCustomerAccListByCustomerIdAsync(Guid customerId, CancellationToken token = default(CancellationToken));
    }
}