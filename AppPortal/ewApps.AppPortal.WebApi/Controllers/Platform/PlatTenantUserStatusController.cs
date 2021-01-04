using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi.Controllers.Platform {

    /// <summary>
    /// Controler exposing all the api related to user status of the platform.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PlatTenantUserStatusController:ControllerBase {

        #region Local Member

        IPlatTenantUserStatusDS _platTenantUserStatusDS;

        #endregion Local Member

        #region Constructor

        /// <summary>
        /// Initializing the local members
        /// </summary>
        public PlatTenantUserStatusController(IPlatTenantUserStatusDS platTenantUserStatusDS) {
            _platTenantUserStatusDS = platTenantUserStatusDS;
        }

        #endregion Constructor

        #region Update

        [HttpPut]
        [Route("update")]
        public async Task<bool> UpdateTenantUserStatus(TenantUserAppLastAccessInfoRequestDTO tenantUserAppLastAccessInfoRequestDTO) {
            return await _platTenantUserStatusDS.UpdateTenantUserLoginJoinedStatus(tenantUserAppLastAccessInfoRequestDTO);
        }

        #endregion Update
    }
}