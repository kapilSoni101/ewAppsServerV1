using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO.PreferenceDTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.ServiceProcessor;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
    public class VendorUserPreferenceDS:IVendorUserPreferenceDS {

        #region Local variable

        AppPortalAppSettings _appSettings;
        ITenantUserAppPreferenceDS _tenantUserAppPreferenceDS;

        #endregion

        #region Constructor

        public VendorUserPreferenceDS(ITenantUserAppPreferenceDS tenantUserAppPreferenceDS, IOptions<AppPortalAppSettings> appSettingsOption) {
            _tenantUserAppPreferenceDS = tenantUserAppPreferenceDS;
            _appSettings = appSettingsOption.Value;
        }

        #endregion

        public async Task<bool> AddAPPrefValueAsync(Guid tenantId, Guid tenantUserId, Guid createdBy, Guid appId, long emailPreference, long smsPreference, long asPreference, CancellationToken cancellationToken = default(CancellationToken)) {
            TenantUserAppPreference existingTenantUserAppPreference = await _tenantUserAppPreferenceDS.FindAsync(RL => RL.TenantUserId == tenantUserId && RL.TenantId == tenantId && RL.AppId == appId && RL.Deleted == false);
            if(existingTenantUserAppPreference != null) {
                existingTenantUserAppPreference.EmailPreference = emailPreference;
                existingTenantUserAppPreference.SMSPreference = smsPreference;
                existingTenantUserAppPreference.ASPreference = asPreference;
                await _tenantUserAppPreferenceDS.UpdateAsync(existingTenantUserAppPreference, existingTenantUserAppPreference.ID);
            }
            else {
                TenantUserAppPreference tenantUserAppPreference = new TenantUserAppPreference();
                tenantUserAppPreference.ID = Guid.NewGuid();
                tenantUserAppPreference.AppId = appId;
                tenantUserAppPreference.CreatedBy = createdBy;
                tenantUserAppPreference.CreatedOn = DateTime.UtcNow;
                tenantUserAppPreference.Deleted = false;
                tenantUserAppPreference.EmailPreference = emailPreference;
                tenantUserAppPreference.SMSPreference = smsPreference;
                tenantUserAppPreference.ASPreference = asPreference;
                tenantUserAppPreference.TenantId = tenantId;
                tenantUserAppPreference.TenantUserId = tenantUserId;
                tenantUserAppPreference.UpdatedBy = tenantUserAppPreference.CreatedBy;
                tenantUserAppPreference.UpdatedOn = tenantUserAppPreference.CreatedOn;
                await _tenantUserAppPreferenceDS.AddAsync(tenantUserAppPreference, cancellationToken);
            }
            return true;
        }

        public async Task<bool> AddBEPrefValueAsync(Guid tenantId, Guid tenantUserId, Guid createdBy, Guid appId, string userSessionID, long emailPreference, long smsPreference, long asPreference, CancellationToken cancellationToken) {
            PreferenceUpdateDTO preferenceUpdateDTO = new PreferenceUpdateDTO();
            preferenceUpdateDTO.ID = Guid.NewGuid();
            preferenceUpdateDTO.CreatedBy = createdBy;
            preferenceUpdateDTO.CreatedOn = DateTime.UtcNow;
            preferenceUpdateDTO.UpdatedBy = preferenceUpdateDTO.CreatedBy;
            preferenceUpdateDTO.UpdatedOn = preferenceUpdateDTO.CreatedOn;
            preferenceUpdateDTO.Deleted = false;
            preferenceUpdateDTO.TenantId = tenantId;
            preferenceUpdateDTO.TenantUserId = tenantUserId;
            preferenceUpdateDTO.EmailPreference = emailPreference;
            preferenceUpdateDTO.SMSPreference = smsPreference;
            preferenceUpdateDTO.ASPreference = asPreference;
            preferenceUpdateDTO.AppId = appId;

            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = "bepreference/addvendorbepreference";
            requestOptions.MethodData = preferenceUpdateDTO;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Post;
            requestOptions.BearerTokenInfo = new BearerTokenOption();
            requestOptions.BearerTokenInfo.AppClientName = _appSettings.AppName;
            requestOptions.BearerTokenInfo.AuthServiceUrl = _appSettings.IdentityServerUrl;
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSessionID));
            requestOptions.HeaderParameters = headerParams;
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSettings.BusinessEntityApiUrl);
            await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);
            return true;
        }

        public async Task<bool> AddPaymentPrefValueAsync(Guid tenantId, Guid tenantUserId, Guid createdBy, Guid appId, string userSessionID, long emailPreference, long smsPreference, long asPreference, CancellationToken cancellationToken) {
            PreferenceUpdateDTO preferenceUpdateDTO = new PreferenceUpdateDTO();
            preferenceUpdateDTO.ID = Guid.NewGuid();
            preferenceUpdateDTO.CreatedBy = createdBy;
            preferenceUpdateDTO.CreatedOn = DateTime.UtcNow;
            preferenceUpdateDTO.UpdatedBy = preferenceUpdateDTO.CreatedBy;
            preferenceUpdateDTO.UpdatedOn = preferenceUpdateDTO.CreatedOn;
            preferenceUpdateDTO.Deleted = false;
            preferenceUpdateDTO.TenantId = tenantId;
            preferenceUpdateDTO.TenantUserId = tenantUserId;
            preferenceUpdateDTO.EmailPreference = emailPreference;
            preferenceUpdateDTO.SMSPreference = smsPreference;
            preferenceUpdateDTO.ASPreference = asPreference;
            preferenceUpdateDTO.AppId = appId;

            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = "paymentpreference/addvendpaypreference";
            requestOptions.MethodData = preferenceUpdateDTO;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Post;
            requestOptions.BearerTokenInfo = new BearerTokenOption();
            requestOptions.BearerTokenInfo.AppClientName = _appSettings.AppName;
            requestOptions.BearerTokenInfo.AuthServiceUrl = _appSettings.IdentityServerUrl;
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSessionID));
            requestOptions.HeaderParameters = headerParams;
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSettings.PaymentApiUrl);
            await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);
            return true;
        }
    }
}
