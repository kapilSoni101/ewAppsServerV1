using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.QData;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
    public class QPlatformUserSessionDS:IQPlatformUserSessionDS {

        IQPlatformUserSessionRepository _iQPlatformUserSession;
        ILogger<QPlatformUserSessionDS> _loggerService;
        DMServiceSettings _thumbnailAppSetting;
        IUserSessionManager _userSessionManager;
        AppPortalAppSettings _appPortalAppSettings;

        public QPlatformUserSessionDS(IQPlatformUserSessionRepository iQPlatformUserSession, IUserSessionManager userSessionManager,
            IOptions<DMServiceSettings> thumbnailAppSetting, IOptions<AppPortalAppSettings> appPortalAppSettings) {
            _iQPlatformUserSession = iQPlatformUserSession;
            _thumbnailAppSetting = thumbnailAppSetting.Value;
            _userSessionManager = userSessionManager;
            _appPortalAppSettings = appPortalAppSettings.Value;
        }

        #region Get

        ///<inheritdoc/>
        public async Task<PlatfromUserSessionDTO> GetUserSessionAsync(Guid identityUserId, Guid tenantId, string portalKey) {

            // Get flat data for user, branding and portal details.
            Task<PlatfromUserSessionDTO> getPlafromDataTask = _iQPlatformUserSession.GetPlaformPortalUserSessionInfoAsync(identityUserId, tenantId, portalKey);
            // Get user application details.
            Task<UserSessionAppDTO> appListTask = _iQPlatformUserSession.GetSessionAppDetailsAsync(identityUserId, tenantId, AppKeyEnum.plat.ToString());

            // Wait for the Task to complete before assigning it to dTO.
            getPlafromDataTask.Wait();
            PlatfromUserSessionDTO userSessionDTO = getPlafromDataTask.Result;

            if(userSessionDTO.UserThumbnailUrl != null) {
                userSessionDTO.UserThumbnailUrl = System.IO.Path.Combine(_thumbnailAppSetting.ThumbnailUrl, userSessionDTO.UserThumbnailUrl);
            }

            if(userSessionDTO.PortalTopHeaderLeftLogoUrl != null) {
                userSessionDTO.PortalTopHeaderLeftLogoUrl = System.IO.Path.Combine(_thumbnailAppSetting.ThumbnailUrl, userSessionDTO.PortalTopHeaderLeftLogoUrl);
            }
            // Initialize the variables and wait for the data.
            userSessionDTO.AppList = new List<UserSessionAppDTO>();
            appListTask.Wait();

            // Asign the data and return the result.
            userSessionDTO.AppList.Add(appListTask.Result);

            if(userSessionDTO.AppList.Count > 0) {
                if(userSessionDTO.AppList[0].ApplicationLogoUrl != null)
                    {
                    userSessionDTO.AppList[0].ApplicationLogoUrl = System.IO.Path.Combine(_thumbnailAppSetting.ThumbnailUrl, userSessionDTO.AppList[0].ApplicationLogoUrl);
                }
                if(userSessionDTO.AppList[0].TopHeaderLeftLogoUrl != null) {
                    userSessionDTO.AppList[0].TopHeaderLeftLogoUrl = System.IO.Path.Combine(_thumbnailAppSetting.ThumbnailUrl, userSessionDTO.AppList[0].TopHeaderLeftLogoUrl);
                }
            }

            return userSessionDTO;
        }

        public async Task LogOut(string identityServerId) {

            // Get user session.
            UserSession userSession = _userSessionManager.GetSession();

            string methodUri = "usersession/logout/" + identityServerId;
           // List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            //headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            RequestOptions logOutRequestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, methodUri, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, null);
            // Execute api to generate tenant and user data.
            ServiceExecutor serviceExecutor = new ServiceExecutor(_appPortalAppSettings.AppMgmtApiUrl);
            await serviceExecutor.ExecuteAsync<TenantSignUpResponseDTO>(logOutRequestOptions, false);
        }

        #endregion
    }
}