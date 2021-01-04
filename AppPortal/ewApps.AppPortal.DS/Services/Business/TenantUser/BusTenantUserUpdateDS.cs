using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.CommonService;
using ewApps.Core.ExceptionService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
    public class BusTenantUserUpdateDS:IBusTenantUserUpdateDS {

        #region Local Member 

        IUserSessionManager _userSessionManager;
        AppPortalAppSettings _appSettings;
        IQPlatformAndUserDS _qPlatformAndUserDS;
        IRoleDS _roleDS;
        IRoleLinkingDS _roleLinkingDS;
        IUnitOfWork _unitOfWork;
        ITenantUserAppPreferenceDS _tenantUserAppPreferenceDS;
        IBusinessNotificationHandler _businessNotificationHandler;
        ITokenInfoDS _tokenInfoDS;
        IQBusinessAndUserDS _businessAndUserDS;
        IBizNotificationHandler _bizNotificationHandler;
        IBusUserPreferenceDS _busUserPreferenceDS;
        IQBusinessAppDS _qBusinessAppDS;
        #endregion Local Member

        #region Constructor

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        public BusTenantUserUpdateDS(IUserSessionManager userSessionManager, IBusinessNotificationHandler businessNotificationHandler,
            IRoleLinkingDS roleLinkingDS, ITenantUserAppPreferenceDS tenantUserAppPreferenceDS,
            IQPlatformAndUserDS qPlatformAndUserDS, IRoleDS roleDS, ITokenInfoDS tokenInfoDS,
            IQBusinessAndUserDS businessAndUserDS, IBizNotificationHandler bizNotificationHandler,
        IOptions<AppPortalAppSettings> appSettings, IUnitOfWork unitOfWork, IBusUserPreferenceDS busUserPreferenceDS, IQBusinessAppDS qBusinessAppDS) {
            _userSessionManager = userSessionManager;
            _appSettings = appSettings.Value;
            _qPlatformAndUserDS = qPlatformAndUserDS;
            _roleDS = roleDS;
            _roleLinkingDS = roleLinkingDS;
            _unitOfWork = unitOfWork;
            _businessNotificationHandler = businessNotificationHandler;
            _tokenInfoDS = tokenInfoDS;
            _tenantUserAppPreferenceDS = tenantUserAppPreferenceDS;
            _businessAndUserDS = businessAndUserDS;
            _bizNotificationHandler = bizNotificationHandler;
            _busUserPreferenceDS = busUserPreferenceDS;
            _qBusinessAppDS = qBusinessAppDS;
        }

        #endregion Constructor

        #region Public Method

        ///<inheritdoc/>
        public async Task<UpdateTenantUserResponseDTO> UpdateTenantUserAsync(TenantUserUpdateRequestDTO tenantUserUpdateRequestDTO, CancellationToken cancellationToken = default(CancellationToken)) {

            UserSession userSession = _userSessionManager.GetSession();

            string baseuri = _appSettings.AppMgmtApiUrl;
            string requesturl = "tenantuser/updatebusinessuser";

            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturl, tenantUserUpdateRequestDTO, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParams);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            //API calling.
            UpdateTenantUserResponseDTO updateTenantUserResponseDTO = await httpRequestProcessor.ExecuteAsync<UpdateTenantUserResponseDTO>(requestOptions, false);

            // Get the list of application to be added update and delete.
            List<UserAppRelationDTO> addAppList = tenantUserUpdateRequestDTO.UserAppRelationDTOList.Where(a => a.OperationType == OperationType.Add).ToList();
            List<UserAppRelationDTO> deleteAppList = tenantUserUpdateRequestDTO.UserAppRelationDTOList.Where(a => a.OperationType == OperationType.Delete).ToList();
            List<UserAppRelationDTO> updateAppList = tenantUserUpdateRequestDTO.UserAppRelationDTOList.Where(a => a.OperationType == OperationType.Update).ToList();

            #region Add
            List<AppShortInfoDTO> appShortInfoDTO = new List<AppShortInfoDTO>();
            int deletedflag = 0;
            foreach(UserAppRelationDTO addApp in addAppList) {
                await AddRoleLinkingAndAppPrefrencesForUserAsync(addApp, tenantUserUpdateRequestDTO.TenantId, tenantUserUpdateRequestDTO.TenantUserId, userSession.TenantUserId, cancellationToken);                
                appShortInfoDTO.Add(await _qBusinessAppDS.GetAppShortInfoByAppId(addApp.AppId, tenantUserUpdateRequestDTO.TenantId));
                appShortInfoDTO[deletedflag].Deleted = false;
                deletedflag++;
            }

            
            

            #endregion Add

            #region Update
            bool permissionChange = false;
            foreach(UserAppRelationDTO updateApp in updateAppList) {
                if(!permissionChange) {
                    permissionChange = await UpdateRoleForTenantUser(updateApp, tenantUserUpdateRequestDTO.TenantId, tenantUserUpdateRequestDTO.TenantUserId, userSession.TenantUserId, cancellationToken);
                    //If Any Application Permission changed we have to send mail 
                    if(permissionChange)
                    await GenerateAppPermissionChangeNotification(userSession.TenantUserId, tenantUserUpdateRequestDTO.TenantUserId, tenantUserUpdateRequestDTO.TenantId, updateApp.AppKey);
                }
                else {
                    bool permissioNotChange = await UpdateRoleForTenantUser(updateApp, tenantUserUpdateRequestDTO.TenantId, tenantUserUpdateRequestDTO.TenantUserId, userSession.TenantUserId, cancellationToken);
                    if(permissioNotChange)
                        await GenerateAppPermissionChangeNotification(userSession.TenantUserId, tenantUserUpdateRequestDTO.TenantUserId, tenantUserUpdateRequestDTO.TenantId, updateApp.AppKey);
                    

                }

            }
            updateTenantUserResponseDTO.PermissionsChanged = permissionChange;

            //TenantUserAndAppViewDTO userInfoDTO = await _businessAndUserDS.get .GetTenantUserDetails(tenantUserUpdateRequestDTO.TenantUserId, tenantUserUpdateRequestDTO.TenantId, tenantUserUpdateRequestDTO.UserAppRelationDTOList[0].AppKey);
            //userInfoDTO.

            //// send status change notification to biz user if user stats is changed.
            //if (tenantUserUpdateRequestDTO.UserAppRelationDTOList[0].Active.Equals(true)) {
            //    await GenerateUserStatusChangeNotification(tenantUserUpdateRequestDTO, userSession);
            //}

            #endregion Update

            #region Delete
            deletedflag = 0;
            foreach(UserAppRelationDTO deleteApp in deleteAppList) {
                //await GenerateAppReomoveNotification(tenantUserUpdateRequestDTO.TenantUserId, tenantUserUpdateRequestDTO.TenantId, deleteApp.AppKey);
                appShortInfoDTO.Add(await _qBusinessAppDS.GetAppShortInfoByAppId(deleteApp.AppId, tenantUserUpdateRequestDTO.TenantId));
                appShortInfoDTO[deletedflag].Deleted = true;
                deletedflag++;
                await DeleteRoleLinkingAndAppPrefrencesForUserAsync(deleteApp, tenantUserUpdateRequestDTO.TenantId, tenantUserUpdateRequestDTO.TenantUserId, userSession.TenantUserId, cancellationToken);
                
            }

            if(addAppList.Count > 0 || deleteAppList.Count > 0 || (addAppList.Count > 0 && deleteAppList.Count > 0)) {
                await GenerateAppAddAndReomoveNotification(appShortInfoDTO, tenantUserUpdateRequestDTO.TenantUserId, tenantUserUpdateRequestDTO.TenantId);
            }

            #endregion Delete

            _unitOfWork.SaveAll();

      // new changes for delete session if permision change
      if(permissionChange)
      await _userSessionManager.DeletedByUserIdAndTenantId(tenantUserUpdateRequestDTO.TenantUserId, tenantUserUpdateRequestDTO.TenantId);

      return updateTenantUserResponseDTO;
        }

        #endregion Public Method

        #region Private Method

        private async Task AddRoleLinkingAndAppPrefrencesForUserAsync(UserAppRelationDTO userAppRelationDTO, Guid tenantId, Guid tenantUserId, Guid createdBy, CancellationToken cancellationToken = default(CancellationToken)) {

            UserSession userSession = _userSessionManager.GetSession();
// Business Portal - Business Setup App
            if(userAppRelationDTO.AppKey.Equals(AppKeyEnum.biz.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                await AddUserRoleAndRoleLinkingAsync(tenantId, tenantUserId, userAppRelationDTO.AppId, createdBy, userAppRelationDTO.PermissionBitMask, cancellationToken);

                // ToDo: Review code to create tenant user preference entry.
                //await AddTenantUserAppPreferncesAsync(tenantId, tenantUserId, userAppRelationDTO.AppId, createdBy, (long)BusinessUserEmailPrefrenceEnum.All, (long)BusinessUserSMSPrefrenceEnum.All, cancellationToken);

                //--old code
                //await AddTenantUserAppPreferncesAsync(tenantId, tenantUserId, userAppRelationDTO.AppId, createdBy, (long)BusinessUserBusinessSetupAppPreferenceEnum.All, (long)BusinessUserBusinessSetupAppPreferenceEnum.All, (long)BusinessUserBusinessSetupAppPreferenceEnum.All, cancellationToken);

                ////--new code
                //await _busUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId, createdBy, userAppRelationDTO.AppId, (long)BusinessUserBusinessSetupAppPreferenceEnum.AllAPPreference, (long)BusinessUserBusinessSetupAppPreferenceEnum.AllAPPreference, (long)BusinessUserBusinessSetupAppPreferenceEnum.AllAPPreference, cancellationToken);
                //await _busUserPreferenceDS.AddBEPrefValue(tenantId, tenantUserId, createdBy, userAppRelationDTO.AppId, userSession.ID.ToString(), (long)BusinessUserBusinessSetupAppPreferenceEnum.AllBEPreference, (long)BusinessUserBusinessSetupAppPreferenceEnum.AllBEPreference, (long)BusinessUserBusinessSetupAppPreferenceEnum.AllBEPreference, cancellationToken);
                await _busUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId, createdBy, userAppRelationDTO.AppId, (long)BusinessUserBusinessSetupAppPreferenceEnum.AllEmail, (long)BusinessUserBusinessSetupAppPreferenceEnum.AllSMS, (long)BusinessUserBusinessSetupAppPreferenceEnum.AllAS, cancellationToken);
            }
            // Business Portal - Business Payment App
            else if(userAppRelationDTO.AppKey.Equals(AppKeyEnum.pay.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                TenantUserAppManagmentDTO tenantUserAppManagmentDTO = new TenantUserAppManagmentDTO();
                tenantUserAppManagmentDTO.AppId = userAppRelationDTO.AppId;
                tenantUserAppManagmentDTO.CreatedBy = createdBy;
                tenantUserAppManagmentDTO.TenantId = tenantId;
                tenantUserAppManagmentDTO.TenantUserId = tenantUserId;
                tenantUserAppManagmentDTO.UserType = (int)UserTypeEnum.Business;
                tenantUserAppManagmentDTO.PermissionBitMask = userAppRelationDTO.PermissionBitMask;

                ////New code
                //tenantUserAppManagmentDTO.EmailPreference = (long)BusinessUserPaymentAppPreferenceEnum.AllPayPreference;
                //tenantUserAppManagmentDTO.SMSPreference = (long)BusinessUserPaymentAppPreferenceEnum.AllPayPreference;
                //tenantUserAppManagmentDTO.ASPreference = (long)BusinessUserPaymentAppPreferenceEnum.AllPayPreference;

                tenantUserAppManagmentDTO.EmailPreference = (long)BusinessUserPaymentAppPreferenceEnum.AllEmail;
                tenantUserAppManagmentDTO.SMSPreference = (long)BusinessUserPaymentAppPreferenceEnum.AllSMS ;
                tenantUserAppManagmentDTO.ASPreference = (long)BusinessUserPaymentAppPreferenceEnum.AllAS;

                RequestOptions requestOptions = new RequestOptions();
                requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
                requestOptions.Method = "businessuser/appassign";
                requestOptions.MethodData = tenantUserAppManagmentDTO;
                requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
                requestOptions.ServiceRequestType = RequestTypeEnum.Post;
                requestOptions.BearerTokenInfo = new BearerTokenOption();
                requestOptions.BearerTokenInfo.AppClientName = _appSettings.AppName;
                requestOptions.BearerTokenInfo.AuthServiceUrl = _appSettings.IdentityServerUrl;
                ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSettings.PaymentApiUrl);
                await httpRequestProcessor.ExecuteAsync<BusinessTenantSignUpResponseDTO>(requestOptions, false);

                ////New code
                //await _busUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId, createdBy, userAppRelationDTO.AppId, (long)BusinessUserPaymentAppPreferenceEnum.AllAPPreference, (long)BusinessUserPaymentAppPreferenceEnum.AllAPPreference, (long)BusinessUserPaymentAppPreferenceEnum.AllAPPreference, cancellationToken);
                //await _busUserPreferenceDS.AddBEPrefValue(tenantId, tenantUserId, createdBy, userAppRelationDTO.AppId, userSession.ID.ToString(), (long)BusinessUserPaymentAppPreferenceEnum.AllBEPreference, (long)BusinessUserPaymentAppPreferenceEnum.AllBEPreference, (long)BusinessUserPaymentAppPreferenceEnum.AllBEPreference, cancellationToken);
            }
            // Business Portal - Business Ship App
            else if(userAppRelationDTO.AppKey.Equals(AppKeyEnum.ship.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                TenantUserAppManagmentDTO tenantUserAppManagmentDTO = new TenantUserAppManagmentDTO();
                tenantUserAppManagmentDTO.AppId = userAppRelationDTO.AppId;
                tenantUserAppManagmentDTO.CreatedBy = createdBy;
                tenantUserAppManagmentDTO.TenantId = tenantId;
                tenantUserAppManagmentDTO.TenantUserId = tenantUserId;
                tenantUserAppManagmentDTO.UserType = (int)UserTypeEnum.Business;
                tenantUserAppManagmentDTO.PermissionBitMask = userAppRelationDTO.PermissionBitMask;

                RequestOptions requestOptions = new RequestOptions();
                requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
                requestOptions.Method = "businessuser/appassign";
                requestOptions.MethodData = tenantUserAppManagmentDTO;
                requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
                requestOptions.ServiceRequestType = RequestTypeEnum.Post;
                requestOptions.BearerTokenInfo = new BearerTokenOption();
                requestOptions.BearerTokenInfo.AppClientName = _appSettings.AppName;
                requestOptions.BearerTokenInfo.AuthServiceUrl = _appSettings.IdentityServerUrl;
                ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSettings.ShipmentApiUrl);
                await httpRequestProcessor.ExecuteAsync<BusinessTenantSignUpResponseDTO>(requestOptions, false);
            }
            // Business Portal - Business Customer App
            else if(userAppRelationDTO.AppKey.Equals(AppKeyEnum.cust.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                await AddUserRoleAndRoleLinkingAsync(tenantId, tenantUserId, userAppRelationDTO.AppId, createdBy, userAppRelationDTO.PermissionBitMask, cancellationToken);

                //--old code
                //await AddTenantUserAppPreferncesAsync(tenantId, tenantUserId, userAppRelationDTO.AppId, createdBy, (long)BusinessUserCustomerAppPreferenceEnum.All, (long)BusinessUserCustomerAppPreferenceEnum.All, (long)BusinessUserCustomerAppPreferenceEnum.All, cancellationToken);

                ////--new code
                //await _busUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId, createdBy, userAppRelationDTO.AppId, (long)BusinessUserCustomerAppPreferenceEnum.AllAPPreference, (long)BusinessUserCustomerAppPreferenceEnum.AllAPPreference, (long)BusinessUserCustomerAppPreferenceEnum.AllAPPreference, cancellationToken);
                //await _busUserPreferenceDS.AddBEPrefValue(tenantId, tenantUserId, createdBy, userAppRelationDTO.AppId, userSession.ID.ToString(), (long)BusinessUserCustomerAppPreferenceEnum.AllBEPreference, (long)BusinessUserCustomerAppPreferenceEnum.AllBEPreference, (long)BusinessUserCustomerAppPreferenceEnum.AllBEPreference, cancellationToken);
                //await _busUserPreferenceDS.AddPaymentPrefValue(tenantId, tenantUserId, createdBy, userAppRelationDTO.AppId, userSession.ID.ToString(), (long)BusinessUserCustomerAppPreferenceEnum.AllPayPreference, (long)BusinessUserCustomerAppPreferenceEnum.AllPayPreference, (long)BusinessUserCustomerAppPreferenceEnum.AllPayPreference, cancellationToken);

                await _busUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId, createdBy, userAppRelationDTO.AppId, (long)BusinessUserCustomerAppPreferenceEnum.AllEmail, (long)BusinessUserCustomerAppPreferenceEnum.AllSMS, (long)BusinessUserCustomerAppPreferenceEnum.AllAS, cancellationToken);
            }
      // Business Portal - Business Vendor App
      else if(userAppRelationDTO.AppKey.Equals(AppKeyEnum.vend.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
        await AddUserRoleAndRoleLinkingAsync(tenantId, tenantUserId, userAppRelationDTO.AppId, createdBy, userAppRelationDTO.PermissionBitMask, cancellationToken);
       
        await _busUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId, createdBy, userAppRelationDTO.AppId, (long)BusinessUserVendorAppPreferenceEnum.AllEmail, (long)BusinessUserVendorAppPreferenceEnum.AllSMS, (long)BusinessUserVendorAppPreferenceEnum.AllAS, cancellationToken);
      }
    }

        private async Task AddUserRoleAndRoleLinkingAsync(Guid tenantId, Guid tenantUserId, Guid appId, Guid createdBy, long permissionbitMask, CancellationToken cancellationToken) {
            // Get role/add role based on input permission mask bit.
            Guid roleId = await _roleDS.GetOrCreateRoleAsync(permissionbitMask, appId, (int)UserTypeEnum.Business, createdBy, cancellationToken);

            // Add Role linking entry for primary user.
            RoleLinking roleLinking = new RoleLinking();
            roleLinking.RoleId = roleId;
            roleLinking.AppId = appId;
            roleLinking.TenantId = tenantId;
            roleLinking.TenantUserId = tenantUserId;
            roleLinking.CreatedBy = createdBy;
            roleLinking.CreatedOn = DateTime.UtcNow;
            roleLinking.UpdatedBy = roleLinking.CreatedBy;
            roleLinking.UpdatedOn = roleLinking.CreatedOn;
            await _roleLinkingDS.AddAsync(roleLinking, cancellationToken);
        }

        private async Task AddTenantUserAppPreferncesAsync(Guid tenantId, Guid tenantUserId, Guid appId, Guid createdBy, long emailPrefrence, long smsPreference, long asPreference, CancellationToken cancellationToken) {
            // Add tenant user preference entry.
            TenantUserAppPreference tenantUserAppPreference = new TenantUserAppPreference();
            tenantUserAppPreference.ID = Guid.NewGuid();
            tenantUserAppPreference.AppId = appId;
            tenantUserAppPreference.CreatedBy = createdBy;
            tenantUserAppPreference.CreatedOn = DateTime.UtcNow;
            tenantUserAppPreference.Deleted = false;
            tenantUserAppPreference.EmailPreference = emailPrefrence;
            tenantUserAppPreference.SMSPreference = smsPreference;
            tenantUserAppPreference.ASPreference = asPreference;
            tenantUserAppPreference.TenantId = tenantId;
            tenantUserAppPreference.TenantUserId = tenantUserId;
            tenantUserAppPreference.UpdatedBy = tenantUserAppPreference.CreatedBy;
            tenantUserAppPreference.UpdatedOn = tenantUserAppPreference.CreatedOn;
            await _tenantUserAppPreferenceDS.AddAsync(tenantUserAppPreference, cancellationToken);
        }

        private async Task<bool> UpdateRoleForTenantUser(UserAppRelationDTO userAppRelationDTO, Guid tenantId, Guid tenantUserId, Guid createdBy, CancellationToken cancellationToken = default(CancellationToken)) {

            UserSession userSession = _userSessionManager.GetSession();
            bool permissionUpdate = false;
            if(userAppRelationDTO.AppKey.Equals(AppKeyEnum.pay.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                TenantUserAppManagmentDTO roleLinkingAndPreferneceDTO = new TenantUserAppManagmentDTO();
                roleLinkingAndPreferneceDTO.AppId = userAppRelationDTO.AppId;
                roleLinkingAndPreferneceDTO.CreatedBy = createdBy;
                roleLinkingAndPreferneceDTO.TenantId = tenantId;
                roleLinkingAndPreferneceDTO.TenantUserId = tenantUserId;
                roleLinkingAndPreferneceDTO.UserType = (int)UserTypeEnum.Business;
                roleLinkingAndPreferneceDTO.PermissionBitMask = userAppRelationDTO.PermissionBitMask;

                RequestOptions requestOptions = new RequestOptions();
                requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
                requestOptions.Method = "businessuser/updaterole";
                requestOptions.MethodData = roleLinkingAndPreferneceDTO;
                requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
                requestOptions.ServiceRequestType = RequestTypeEnum.Put;
                requestOptions.BearerTokenInfo = new BearerTokenOption();
                requestOptions.BearerTokenInfo.AppClientName = _appSettings.AppName;
                requestOptions.BearerTokenInfo.AuthServiceUrl = _appSettings.IdentityServerUrl;
                List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
                headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
                requestOptions.HeaderParameters = headerParams;
                ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSettings.PaymentApiUrl);
                RoleUpdateResponseDTO roleUpdateResponseDTO = await httpRequestProcessor.ExecuteAsync<RoleUpdateResponseDTO>(requestOptions, false);
                if(roleUpdateResponseDTO != null) {
                    permissionUpdate = roleUpdateResponseDTO.RoleUpdated;
                }

            }
            else if(userAppRelationDTO.AppKey.Equals(AppKeyEnum.ship.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                TenantUserAppManagmentDTO roleLinkingAndPreferneceDTO = new TenantUserAppManagmentDTO();
                roleLinkingAndPreferneceDTO.AppId = userAppRelationDTO.AppId;
                roleLinkingAndPreferneceDTO.CreatedBy = createdBy;
                roleLinkingAndPreferneceDTO.TenantId = tenantId;
                roleLinkingAndPreferneceDTO.TenantUserId = tenantUserId;
                roleLinkingAndPreferneceDTO.UserType = (int)UserTypeEnum.Business;
                roleLinkingAndPreferneceDTO.PermissionBitMask = userAppRelationDTO.PermissionBitMask;

                RequestOptions requestOptions = new RequestOptions();
                requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
                requestOptions.Method = "businessuser/updaterole";
                requestOptions.MethodData = roleLinkingAndPreferneceDTO;
                requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
                requestOptions.ServiceRequestType = RequestTypeEnum.Put;
                requestOptions.BearerTokenInfo = new BearerTokenOption();
                requestOptions.BearerTokenInfo.AppClientName = _appSettings.AppName;
                requestOptions.BearerTokenInfo.AuthServiceUrl = _appSettings.IdentityServerUrl;
                List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
                headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
                requestOptions.HeaderParameters = headerParams;
                ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSettings.ShipmentApiUrl);
                RoleUpdateResponseDTO roleUpdateResponseDTO = await httpRequestProcessor.ExecuteAsync<RoleUpdateResponseDTO>(requestOptions, false);
                if(roleUpdateResponseDTO != null) {
                    permissionUpdate = roleUpdateResponseDTO.RoleUpdated;
                }
            }
            else {
                // Get role/add role and add a entry in rolelinking table  // Add/Update  rolelinking for the user.
                Guid roleId = await _roleDS.GetOrCreateRoleAsync(userAppRelationDTO.PermissionBitMask, userAppRelationDTO.AppId, (int)UserTypeEnum.Business, tenantUserId);

                RoleLinking roleLinking = await _roleLinkingDS.FindAsync(a => a.TenantUserId == tenantUserId && a.AppId == userAppRelationDTO.AppId && a.TenantId == tenantId && a.Deleted == false);
                if(roleLinking != null && (roleLinking.RoleId != roleId)) {

                    // get role
                    Role role = await _roleDS.GetAsync(roleId);

                    // Delete user session if permissions are changed
                    permissionUpdate = true;
                    await ValidationOnUpdateUser(tenantUserId, tenantId, (int)UserTypeEnum.Business, userAppRelationDTO.AppId, null, role.RoleKey);
                    //await _userSessionManager.DeletedByAppUserAndAppId(tenantUserId, userAppRelationDTO.AppId);

                    roleLinking.RoleId = roleId;
                    _roleLinkingDS.UpdateSystemFieldsByOpType(roleLinking, OperationType.Update);
                    await _roleLinkingDS.UpdateAsync(roleLinking, roleLinking.ID);
                    _unitOfWork.SaveAll();
                }
            }
            return permissionUpdate;
        }

        private async Task DeleteRoleLinkingAndAppPrefrencesForUserAsync(UserAppRelationDTO userAppRelationDTO, Guid tenantId, Guid tenantUserId, Guid createdBy, CancellationToken cancellationToken = default(CancellationToken)) {

            UserSession userSession = _userSessionManager.GetSession();

            if(userAppRelationDTO.AppKey.Equals(AppKeyEnum.biz.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                RoleLinking roleLinking = await _roleLinkingDS.FindAsync(RL => RL.TenantUserId == tenantUserId && RL.TenantId == tenantId && RL.AppId == userAppRelationDTO.AppId && RL.Deleted == false);
                if(roleLinking != null) {
                    roleLinking.Deleted = true;
                    await _roleLinkingDS.UpdateAsync(roleLinking, roleLinking.ID);
                }
                TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceDS.FindAsync(RL => RL.TenantUserId == tenantUserId && RL.TenantId == tenantId && RL.AppId == userAppRelationDTO.AppId && RL.Deleted == false);
                if(tenantUserAppPreference != null) {
                    tenantUserAppPreference.Deleted = true;
                    await _tenantUserAppPreferenceDS.UpdateAsync(tenantUserAppPreference, tenantUserAppPreference.ID);
                }
            }
            else if(userAppRelationDTO.AppKey.Equals(AppKeyEnum.pay.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                // Call payment api to deassign 
                TenantUserAppManagmentDTO roleLinkingAndPreferneceDTO = new TenantUserAppManagmentDTO();
                roleLinkingAndPreferneceDTO.AppId = userAppRelationDTO.AppId;
                roleLinkingAndPreferneceDTO.CreatedBy = createdBy;
                roleLinkingAndPreferneceDTO.TenantId = tenantId;
                roleLinkingAndPreferneceDTO.TenantUserId = tenantUserId;
                roleLinkingAndPreferneceDTO.UserType = (int)UserTypeEnum.Business;
                roleLinkingAndPreferneceDTO.PermissionBitMask = userAppRelationDTO.PermissionBitMask;

                RequestOptions requestOptions = new RequestOptions();
                requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
                requestOptions.Method = "businessuser/appdeassign";
                requestOptions.MethodData = roleLinkingAndPreferneceDTO;
                requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
                requestOptions.ServiceRequestType = RequestTypeEnum.Put;
                requestOptions.BearerTokenInfo = new BearerTokenOption();
                requestOptions.BearerTokenInfo.AppClientName = _appSettings.AppName;
                requestOptions.BearerTokenInfo.AuthServiceUrl = _appSettings.IdentityServerUrl;
                List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
                headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
                requestOptions.HeaderParameters = headerParams;
                ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSettings.PaymentApiUrl);
                await httpRequestProcessor.ExecuteAsync<BusinessTenantSignUpResponseDTO>(requestOptions, false);
            }
            else if(userAppRelationDTO.AppKey.Equals(AppKeyEnum.ship.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                // call shipmetn api to deassign
                TenantUserAppManagmentDTO roleLinkingAndPreferneceDTO = new TenantUserAppManagmentDTO();
                roleLinkingAndPreferneceDTO.AppId = userAppRelationDTO.AppId;
                roleLinkingAndPreferneceDTO.CreatedBy = createdBy;
                roleLinkingAndPreferneceDTO.TenantId = tenantId;
                roleLinkingAndPreferneceDTO.TenantUserId = tenantUserId;
                roleLinkingAndPreferneceDTO.UserType = (int)UserTypeEnum.Business;
                roleLinkingAndPreferneceDTO.PermissionBitMask = userAppRelationDTO.PermissionBitMask;

                RequestOptions requestOptions = new RequestOptions();
                requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
                requestOptions.Method = "businessuser/appdeassign";
                requestOptions.MethodData = roleLinkingAndPreferneceDTO;
                requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
                requestOptions.ServiceRequestType = RequestTypeEnum.Put;
                requestOptions.BearerTokenInfo = new BearerTokenOption();
                requestOptions.BearerTokenInfo.AppClientName = _appSettings.AppName;
                requestOptions.BearerTokenInfo.AuthServiceUrl = _appSettings.IdentityServerUrl;
                List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
                headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
                requestOptions.HeaderParameters = headerParams;
                ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSettings.ShipmentApiUrl);
                await httpRequestProcessor.ExecuteAsync<BusinessTenantSignUpResponseDTO>(requestOptions, false);
            }
            else {
                RoleLinking roleLinking = await _roleLinkingDS.FindAsync(RL => RL.TenantUserId == tenantUserId && RL.TenantId == tenantId && RL.AppId == userAppRelationDTO.AppId && RL.Deleted == false);
                if(roleLinking != null) {
                    roleLinking.Deleted = true;
                    await _roleLinkingDS.UpdateAsync(roleLinking, roleLinking.ID);
                }
                TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceDS.FindAsync(RL => RL.TenantUserId == tenantUserId && RL.TenantId == tenantId && RL.AppId == userAppRelationDTO.AppId && RL.Deleted == false);
                if(tenantUserAppPreference != null) {
                    tenantUserAppPreference.Deleted = true;
                    await _tenantUserAppPreferenceDS.UpdateAsync(tenantUserAppPreference, tenantUserAppPreference.ID);
                }
            }
        }

        #region Validation 

        private async Task ValidationOnUpdateUser(Guid tenantUserID, Guid tenantId, int userType, Guid appId, Guid? businessPartnerTenantId, string roleKey) {

            Tuple<bool, Guid> lastAdmin = await _qPlatformAndUserDS.CheckUserIsLastAdminUserAsync(tenantId, userType, appId, businessPartnerTenantId);

            if(lastAdmin.Item1 && lastAdmin.Item2 == tenantUserID && (roleKey.ToLower() != ewApps.AppPortal.Common.AppPortalConstants.AdminRoleKey.ToLower())) {
                EwpError error = new EwpError();
                error.ErrorType = ErrorType.Security;
                EwpErrorData errorData = new EwpErrorData();
                errorData.ErrorSubType = (int)SecurityErrorSubType.Update;
                errorData.Message = "Can not change the last users admin rights";
                error.EwpErrorDataList.Add(errorData);
                EwpDuplicateNameException exc = new EwpDuplicateNameException("User permission can not be changed", error.EwpErrorDataList);
                throw exc;
            }
        }

        #endregion Validation 

        #region Notification

        private async Task GenerateAppReomoveNotification(Guid tenantUserId, Guid tenantId, string appkey) {
            UserSession userSession = _userSessionManager.GetSession();
            BusinessUserNotificationGeneralDTO businessAccountNotificationDTO = new BusinessUserNotificationGeneralDTO();
            businessAccountNotificationDTO = await _businessAndUserDS.GetBusinessUserNotificationGeneralDataAsync(tenantUserId, tenantId, appkey);
            if(businessAccountNotificationDTO != null) {
                businessAccountNotificationDTO.ActionDate = DateTime.Now;
                businessAccountNotificationDTO.userSession = userSession;
                await _bizNotificationHandler.GenerateAppReomoveNotification(businessAccountNotificationDTO);
            }
        }

        private async Task GenerateAppAddAndReomoveNotification(List<AppShortInfoDTO> appShortInfoDTOs, Guid tenantUserId, Guid tenantId) {
            UserSession userSession = _userSessionManager.GetSession();
            BusinessUserNotificationAppAccessUpdateDTO businessAccountNotificationDTO = new BusinessUserNotificationAppAccessUpdateDTO();
            businessAccountNotificationDTO = await _businessAndUserDS.GetBusinessUserNotificationGenDataAsync(tenantUserId, tenantId, userSession.TenantUserId);
            if(businessAccountNotificationDTO != null) {
                businessAccountNotificationDTO.ActionDate = DateTime.Now;
                businessAccountNotificationDTO.userSession = userSession;
                await _bizNotificationHandler.GenerateAppAddAndReomoveNotification(businessAccountNotificationDTO, appShortInfoDTOs);
            }
        }

        private async Task GenerateAppPermissionChangeNotification(Guid tenantUserId, Guid invitedTenantUserId, Guid tenantId, string appkey) {
            UserSession userSession = _userSessionManager.GetSession();
            BusinessUserPermissionNotificationGeneralDTO businessUserPermissionNotificationGeneralDTO = new BusinessUserPermissionNotificationGeneralDTO();
            businessUserPermissionNotificationGeneralDTO = await _businessAndUserDS.GetBusinessUserPermissionChangeNotificationGeneralDataAsync(tenantUserId, invitedTenantUserId, tenantId, appkey);
            if(businessUserPermissionNotificationGeneralDTO != null) {
               
               
                businessUserPermissionNotificationGeneralDTO.ActionDate = DateTime.Now;
                businessUserPermissionNotificationGeneralDTO.userSession = userSession;
                await _bizNotificationHandler.GenerateAppPermissionChangeNotification(businessUserPermissionNotificationGeneralDTO);
            }
        }

        // Method used to generate  user status change notification.
        private async Task GenerateUserStatusChangeNotification(TenantUserUpdateRequestDTO tenantUserUpdateDTO, UserSession userSession) {
          try
          {
            BusinessUserNotificationGeneralDTO businessUserAccountNotificationDTO = await _businessAndUserDS.GetBusinessUserNotificationGeneralDataAsync( tenantUserUpdateDTO.TenantUserId, tenantUserUpdateDTO.TenantId, tenantUserUpdateDTO.UserAppRelationDTOList[0].AppKey);

            BusinessAccountNotificationDTO businessAccountNotificationDTO = new BusinessAccountNotificationDTO();
            // Publisher publisher = await _publisherDS.FindAsync(p => p.TenantId == userSession.TenantId);
            Guid appId = tenantUserUpdateDTO.UserAppRelationDTOList[0].AppId;// Where(u => u.AppKey == AppKeyEnum.custsetup.ToString()).FirstOrDefault().AppId;

            businessAccountNotificationDTO.PasswordCode = "";
            businessAccountNotificationDTO.InvitedUserEmail = tenantUserUpdateDTO.Email;
            businessAccountNotificationDTO.InvitedUserIdentityUserId = Guid.Empty;
            businessAccountNotificationDTO.TenantId = userSession.TenantId;
            businessAccountNotificationDTO.InvitedUserAppKey = AppKeyEnum.biz.ToString();
            businessAccountNotificationDTO.InvitedUserId = tenantUserUpdateDTO.TenantUserId;
            businessAccountNotificationDTO.InvitedUserAppId = appId;
            businessAccountNotificationDTO.PublisherName = businessUserAccountNotificationDTO.PublisherName;
            businessAccountNotificationDTO.BusinessCompanyName = businessUserAccountNotificationDTO.BusinessCompanyName;
            businessAccountNotificationDTO.PartnerType = "Business";
            businessAccountNotificationDTO.BusinessPartnerCompanyName = " ";
            businessAccountNotificationDTO.InvitorUserName = userSession.UserName;
            businessAccountNotificationDTO.InvitedUserFullName = tenantUserUpdateDTO.FullName;//   tenantUserSignUpResponseDTO.TenantUserDTO.FullName;
            businessAccountNotificationDTO.ApplicationName = "Payment";
            businessAccountNotificationDTO.SubDomain = userSession.Subdomain;
            businessAccountNotificationDTO.PortalURL = string.Format(_appSettings.CustomerPortalClientURL, userSession.Subdomain);
            businessAccountNotificationDTO.CopyRightText = businessUserAccountNotificationDTO.CopyRightText;
            businessAccountNotificationDTO.UserSession = userSession;

            if(tenantUserUpdateDTO.UserAppRelationDTOList[0].Active == false)
              businessAccountNotificationDTO.NewUserStatus =   "Inactive";
            else if (tenantUserUpdateDTO.UserAppRelationDTOList[0].Active == true)
              businessAccountNotificationDTO.OldUserStatus = "Active";

            await _bizNotificationHandler.GenerateBusinessUserAccountStatusChangedNotification(businessAccountNotificationDTO, (int)BizNotificationEventEnum.BusinessUserAccountStatusChanged);
          }
          catch (Exception)
          {
            throw;
          }
    }

    #endregion Notification

    #endregion Private Method
  }
}
