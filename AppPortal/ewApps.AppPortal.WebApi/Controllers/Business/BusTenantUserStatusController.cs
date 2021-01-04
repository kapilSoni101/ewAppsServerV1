using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {
    [Route("api/[controller]")]
    [ApiController]
    public class BusTenantUserStatusController:ControllerBase {

        #region Local Member

        IBusTenantUserStatusDS _busTenantUserStatusDS;

        #endregion Local Member

        #region Constructor
        
        public BusTenantUserStatusController(IBusTenantUserStatusDS busTenantUserStatusDS) {
            _busTenantUserStatusDS = busTenantUserStatusDS;
        }

        #endregion Constructor

        #region Update

        [HttpPut]
        [Route("update")]
        public async Task<bool> UpdateTenantUserStatus(TenantUserAppLastAccessInfoRequestDTO tenantUserAppLastAccessInfoRequestDTO) {
            return await _busTenantUserStatusDS.UpdateTenantUserLoginJoinedStatusAsync(tenantUserAppLastAccessInfoRequestDTO);
        }

        #endregion Update
    }
}