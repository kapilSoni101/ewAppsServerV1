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
    public class PublisherUserSessionController:ControllerBase {

        IQPublisherUserSessionDS _qPublisherUserSessionDS;

        public PublisherUserSessionController(IQPublisherUserSessionDS qPublisherUserSessionDS) {
            _qPublisherUserSessionDS = qPublisherUserSessionDS;
        }


        #region Get

        /// <summary>
        /// Get session for platform users.
        /// </summary>
        /// <returns></returns>
        [Route("info/{identityuserid}/{tenantid}/{portalKey}")]
        [HttpGet]
        public async Task<PublisherUserSessionDTO> GetSessionInfoAsync([FromRoute]Guid identityuserid, [FromRoute]Guid tenantid, [FromRoute]string portalKey) {

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            PublisherUserSessionDTO publisherUserSessionDTO = await _qPublisherUserSessionDS.GetUserSessionAsync(identityuserid, tenantid, portalKey);
            publisherUserSessionDTO.TimeTakenInMiliseconds = stopwatch.ElapsedMilliseconds;
            stopwatch.Stop();
            return publisherUserSessionDTO;
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
            await _qPublisherUserSessionDS.LogOut(identityServerId);
        }

        #endregion LogOut

    }
}