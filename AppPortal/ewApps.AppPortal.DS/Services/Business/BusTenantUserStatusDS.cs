/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

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
  public class BusTenantUserStatusDS:IBusTenantUserStatusDS {

    AppPortalAppSettings _appPortalAppSettings;
    IPublisherAppSettingDS _publisherAppSettingDS;
    IQBusinessAndUserDS _qBusinessAndUserDS;
    IQCustomerAndUserDS _qCustomerAndUserDS;
    IBizNotificationHandler _bizPaymentNotificationHandler;
    ICustNotificationHandler _custNotificationHandler;
    IUserSessionManager _userSessionManager;

    public BusTenantUserStatusDS(IUserSessionManager userSessionManager, IBizNotificationHandler bizPaymentNotificationHandler, ICustNotificationHandler custNotificationHandler, IQBusinessAndUserDS qBusinessAndUserDS, IQCustomerAndUserDS qCustomerAndUserDS, IOptions<AppPortalAppSettings> appPortalAppSettings, IPublisherAppSettingDS publisherAppSettingDS) {
      _bizPaymentNotificationHandler = bizPaymentNotificationHandler;
      _custNotificationHandler = custNotificationHandler;
      _appPortalAppSettings = appPortalAppSettings.Value;
      _publisherAppSettingDS = publisherAppSettingDS;
      _qBusinessAndUserDS = qBusinessAndUserDS;
      _qCustomerAndUserDS = qCustomerAndUserDS;
      _userSessionManager = userSessionManager;
    }

    public async Task<bool> UpdateTenantUserLoginJoinedStatusAsync(TenantUserAppLastAccessInfoRequestDTO tenantUserAppLastAccessInfoRequestDTO) {

      string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
      string requesturl = "tenantuserstatus/update";

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturl, tenantUserAppLastAccessInfoRequestDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, null);
      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
      bool firstLogin = await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);
      if(firstLogin) {
        if(tenantUserAppLastAccessInfoRequestDTO.AppKey == AppKeyEnum.pay.ToString()) {
          BusinessOnBoardNotificationDTO businessPayUserOnBoardNotificationDTO = await _qBusinessAndUserDS.GetBusinessOnBoardDetailByAppAndBusinessAndUserIdAsync(tenantUserAppLastAccessInfoRequestDTO.AppKey, tenantUserAppLastAccessInfoRequestDTO.TenantId, tenantUserAppLastAccessInfoRequestDTO.TenantUserId);
          if(businessPayUserOnBoardNotificationDTO != null) {
            businessPayUserOnBoardNotificationDTO.UserSessionInfo = _userSessionManager.GetSession();
            await _bizPaymentNotificationHandler.SendBizPaymentUserOnBoardNotificationAsync(businessPayUserOnBoardNotificationDTO);
          }
        }
        if(tenantUserAppLastAccessInfoRequestDTO.AppKey == AppKeyEnum.cust.ToString()) {
          BusinessOnBoardNotificationDTO businessCustUserOnBoardNotificationDTO = await _qBusinessAndUserDS.GetBusinessOnBoardDetailByAppAndBusinessAndUserIdAsync(tenantUserAppLastAccessInfoRequestDTO.AppKey, tenantUserAppLastAccessInfoRequestDTO.TenantId, tenantUserAppLastAccessInfoRequestDTO.TenantUserId);
          if(businessCustUserOnBoardNotificationDTO != null) {
            businessCustUserOnBoardNotificationDTO.UserSessionInfo = _userSessionManager.GetSession();
            await _bizPaymentNotificationHandler.SendBizCustUserOnBoardNotificationAsync(businessCustUserOnBoardNotificationDTO);
          }
        }
        if(tenantUserAppLastAccessInfoRequestDTO.AppKey == AppKeyEnum.vend.ToString()) {
          BusinessOnBoardNotificationDTO businessVendUserOnBoardNotificationDTO = await _qBusinessAndUserDS.GetBusinessOnBoardDetailByAppAndBusinessAndUserIdAsync(tenantUserAppLastAccessInfoRequestDTO.AppKey, tenantUserAppLastAccessInfoRequestDTO.TenantId, tenantUserAppLastAccessInfoRequestDTO.TenantUserId);
          if(businessVendUserOnBoardNotificationDTO != null) {
            businessVendUserOnBoardNotificationDTO.UserSessionInfo = _userSessionManager.GetSession();
            await _bizPaymentNotificationHandler.SendBizCustUserOnBoardNotificationAsync(businessVendUserOnBoardNotificationDTO);
          }
        }
        if(tenantUserAppLastAccessInfoRequestDTO.AppKey == AppKeyEnum.biz.ToString()) {
          // BusinessOnBoardNotificationDTO businessSetupUserOnBoardNotificationDTO = await _qBusinessAndUserDS.GetBusinessOnBoardDetailByAppAndBusinessAndUserIdAsync(tenantUserAppLastAccessInfoRequestDTO.AppKey, tenantUserAppLastAccessInfoRequestDTO.TenantId, tenantUserAppLastAccessInfoRequestDTO.TenantUserId);
          BusinessOnBoardNotificationDTO businessSetupUserOnBoardNotificationDTO = await _qBusinessAndUserDS.GetBusinessSetupOnBoardDetailByAppAndBusinessAndUserIdAsync(tenantUserAppLastAccessInfoRequestDTO.AppKey, tenantUserAppLastAccessInfoRequestDTO.TenantId, tenantUserAppLastAccessInfoRequestDTO.TenantUserId);
          if(businessSetupUserOnBoardNotificationDTO != null) {
            businessSetupUserOnBoardNotificationDTO.UserSessionInfo = _userSessionManager.GetSession();
            await _bizPaymentNotificationHandler.SendBizSetupUserOnBoardNotificationAsync(businessSetupUserOnBoardNotificationDTO);
          }
        }
      }

      return firstLogin;
    }
  }
}