using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.AppMgmt.DS;
using ewApps.AppMgmt.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppMgmt.WebApi {
    [Route("api/[controller]")]
    [ApiController]
    public class TenantUserStatusController:ControllerBase {

        #region Local Member

        ITenantUserStatusDS _tenantUserStatusDS;

        #endregion Local Member

        #region Constructor

        public TenantUserStatusController(ITenantUserStatusDS tenantUserStatusDS) {
            _tenantUserStatusDS = tenantUserStatusDS;
        }

        #endregion Constructor

        [HttpPut]
        [Route("update")]
        public async Task<bool> UpdateTenantUserStatus(TenantUserAppLastAccessInfoRequestDTO tenantUserAppLastAccessInfoRequestDTO) {
            return await _tenantUserStatusDS.UpdateTenantUserLoginJoinedStatus(tenantUserAppLastAccessInfoRequestDTO);
        }

    }
}