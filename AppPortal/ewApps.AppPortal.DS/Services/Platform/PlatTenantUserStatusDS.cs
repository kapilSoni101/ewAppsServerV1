using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.Core.ServiceProcessor;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
    public class PlatTenantUserStatusDS:IPlatTenantUserStatusDS {

        AppPortalAppSettings _appPortalAppSettings;

        public PlatTenantUserStatusDS(IOptions<AppPortalAppSettings> appPortalAppSettings) {
            _appPortalAppSettings = appPortalAppSettings.Value;
        }

        public async Task<bool> UpdateTenantUserLoginJoinedStatus(TenantUserAppLastAccessInfoRequestDTO tenantUserAppLastAccessInfoRequestDTO) {

            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = "tenantuserstatus/update";

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturl, tenantUserAppLastAccessInfoRequestDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, null);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            bool firstLogin = await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);
            return firstLogin;
        }

    }
}
