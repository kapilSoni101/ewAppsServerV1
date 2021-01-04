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

namespace ewApps.AppPortal.DS {
    public class VendTenantUserStatusDS:IVendTenantUserStatusDS {

        AppPortalAppSettings _appPortalAppSettings;
        // IQCustomerAndUserDS _qCustomerAndUserDS;
        IUserSessionManager _userSessionManager;
        IVendorNotificationHandler _vendorNotificationHandler;

        public VendTenantUserStatusDS(IVendorNotificationHandler vendorNotificationHandler, IOptions<AppPortalAppSettings> appPortalAppSettings,
            IUserSessionManager userSessionManager) {
            _appPortalAppSettings = appPortalAppSettings.Value;
            //_qCustomerAndUserDS = qCustomerAndUserDS;
            _userSessionManager = userSessionManager;
            _vendorNotificationHandler = vendorNotificationHandler;
        }

        public async Task<bool> UpdateTenantUserLoginJoinedStatusAsync(TenantUserAppLastAccessInfoRequestDTO tenantUserAppLastAccessInfoRequestDTO) {

            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = "tenantuserstatus/update";

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturl, tenantUserAppLastAccessInfoRequestDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, null);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            bool firstLogin = await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);

            if(firstLogin) {

                if(tenantUserAppLastAccessInfoRequestDTO.AppKey == AppKeyEnum.vend.ToString()) {
                    ////TODO: Vendor User Onboard email
                    //if(tenantUserAppLastAccessInfoRequestDTO.BusinessPartnerTenantId.HasValue) {
                    //    VendorOnBoardNotificationDTO vendorOnBoardNotificationDTO = await _qCustomerAndUserDS.GetCustomerOnBoardDetailByAppAndBusinessAndUserIdAsync(tenantUserAppLastAccessInfoRequestDTO.AppKey, tenantUserAppLastAccessInfoRequestDTO.TenantId, tenantUserAppLastAccessInfoRequestDTO.BusinessPartnerTenantId.Value, tenantUserAppLastAccessInfoRequestDTO.TenantUserId);
                    //    if(vendorOnBoardNotificationDTO != null) {
                    //        vendorOnBoardNotificationDTO.UserSessionInfo = _userSessionManager.GetSession();
                    //        await _vendorNotificationHandler.SendVendorSetupUserOnBoardNotificationAsync(vendorOnBoardNotificationDTO);
                    //    }
                    //}
                }
                if(tenantUserAppLastAccessInfoRequestDTO.AppKey == AppKeyEnum.vendsetup.ToString()) {
                    /*
                                        CustomerOnBoardNotificationDTO customerSetupUserOnBoardNotificationDTO = await _qCustomerAndUserDS.GetCustomerSetupOnBoardDetailByAppAndBusinessAndUserIdAsync(tenantUserAppLastAccessInfoRequestDTO.AppKey, tenantUserAppLastAccessInfoRequestDTO.TenantId, tenantUserAppLastAccessInfoRequestDTO.BusinessPartnerTenantId.Value, tenantUserAppLastAccessInfoRequestDTO.TenantUserId);
                                        if(customerSetupUserOnBoardNotificationDTO != null) {
                                            customerSetupUserOnBoardNotificationDTO.UserSessionInfo = _userSessionManager.GetSession();
                                            await _custNotificationHandler.SendCustSetupUserOnBoardNotificationAsync(customerSetupUserOnBoardNotificationDTO);
                                        }
                    */
                }

            }
            return firstLogin;
        }

    }
}
