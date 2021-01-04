using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;

namespace ewApps.AppMgmt.DS {

  public interface ITenantSignUpForCustomerDS {

    Task<TenantSignUpForCustomerResDTO> TenantSignUpForCustomerAsync(List<TenantSignUpForCustomerReqDTO> customerSignUpDTO, CancellationToken token = default(CancellationToken));
  }
}
