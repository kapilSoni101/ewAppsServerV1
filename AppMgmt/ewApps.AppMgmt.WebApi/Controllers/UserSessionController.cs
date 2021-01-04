using System;
using System.Threading.Tasks;
using ewApps.AppMgmt.DS;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppMgmt.WebApi.Controllers {

    //1) (Clear Session) -- Method call -- 
    //2) (DeletedByAppUserAndAppId) -- Method call
    //3) (DeletedByClientSessionId) -- Method call
    //4) (AddSession) -- DS usesession.add()
    //5) (GetSession) -- DS usesession.get()
    //6) (UpdateSession) -- DS usesession.update()

    [Route("api/[controller]")]
    [ApiController]
    public class UserSessionController:ControllerBase {

        ISessionMgmtDS _sessionMgmtDS;

        public UserSessionController(ISessionMgmtDS sessionMgmtDS) {
            _sessionMgmtDS = sessionMgmtDS;
        }

        [HttpPut]
        [Route("clearsession")]
        public async Task ClearSessionAsync([FromBody]UserSession userSession) {
            await _sessionMgmtDS.ClearSessionAsync(userSession);
        }

        [HttpDelete]
        [Route("deletesession/{appUserId:Guid}/{appId:Guid}")]
        public async Task DeletedByAppUserAndAppIdAsync([FromRoute]Guid appUserId, [FromRoute]Guid appId) {
            await _sessionMgmtDS.DeletedByAppUserAndAppIdAsync(appUserId, appId);
        }

        [HttpDelete]
        [Route("deletesessionby/{clientSessionId:Guid}/{token}")]
        public async Task DeletedByClientSessionIdAsync([FromRoute]Guid clientSessionId, [FromRoute]string token) {
            await _sessionMgmtDS.DeletedByClientSessionIdAsync(clientSessionId, token);
        }

        [HttpPost]
        [Route("add")]
        public async Task<UserSession> AddUserSessionAsync([FromBody]UserSession userSession) {
            return await _sessionMgmtDS.AddUserSessionAsync(userSession);
        }

        [HttpGet]
        [Route("get/{userSessionId:Guid}")]
        public async Task<UserSession> GetUserSessionAsync([FromRoute]Guid userSessionId) {
            return await _sessionMgmtDS.GetUserSessionAsync(userSessionId);
        }

        [HttpPut]
        [Route("update")]
        public async Task<UserSession> UpdateUserSessionAsync([FromBody]UserSession userSession) {
            return await _sessionMgmtDS.UpdateUserSessionAsync(userSession);
        }

        [HttpPut]
        [Route("logout/{identityserverid:Guid}")]
        public async Task LogOut([FromRoute]string identityServerId) {
            await _sessionMgmtDS.LogOut(identityServerId);
        }

    }
}