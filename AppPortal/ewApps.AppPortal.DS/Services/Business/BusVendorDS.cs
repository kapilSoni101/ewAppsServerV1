using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO.PreferenceDTO;
using ewApps.Core.ExceptionService;

namespace ewApps.AppPortal.DS {
    public class BusVendorDS:IBusVendorDS {

        #region Local Variables

        IUserSessionManager _userSessionManager;
        AppPortalAppSettings _appSettings;
        IVendorDS _vendorDS;
        IUnitOfWork _unitOfWork;
        IQBusinessAppDS _qBusinessAppDS;
        IQBusinessAndUserDS _qBusinessAndUserDS;
        IRoleDS _roleDS;
        IRoleLinkingDS _roleLinkingDS;
        ITenantUserAppPreferenceDS _tenantUserAppPreferenceDS;
        IBusinessNotificationHandler _businessNotificationHandler;
        ICustUserPreferenceDS _custUserPreferenceDS;
        IBusinessAddressDS _businessAddressDS;
        IBusinessDS _businessDS;
        IQCustomerAppDS _qCustomerAppDS;
        IVendorNotificationHandler _vendorNotificationHandler;


        #endregion Local Variables

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BusVendorDS(IUserSessionManager userSessionManager,
             IVendorDS vendorDS, IUnitOfWork unitOfWork, IOptions<AppPortalAppSettings> appSettings, IQBusinessAppDS qBusinessAppDS, IQBusinessAndUserDS qBusinessAndUserDS,
             IRoleDS roleDS, IRoleLinkingDS roleLinkingDS, ITenantUserAppPreferenceDS tenantUserAppPreferenceDS, IBusinessNotificationHandler businessNotificationHandler,
             ICustUserPreferenceDS custUserPreferenceDS, IBusinessAddressDS businessAddressDS, IBusinessDS businessDS, IQCustomerAppDS qCustomerAppDS, IVendorNotificationHandler vendorNotificationHandler) {
            _userSessionManager = userSessionManager;
            _vendorDS = vendorDS;
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
            _qBusinessAppDS = qBusinessAppDS;
            _qBusinessAndUserDS = qBusinessAndUserDS;
            _roleDS = roleDS;
            _roleLinkingDS = roleLinkingDS;
            _tenantUserAppPreferenceDS = tenantUserAppPreferenceDS;
            _businessNotificationHandler = businessNotificationHandler;
            _custUserPreferenceDS = custUserPreferenceDS;
            _businessAddressDS = businessAddressDS;
            _businessDS = businessDS;
            _qCustomerAppDS = qCustomerAppDS;
            _vendorNotificationHandler = vendorNotificationHandler;
        }

    #endregion Constructor

    #region Get

    ///<inheritdoc/>   
    public async Task<List<BusVendorDTO>> GetVendorListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      string baseUrl = _appSettings.BusinessEntityApiUrl;
      string reqeustMethod = "busvendor/list/" + tenantId;

      UserSession session = _userSessionManager.GetSession();

      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
      List<BusVendorDTO> response = await httpRequestProcessor.ExecuteAsync<List<BusVendorDTO>>(requestOptions, false);
      return response;
    }

    ///<inheritdoc/>   
    public async Task<List<BusVendorDTO>> GetVendorListByStatusAndTenantIdAsync(Guid tenantId, int status, CancellationToken token = default(CancellationToken))
    {
      string baseUrl = _appSettings.BusinessEntityApiUrl;
      string reqeustMethod = "busvendor/vendorlist/" + tenantId + "/" + status.ToString();

      UserSession session = _userSessionManager.GetSession();

      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
      List<BusVendorDTO> response = await httpRequestProcessor.ExecuteAsync<List<BusVendorDTO>>(requestOptions, false);
      return response;
    }

