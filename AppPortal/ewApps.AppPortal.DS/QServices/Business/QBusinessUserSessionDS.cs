using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.AppPortal.QData;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.Money;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
    public class QBusinessUserSessionDS:IQBusinessUserSessionDS {

        IQBusinessUserSessionRepository _qBusinessUserSessionRepository;
        ILogger<QBusinessUserSessionDS> _loggerService;
        DMServiceSettings _thumbnailAppSetting;
        AppPortalAppSettings _appPortalAppSettings;
        IUserSessionManager _userSessionManager;
        IBusinessDS _businessDS;
        CurrencyCultureInfoTable _currencyCultureInfoTable;

        public QBusinessUserSessionDS(IQBusinessUserSessionRepository qBusinessUserSessionRepository, IOptions<AppPortalAppSettings> appPortalAppSettings,
            IOptions<DMServiceSettings> thumbnailAppSetting, IUserSessionManager userSessionManager, IBusinessDS businessDS, CurrencyCultureInfoTable currencyCultureInfoTable) {
            _qBusinessUserSessionRepository = qBusinessUserSessionRepository;
            _thumbnailAppSetting = thumbnailAppSetting.Value;
            _appPortalAppSettings = appPortalAppSettings.Value;
            _userSessionManager = userSessionManager;
            _businessDS = businessDS;
            _currencyCultureInfoTable = currencyCultureInfoTable;
        }

        #region Get

        ///<inheritdoc/>
        public async Task<BusinessUserSessionDTO> GetUserSessionAsync(Guid identityUserId, Guid tenantId, string portalKey) {

            // Get flat data for user, branding and portal details.
            Task<BusinessUserSessionDTO> getPlafromDataTask = _qBusinessUserSessionRepository.GetBusinessPortalUserSessionInfoAsync(identityUserId, tenantId, portalKey, (int)UserTypeEnum.Business);
            // Get user application details.
            Task<List<UserSessionAppDTO>> appListTask = _qBusinessUserSessionRepository.GetSessionAppDetailsAsync(identityUserId, tenantId, (int)UserTypeEnum.Business);

            // Wait for the Task to complete before assigning it to dTO.
            getPlafromDataTask.Wait();
            BusinessUserSessionDTO userSessionDTO = getPlafromDataTask.Result;

            if(userSessionDTO.UserThumbnailUrl != null) {
                userSessionDTO.UserThumbnailUrl = System.IO.Path.Combine(_thumbnailAppSetting.ThumbnailUrl, userSessionDTO.UserThumbnailUrl);
            }

            if(userSessionDTO.PortalTopHeaderLeftLogoUrl != null) {
                userSessionDTO.PortalTopHeaderLeftLogoUrl = System.IO.Path.Combine(_thumbnailAppSetting.ThumbnailUrl, userSessionDTO.PortalTopHeaderLeftLogoUrl);
            }

            if(userSessionDTO.LeftBusinessLogoUrl != null) {
                userSessionDTO.LeftBusinessLogoUrl = System.IO.Path.Combine(_thumbnailAppSetting.ThumbnailUrl, userSessionDTO.LeftBusinessLogoUrl);
            }

            // Currency
            userSessionDTO.UserSessionCurrency = new UserSessionCurrencyDTO();
            Business business = await _businessDS.FindAsync(b => b.TenantId == userSessionDTO.TenantId);

            userSessionDTO.UserSessionCurrency.CurrencyCode = business.CurrencyCode;
            userSessionDTO.UserSessionCurrency.DecimalPrecision = business.DecimalPrecision;
            userSessionDTO.UserSessionCurrency.DecimalSeperator = business.DecimalSeperator;
            userSessionDTO.UserSessionCurrency.GroupSeperator = business.GroupSeperator;
            userSessionDTO.UserSessionCurrency.GroupValue = business.GroupValue;

            CurrencyCultureInfo culInfo = _currencyCultureInfoTable.GetCultureInfo((CurrencyISOCode)business.CurrencyCode);
            if(culInfo != null) {
                userSessionDTO.UserSessionCurrency.Currency = culInfo.Symbol;
                userSessionDTO.Currency = culInfo.Symbol;
            }

            // Initialize the variables and wait for the data.
            userSessionDTO.AppList = new List<UserSessionAppDTO>();
            appListTask.Wait();

            // Asign the data and return the result.
            userSessionDTO.AppList.AddRange(appListTask.Result);

            foreach(UserSessionAppDTO item in userSessionDTO.AppList) {
                if(item.ApplicationLogoUrl != null) {
                    item.ApplicationLogoUrl = System.IO.Path.Combine(_thumbnailAppSetting.ThumbnailUrl, item.ApplicationLogoUrl);
                }
                if(item.TopHeaderLeftLogoUrl != null) {
                    item.TopHeaderLeftLogoUrl = System.IO.Path.Combine(_thumbnailAppSetting.ThumbnailUrl, item.TopHeaderLeftLogoUrl);
                }
            }

            return userSessionDTO;
        }

        ///<inheritdoc/>
        public async Task LogOut(string identityServerId) {

            // Get user session.
            UserSession userSession = _userSessionManager.GetSession();

            string methodUri = "usersession/logout/" + identityServerId;
            //List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            //headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            RequestOptions logOutRequestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, methodUri, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, null);
            // Execute api to generate tenant and user data.
            ServiceExecutor serviceExecutor = new ServiceExecutor(_appPortalAppSettings.AppMgmtApiUrl);
            await serviceExecutor.ExecuteAsync<TenantSignUpResponseDTO>(logOutRequestOptions, false);
        }

        #endregion
    }
}