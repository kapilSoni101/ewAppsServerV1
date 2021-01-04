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
using ewApps.Core.DMService;
using ewApps.Core.ExceptionService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
    /// <summary>
    /// This class defines methods to manage Publisher signup operations.
    /// </summary>
    /// <seealso cref="ewApps.AppPortal.DS.IPublisherSignUpDS" />
    public class PublisherSignUpDS:IPublisherSignUpDS {

        #region Private Members

        private IPublisherDS _publisherDS;
        private IPublisherAddressDS _publisherAddressDS;
        private IPubBusinessSubsPlanAppServiceDS _publisherAppServiceDS;
        private IPublisherAppSettingDS _publisherAppSettingDS;
        private IUserSessionManager _userSessionManager;
        private IRoleDS _roleDS;
        private IRoleLinkingDS _roleLinkingDS;
        private ITenantUserAppPreferenceDS _tenantUserAppPreferenceDS;
        private AppPortalAppSettings _appPortalAppSettings;
        private IEntityThumbnailDS _entityThumbnailDS;
        private IPlatformDS _platformDS;
        private IPlatformNotificationHandler _platformNotificationHandler;
        private IUnitOfWork _unitOfWork;
        private IPubBusinessSubsPlanDS _pubBusinessSubsPlanDS;
        private ITenantAppLinkingDS _tenantAppLinkingDS;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a instance of the <see cref="PublisherSignUpDS"/> class and it's dependencies.
        /// </summary>
        /// <param name="publisherDS">The publisher data service instance.</param>
        /// <param name="publisherAddressDS">The publisher address data service instance.</param>
        /// <param name="publisherAppSettingDS">The publisher application setting data service instance.</param>
        /// <param name="publisherAppServiceDS">The publisher application service data service instance.</param>
        /// <param name="appPortalAppSettingsOptions">The application portal application settings options.</param>
        /// <param name="userSessionManager">The user session manager.</param>
        /// <param name="roleDS">The role data service instance.</param>
        /// <param name="roleLinkingDS">The role linking data service instance.</param>
        /// <param name="tenantUserAppPreferenceDS">The tenant user application preference data service instance.</param>
        /// <param name="entityThumbnailDS">The entity thumbnail data service instance.</param>
        /// <param name="pubBusinessSubsPlanDS">The publisher business subscriptoin plan data service instance.</param>
        /// <param name="unitOfWork">The unit of work to manage transaction.</param>
        /// <param name="platformDS">The platform data service instance fro notification.</param>
        /// <param name="platformNotificationHandler">The notification handler for platfrom.</param>
        public PublisherSignUpDS(ITenantAppLinkingDS tenantAppLinkingDS, IPublisherDS publisherDS, IPlatformDS platformDS, IPublisherAddressDS publisherAddressDS, IPublisherAppSettingDS publisherAppSettingDS, IPubBusinessSubsPlanAppServiceDS publisherAppServiceDS, IOptions<AppPortalAppSettings> appPortalAppSettingsOptions, IUserSessionManager userSessionManager, IRoleDS roleDS, IRoleLinkingDS roleLinkingDS,
            IPlatformNotificationHandler platformNotificationHandler, ITenantUserAppPreferenceDS tenantUserAppPreferenceDS, IEntityThumbnailDS entityThumbnailDS, IPubBusinessSubsPlanDS pubBusinessSubsPlanDS, IUnitOfWork unitOfWork) {
            _publisherDS = publisherDS;
            _publisherAddressDS = publisherAddressDS;
            _publisherAppSettingDS = publisherAppSettingDS;
            _publisherAppServiceDS = publisherAppServiceDS;
            _userSessionManager = userSessionManager;
            _roleDS = roleDS;
            _roleLinkingDS = roleLinkingDS;
            _tenantUserAppPreferenceDS = tenantUserAppPreferenceDS;
            _appPortalAppSettings = appPortalAppSettingsOptions.Value;
            _entityThumbnailDS = entityThumbnailDS;
            _unitOfWork = unitOfWork;
            _platformDS = platformDS;
            _platformNotificationHandler = platformNotificationHandler;
            _pubBusinessSubsPlanDS = pubBusinessSubsPlanDS;
            _tenantAppLinkingDS = tenantAppLinkingDS;
        }

        #endregion

        /// <summary>
        /// This method generates Publisher tenant and also it's related primary user, application subscription and other system data. Also sends invites to publisher's primary user.
        /// </summary>
        /// <param name="pubRequestDTO">It contains publisher infomration to be signup.</param>
        /// <param name="cancellationToken">The async task cancellation token.</param>
        /// <returns>Returns signedup publisher tenant and primary user information.</returns>
        public async Task<TenantSignUpResponseDTO> PublisherSignUpAsync(PublisherSignUpRequestDTO pubRequestDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            TenantUserDTO tenantUser = null;
            List<AppInfoDTO> appList = null;
            AppInfoDTO pubApp = null;

            // Get current login session.
            UserSession userSession = _userSessionManager.GetSession();

            // Generates publisher tennat id. This id will be use to generate related data.
            Guid publisherTenantId = Guid.NewGuid();
            Guid publisherPrimaryUserId;

            #region Get Applicaiton Data From AppMgmt

            PublisherPreSignUpReqDTO publisherPreSignUpReqDTO = new PublisherPreSignUpReqDTO();

            publisherPreSignUpReqDTO.SubDomain = pubRequestDTO.SubDomain;
            publisherPreSignUpReqDTO.UserEmail = pubRequestDTO.PrimaryUserEmail;
            publisherPreSignUpReqDTO.AppKeyList = new List<string>();
            publisherPreSignUpReqDTO.AppIdList = new List<Guid>();

            foreach(var item in pubRequestDTO.ApplicationList) {
                publisherPreSignUpReqDTO.AppIdList.Add(item.AppID);
            }

            publisherPreSignUpReqDTO.AppKeyList.Add(AppKeyEnum.pub.ToString());

            // Check if user with same email id already exist.
            string methodUri = "tenant/publishersignup/validate";
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, methodUri, publisherPreSignUpReqDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);

            ServiceExecutor tenantUserServiceExecutor = new ServiceExecutor(_appPortalAppSettings.AppMgmtApiUrl);
            PublisherPreSignUpRespDTO publisherPreSignUpRespDTO = await tenantUserServiceExecutor.ExecuteAsync<PublisherPreSignUpRespDTO>(requestOptions, false);

            // If any validation get failed raise validation execption.
            if(publisherPreSignUpRespDTO.ErrorDataList != null && publisherPreSignUpRespDTO.ErrorDataList.Count > 0) {
                EwpValidationException validationException = new EwpValidationException("Validation Error", publisherPreSignUpRespDTO.ErrorDataList);
                throw validationException;
            }
            // Initialize local data fetched from AppMgmt API.
            else {
                tenantUser = publisherPreSignUpRespDTO.TenantUser;
                appList = publisherPreSignUpRespDTO.ApplicationList.FindAll(i => i.AppKey.ToLower() != AppKeyEnum.pub.ToString().ToLower());
                pubApp = publisherPreSignUpRespDTO.ApplicationList.First(i => i.AppKey.ToLower() == AppKeyEnum.pub.ToString().ToLower());

                // If user with same TenantUserId otherwise generate new id.
                if(tenantUser != null) {
                    publisherPrimaryUserId = tenantUser.TenantUserId;
                }
                else {
                    publisherPrimaryUserId = Guid.NewGuid();
                }
            }

            #endregion Get Applicaiton Data from AppMgmt

            #region Add PublihserAppSettings and Business App Thumbnail

            // Add PublisherAppSetting entry for each business application.
            foreach(PubAppSettingDTO pubAppSubs in pubRequestDTO.ApplicationList) {
                AppInfoDTO appInfo = appList.First(i => i.Id == pubAppSubs.AppID);

                await AddPublisherAppSettingsAsync(pubAppSubs, publisherTenantId, appInfo, cancellationToken);
            }

            #endregion Add Publihser AppSettings and Thumbnail

            #region Add Publisher

            // Initialize Publisher entity from request DTO.
            Publisher publisher = await AddPublisher(pubRequestDTO, publisherTenantId, cancellationToken);

            #endregion Add Publisher

            #region Tenant App Linking

            await AddTenantAppLinkingAsync(pubApp, publisherTenantId, cancellationToken);

            #endregion

            #region Add Publisher Address

            // Add Publisher Addresses
            foreach(PublisherAddressDTO pubAddress in pubRequestDTO.PublisherAddressDTO) {
                await _publisherAddressDS.AddPublisherAddressAsync(pubAddress, publisherTenantId, publisher.ID, cancellationToken);
            }

            #endregion Add Publisher Address

            #region Add RoleLinking

            await AddPrimaryUserRoleAndRoleLinking(userSession, publisherTenantId, publisherPrimaryUserId, pubApp, cancellationToken);

            #endregion add RoleLinking

            #region Add TenantUser App Preference

            await AddTenantUserAppPrefernces(publisherTenantId, publisherPrimaryUserId, pubApp, cancellationToken);

            #endregion Add TenantUser App Preference

            #region Publisher Signup On AppMgmt
            TenantSignUpResponseDTO signUpTenantResponseDTO = null;
            try {
                signUpTenantResponseDTO = await SignUpTenantForPublisher(pubRequestDTO, publisherTenantId, publisherPrimaryUserId, appList);

            }
            catch(Exception) {
                // ToDo: Log Error Here.
                // ToDo: Also call Api to rollback transactions.
                throw;
            }
            #endregion Publisher Signup On AppMgmt

            // Commit all database changes.
            _unitOfWork.SaveAll();

            #region Notification

            try {
                Platform platform = await _platformDS.FindAsync(p => p.TenantId == userSession.TenantId);

                PlatformNotificationDTO platformNotificationDTO = new PlatformNotificationDTO();
                platformNotificationDTO.PasswordCode = signUpTenantResponseDTO.TenantUserInfo.Code;
                platformNotificationDTO.InvitedUserEmail = signUpTenantResponseDTO.TenantUserInfo.Email;
                platformNotificationDTO.InvitedUserIdentityUserId = signUpTenantResponseDTO.TenantUserInfo.IdentityUserId;
                platformNotificationDTO.InvitedUserTenantId = signUpTenantResponseDTO.TenantInfo.TenantId;
                platformNotificationDTO.InvitedUserAppKey = pubApp.AppKey;
                platformNotificationDTO.InvitedUserId = signUpTenantResponseDTO.TenantUserInfo.TenantUserId;
                platformNotificationDTO.InvitedUserAppId = pubApp.Id;
                platformNotificationDTO.PublisherCompanyName = publisher.Name;
                platformNotificationDTO.PlatformCompanyName = platform.Name;
                platformNotificationDTO.InvitedUserFullName = signUpTenantResponseDTO.TenantUserInfo.FullName;
                platformNotificationDTO.SubDomain = pubRequestDTO.SubDomain;
                platformNotificationDTO.PublisherPortalURL = string.Format(_appPortalAppSettings.PublisherPortalClientURL, signUpTenantResponseDTO.TenantInfo.SubDomainName);
                platformNotificationDTO.CopyRigthText = platform.Copyright;
                platformNotificationDTO.TenantLanguage = publisher.Language;
                platformNotificationDTO.UserSession = userSession;
                await _platformNotificationHandler.SendPublisherPrimaryUserWithNewEmailInvitateAsync(platformNotificationDTO);
            }
            catch(Exception) {
                // ToDo: Handle Service Call Rollback Operation.
                //return;
            }

            #endregion Notification

            return signUpTenantResponseDTO;
        }

        public async Task ReInviteTenantUserAsync(TenantUserIdentificationDTO tenantUserIdentificationDTO) {

            // Get the current user session.
            UserSession userSession = _userSessionManager.GetSession();

            #region Get user data

            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = "tenantuser/info/" + tenantUserIdentificationDTO.TenantUserId.ToString();

            // Get user detials by calling th API.
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, requesturl, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            TenantUserInfoDTO tenantUserInfoDTO = await httpRequestProcessor.ExecuteAsync<TenantUserInfoDTO>(requestOptions, false);

            #endregion Get user data

            #region Notification

            try {
                Platform platform = await _platformDS.FindAsync(p => p.TenantId == userSession.TenantId);
                Publisher publisher = await _publisherDS.FindAsync(pub => pub.TenantId == tenantUserIdentificationDTO.TenantId);

                PlatformNotificationDTO platformNotificationDTO = new PlatformNotificationDTO();
                platformNotificationDTO.PasswordCode = tenantUserInfoDTO.Code;
                platformNotificationDTO.InvitedUserEmail = tenantUserInfoDTO.Email;
                platformNotificationDTO.InvitedUserIdentityUserId = tenantUserInfoDTO.IdentityUserId;
                platformNotificationDTO.InvitedUserTenantId = tenantUserIdentificationDTO.TenantId;
                platformNotificationDTO.InvitedUserAppKey = AppKeyEnum.pub.ToString();
                platformNotificationDTO.InvitedUserId = tenantUserInfoDTO.TenantUserId;
                platformNotificationDTO.InvitedUserAppId = tenantUserIdentificationDTO.AppId;
                platformNotificationDTO.PublisherCompanyName = publisher.Name;
                platformNotificationDTO.PlatformCompanyName = platform.Name;
                platformNotificationDTO.InvitedUserFullName = tenantUserInfoDTO.FullName;
                platformNotificationDTO.SubDomain = " ";
                platformNotificationDTO.PublisherPortalURL = string.Format(_appPortalAppSettings.PublisherPortalClientURL, " ");
                platformNotificationDTO.CopyRigthText = platform.Copyright;
                platformNotificationDTO.TenantLanguage = publisher.Language;
                platformNotificationDTO.UserSession = userSession;
                await _platformNotificationHandler.SendPublisherPrimaryUserWithNewEmailInvitateAsync(platformNotificationDTO);
            }
            catch(Exception) {
                // ToDo: Handle Service Call Rollback Operation.
                //return;
            }

            #endregion Notification

            #region Update invitaion status

            // APi calling to cahnge the status again
            requesturl = "tenantuser/updateinvitationstatus/" + (int)TenantUserInvitaionStatusEnum.Invited;
            requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturl, tenantUserIdentificationDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
            await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);

            #endregion Update invitaion status
        }

        /// <inheritdoc/>
        public async Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByAppIdAsync(Guid appId, BooleanFilterEnum planState, CancellationToken cancellationToken = default(CancellationToken)) {
            string methodUri = string.Format("subscriptionplan/list/{0}/{1}", appId.ToString(), planState);

            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            RequestOptions signupPubRequestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, methodUri, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);

            ServiceExecutor serviceExecutor = new ServiceExecutor(_appPortalAppSettings.AppMgmtApiUrl);
            return await serviceExecutor.ExecuteAsync<List<SubscriptionPlanInfoDTO>>(signupPubRequestOptions, false);
        }

        #region Private Methods

        private async Task<TenantSignUpResponseDTO> SignUpTenantForPublisher(PublisherSignUpRequestDTO pubRequestDTO, Guid publisherTenantId, Guid publisherPrimaryUserId, List<AppInfoDTO> appList) {
            UserSession userSession = _userSessionManager.GetSession();

            // Fill publisher signup request dto to generate tenant and primary user related data.
            PublisherSignUpDTO publisherSignUpDTO = new PublisherSignUpDTO();
            publisherSignUpDTO.AppInfoList = appList;
            publisherSignUpDTO.PublisherTenantId = publisherTenantId;
            publisherSignUpDTO.PublisherName = pubRequestDTO.PublisherName;
            publisherSignUpDTO.PrimaryTenantUserId = publisherPrimaryUserId;
            publisherSignUpDTO.PrimaryUserEmail = pubRequestDTO.PrimaryUserEmail;
            publisherSignUpDTO.PrimaryUserFirstName = pubRequestDTO.PrimaryUserFirstName;
            publisherSignUpDTO.PrimaryUserLastName = pubRequestDTO.PrimaryUserLastName;
            publisherSignUpDTO.PrimaryUserFullName = pubRequestDTO.PrimaryUserFullName;
            publisherSignUpDTO.SubDomain = pubRequestDTO.SubDomain;
            publisherSignUpDTO.Phone = pubRequestDTO.ContactPersonPhone;

            string methodUri = "tenant/publishersignup";
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            RequestOptions signupPubRequestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, methodUri, publisherSignUpDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);

            //RequestOptions signupPubRequestOptions = new RequestOptions();
            //signupPubRequestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            //signupPubRequestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            //signupPubRequestOptions.Method = "tenant/publishersignup";
            //signupPubRequestOptions.MethodData = publisherSignUpDTO;
            //signupPubRequestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            //signupPubRequestOptions.ServiceRequestType = RequestTypeEnum.Post;
            //signupPubRequestOptions.BearerTokenInfo = new BearerTokenOption();
            //signupPubRequestOptions.BearerTokenInfo.AppClientName = _appPortalAppSettings.AppName;
            //signupPubRequestOptions.BearerTokenInfo.AuthServiceUrl = _appPortalAppSettings.IdentityServerUrl;

            // Execute api to generate tenant and user data.
            ServiceExecutor serviceExecutor = new ServiceExecutor(_appPortalAppSettings.AppMgmtApiUrl);
            TenantSignUpResponseDTO signUpTenantResponseDTO = await serviceExecutor.ExecuteAsync<TenantSignUpResponseDTO>(signupPubRequestOptions, false);
            return signUpTenantResponseDTO;
        }

        private async Task<Publisher> AddPublisher(PublisherSignUpRequestDTO pubRequestDTO, Guid publisherTenantId, CancellationToken cancellationToken) {
            UserSession userSession = _userSessionManager.GetSession();

            ewApps.AppPortal.Entity.Publisher publisher = new ewApps.AppPortal.Entity.Publisher();
            publisher.Name = pubRequestDTO.PublisherName;
            publisher.Active = true;
            publisher.Deleted = false;
            publisher.ContactPersonDesignation = pubRequestDTO.ContactPersonDesignation;
            publisher.ContactPersonEmail = pubRequestDTO.ContactPersonEmail;
            publisher.ContactPersonPhone = pubRequestDTO.ContactPersonPhone;
            publisher.ContactPersonName = pubRequestDTO.ContactPersonName;
            publisher.Website = pubRequestDTO.Website;
            publisher.ApplyPoweredBy = pubRequestDTO.ApplyPoweredBy;
            publisher.CanUpdateCopyright = pubRequestDTO.CanUpdateCopyright;
            publisher.Copyright = pubRequestDTO.CopyrightText;
            publisher.CustomizedCopyright = false;
            publisher.CustomizedLogoThumbnail = false;
            publisher.CanUpdateCurrency = null;
            publisher.CurrencyCode = null;
            publisher.DateTimeFormat = null;
            publisher.DecimalPrecision = null;
            publisher.DecimalSeperator = null;
            publisher.GroupSeperator = null;
            publisher.GroupValue = null;
            publisher.Language = Core.BaseService.Constants.DefaultLanguage;
            publisher.LogoThumbnailId = pubRequestDTO.PlatformLogoThumbnailId;
            publisher.PoweredBy = pubRequestDTO.PoweredBy;
            publisher.TimeZone = null;
            publisher.TenantId = publisherTenantId;
            publisher.IdentityNumber = _publisherDS.GetNextMaxNo(userSession.TenantId, (int)AppPortalEntityTypeEnum.Publisher);
            _publisherDS.UpdateSystemFields(publisher, SystemFieldMask.AddOpSystemFields & ~SystemFieldMask.TenantId);
            await _publisherDS.AddAsync(publisher, cancellationToken);
            return publisher;
        }

        private async Task AddPrimaryUserRoleAndRoleLinking(UserSession userSession, Guid publisherTenantId, Guid publisherPrimaryUserId, AppInfoDTO pubApp, CancellationToken cancellationToken) {
            // Get role/add role based on input permission mask bit.
            Guid roleId = await _roleDS.GetOrCreateRoleAsync((long)PublisherUserPublisherAppPermissionEnum.All, pubApp.Id, (int)UserTypeEnum.Publisher, userSession.TenantUserId, cancellationToken);

            // Add Role linking entry for primary user.
            RoleLinking roleLinking = new RoleLinking();
            roleLinking.RoleId = roleId;
            roleLinking.AppId = pubApp.Id;
            roleLinking.TenantId = publisherTenantId;
            roleLinking.TenantUserId = publisherPrimaryUserId;
            _roleLinkingDS.UpdateSystemFields(roleLinking, SystemFieldMask.Id | SystemFieldMask.CreatedBy | SystemFieldMask.CreatedOn | SystemFieldMask.UpdatedBy | SystemFieldMask.UpdatedOn);
            await _roleLinkingDS.AddAsync(roleLinking, cancellationToken);
        }

        private async Task AddTenantUserAppPrefernces(Guid publisherTenantId, Guid publisherPrimaryUserId, AppInfoDTO pubApp, CancellationToken cancellationToken) {
            // Add tenant user preference entry.
            TenantUserAppPreference tenantUserAppPreference = new TenantUserAppPreference();
            tenantUserAppPreference.EmailPreference = (long)PublisherEmailPreferenceEnum.All;
            tenantUserAppPreference.SMSPreference = (long)PublisherSMSPreferenceEnum.All;
            tenantUserAppPreference.AppId = pubApp.Id;
            tenantUserAppPreference.TenantUserId = publisherPrimaryUserId;
            tenantUserAppPreference.TenantId = publisherTenantId;
            _tenantUserAppPreferenceDS.UpdateSystemFields(tenantUserAppPreference, SystemFieldMask.Id | SystemFieldMask.CreatedBy | SystemFieldMask.CreatedOn | SystemFieldMask.UpdatedBy | SystemFieldMask.UpdatedOn);
            await _tenantUserAppPreferenceDS.AddAsync(tenantUserAppPreference, cancellationToken);
        }

        // This method add PublisherAppSetting entry for each assigned Business applications.
        private async Task AddPublisherAppSettingsAsync(PubAppSettingDTO pubAppSettingDTO, Guid publisherTenantId, AppInfoDTO app, CancellationToken cancellationToken = default(CancellationToken)) {
            //// Add PublisherAppSetting entry for each business application.
            //foreach(PubAppSettingDTO pubAppSubs in pubRequestDTO.ApplicationList) {

            PublisherAppSetting publisherAppSetting = new PublisherAppSetting();
            publisherAppSetting.Active = true;
            publisherAppSetting.AppId = app.Id;
            publisherAppSetting.Name = app.Name;
            publisherAppSetting.ThemeId = app.ThemeId;
            publisherAppSetting.Customized = false;
            publisherAppSetting.Deleted = false;

            // Generates application thumbnail id.
            ThumbnailAddAndUpdateDTO thumbnail = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(app.Id);
            if(thumbnail != null) {
                publisherAppSetting.ThumbnailId = (Guid)thumbnail.ID;
            }

            // Assign already generated tenant id.
            publisherAppSetting.TenantId = publisherTenantId;

            // Update system fields and Add Publisher App Setting.
            _publisherAppSettingDS.UpdateSystemFields(publisherAppSetting, SystemFieldMask.AddOpSystemFields & ~SystemFieldMask.TenantId);
            await _publisherAppSettingDS.AddAsync(publisherAppSetting, cancellationToken);

            // Get plan's service and attribute list
            IEnumerable<Guid> planIdList = pubAppSettingDTO.AppSubscriptionList.Select(i => i.Id);

            // Check if user with same email id already exist.
            string methodUri = "subscriptionplan/services/list";

            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, methodUri, planIdList, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
            ServiceExecutor serviceExecutor = new ServiceExecutor(_appPortalAppSettings.AppMgmtApiUrl);
            List<SubsPlanServiceInfoDTO> appSubscriptionServiceList = await serviceExecutor.ExecuteAsync<List<SubsPlanServiceInfoDTO>>(requestOptions, false);

            // Add selected app subscription
            if(pubAppSettingDTO.AppSubscriptionList != null && pubAppSettingDTO.AppSubscriptionList.Count > 0) {
                await AddPubBusinessSubscriptionAsync(pubAppSettingDTO.AppSubscriptionList, appSubscriptionServiceList, publisherTenantId);
            }
        }

        // This method generates publisher's business application service and attribute linking.
        private async Task AddPublisherAppServiceAndAttributeAsync(List<SubsPlanServiceInfoDTO> appServiceList, Guid pubBusinessSubsPlanId, Guid appId, Guid publisherTenantId, CancellationToken cancellationToken) {
            foreach(SubsPlanServiceInfoDTO appService in appServiceList) {
                foreach(SubsPlanServiceAttributeInfoDTO serviceAttribute in appService.ServiceAttributeList) {
                    Entity.PubBusinessSubsPlanAppService pubAppService = new Entity.PubBusinessSubsPlanAppService();
                    pubAppService.AppServiceId = appService.AppServiceId;
                    pubAppService.AppServiceAttributeId = serviceAttribute.AppServiceAttributeId;
                    pubAppService.ServiceName = appService.ServiceName;
                    pubAppService.ServiceAttributeName = serviceAttribute.AttributeName;
                    pubAppService.SubsPlanAppServiceId = appService.SubscriptionPlanServiceId;
                    pubAppService.SubsPlanAppServiceAttributeId= serviceAttribute.SubsPlanServiceAttributeId; 
                    pubAppService.AppId = appId;
                    pubAppService.TenantId = publisherTenantId;
                    pubAppService.PubBusinessSubsPlanId = pubBusinessSubsPlanId;
                    pubAppService.Deleted = false;
                    _publisherAppServiceDS.UpdateSystemFields(pubAppService, SystemFieldMask.AddOpSystemFields & ~SystemFieldMask.TenantId);
                    // Add application's service and attribute linking.
                    await _publisherAppServiceDS.AddAsync(pubAppService, cancellationToken);
                }
            }
        }

        private async Task AddPubBusinessSubscriptionAsync(List<SubscriptionPlanInfoDTO> appSubscriptionPlanList, List<SubsPlanServiceInfoDTO> subsPlanServiceInfoDTOs, Guid publisherTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            foreach(SubscriptionPlanInfoDTO subscriptionPlan in appSubscriptionPlanList) {
                PubBusinessSubsPlan pubBusinessSubsPlan = SubscriptionPlanInfoDTO.MapToPubBusinessSubsPlan(subscriptionPlan);
                _pubBusinessSubsPlanDS.UpdateSystemFieldsByOpType(pubBusinessSubsPlan, OperationType.Add);
                pubBusinessSubsPlan.IdentityNumber = subscriptionPlan.IdentityNumber;
                pubBusinessSubsPlan.TenantId = publisherTenantId;
                _pubBusinessSubsPlanDS.Add(pubBusinessSubsPlan);

                // Add selected application's service and attribute linking.
                if(subsPlanServiceInfoDTOs != null) {

                    List<SubsPlanServiceInfoDTO> subsPlanServiceInfoDTO = subsPlanServiceInfoDTOs.FindAll(i => i.SubscriptionPlanId == subscriptionPlan.Id);
                    await AddPublisherAppServiceAndAttributeAsync(subsPlanServiceInfoDTO, pubBusinessSubsPlan.ID, subscriptionPlan.AppId, publisherTenantId, cancellationToken);

                }

            }
        }

        private async Task AddTenantAppLinkingAsync(AppInfoDTO pubApp, Guid publisherTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            TenantAppLinking tenantAppLinking = new TenantAppLinking();
            tenantAppLinking.AppId = pubApp.Id;
            tenantAppLinking.ThemeId = pubApp.ThemeId;
            _tenantAppLinkingDS.UpdateSystemFieldsByOpType(tenantAppLinking, OperationType.Add);
            tenantAppLinking.TenantId = publisherTenantId;

            await _tenantAppLinkingDS.AddAsync(tenantAppLinking, cancellationToken);
        }

        #endregion

        /// <inheritdoc/>
        public async Task<List<SubsPlanServiceInfoDTO>> GetServiceAndAttributeDetailByPlanId(Guid planId, CancellationToken cancellationToken = default(CancellationToken)) {
            string methodUri = string.Format("subscriptionplan/{0}/services", planId.ToString());

            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            RequestOptions signupPubRequestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, methodUri, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);

            // Execute api to generate tenant and user data.
            ServiceExecutor serviceExecutor = new ServiceExecutor(_appPortalAppSettings.AppMgmtApiUrl);

            // Get api result
            List<SubsPlanServiceInfoDTO> subsPlanServiceInfoDTOs = await serviceExecutor.ExecuteAsync<List<SubsPlanServiceInfoDTO>>(signupPubRequestOptions, false);

            return subsPlanServiceInfoDTOs;
        }

    public async Task ReInvitePrimaryPublisherUserAsync(Guid tenantUserId, Guid pubTenantId,  string subDomain, CancellationToken token = default(CancellationToken))
    {


      // Get the current user session.
      UserSession userSession = _userSessionManager.GetSession();
      string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
      string requesturl = "tenantuser/info/" + tenantUserId.ToString();

      // Get user detials by calling th API.
      List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
      headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, requesturl, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
      TenantUserInfoDTO tenantUserInfoDTO = await httpRequestProcessor.ExecuteAsync<TenantUserInfoDTO>(requestOptions, false);

      List<string> appKeyList = new List<string>();
      appKeyList.Add(AppKeyEnum.pub.ToString().ToLower());   


      // Get App ID by calling th API.
      string requesturlappkey = "App/getappinfobykey";
      
      RequestOptions appkeyrequestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturlappkey, appKeyList, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
      ServiceExecutor appkeyhttpRequestProcessor = new ServiceExecutor(baseuri);
      List<AppInfoDTO> appInfoDTO = await appkeyhttpRequestProcessor.ExecuteAsync <List<AppInfoDTO>>(appkeyrequestOptions, false);



      try {
        Platform platform = await _platformDS.FindAsync(p => p.TenantId == userSession.TenantId);
        Publisher publisher = await _publisherDS.FindAsync(b => b.TenantId == pubTenantId);

        PlatformNotificationDTO platformNotificationDTO = new PlatformNotificationDTO();
        platformNotificationDTO.PasswordCode = tenantUserInfoDTO.Code;
        platformNotificationDTO.InvitedUserEmail = tenantUserInfoDTO.Email;
        platformNotificationDTO.InvitedUserIdentityUserId = tenantUserInfoDTO.IdentityUserId;
        platformNotificationDTO.InvitedUserTenantId = pubTenantId;
        platformNotificationDTO.InvitedUserAppKey = AppKeyEnum.pub.ToString();
        platformNotificationDTO.InvitedUserId = tenantUserInfoDTO.TenantUserId;
        platformNotificationDTO.InvitedUserAppId = appInfoDTO[0].Id;// Guid.Empty; //appId;
        platformNotificationDTO.PublisherCompanyName = publisher.Name;
        platformNotificationDTO.PlatformCompanyName = platform.Name;
        platformNotificationDTO.InvitedUserFullName = tenantUserInfoDTO.FullName;
        platformNotificationDTO.SubDomain = subDomain;
        platformNotificationDTO.PublisherPortalURL = string.Format(_appPortalAppSettings.PublisherPortalClientURL, subDomain);
        platformNotificationDTO.CopyRigthText = platform.Copyright;
        platformNotificationDTO.TenantLanguage = publisher.Language;
        platformNotificationDTO.UserSession = userSession;
        await _platformNotificationHandler.SendPublisherPrimaryUserWithNewEmailInvitateAsync(platformNotificationDTO);
      }
      catch (Exception)
      {
        // ToDo: Handle Service Call Rollback Operation.
        //return;
      }


    }
  }
}