    ///<inheritdoc/>   
    public async Task<BusVendorDetailDTO> GetVendorDetailByIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken))
    {
      string baseUrl = _appSettings.BusinessEntityApiUrl;
      string reqeustMethod = "busvendor/detail/" + vendorId;

      UserSession session = _userSessionManager.GetSession();

      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
      BusVendorDetailDTO response = await httpRequestProcessor.ExecuteAsync<BusVendorDetailDTO>(requestOptions, false);
      //if (response != null && response.Customer != null)
      //{
      //  Customer info = await _vendorDS.GetCustomerByBusinesPartnerIdAsync(response.Customer.BusinessPartnerTenantId, token);
      //  if (info != null)
      //  {
      //    Business bus = await _businessDS.GetBusinessByTenantIdAsync(info.TenantId, token);
      //    response.Customer.CurrencyCode = info.Currency;
      //    if (bus != null)
      //    {
      //      List<BusinessAddressModelDTO> listAddress = await _businessAddressDS.GetAddressListByParentEntityIdAndAddressTypeAsync(response.Customer.TenantId, bus.ID, (int)BusinessAddressTypeEnum.DefaultBusinessAddress, token);
      //      response.listBusinessAddress = listAddress;
      //    }

      //  }
      //}


      //CustomeAccDetailDTO customeAccDetail = await _customerAccountDetail.GetCustomerAccDetailCustomerIdAsync(customerId, token);
      //response.CustomerAcctDetail = customeAccDetail;
      return response;
    }


    ///<inheritdoc/>   
    public async Task<List<BusVendorSetUpAppDTO>> GetVendorListForBizSetupApp(Guid tenantId, bool isDeleted, CancellationToken token = default(CancellationToken)) {
            string baseUrl = _appSettings.BusinessEntityApiUrl;
            string reqeustMethod = "busvendor/setupapp/vendorlist/" + tenantId + "/" + isDeleted;

            UserSession session = _userSessionManager.GetSession();

            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                      RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
            List<BusVendorSetUpAppDTO> response = await httpRequestProcessor.ExecuteAsync<List<BusVendorSetUpAppDTO>>(requestOptions, false);
            for(int i = 0; i < response.Count; i++) {
                BusVendorApplicationCountDTO vendorApplicationCount = await _qBusinessAppDS.GetAppCountForVendorAsync(response[i].BusinessPartnerTenantId);
                response[i].ApplicationCount = vendorApplicationCount.ApplicationCount;
                response[i].AssignedAppInfo = await _qBusinessAppDS.GetVendorAsignedAppInfoAsync(response[i].BusinessPartnerTenantId);
            }
            return response;
        }
        ///<inheritdoc/>   
        public async Task<BusVendorSetUpAppViewDTO> GetVendorDetailForBizSetupApp(Guid vendorId, CancellationToken token = default(CancellationToken)) {
            string baseUrl = _appSettings.BusinessEntityApiUrl;
            string reqeustMethod = "busvendor/setupapp/vendordetail/" + vendorId;

            UserSession session = _userSessionManager.GetSession();

            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                      RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
            BusVendorSetUpAppViewDTO response = await httpRequestProcessor.ExecuteAsync<BusVendorSetUpAppViewDTO>(requestOptions, false);
            Vendor vend = await _vendorDS.FindAsync(del => del.BusinessPartnerTenantId == response.BusinessPartnerTenantId);
            response.DateTimeFormat = vend.DateTimeFormat;
            response.DecimalPrecision = vend.DecimalPrecision;
            response.DecimalSeperator = vend.DecimalSeperator;
            //response.CurrencyCode = cust.CurrencyCode;
            response.Language = vend.Language;
            response.TimeZone = vend.TimeZone;
            response.GroupValue = vend.GroupValue;
            response.GroupSeperator = vend.GroupSeperator;

            // APplication details
            response.VendorApplicationList = await _qBusinessAppDS.GetAppForVendor(session.TenantId, response.BusinessPartnerTenantId);

            // USer Details of the customer
            response.VendorUserList = await _qBusinessAndUserDS.GetAllVendorUserByBusinessPartnerId(response.BusinessPartnerTenantId);

            return response;
        }

        /// <summary>
        /// Get vendor detail.
        /// </summary>
        /// <param name="vendorId">Vendor Id</param>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<BusinessVendorResponse> GetBusinessVendorDetail(Guid vendorId, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            BusVendorSetUpAppViewDTO response = await GetVendorDetailForBizSetupApp(vendorId, token);
            BusinessVendorResponse busVendorResponse = null;
            if(response != null) {
                busVendorResponse = MapVendorProperties(response);
                Business bus = await _businessDS.GetBusinessByTenantIdAsync(tenantId, token);
                if(bus != null) {
                    List<BusinessAddressModelDTO> listAddress = await _businessAddressDS.GetAddressListByParentEntityIdAndAddressTypeAsync(tenantId, bus.ID, (int)BusinessAddressTypeEnum.DefaultBusinessAddress, token);
                    busVendorResponse.listBusinessAddress = listAddress;
                }

            }

            return busVendorResponse;
        }

        private BusinessVendorResponse MapVendorProperties(BusVendorSetUpAppViewDTO response) {
            BusinessVendorResponse busVendorResponse = new BusinessVendorResponse();
            busVendorResponse.BillToAddressList = response.BillToAddressList;
            busVendorResponse.ShipToAddressList = response.ShipToAddressList;
            busVendorResponse.ID = response.ID;
            busVendorResponse.VendorName = response.VendorName;
            busVendorResponse.ERPVendorKey = response.ERPVendorKey;
            busVendorResponse.Currency = response.Currency;
            busVendorResponse.CurrencyCode = response.CurrencyCode;
            busVendorResponse.BusinessPartnerTenantId = response.BusinessPartnerTenantId;

            return busVendorResponse;
        }

        #endregion Get

        ///<inheritdoc/>   
        public async Task<bool> UpdateVendorDetailForBizSetupApp(BusVendorUpdateDTO customerUpdateDTO, CancellationToken token = default(CancellationToken)) {
            string baseUrl = _appSettings.AppMgmtApiUrl;
            string reqeustMethod = "";

            UserSession session = _userSessionManager.GetSession();

            string businessEntityBaseUrl = _appSettings.BusinessEntityApiUrl;

            string businessEntityReqeustMethod = "busvendor/setupapp/updatevendordetail";

            List<KeyValuePair<string, string>> businessEntityHeaderParameters = new List<KeyValuePair<string, string>>();
            businessEntityHeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            RequestOptions businessEntityRequestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
           RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, businessEntityReqeustMethod, customerUpdateDTO, _appSettings.AppName, _appSettings.IdentityServerUrl, businessEntityHeaderParameters);

            ServiceExecutor businessEntityHeaderParametersHttpRequestProcessor = new ServiceExecutor(businessEntityBaseUrl);

            await businessEntityHeaderParametersHttpRequestProcessor.ExecuteAsync<bool>(businessEntityRequestOptions, false);

            TenantUserSignUpDTO tenantUserSignUpDTO = null;
            foreach(BusVendorUserDTO userDetail in customerUpdateDTO.UserList) {
                tenantUserSignUpDTO = new TenantUserSignUpDTO();
                tenantUserSignUpDTO.BusinessPartnerTenantId = customerUpdateDTO.BusinessPartnerTenantId;
                tenantUserSignUpDTO.Email = userDetail.Email;
                tenantUserSignUpDTO.FirstName = userDetail.FirstName;
                tenantUserSignUpDTO.LastName = userDetail.LastName;
                tenantUserSignUpDTO.FullName = userDetail.FirstName + " " + userDetail.LastName;
                tenantUserSignUpDTO.PermissionBitMask = 1023;
                tenantUserSignUpDTO.TenantId = session.TenantId;
                tenantUserSignUpDTO.Phone = "";
                tenantUserSignUpDTO.IsPrimary = userDetail.IsPrimary;
                tenantUserSignUpDTO.UserType = (int)UserTypeEnum.Vendor;
                List<UserAppRelationDTO> userApplist = new List<UserAppRelationDTO>();
                foreach(AppInfoDTO appInfo in userDetail.AssignedAppInfo) {
                    UserAppRelationDTO userApp = new UserAppRelationDTO();
                    userApp.AppId = appInfo.Id;
                    userApp.AppKey = appInfo.AppKey;
                    userApp.OperationType = (OperationType)appInfo.OperationType;
                    userApplist.Add(userApp);
                }
                UserAppRelationDTO vendSetupApp = new UserAppRelationDTO();
                vendSetupApp.AppId = new Guid(AppPortalConstants.VendorSetUpApplicationId);
                vendSetupApp.AppKey = AppKeyEnum.vendsetup.ToString();
                userApplist.Add(vendSetupApp);

                tenantUserSignUpDTO.UserAppRelationDTOList = userApplist;
                if(userDetail.OperationType == (int)OperationType.Add) {
                    reqeustMethod = "tenantuser/addvendoruser";
                    List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
                    headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

                    RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                              RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, tenantUserSignUpDTO, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);

                    ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
                    TenantUserSignUpResponseDTO tenantUserSignUpResponseDTO = await httpRequestProcessor.ExecuteAsync<TenantUserSignUpResponseDTO>(requestOptions, false);

                    await AddRoleLinkingAndAppPrefrencesForVendorUser(tenantUserSignUpResponseDTO.UserAppRelationDTOs, session.TenantId, tenantUserSignUpResponseDTO.TenantUserDTO.TenantUserId, session.TenantUserId, token);
                    _unitOfWork.SaveAll();
                    await GenerateAddUserNotification(tenantUserSignUpResponseDTO, session);
                }
                else if(userDetail.OperationType == (int)OperationType.Update) {
                    reqeustMethod = "tenantuser/updatevendoruser";
                    tenantUserSignUpDTO.ID = userDetail.TenantUserId;
                    List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
                    headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

                    RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                              RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, tenantUserSignUpDTO, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);

                    ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
                    TenantUserSignUpResponseDTO tenantUserSignUpResponseDTO = await httpRequestProcessor.ExecuteAsync<TenantUserSignUpResponseDTO>(requestOptions, false);

                    await AddDeleteRoleLinkingAndAppPrefrencesForVendorUser(tenantUserSignUpResponseDTO.UserAppRelationDTOs, session.TenantId, tenantUserSignUpResponseDTO.TenantUserDTO.TenantUserId, session.TenantUserId, token);
                    _unitOfWork.SaveAll();
                }
            }


            return true;
        }

        private async Task AddDeleteRoleLinkingAndAppPrefrencesForVendorUser(List<UserAppRelationDTO> userAppRelationDTOs, Guid tenantId, Guid tenantUserId, Guid createdBy, CancellationToken cancellationToken = default(CancellationToken)) {

            // Get all the added applications
            List<UserAppRelationDTO> addApps = userAppRelationDTOs.Where(u => u.OperationType == OperationType.Add).ToList();
            if(addApps != null && addApps.Count > 0) {
                await AddRoleLinkingAndAppPrefrencesForVendorUser(addApps, tenantId, tenantUserId, createdBy, cancellationToken);
            }

            // Get all the deleted applications
            List<UserAppRelationDTO> deleteApps = userAppRelationDTOs.Where(u => u.OperationType == OperationType.Delete).ToList();
            if(deleteApps != null && deleteApps.Count > 0) {

            }

        }

        private async Task DeleteRoleLinkingAndAppPrefrencesForUser(List<UserAppRelationDTO> userAppRelationDTOs, Guid tenantId, Guid tenantUserId, Guid createdBy, CancellationToken cancellationToken = default(CancellationToken)) {

            UserSession userSession = _userSessionManager.GetSession();

            foreach(UserAppRelationDTO item in userAppRelationDTOs) {

                if(item.AppKey.Equals(AppKeyEnum.biz.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                    RoleLinking roleLinking = await _roleLinkingDS.FindAsync(RL => RL.TenantUserId == tenantUserId && RL.TenantId == tenantId && RL.AppId == item.AppId && RL.Deleted == false);
                    if(roleLinking != null) {
                        roleLinking.Deleted = true;
                        await _roleLinkingDS.UpdateAsync(roleLinking, roleLinking.ID);
                    }
                    TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceDS.FindAsync(RL => RL.TenantUserId == tenantUserId && RL.TenantId == tenantId && RL.AppId == item.AppId && RL.Deleted == false);
                    if(tenantUserAppPreference != null) {
                        tenantUserAppPreference.Deleted = true;
                        await _tenantUserAppPreferenceDS.UpdateAsync(tenantUserAppPreference, tenantUserAppPreference.ID);
                    }
                }
                else if(item.AppKey.Equals(AppKeyEnum.pay.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                    // Call payment api to deassign 
                    TenantUserAppManagmentDTO tenantUserAppManagmentDTO = new TenantUserAppManagmentDTO();
                    tenantUserAppManagmentDTO.AppId = item.AppId;
                    tenantUserAppManagmentDTO.CreatedBy = createdBy;
                    tenantUserAppManagmentDTO.TenantId = tenantId;
                    tenantUserAppManagmentDTO.TenantUserId = tenantUserId;
                    tenantUserAppManagmentDTO.UserType = (int)UserTypeEnum.Business;
                    tenantUserAppManagmentDTO.PermissionBitMask = item.PermissionBitMask;

                    RequestOptions requestOptions = new RequestOptions();
                    requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
                    requestOptions.Method = "customeruser/appdeassign";
                    requestOptions.MethodData = tenantUserAppManagmentDTO;
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
                else {
                    RoleLinking roleLinking = await _roleLinkingDS.FindAsync(RL => RL.TenantUserId == tenantUserId && RL.TenantId == tenantId && RL.AppId == item.AppId && RL.Deleted == false);
                    if(roleLinking != null) {
                        roleLinking.Deleted = true;
                        await _roleLinkingDS.UpdateAsync(roleLinking, roleLinking.ID);
                    }
                    TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceDS.FindAsync(RL => RL.TenantUserId == tenantUserId && RL.TenantId == tenantId && RL.AppId == item.AppId && RL.Deleted == false);
                    if(tenantUserAppPreference != null) {
                        tenantUserAppPreference.Deleted = true;
                        await _tenantUserAppPreferenceDS.UpdateAsync(tenantUserAppPreference, tenantUserAppPreference.ID);
                    }
                }
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

        private async Task AddTenantUserAppPrefernces(Guid tenantId, Guid tenantUserId, Guid appId, Guid createdBy, long emailPrefrence, long smsPreference, long asPreference, CancellationToken cancellationToken) {
            // Add tenant user preference entry.
            TenantUserAppPreference tenantUserAppPreference = new TenantUserAppPreference();
            tenantUserAppPreference.ID = Guid.NewGuid();
            tenantUserAppPreference.AppId = appId;
            tenantUserAppPreference.CreatedBy = createdBy;
            tenantUserAppPreference.CreatedOn = DateTime.UtcNow;
            tenantUserAppPreference.Deleted = false;
            tenantUserAppPreference.EmailPreference = emailPrefrence;
            tenantUserAppPreference.SMSPreference = smsPreference;
            tenantUserAppPreference.TenantId = tenantId;
            tenantUserAppPreference.TenantUserId = tenantUserId;
            tenantUserAppPreference.UpdatedBy = tenantUserAppPreference.CreatedBy;
            tenantUserAppPreference.UpdatedOn = tenantUserAppPreference.CreatedOn;
            await _tenantUserAppPreferenceDS.AddAsync(tenantUserAppPreference, cancellationToken);
        }

        /// <summary>
        /// Method used to generate the add user notification.
        /// </summary>
        /// <returns></returns>
        private async Task GenerateAddUserNotification(TenantUserSignUpResponseDTO tenantUserSignUpResponseDTO, UserSession userSession) {

            try {
                BusinessAccountNotificationDTO businessAccountNotificationDTO = new BusinessAccountNotificationDTO();
                // Publisher publisher = await _publisherDS.FindAsync(p => p.TenantId == userSession.TenantId);
                Guid appId = tenantUserSignUpResponseDTO.UserAppRelationDTOs.Where(u => u.AppKey == AppKeyEnum.vendsetup.ToString()).FirstOrDefault().AppId;

                businessAccountNotificationDTO.PasswordCode = tenantUserSignUpResponseDTO.TenantUserDTO.Code;
                businessAccountNotificationDTO.InvitedUserEmail = tenantUserSignUpResponseDTO.TenantUserDTO.Email;
                businessAccountNotificationDTO.InvitedUserIdentityUserId = tenantUserSignUpResponseDTO.TenantUserDTO.IdentityUserId;
                businessAccountNotificationDTO.TenantId = userSession.TenantId;
                businessAccountNotificationDTO.InvitedUserAppKey = AppKeyEnum.biz.ToString();
                businessAccountNotificationDTO.InvitedUserId = tenantUserSignUpResponseDTO.TenantUserDTO.TenantUserId;
                businessAccountNotificationDTO.InvitedUserAppId = appId;
                businessAccountNotificationDTO.PublisherName = " ";
                businessAccountNotificationDTO.BusinessCompanyName = " ";
                businessAccountNotificationDTO.PartnerType = "Vendor";
                businessAccountNotificationDTO.BusinessPartnerCompanyName = " ";
                businessAccountNotificationDTO.InvitorUserName = userSession.UserName;
                businessAccountNotificationDTO.InvitedUserFullName = tenantUserSignUpResponseDTO.TenantUserDTO.FullName;
                businessAccountNotificationDTO.ApplicationName = "Vendor";
                businessAccountNotificationDTO.SubDomain = userSession.Subdomain;
                businessAccountNotificationDTO.PortalURL = string.Format(_appSettings.CustomerPortalClientURL, userSession.Subdomain);
                businessAccountNotificationDTO.CopyRightText = " ";
                businessAccountNotificationDTO.UserSession = userSession;
                await _vendorNotificationHandler.GenerateNewVendorNewEmailIdInvitedNotification(businessAccountNotificationDTO);

                //if(tenantUserSignUpResponseDTO.NewUser != null && tenantUserSignUpResponseDTO.NewUser.Item2) {
                //    businessAccountNotificationDTO.PasswordCode = tenantUserSignUpResponseDTO.TenantUserDTO.Code;
                //    businessAccountNotificationDTO.InvitedUserEmail = tenantUserSignUpResponseDTO.TenantUserDTO.Email;
                //    businessAccountNotificationDTO.InvitedUserIdentityUserId = tenantUserSignUpResponseDTO.TenantUserDTO.IdentityUserId;
                //    businessAccountNotificationDTO.TenantId = userSession.TenantId;
                //    businessAccountNotificationDTO.InvitedUserAppKey = AppKeyEnum.biz.ToString();
                //    businessAccountNotificationDTO.InvitedUserId = tenantUserSignUpResponseDTO.TenantUserDTO.TenantUserId;
                //    businessAccountNotificationDTO.InvitedUserAppId = appId;
                //    businessAccountNotificationDTO.PublisherName = " ";
                //    businessAccountNotificationDTO.BusinessCompanyName = " ";
                //    businessAccountNotificationDTO.InvitorUserName = " ";
                //    businessAccountNotificationDTO.InvitedUserFullName = tenantUserSignUpResponseDTO.TenantUserDTO.FullName;
                //    businessAccountNotificationDTO.SubDomain = userSession.Subdomain;
                //    businessAccountNotificationDTO.PortalURL = " ";
                //    businessAccountNotificationDTO.CopyRightText = " ";
                //    businessAccountNotificationDTO.UserSession = userSession;
                //    businessAccountNotificationDTO.PortalName = tenantUserSignUpResponseDTO.NewUser.Item1;
                //    await _businessNotificationHandler.GenerateBusinessUserWithExistingEmailIdInviteNotification(businessAccountNotificationDTO);
                //}
                //else {

                //}
            }
            catch(Exception) {
                throw;
            }
        }


        private async Task AddRoleLinkingAndAppPrefrencesForVendorUser(List<UserAppRelationDTO> userAppRelationDTOs, Guid tenantId, Guid tenantUserId, Guid createdBy, CancellationToken cancellationToken = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();

            foreach(UserAppRelationDTO item in userAppRelationDTOs) {
                // Vendor Portal - Vendor Setup
                if(item.AppKey.Equals(AppKeyEnum.vendsetup.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                    await AddUserRoleAndRoleLinkingAsync(tenantId, tenantUserId, item.AppId, createdBy, (long)VendorUserVendorSetupPermissionEnum.All, cancellationToken);

                }
                // Vendor Portal - Vendor App
                else if(item.AppKey.Equals(AppKeyEnum.vend.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                    await AddUserRoleAndRoleLinkingAsync(tenantId, tenantUserId, item.AppId, createdBy, (long)VendorUserVendorAppPermissionEnum.All, cancellationToken);
                    //New code
                    await _custUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId, createdBy, item.AppId, (long)VendorUserVendorAppPreferenceEnum.AllEmail, (long)VendorUserVendorAppPreferenceEnum.AllSMS, (long)VendorUserVendorAppPreferenceEnum.AllAS, cancellationToken);

                }
                else {
                    throw new InvalidOperationException("Invalid Application - BusCustomerDS.AddRoleLinkingAndAppPreferencesForUser");
                }
            }
        }

        private async Task DeleteRoleLinkingAndAppPrefrencesForVendorUser(List<UserAppRelationDTO> userAppRelationDTOs, Guid tenantId, Guid tenantUserId, Guid createdBy, CancellationToken cancellationToken = default(CancellationToken)) {

            UserSession userSession = _userSessionManager.GetSession();

            foreach(UserAppRelationDTO item in userAppRelationDTOs) {

                if(item.AppKey.Equals(AppKeyEnum.vendsetup.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                    RoleLinking roleLinking = await _roleLinkingDS.FindAsync(RL => RL.TenantUserId == tenantUserId && RL.TenantId == tenantId && RL.AppId == item.AppId && RL.Deleted == false);
                    if(roleLinking != null) {
                        roleLinking.Deleted = true;
                        await _roleLinkingDS.UpdateAsync(roleLinking, roleLinking.ID);
                    }
                    TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceDS.FindAsync(RL => RL.TenantUserId == tenantUserId && RL.TenantId == tenantId && RL.AppId == item.AppId && RL.Deleted == false);
                    if(tenantUserAppPreference != null) {
                        tenantUserAppPreference.Deleted = true;
                        await _tenantUserAppPreferenceDS.UpdateAsync(tenantUserAppPreference, tenantUserAppPreference.ID);
                    }
                }
                else {
                    RoleLinking roleLinking = await _roleLinkingDS.FindAsync(RL => RL.TenantUserId == tenantUserId && RL.TenantId == tenantId && RL.AppId == item.AppId && RL.Deleted == false);
                    if(roleLinking != null) {
                        roleLinking.Deleted = true;
                        await _roleLinkingDS.UpdateAsync(roleLinking, roleLinking.ID);
                    }
                    TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceDS.FindAsync(RL => RL.TenantUserId == tenantUserId && RL.TenantId == tenantId && RL.AppId == item.AppId && RL.Deleted == false);
                    if(tenantUserAppPreference != null) {
                        tenantUserAppPreference.Deleted = true;
                        await _tenantUserAppPreferenceDS.UpdateAsync(tenantUserAppPreference, tenantUserAppPreference.ID);
                    }
                }
            }
        }




    }
}
