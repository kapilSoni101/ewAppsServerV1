using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;

namespace ewApps.AppMgmt.DS {
    public interface ITenantUpdateForCustomerDS {

        Task<TenantUserSignUpResponseDTO> SignUpCustomerUserAsync(TenantUserSignUpDTO tenantUserSignUpDTO, CancellationToken token = default(CancellationToken));
        Task<TenantUserSignUpResponseDTO> UpdateCustomerTenantUser(TenantUserSignUpDTO tenantUserSignUpDTO);
    }
}
