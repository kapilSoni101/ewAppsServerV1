using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS{
   public class CustTenantUserStatusDS :ICustTenantUserStatusDS {

        AppPortalAppSettings _appPortalAppSettings;
        IQCustomerAndUserDS _qCustomerAndUserDS;
        IUserSessionManager _userSessionManager;
        ICustNotificationHandler _custNotificationHandler;

        public CustTenantUserStatusDS(IOptions<AppPortalAppSettings> appPortalAppSettings, IQCustomerAndUserDS qCustomerAndUserDS ,
            IUserSessionManager userSessionManager, ICustNotificationHandler custNotificationHandler) {
            _appPortalAppSettings = appPortalAppSettings.Value;
            _qCustomerAndUserDS = qCustomerAndUserDS;
            _userSessionManager = userSessionManager;
            _custNotificationHandler = custNotificationHandler;
        }

        public async Task<bool> UpdateTenantUserLoginJoinedStatusAsync(TenantUserAppLastAccessInfoRequestDTO tenantUserAppLastAccessInfoRequestDTO) {

            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = "tenantuserstatus/update";

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturl, tenantUserAppLastAccessInfoRequestDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, null);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            bool firstLogin = await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);

            if(firstLogin) {
                if(tenantUserAppLastAccessInfoRequestDTO.AppKey == AppKeyEnum.pay.ToString()) {
                    CustomerOnBoardNotificationDTO customerOnBoardNotificationDTO = await _qCustomerAndUserDS.GetCustomerOnBoardDetailByAppAndBusinessAndUserIdAsync(tenantUserAppLastAccessInfoRequestDTO.AppKey, tenantUserAppLastAccessInfoRequestDTO.TenantId, (Guid)tenantUserAppLastAccessInfoRequestDTO.BusinessPartnerTenantId, tenantUserAppLastAccessInfoRequestDTO.TenantUserId);
                    if(customerOnBoardNotificationDTO != null) {
                        customerOnBoardNotificationDTO.UserSessionInfo = _userSessionManager.GetSession();
                        await _custNotificationHandler.SendCustPaymentUserOnBoardNotificationAsync(customerOnBoardNotificationDTO);
                    }
                }
                if(tenantUserAppLastAccessInfoRequestDTO.AppKey == AppKeyEnum.cust.ToString()) {
                    if(tenantUserAppLastAccessInfoRequestDTO.BusinessPartnerTenantId.HasValue) {
                    CustomerOnBoardNotificationDTO businessOnBoardNotificationDTO = await _qCustomerAndUserDS.GetCustomerOnBoardDetailByAppAndBusinessAndUserIdAsync(tenantUserAppLastAccessInfoRequestDTO.AppKey, tenantUserAppLastAccessInfoRequestDTO.TenantId, tenantUserAppLastAccessInfoRequestDTO.BusinessPartnerTenantId.Value, tenantUserAppLastAccessInfoRequestDTO.TenantUserId);
                        if(businessOnBoardNotificationDTO != null) {
                            businessOnBoardNotificationDTO.UserSessionInfo = _userSessionManager.GetSession();
                            await _custNotificationHandler.SendCustCustomerUserOnBoardNotificationAsync(businessOnBoardNotificationDTO);
                        }
                    }
                    
                }
                if(tenantUserAppLastAccessInfoRequestDTO.AppKey == AppKeyEnum.custsetup.ToString()) {
                    CustomerOnBoardNotificationDTO customerSetupUserOnBoardNotificationDTO = await _qCustomerAndUserDS.GetCustomerSetupOnBoardDetailByAppAndBusinessAndUserIdAsync(tenantUserAppLastAccessInfoRequestDTO.AppKey, tenantUserAppLastAccessInfoRequestDTO.TenantId, tenantUserAppLastAccessInfoRequestDTO.BusinessPartnerTenantId.Value, tenantUserAppLastAccessInfoRequestDTO.TenantUserId);
                    if(customerSetupUserOnBoardNotificationDTO != null) {
                        customerSetupUserOnBoardNotificationDTO.UserSessionInfo = _userSessionManager.GetSession();
                        await _custNotificationHandler.SendCustSetupUserOnBoardNotificationAsync(customerSetupUserOnBoardNotificationDTO);
                    }
                }

            }
            return firstLogin;
        }

    }
}
