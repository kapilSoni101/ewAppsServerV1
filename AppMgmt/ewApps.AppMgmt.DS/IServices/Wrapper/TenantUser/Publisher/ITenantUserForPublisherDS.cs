using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;

namespace ewApps.AppMgmt.DS {
    public interface ITenantUserForPublisherDS {

        Task<PublisherTenantInfoDTO> GetPublisherAndUserAsync(PublisherRequestDTO reqDto, CancellationToken token = default(CancellationToken));
    }
}
