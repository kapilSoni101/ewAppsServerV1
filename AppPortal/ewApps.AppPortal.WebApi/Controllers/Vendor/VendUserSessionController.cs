using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {
    [Route("api/[controller]")]
    [ApiController]
    public class VendUserSessionController:ControllerBase {

        IQVendUserSessionDS _qVendUserSessionDS;

        public VendUserSessionController(IQVendUserSessionDS qVendUserSessionDS) {
      _qVendUserSessionDS = qVendUserSessionDS;
        }

        #region Get

        /// <summary>
        /// Get session for platform users.
        /// </summary>
        /// <returns></returns>
        [Route("info/{identityuserid}/{tenantid}/{portalKey}")]
        [HttpGet]
        public async Task<VendorUserSessionDTO> GetSessionInfoAsync([FromRoute]Guid identityuserid, [FromRoute]Guid tenantid, [FromRoute]string portalKey) {

            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
      VendorUserSessionDTO UserSessionDTO = await _qVendUserSessionDS.GetUserSessionAsync(identityuserid, tenantid, portalKey);
            //UserSessionDTO.TimeTakenInMiliseconds = stopwatch.ElapsedMilliseconds;
            //stopwatch.Stop();
            return UserSessionDTO;
        }

        #endregion Get

        #region Set session

        [Route("set/{userIdentity}")]
        [HttpPost]
        public async Task<ResponseModelDTO> SetUserSession([FromRoute] Guid userIdentity) {
            return new ResponseModelDTO() { IsSuccess = true };
        }

        #endregion Set session

        #region LogOut

        [HttpPut]
        [Route("logout/{identityserverid}")]
        public async Task LogOut([FromRoute]string identityServerId) {
            await _qVendUserSessionDS.LogOut(identityServerId);
        }

        #endregion LogOut
    }
}