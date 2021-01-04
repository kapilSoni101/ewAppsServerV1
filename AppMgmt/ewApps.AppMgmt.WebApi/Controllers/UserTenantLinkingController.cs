using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppMgmt.WebApi {
    [Route("api/[controller]")]
    [ApiController]
    public class UserTenantLinkingController:ControllerBase {

        IUserTenantLinkingDS _userTenantLinkingDS;

        public UserTenantLinkingController(IUserTenantLinkingDS userTenantLinkingDS) {
            _userTenantLinkingDS = userTenantLinkingDS;
        }

        /// <summary>
        /// Get  tenant primary user.
        /// </summary>
        /// <param name="tenantid"></param>
        /// <param name="userType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("gettenantprimaryuser/{tenantid}/{userType}")]
        public async Task<Guid> GetTenantPrimaryUserAsync([FromRoute] Guid tenantid, [FromRoute] int userType, CancellationToken token = default(CancellationToken)) {
            return await _userTenantLinkingDS.GetTenantPrimaryUserAsync(tenantid, userType, token);
        }

    }
}