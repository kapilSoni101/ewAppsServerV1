using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.Common;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ewApps.AppMgmt.DS {
    /// <summary>
    /// This class defines methods to manage tenant signup operations.
    /// </summary>
    /// <seealso cref="ewApps.AppMgmt.DS.ITenantSignUpForPublisherDS" />
    public class TenantSignUpForPublisherDS:ITenantSignUpForPublisherDS {

        #region Local Member

        ILogger<TenantSignUpForPublisherDS> _loggerService;
        IUnitOfWork _unitOfWork;
        ITenantUserDS _tenantUserDS;
        IIdentityServerDS _identityServerDS;
        IUserTenantLinkingDS _userTenantLinkingDS;
        ITenantUserAppLinkingDS _tenantUserAppLinkingDS;
        IUserSessionManager _userSessionManager;
        ITenantDS _tenantDS;
        ITenantLinkingDS _tenantLinkingDS;
        ITenantSubscriptionDS _tenantSubscriptionDS;
        ISubscriptionPlanDS _subscriptionPlanDataService;
        IAppDS _appDS;
        IServiceAccountDetailDS _serviceAccountDetailDS;
        ITenantAppServiceLinkingDS _tenantAppServiceLinkingDS;
        ISubscriptionPlanServiceDS _subscriptionPlanServiceDS;
        #endregion Local Member

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantSignUpForPublisherDS"/> class and it's dependencies.
        /// </summary>
        /// <param name="tenantUserDS">The tenant user data service instance.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="identityServerDS">The identity server data service instance.</param>
        /// <param name="userTenantLinkingDS">The user tenant linking data service instance.</param>
        /// <param name="tenantUserAppLinkingDS">The tenant user application linking data service instance.</param>
        /// <param name="userSessionManager">The user session manager.</param>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="tenantDS">The tenant data service instance.</param>
        /// <param name="tenantLinkingDS">The tenant linking data service instance.</param>
        /// <param name="tenantSubscriptionDS">The tenant subscription data service instance.</param>
        /// <param name="subscriptionPlanDataService">The subscription plan data service instance.</param>
        /// <param name="appDS">The application data service instance.</param>
        /// <param name="serviceAccountDetailDS">The service account detail data service instance.</param>
        /// <param name="tenantAppServiceLinkingDS">The tenant application service linking data service instance.</param>
        public TenantSignUpForPublisherDS(ISubscriptionPlanServiceDS subscriptionPlanServiceDS, ITenantUserDS tenantUserDS, IUnitOfWork unitOfWork, IIdentityServerDS identityServerDS, IUserTenantLinkingDS userTenantLinkingDS, ITenantUserAppLinkingDS tenantUserAppLinkingDS
              , IUserSessionManager userSessionManager, ILogger<TenantSignUpForPublisherDS> loggerService
             , ITenantDS tenantDS, ITenantLinkingDS tenantLinkingDS, ITenantSubscriptionDS tenantSubscriptionDS,
               ISubscriptionPlanDS subscriptionPlanDataService, IAppDS appDS,
              IServiceAccountDetailDS serviceAccountDetailDS, ITenantAppServiceLinkingDS tenantAppServiceLinkingDS) {
            _tenantUserDS = tenantUserDS;
            _identityServerDS = identityServerDS;
            _userTenantLinkingDS = userTenantLinkingDS;
            _tenantUserAppLinkingDS = tenantUserAppLinkingDS;
            _userSessionManager = userSessionManager;
            _unitOfWork = unitOfWork;
            _loggerService = loggerService;
            _tenantDS = tenantDS;
            _tenantLinkingDS = tenantLinkingDS;
            _tenantSubscriptionDS = tenantSubscriptionDS;
            _subscriptionPlanDataService = subscriptionPlanDataService;
            _appDS = appDS;
            _serviceAccountDetailDS = serviceAccountDetailDS;
            _tenantAppServiceLinkingDS = tenantAppServiceLinkingDS;
            _subscriptionPlanServiceDS = subscriptionPlanServiceDS;
        }
        #endregion Constructor 

        #region Publisher SignUp Validation

        /// <inheritdoc/>
        public async Task<PublisherPreSignUpRespDTO> ValidatePublisherPreSignUpRequest(PublisherPreSignUpReqDTO publisherPreSignUpReqDTO) {
            PublisherPreSignUpRespDTO publisherPreSignUpRespDTO = new PublisherPreSignUpRespDTO();
            IList<EwpErrorData> errorDataList = new List<EwpErrorData>();

            // Check subdomain with existing tenant.
            bool subdomainExist = await _tenantDS.IsSubdomainExistAsync(publisherPreSignUpReqDTO.SubDomain, Guid.Empty);
            // If sub-domain already exist add error data object.
            if(subdomainExist) {
                EwpErrorData ewpErrorData = new EwpErrorData() {
                    Data = publisherPreSignUpReqDTO.SubDomain,
                    ErrorSubType = (int)DuplicateErrorSubType.None,
                    Message = "Sub-domain already exist."
                };
                EwpDuplicateNameException ewpDuplicateNameException = new EwpDuplicateNameException("Sub-domain already exist.");
                throw ewpDuplicateNameException;
                //publisherPreSignUpRespDTO.ErrorDataList.Add(ewpErrorData);
            }

            // Get application list by application key.
            publisherPreSignUpRespDTO.ApplicationList = await _appDS.GetAppInfoListByAppKeyListAsync(publisherPreSignUpReqDTO.AppKeyList);

            List<AppInfoDTO> appListById = await _appDS.GetAppInfoListByAppIdListAsync(publisherPreSignUpReqDTO.AppIdList);

            publisherPreSignUpRespDTO.ApplicationList.AddRange(appListById);

            publisherPreSignUpRespDTO.TenantUser = await _tenantUserDS.GetTenantUserByEmailAsync(publisherPreSignUpReqDTO.UserEmail);

            return publisherPreSignUpRespDTO;
        }


        #endregion

        #region Publisher Update Methods

        /// <inheritdoc/>
        public async Task<PublisherPreUpdateRespDTO> GetPublisherPreUpdateRequestDataAsync(PublisherPreUpdateReqDTO publisherPreUpdateReqDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            PublisherPreUpdateRespDTO publisherPreUpdateRespDTO = new PublisherPreUpdateRespDTO();

            if(publisherPreUpdateReqDTO.AppIdList.Count() > 0) {
                // Get application list by app ids
                publisherPreUpdateRespDTO.ApplicationList = await _appDS.GetAppInfoListByAppIdListAsync(publisherPreUpdateReqDTO.AppIdList.ToList());
            }

            if(publisherPreUpdateReqDTO.SuscriptionPlanIdList.Count() > 0) {

                publisherPreUpdateRespDTO.SubsriptionPlanInfoList = await _subscriptionPlanDataService.GetSubscriptionPlanListByPlanIdListAsync(publisherPreUpdateReqDTO.SuscriptionPlanIdList.ToList(), BooleanFilterEnum.True, cancellationToken);

                // Get suscription plan and service detail by plan ids.
                publisherPreUpdateRespDTO.SuscriptionPlanServiceInfoList = await _subscriptionPlanServiceDS.GetPlanServiceAndAttributeListByPlanIdsAsync(publisherPreUpdateReqDTO.SuscriptionPlanIdList.ToList(), cancellationToken);
            }

            return publisherPreUpdateRespDTO;
        }

        #endregion

        #region Publisher SignUp

        /// <summary>
        /// Publishers the sign up asynchronous.
        /// </summary>
        /// <param name="publisherSignUpDTO">The publisher sign up dto.</param>
        /// <param name="cancellationToken">The async task token to notify about task cancellation.</param>
        /// <returns>Returns signedup tenant and user detail.</returns>
        public async Task<TenantSignUpResponseDTO> PublisherSignUpAsync(PublisherSignUpDTO publisherSignUpDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            // Get User Session
            UserSession userSession = _userSessionManager.GetSession();

            // Get Publisher App
            App pubApp = await _appDS.GetAppByAppKeyAsync(AppKeyEnum.pub.ToString(), cancellationToken);

            #region Add Tenant

            Tenant tenant = await AddTenant(publisherSignUpDTO);

            #endregion

            #region TenantLinking

            await AddTenantLinking(publisherSignUpDTO, userSession);

            #endregion TenantLinking

            #region TenantSubscription

            await AddTenantSubscription(pubApp, tenant);

            #endregion TenantSubscription

            // Add Primary User.
            TenantUser tenantUser = await SignUpPublisherPrimaryUserAsync(publisherSignUpDTO, cancellationToken);

            // Commit all database changes.
            _unitOfWork.SaveAll();

            TenantSignUpResponseDTO signUpTenantResponseDTO = new TenantSignUpResponseDTO();
            signUpTenantResponseDTO.TenantInfo = TenantDTO.MapFromTenant(tenant);
            signUpTenantResponseDTO.TenantUserInfo = TenantUserDTO.MapFromTenantUser(tenantUser);
            return signUpTenantResponseDTO;
        }


        // Adds the tenant linking entry for publisher tenant.
        private async Task AddTenantLinking(PublisherSignUpDTO publisherSignUpDTO, UserSession userSession) {
            // Add tenant linking
            TenantLinking tenantLinking = new TenantLinking();
            tenantLinking.PlatformTenantId = userSession.TenantId;
            tenantLinking.PublisherTenantId = publisherSignUpDTO.PublisherTenantId;

            _tenantLinkingDS.UpdateSystemFieldsByOpType(tenantLinking, OperationType.Add);
            await _tenantLinkingDS.AddAsync(tenantLinking);
        }

        // Adds the tenant subscription for Publisher application.
        private async Task AddTenantSubscription(App pubApp, Tenant tenant) {
            //ToDo: This code will get update as per subscription implementation.
            // Add Tenant App Subscription
            TenantSubscription tenantSubscription = new TenantSubscription();
            tenantSubscription.AppId = pubApp.ID;
            tenantSubscription.CustomizeSubscription = true;
            tenantSubscription.ThemeId = pubApp.ThemeId;
            tenantSubscription.AutoRenewal = true;
            tenantSubscription.Deleted = false;
            tenantSubscription.PaymentCycle = 1;
            tenantSubscription.PriceInDollar = 0;
            tenantSubscription.Status = 0;
            tenantSubscription.SubscriptionStartDate = DateTime.UtcNow;
            tenantSubscription.SubscriptionStartEnd = DateTime.UtcNow.AddYears(1);
            tenantSubscription.Status = 1;
            tenantSubscription.BusinessUserCount = 2;
            tenantSubscription.SubscriptionPlanId = Guid.Empty;
            tenantSubscription.TenantId = tenant.ID;
            _tenantSubscriptionDS.UpdateSystemFields(tenantSubscription, SystemFieldMask.AddOpSystemFields & ~SystemFieldMask.TenantId);
            await _tenantSubscriptionDS.AddAsync(tenantSubscription);
        }

        // Add publisher tenant.
        private async Task<Tenant> AddTenant(PublisherSignUpDTO publisherSignUpDTO) {
            UserSession userSession = _userSessionManager.GetSession();

            // Add Publisher Tenant
            Tenant tenant = new Tenant();
            tenant.IdentityNumber = "TNT001";
            tenant.TenantType = (int)TenantType.Publisher;
            tenant.Active = true;
            tenant.Deleted = false;
            tenant.Name = publisherSignUpDTO.PublisherName;
            tenant.SubDomainName = publisherSignUpDTO.SubDomain;
            tenant.InvitedBy = userSession.TenantUserId;
            tenant.InvitedOn = DateTime.UtcNow;
            tenant.Currency = "";
            tenant.Language = "";
            tenant.TimeZone = "";

            tenant.ID = publisherSignUpDTO.PublisherTenantId;
            _tenantDS.UpdateSystemFields(tenant, SystemFieldMask.CreatedBy | SystemFieldMask.CreatedOn | SystemFieldMask.UpdatedBy | SystemFieldMask.UpdatedOn);
            tenant = await _tenantDS.AddTenantAsync(tenant);
            return tenant;
        }

        // Add's publisher primary user, add identity user and related application linking data.
        private async Task<TenantUser> SignUpPublisherPrimaryUserAsync(PublisherSignUpDTO publisherSignUpDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            IdentityServerAddUserResponseDTO identityServerAddUserResponseDTO = null;
            TenantUser tenantUser = null;
            UserSession userSession = _userSessionManager.GetSession();

            try {

                #region TenantUser

                // Add TenantUser entity.

                // Check if user with same email id is already exist.
                tenantUser = await _tenantUserDS.FindAsync(tu => tu.Email == publisherSignUpDTO.PrimaryUserEmail && tu.Deleted == false);
                if(tenantUser == null) {
                    tenantUser = new TenantUser();
                    tenantUser.Email = publisherSignUpDTO.PrimaryUserEmail;
                    tenantUser.FirstName = publisherSignUpDTO.PrimaryUserFirstName;
                    tenantUser.LastName = publisherSignUpDTO.PrimaryUserLastName;
                    tenantUser.FullName = publisherSignUpDTO.PrimaryUserFullName;
                    tenantUser.Phone = publisherSignUpDTO.Phone;
                    tenantUser.ID = publisherSignUpDTO.PrimaryTenantUserId;
                    _tenantUserDS.UpdateSystemFields(tenantUser, SystemFieldMask.CreatedOn | SystemFieldMask.CreatedBy | SystemFieldMask.UpdatedOn | SystemFieldMask.UpdatedBy);
                    tenantUser = await _tenantUserDS.AddTenantUserAsync(tenantUser);
                }

                #endregion TenantUser

                #region TenantUserLinking

                UserTenantLinking userTenantLinking = new UserTenantLinking();
                userTenantLinking.TenantUserId = tenantUser.ID;
                userTenantLinking.UserType = (int)UserTypeEnum.Publisher;
                userTenantLinking.IsPrimary = true;
                userTenantLinking.Deleted = false;
                userTenantLinking.PartnerType = null;
                userTenantLinking.BusinessPartnerTenantId = null;
                _userTenantLinkingDS.UpdateSystemFieldsByOpType(userTenantLinking, OperationType.Add);
                userTenantLinking.TenantId = publisherSignUpDTO.PublisherTenantId;
                await _userTenantLinkingDS.AddAsync(userTenantLinking, cancellationToken);

                #endregion TenantUserLinking

                #region Assign application (TenantUserAppLinking)

                App publisherApp = await _appDS.GetAppByAppKeyAsync(AppKeyEnum.pub.ToString(), cancellationToken);

                TenantUserAppLinking tenantUserAppLinking = new TenantUserAppLinking();
                tenantUserAppLinking.Deleted = false;
                tenantUserAppLinking.AppId = publisherApp.ID;
                tenantUserAppLinking.TenantUserId = tenantUser.ID;
                tenantUserAppLinking.UserType = userTenantLinking.UserType;
                tenantUserAppLinking.Active = true;
                tenantUserAppLinking.Status = (int)TenantUserInvitaionStatusEnum.Invited;

                _tenantUserAppLinkingDS.UpdateSystemFieldsByOpType(tenantUserAppLinking, OperationType.Add);
                tenantUserAppLinking.TenantId = publisherSignUpDTO.PublisherTenantId;
                tenantUserAppLinking.InvitedBy = tenantUserAppLinking.CreatedBy;
                tenantUserAppLinking.InvitedOn = tenantUserAppLinking.CreatedOn;
                tenantUserAppLinking.BusinessPartnerTenantId = null;

                await _tenantUserAppLinkingDS.AddAsync(tenantUserAppLinking, cancellationToken);

                #endregion Asign application (TenantUserAppLinking)

                #region Add User In Identity Server

                // Add user on identity server.
                string appKeys = AppKeyEnum.pub.ToString();

                // Create identityUserDTO for adding user on identity server.
                IdentityUserDTO identityUserDTO = new IdentityUserDTO {
                    FirstName = publisherSignUpDTO.PrimaryUserFirstName,
                    LastName = publisherSignUpDTO.PrimaryUserLastName,
                    ClientAppType = appKeys,
                    Email = publisherSignUpDTO.PrimaryUserEmail,
                    IsActive = true,
                    TenantId = publisherSignUpDTO.PublisherTenantId,
                    UserType = (int)UserTypeEnum.Publisher
                };

                identityServerAddUserResponseDTO = await _identityServerDS.AddUserOnIdentityServerAsync(identityUserDTO);

                // Update TenantUser entity with identity server user information.
                if(identityServerAddUserResponseDTO.Code != null) {
                    tenantUser.IdentityUserId = identityServerAddUserResponseDTO.UserId;
                    tenantUser.Code = identityServerAddUserResponseDTO.Code;
                    await _tenantUserDS.UpdateAsync(tenantUser, tenantUser.ID);
                }

                #endregion

                // Commit all data base changes.
                _unitOfWork.SaveAll();

                return tenantUser;
            }
            catch(Exception ex) {
                // Log error
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in TenantSignUpDs.PublisherSignUpPrimaryUserAsync:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());

                // To delete the user created on identity server incase of any exception after creating the user.
                if(identityServerAddUserResponseDTO != null) {
                    await _identityServerDS.DeleteUserByTenantIdOnIdentityServerAsync(tenantUser.IdentityUserId, publisherSignUpDTO.PublisherTenantId);
                }
                throw;
            }
        }

        #endregion        

    }
}
