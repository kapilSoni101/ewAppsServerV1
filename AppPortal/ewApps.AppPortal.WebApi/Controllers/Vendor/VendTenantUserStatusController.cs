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
    public class VendTenantUserStatusController:ControllerBase {

        #region Local Member

        IVendTenantUserStatusDS _vendTenantUserStatusDS;

        #endregion Local Member

        #region Constructor

        public VendTenantUserStatusController(IVendTenantUserStatusDS vendTenantUserStatusDS) {
      _vendTenantUserStatusDS = vendTenantUserStatusDS;
        }

        #endregion Constructor

        #region Update

        [HttpPut]
        [Route("update")]
        public async Task<bool> UpdateTenantUserStatus(TenantUserAppLastAccessInfoRequestDTO tenantUserAppLastAccessInfoRequestDTO) {
            return await _vendTenantUserStatusDS.UpdateTenantUserLoginJoinedStatusAsync(tenantUserAppLastAccessInfoRequestDTO);
        }

        #endregion Update
    }
}