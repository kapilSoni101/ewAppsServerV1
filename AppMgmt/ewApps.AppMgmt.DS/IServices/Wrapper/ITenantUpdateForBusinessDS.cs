using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.Common;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.Core.UserSessionService;
using Newtonsoft.Json;

namespace ewApps.AppMgmt.DS {
    public interface ITenantUpdateForBusinessDS {

        /// <summary>
        /// Update busines and supported entities.
        /// </summary>
        /// <param name="tenantRegistrtionDTO">Update tenant model.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<BusinessResponseModelDTO> UpdateBusinessTenantAsync(UpdateTenantModelDTO tenantRegistrtionDTO, CancellationToken token = default(CancellationToken));

    }
}
