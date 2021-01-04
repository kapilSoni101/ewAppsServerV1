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
    public class PubTenantUserStatusController:ControllerBase {

        #region Local Member

        IPubTenantUserStatusDS _pubTenantUserStatusDS;

        #endregion Local Member

        #region Constructor

        /// <summary>
        /// Initializing the local members
        /// </summary>
        public PubTenantUserStatusController(IPubTenantUserStatusDS pubTenantUserStatusDS) {
            _pubTenantUserStatusDS = pubTenantUserStatusDS;
        }

        #endregion Constructor

        #region Update

        [HttpPut]
        [Route("update")]
        public async Task<bool> UpdateTenantUserStatus(TenantUserAppLastAccessInfoRequestDTO tenantUserAppLastAccessInfoRequestDTO) {
            return await _pubTenantUserStatusDS.UpdateTenantUserLoginJoinedStatus(tenantUserAppLastAccessInfoRequestDTO);
        }

        #endregion Update
    }
}