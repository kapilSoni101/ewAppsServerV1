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
    public class PlatformUserSessionController:ControllerBase {

        IQPlatformUserSessionDS _qPlatformUserSessionDS;


        public PlatformUserSessionController(IQPlatformUserSessionDS qPlatformUserSessionDS) {
            _qPlatformUserSessionDS = qPlatformUserSessionDS;
        }

        #region Get

        /// <summary>
        /// Get session for platform users.
        /// </summary>
        /// <returns></returns>
        [Route("info/{identityuserid}/{tenantid}/{portalKey}")]
        [HttpGet]
        public async Task<PlatfromUserSessionDTO> GetSessionInfoAsync([FromRoute]Guid identityuserid, [FromRoute]Guid tenantid, [FromRoute]string portalKey) {

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            PlatfromUserSessionDTO platfromUserSessionDTO = await _qPlatformUserSessionDS.GetUserSessionAsync(identityuserid, tenantid, portalKey);
            platfromUserSessionDTO.TimeTakenInMiliseconds = stopwatch.ElapsedMilliseconds;
            stopwatch.Stop();
            return platfromUserSessionDTO;
        }

        #endregion Get

        #region Set session

        [Route("set/{userIdentity}")]
        [HttpPost]
        public async Task<ResponseModelDTO> SetUserSession([FromQuery] Guid userIdentity) {
            return new ResponseModelDTO() { IsSuccess = true };
        }

        #endregion Set session

        #region LogOut

        [HttpPut]
        [Route("logout/{identityserverid}")]
        public async Task LogOut([FromRoute]string identityServerId) {
            await _qPlatformUserSessionDS.LogOut(identityServerId);
        }

        #endregion LogOut
    }
}