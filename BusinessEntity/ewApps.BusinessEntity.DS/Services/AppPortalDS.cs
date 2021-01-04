using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Common;
using ewApps.BusinessEntity.DTO;
using ewApps.Core.BaseService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;
using System.Linq;

namespace ewApps.BusinessEntity.DS {
   public class AppPortalDS : IAppPortalDS {
        #region Local Member

        private BusinessEntityAppSettings _appSettings;

        IUserSessionManager _userSessionManager;

        #endregion Local Member

        #region Constructor

        /// <summary>
        /// initilize constructor
        /// </summary>
        /// <param name="appSettings"></param>
        public AppPortalDS(IOptions<BusinessEntityAppSettings> appSettings, IUserSessionManager userSessionManager) {
            _appSettings = appSettings.Value;
            _userSessionManager = userSessionManager;
        }

        #endregion Constructor

        ///<inheritdoc/>
        public async Task<List<string>> GetTenantSuscribedApplicationKeyAsync(Guid tenantId, CancellationToken token) {
            string baseUrl = _appSettings.AppPortalApiUrl;
           
            string relativeUrl = "bustenantuser/applist/" + tenantId;

            UserSession session = _userSessionManager.GetSession();

            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, relativeUrl, "", _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
            List<AppInfoDTO> response = await httpRequestProcessor.ExecuteAsync<List<AppInfoDTO>>(requestOptions, false);
            List<string> subscribedAppKeys = (from o in response
                                              select o.AppKey.ToString()).ToList();
            return subscribedAppKeys;
        }
    }
}
