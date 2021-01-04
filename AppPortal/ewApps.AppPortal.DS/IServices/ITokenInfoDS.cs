using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DS {
    public interface ITokenInfoDS  : IBaseDS<TokenInfo>{

        Task DeleteTokenAsync(TokenInfoDTO tokenInfoDTO);

        Task DeleteTokenByTenantUserIdAndTokenType(Guid tenantUserId, Guid tenantId, int tokenType);
    }
}
