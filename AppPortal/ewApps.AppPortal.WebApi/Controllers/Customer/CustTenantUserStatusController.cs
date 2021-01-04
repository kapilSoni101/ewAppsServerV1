using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi.Controllers.Customer {
    [Route("api/[controller]")]
    [ApiController]
    public class CustTenantUserStatusController:ControllerBase {

        #region Local Member

        ICustTenantUserStatusDS _custTenantUserStatusDS;

        #endregion Local Member

        #region Constructor

        public CustTenantUserStatusController(ICustTenantUserStatusDS custTenantUserStatusDS) {
            _custTenantUserStatusDS = custTenantUserStatusDS;
        }

        #endregion Constructor

        #region Update

        [HttpPut]
        [Route("update")]
        public async Task<bool> UpdateTenantUserStatus(TenantUserAppLastAccessInfoRequestDTO tenantUserAppLastAccessInfoRequestDTO) {
            return await _custTenantUserStatusDS.UpdateTenantUserLoginJoinedStatusAsync(tenantUserAppLastAccessInfoRequestDTO);
        }

        #endregion Update
    }
}