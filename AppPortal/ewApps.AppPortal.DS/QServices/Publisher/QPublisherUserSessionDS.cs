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
    public class QPublisherUserSessionDS:IQPublisherUserSessionDS {

        IQPublisherUserSessionRepository _qPublisherUserSessionRepository;
        ILogger<QPublisherUserSessionDS> _loggerService;
        DMServiceSettings _thumbnailAppSetting;
        IUserSessionManager _userSessionManager;
        AppPortalAppSettings _appPortalAppSettings;

        public QPublisherUserSessionDS(IQPublisherUserSessionRepository qPublisherUserSessionRepository, IUserSessionManager userSessionManager,
            IOptions<DMServiceSettings> thumbnailAppSetting , IOptions<AppPortalAppSettings > appPortalAppSettings) {
            _qPublisherUserSessionRepository = qPublisherUserSessionRepository;
            _thumbnailAppSetting = thumbnailAppSetting.Value;
            _userSessionManager = userSessionManager;
            _appPortalAppSettings = appPortalAppSettings.Value;
        }

        #region Get

        /// <inheritdoc>
        public async Task<PublisherUserSessionDTO> GetUserSessionAsync(Guid identityUserId, Guid tenantId, string portalKey) {

            // Get flat data for user, branding and portal details.
            Task<PublisherUserSessionDTO> getPlafromDataTask = _qPublisherUserSessionRepository.GetPublisherPortalUserSessionInfoAsync(identityUserId, tenantId, portalKey);
            // Get user application details.
            Task<UserSessionAppDTO> appListTask = _qPublisherUserSessionRepository.GetSessionAppDetailsAsync(identityUserId, tenantId, AppKeyEnum.pub.ToString());

            // Wait for the Task to complete before assigning it to dTO.
            getPlafromDataTask.Wait();
            PublisherUserSessionDTO userSessionDTO = getPlafromDataTask.Result;

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
                if(userSessionDTO.AppList[0].ApplicationLogoUrl != null) {
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
            RequestOptions logOutRequestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, methodUri, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, null);
            // Execute api to generate tenant and user data.
            ServiceExecutor serviceExecutor = new ServiceExecutor(_appPortalAppSettings.AppMgmtApiUrl);
            await serviceExecutor.ExecuteAsync<bool>(logOutRequestOptions, false);
        }

        #endregion
    }
}