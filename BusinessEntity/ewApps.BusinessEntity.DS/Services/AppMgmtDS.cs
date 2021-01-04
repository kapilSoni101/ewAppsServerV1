using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Common;
using ewApps.Core.BaseService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    /// This class defines methods to manage app management operations.
    /// </summary>
    public class AppMgmtDS:IAppMgmtDS {

        #region Local Member

        private BusinessEntityAppSettings _appSettings;

        IUserSessionManager _userSessionManager;

        #endregion Local Member

        #region Constructor

        /// <summary>
        /// initilize constructor
        /// </summary>
        /// <param name="appSettings"></param>
        public AppMgmtDS(IOptions<BusinessEntityAppSettings> appSettings, IUserSessionManager userSessionManager) {
            _appSettings = appSettings.Value;
            _userSessionManager = userSessionManager;
        }

        #endregion Constructor

        ///<inheritdoc/>
        public async Task<Guid> GetTenantPrimaryUserAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            string baseUrl = _appSettings.AppMgmtApiUrl;
            string reqeustMethod = "usertenantlinking/gettenantprimaryuser/" + tenantId + "/" + (int)UserTypeEnum.Business;

            UserSession session = _userSessionManager.GetSession();

            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));


            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                     RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
            Guid businessUserId = await httpRequestProcessor.ExecuteAsync<Guid>(requestOptions, false);
            return businessUserId;
        }
    }
}
