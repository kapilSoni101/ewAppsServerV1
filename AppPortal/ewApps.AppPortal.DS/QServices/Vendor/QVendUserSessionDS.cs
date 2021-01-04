using System;
using System.Collections.Generic;
using System.Text;
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
    public class QVendUserSessionDS :IQVendUserSessionDS {

        IQVendUserSessionRepository _qVendUserSessionRepository;
        ILogger<QCustUserSessionDS> _loggerService;
        DMServiceSettings _thumbnailAppSetting;
        AppPortalAppSettings _appPortalAppSettings;
        IUserSessionManager _userSessionManager;
        IVendorDS _vendorDS;
        CurrencyCultureInfoTable _currencyCultureInfoTable;
        IBusinessDS _businessDS;

        public QVendUserSessionDS(IQVendUserSessionRepository qVendUserSessionRepository, IOptions<AppPortalAppSettings> appPortalAppSettings,
            IOptions<DMServiceSettings> thumbnailAppSetting, IUserSessionManager userSessionManager,
            IVendorDS vendorDS, CurrencyCultureInfoTable currencyCultureInfoTable, IBusinessDS businessDS) {
      _qVendUserSessionRepository = qVendUserSessionRepository;
            _thumbnailAppSetting = thumbnailAppSetting.Value;
            _appPortalAppSettings = appPortalAppSettings.Value;
            _userSessionManager = userSessionManager;
      _vendorDS = vendorDS;
            _currencyCultureInfoTable = currencyCultureInfoTable;
            _businessDS = businessDS;
        }

        #region Get

        ///<inheritdoc/>
        public async Task<VendorUserSessionDTO> GetUserSessionAsync(Guid identityUserId, Guid tenantId, string portalKey) {

            // Get flat data for user, branding and portal details.
            Task<VendorUserSessionDTO> getPlafromDataTask = _qVendUserSessionRepository.GetVendorPortalUserSessionInfoAsync(identityUserId, tenantId, portalKey, (int)UserTypeEnum.Vendor);
            // Get user application details.
            Task<List<UserSessionAppDTO>> appListTask = _qVendUserSessionRepository.GetSessionAppDetailsAsync(identityUserId, tenantId, (int)UserTypeEnum.Vendor);

            // Wait for the Task to complete before assigning it to dTO.
            getPlafromDataTask.Wait();
      VendorUserSessionDTO userSessionDTO = getPlafromDataTask.Result;

            if(userSessionDTO.UserThumbnailUrl != null) {
                userSessionDTO.UserThumbnailUrl = System.IO.Path.Combine(_thumbnailAppSetting.ThumbnailUrl, userSessionDTO.UserThumbnailUrl);
            }

            if(userSessionDTO.PortalTopHeaderLeftLogoUrl != null) {
                userSessionDTO.PortalTopHeaderLeftLogoUrl = System.IO.Path.Combine(_thumbnailAppSetting.ThumbnailUrl, userSessionDTO.PortalTopHeaderLeftLogoUrl);
            }

            if(userSessionDTO.LeftBusinessLogoUrl != null) {
                userSessionDTO.LeftBusinessLogoUrl = System.IO.Path.Combine(_thumbnailAppSetting.ThumbnailUrl, userSessionDTO.LeftBusinessLogoUrl);
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

            // Currency
            userSessionDTO.UserSessionCurrency = new UserSessionCurrencyDTO();
            Vendor vendor = await _vendorDS.FindAsync(b => b.BusinessPartnerTenantId == userSessionDTO.PartnerTenantId);
            Business business = await _businessDS.FindAsync(bu => bu.TenantId == tenantId);
      // New changes due to decimalprecision, now DecimalPrecision should come from business not vendor
      userSessionDTO.UserSessionCurrency.CurrencyCode = int.Parse(vendor.Currency); // 0;
            userSessionDTO.UserSessionCurrency.DecimalPrecision = business.DecimalPrecision;
            userSessionDTO.UserSessionCurrency.DecimalSeperator = vendor.DecimalSeperator;
            userSessionDTO.UserSessionCurrency.GroupSeperator = vendor.GroupSeperator;
            userSessionDTO.UserSessionCurrency.GroupValue = vendor.GroupValue;
            //Configured flag
            userSessionDTO.Configured = vendor.Configured;

            CurrencyCultureInfo culInfo = _currencyCultureInfoTable.GetCultureInfo((CurrencyISOCode)userSessionDTO.UserSessionCurrency.CurrencyCode);
            if(culInfo != null) {
                userSessionDTO.UserSessionCurrency.Currency = culInfo.Symbol;
                userSessionDTO.Currency = culInfo.Symbol;
            }

            return userSessionDTO;
        }

        public async Task LogOut(string identityServerId) {

            // Get user session.
            UserSession userSession = _userSessionManager.GetSession();

            string methodUri = "usersession/logout/" + identityServerId;
            //List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            //headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            RequestOptions logOutRequestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, methodUri, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, null);
            // Execute api to generate tenant and user data.
            ServiceExecutor serviceExecutor = new ServiceExecutor(_appPortalAppSettings.AppMgmtApiUrl);
            await serviceExecutor.ExecuteAsync<bool>(logOutRequestOptions, false);
        }

        #endregion

    }
}
