using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;

namespace ewApps.AppMgmt.DS{
   public  interface ITenantUpdateForPublisherDS {
        Task UpdatePublisherTenantAsync (TenantUpdateForPublisherDTO tenantUpdateForPublisherDTO, CancellationToken cancellationToken = default(CancellationToken));

    }
}
