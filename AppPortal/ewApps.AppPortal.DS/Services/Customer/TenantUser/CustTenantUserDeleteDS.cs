/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 9 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 9 September 2019
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
    public class CustTenantUserDeleteDS:ICustTenantUserDeleteDS {

    IQCustomerAndUserDS _qCustomerAndUserDS;
    AppPortalAppSettings _appPortalAppSettings;
        IUserSessionManager _userSessionManager;

        public CustTenantUserDeleteDS(IQCustomerAndUserDS qCustomerAndUserDS,IOptions<AppPortalAppSettings> appPortalAppSettings,
            IUserSessionManager userSessionManager) {
      _qCustomerAndUserDS = qCustomerAndUserDS;
      _appPortalAppSettings = appPortalAppSettings.Value;
            _userSessionManager = userSessionManager;
        }

        #region Delete

        public async Task<DeleteUserResponseDTO> DeleteTenantUser(TenantUserIdentificationDTO tenantUserIdentificationDTO) {

            // Validte user to be delted for last admin check.
            await ValidationOnDeleteUser(tenantUserIdentificationDTO.TenantUserId, tenantUserIdentificationDTO.TenantId, (int)UserTypeEnum.Customer, tenantUserIdentificationDTO.AppId);

            UserSession userSession = _userSessionManager.GetSession();

            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = "tenantuser/deletecustomeruser";

            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturl, tenantUserIdentificationDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            DeleteUserResponseDTO deleteUserResponseDTO = await httpRequestProcessor.ExecuteAsync<DeleteUserResponseDTO>(requestOptions, false);
            return deleteUserResponseDTO;
        }

    #endregion Delete

    private async Task ValidationOnDeleteUser(Guid tenantUserID, Guid tenantId, int userType, Guid appId) {

      Tuple<bool, Guid> lastAdmin = await _qCustomerAndUserDS.CheckUserIsLastAdminUserAsync(tenantId, userType, appId);

      if(lastAdmin.Item1 && lastAdmin.Item2 == tenantUserID) {
        EwpError error = new EwpError();
        error.ErrorType = ErrorType.Security;
        EwpErrorData errorData = new EwpErrorData();
        errorData.ErrorSubType = (int)SecurityErrorSubType.Update;
        errorData.Message = "Can not  deleted last admin";
        error.EwpErrorDataList.Add(errorData);
        EwpDuplicateNameException exc = new EwpDuplicateNameException("Can not  deleted last admin", error.EwpErrorDataList);
        throw exc;
      }
    }
  }
}
