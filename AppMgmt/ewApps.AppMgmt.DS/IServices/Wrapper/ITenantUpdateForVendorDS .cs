using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;

namespace ewApps.AppMgmt.DS {
    public interface ITenantUpdateForVendorDS {

        Task<TenantUserSignUpResponseDTO> SignUpVendorUserAsync(TenantUserSignUpDTO tenantUserSignUpDTO, CancellationToken token = default(CancellationToken));
        Task<TenantUserSignUpResponseDTO> UpdateVendorTenantUser(TenantUserSignUpDTO tenantUserSignUpDTO);
    }
}
