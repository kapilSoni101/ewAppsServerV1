using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;

namespace ewApps.AppMgmt.DS {
    public interface ITenantUserSignUpForBusiness {

        Task<TenantUserSignUpResponseDTO> SignUpUserAsync(TenantUserSignUpDTO tenantUserSignUpDTO, CancellationToken cancellationToken = default(CancellationToken));
    }
}
