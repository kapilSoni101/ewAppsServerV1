using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    public interface IQContactUsDS {

         Task<UserEmailDTO> GetPubEmailRecipent(Guid tenantId,  CancellationToken token = default(CancellationToken));

        Task<UserEmailDTO> GetPlatEmailRecipent(CancellationToken token = default(CancellationToken));
    }
}
