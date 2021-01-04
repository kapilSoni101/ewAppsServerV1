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
    public class CustUserSessionController:ControllerBase {

        IQCustUserSessionDS _qCustUserSessionDS;

        public CustUserSessionController(IQCustUserSessionDS qCustUserSessionDS) {
            _qCustUserSessionDS = qCustUserSessionDS;
        }

        #region Get

        /// <summary>
        /// Get session for platform users.
        /// </summary>
        /// <returns></returns>
        [Route("info/{identityuserid}/{tenantid}/{portalKey}")]
        [HttpGet]
        public async Task<CustomerUserSessionDTO> GetSessionInfoAsync([FromRoute]Guid identityuserid, [FromRoute]Guid tenantid, [FromRoute]string portalKey) {

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            CustomerUserSessionDTO UserSessionDTO = await _qCustUserSessionDS.GetUserSessionAsync(identityuserid, tenantid, portalKey);
            UserSessionDTO.TimeTakenInMiliseconds = stopwatch.ElapsedMilliseconds;
            stopwatch.Stop();
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
            await _qCustUserSessionDS.LogOut(identityServerId);
        }

        #endregion LogOut
    }
}