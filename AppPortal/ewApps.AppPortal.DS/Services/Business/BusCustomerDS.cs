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
    public class BusCustomerDS:IBusCustomerDS {

        #region Local Variables

        IUserSessionManager _userSessionManager;
        AppPortalAppSettings _appSettings;
        IQBACustomerDS _qBACustomerDS;
        ICustomerDS _customerDS;
        ICustomerAccountDetailDS _customerAccountDetail;
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

        #endregion Local Variables

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BusCustomerDS(IUserSessionManager userSessionManager, IQBACustomerDS qBACustomerDS, ICustomerAccountDetailDS customerAccountDetail,
             ICustomerDS customerDS, IUnitOfWork unitOfWork, IOptions<AppPortalAppSettings> appSettings, IQBusinessAppDS qBusinessAppDS, IQBusinessAndUserDS qBusinessAndUserDS,
             IRoleDS roleDS, IRoleLinkingDS roleLinkingDS, ITenantUserAppPreferenceDS tenantUserAppPreferenceDS, IBusinessNotificationHandler businessNotificationHandler,
             ICustUserPreferenceDS custUserPreferenceDS, IBusinessAddressDS businessAddressDS, IBusinessDS businessDS, IQCustomerAppDS qCustomerAppDS) {
            _userSessionManager = userSessionManager;
            _qBACustomerDS = qBACustomerDS;
            _customerAccountDetail = customerAccountDetail;
            _customerDS = customerDS;
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
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get customer info by business entity customer id.
        /// </summary>
        /// <param name="baCustomerId">BACustomer unique id</param>
        /// <param name="appId">Application id</param>
        /// <param name="includeAccountDetl">Include attribute account detail.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<CustomerPaymentInfoDTO> GetCustomerAndPaymentInfoAsync(Guid baCustomerId, Guid appId, bool includeAccountDetl, CancellationToken token = default(CancellationToken)) {
            CustomerPaymentInfoDTO custInfo = await _qBACustomerDS.GetCustomerInfoAsync(baCustomerId, appId, token);
            if(custInfo != null) {
                custInfo.listAppServiceDTO = await _qBACustomerDS.GetAppServiceListByAppIdAndTenantAsync(appId, custInfo.TenantId, baCustomerId, includeAccountDetl, token);
            }
            return custInfo;
        }

        /// <summary>
        /// Get customer info by businessPartnerTenantId.
        /// </summary>
        /// <param name="businessPartnerTenantId"></param>
        /// <param name="appId">Application id</param>
        /// <param name="includeAccountDetl">Include attribute account detail.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<CustomerPaymentInfoDTO> GetCustomerInfoByBusinessPartnerAsync(Guid businessPartnerTenantId, Guid appId, bool includeAccountDetl, CancellationToken token = default(CancellationToken)) {
            CustomerPaymentInfoDTO custInfo = await _qBACustomerDS.GetCustomerInfoByBusinessPartnerIdAsync(businessPartnerTenantId, token);
            if(custInfo != null) {
                custInfo.listAppServiceDTO = await _qBACustomerDS.GetAppServiceListByAppIdAndTenantAsync(appId, custInfo.TenantId, custInfo.BACustomerId, includeAccountDetl, token);
            }
            return custInfo;
        }

        /// <summary>
        /// Get customer info by business entity customer id.
        /// </summary>
        /// <param name="baCustomerId">BACustomer unique id</param>
        /// <param name="appKey">Application id</param>
        /// <param name="includeAccountDetl">Include attribute account detail.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<CustomerPaymentInfoDTO> GetCustomerAndPaymentAppKeyInfoAsync(Guid baCustomerId, string appKey, bool includeAccountDetl, CancellationToken token = default(CancellationToken)) {
            CustomerPaymentInfoDTO custInfo = await _qBACustomerDS.GetCustomerInfoAsync(baCustomerId, Guid.Empty, token);
            if(custInfo != null) {
                custInfo.listAppServiceDTO = await _qBACustomerDS.GetAppServiceListByAppKeyAndTenantAsync(appKey, custInfo.TenantId, baCustomerId, includeAccountDetl, token);
            }
            return custInfo;
        }

        ///<inheritdoc/>   
        public async Task<List<BusCustomerDTO>> GetCustomerListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            string baseUrl = _appSettings.BusinessEntityApiUrl;
            string reqeustMethod = "bacustomer/list/" + tenantId;

            UserSession session = _userSessionManager.GetSession();

            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                      RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
            List<BusCustomerDTO> response = await httpRequestProcessor.ExecuteAsync<List<BusCustomerDTO>>(requestOptions, false);
            return response;
        }

        ///<inheritdoc/>   
        public async Task<List<BusCustomerDTO>> GetCustomerListByStatusAndTenantIdAsync(Guid tenantId, int status, CancellationToken token = default(CancellationToken)) {
            string baseUrl = _appSettings.BusinessEntityApiUrl;
            string reqeustMethod = "bacustomer/customerlist/" + tenantId + "/" + status.ToString();

            UserSession session = _userSessionManager.GetSession();

            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                      RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
            List<BusCustomerDTO> response = await httpRequestProcessor.ExecuteAsync<List<BusCustomerDTO>>(requestOptions, false);
            return response;
        }

        ///<inheritdoc/>   
        public async Task<BusCustomerDetailDTO> GetCustomerDetailByIdAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            string baseUrl = _appSettings.BusinessEntityApiUrl;
            string reqeustMethod = "bacustomer/detail/" + customerId;

            UserSession session = _userSessionManager.GetSession();

            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                      RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
            BusCustomerDetailDTO response = await httpRequestProcessor.ExecuteAsync<BusCustomerDetailDTO>(requestOptions, false);
            if(response != null && response.Customer != null) {
                Customer info = await _customerDS.GetCustomerByBusinesPartnerIdAsync(response.Customer.BusinessPartnerTenantId, token);
                if(info != null) {
                    Business bus = await _businessDS.GetBusinessByTenantIdAsync(info.TenantId, token);
                    response.Customer.CurrencyCode = info.Currency;
                    if(bus != null) {
                        List<BusinessAddressModelDTO> listAddress = await _businessAddressDS.GetAddressListByParentEntityIdAndAddressTypeAsync(response.Customer.TenantId, bus.ID, (int)BusinessAddressTypeEnum.DefaultBusinessAddress, token);
                        response.listBusinessAddress = listAddress;
                    }

                }
            }


            CustomeAccDetailDTO customeAccDetail = await _customerAccountDetail.GetCustomerAccDetailCustomerIdAsync(customerId, token);
            response.CustomerAcctDetail = customeAccDetail;
            return response;
        }

        ///<inheritdoc/>   
        public async Task<List<BusCustomerSetUpAppDTO>> GetCustomerListForBizSetupApp(Guid tenantId, bool isDeleted, CancellationToken token = default(CancellationToken)) {
            string baseUrl = _appSettings.BusinessEntityApiUrl;
            string reqeustMethod = "bacustomer/setupapp/customerlist/" + tenantId + "/" + isDeleted;

            UserSession session = _userSessionManager.GetSession();

            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                      RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
            List<BusCustomerSetUpAppDTO> response = await httpRequestProcessor.ExecuteAsync<List<BusCustomerSetUpAppDTO>>(requestOptions, false);
            for(int i = 0; i < response.Count; i++) {
                BusCustomerApplicationCountDTO customerApplicationCount = await _qBusinessAppDS.GetAppCountForCustomerAsync(response[i].BusinessPartnerTenantId);
                response[i].ApplicationCount = customerApplicationCount.ApplicationCount;
                response[i].AssignedAppInfo = await _qBusinessAppDS.GetCustomerAsignedAppInfoAsync(response[i].BusinessPartnerTenantId);
            }
            return response;
        }
        ///<inheritdoc/>   
        public async Task<BusCustomerSetUpAppViewDTO> GetCustomerDetailForBizSetupApp(Guid customerId, CancellationToken token = default(CancellationToken)) {
            string baseUrl = _appSettings.BusinessEntityApiUrl;
            string reqeustMethod = "bacustomer/setupapp/customerdetail/" + customerId;

            UserSession session = _userSessionManager.GetSession();

            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                      RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
            BusCustomerSetUpAppViewDTO response = await httpRequestProcessor.ExecuteAsync<BusCustomerSetUpAppViewDTO>(requestOptions, false);
            Customer cust = await _customerDS.FindAsync(del => del.BusinessPartnerTenantId == response.BusinessPartnerTenantId);
            response.DateTimeFormat = cust.DateTimeFormat;
            response.DecimalPrecision = cust.DecimalPrecision;
            response.DecimalSeperator = cust.DecimalSeperator;
            //response.CurrencyCode = cust.CurrencyCode;
            response.Language = cust.Language;
            response.TimeZone = cust.TimeZone;
            response.GroupValue = cust.GroupValue;
            response.GroupSeperator = cust.GroupSeperator;

            // APplication details
            response.CustomerApplicationList = await _qBusinessAppDS.GetAppForCustomer(session.TenantId, response.BusinessPartnerTenantId);

            // USer Details of the customer
            response.CustomerUserList = await _qBusinessAndUserDS.GetAllCustomerUserByBusinessPartnerId(session.TenantId, response.BusinessPartnerTenantId);

            return response;
        }

        #endregion Get

        public async Task UpdateCustomerDetail(BusCustomerDetailDTO customeAccDetailDTO, CancellationToken token = default(CancellationToken)) {
            Guid customerId = customeAccDetailDTO.Customer.ID;
            string baseUrl = _appSettings.BusinessEntityApiUrl;
            string reqeustMethod = "buscustomer/detail/customercontactupdate";//"buscustomer/detail/update";

            UserSession session = _userSessionManager.GetSession();

            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
           RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, reqeustMethod, customeAccDetailDTO, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

            await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);

            await _customerAccountDetail.AddUpdateCustomerAcctDetail(customerId, customeAccDetailDTO.CustomerAcctDetail, token);

        }

        ///<inheritdoc/>   
        public async Task<bool> UpdateCustomerDetailForBizSetupApp(BusCustomerUpdateDTO customerUpdateDTO, CancellationToken token = default(CancellationToken)) {
            string baseUrl = _appSettings.AppMgmtApiUrl;
            string reqeustMethod = "";

            UserSession session = _userSessionManager.GetSession();

            string businessEntityBaseUrl = _appSettings.BusinessEntityApiUrl;

            string businessEntityReqeustMethod = "buscustomer/setupapp/updatecustomerDetail";

            List<KeyValuePair<string, string>> businessEntityHeaderParameters = new List<KeyValuePair<string, string>>();
            businessEntityHeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            RequestOptions businessEntityRequestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
           RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, businessEntityReqeustMethod, customerUpdateDTO, _appSettings.AppName, _appSettings.IdentityServerUrl, businessEntityHeaderParameters);

            ServiceExecutor businessEntityHeaderParametersHttpRequestProcessor = new ServiceExecutor(businessEntityBaseUrl);

            await businessEntityHeaderParametersHttpRequestProcessor.ExecuteAsync<bool>(businessEntityRequestOptions, false);

            TenantUserSignUpDTO tenantUserSignUpDTO = null;
              long permissionBitMask = 0;
            foreach(BusCustomerUserDTO userDetail in customerUpdateDTO.UserList) {
                tenantUserSignUpDTO = new TenantUserSignUpDTO();
                tenantUserSignUpDTO.BusinessPartnerTenantId = customerUpdateDTO.BusinessPartnerTenantId;
                tenantUserSignUpDTO.Email = userDetail.Email;
                tenantUserSignUpDTO.FirstName = userDetail.FirstName;
                tenantUserSignUpDTO.LastName = userDetail.LastName;
                tenantUserSignUpDTO.FullName = userDetail.FirstName + " " + userDetail.LastName;;
                tenantUserSignUpDTO.TenantId = session.TenantId;
                tenantUserSignUpDTO.Phone = "";
                tenantUserSignUpDTO.IsPrimary = userDetail.IsPrimary;
                tenantUserSignUpDTO.UserType = (int)UserTypeEnum.Customer;
                List<UserAppRelationDTO> userApplist = new List<UserAppRelationDTO>();
                foreach(AppInfoDTO appInfo in userDetail.AssignedAppInfo) {
                    UserAppRelationDTO userApp = new UserAppRelationDTO();
                    userApp.AppId = appInfo.Id;
                    userApp.AppKey = appInfo.AppKey;
                   userApp.PermissionBitMask = appInfo.PermissionBitMask;
                  if (appInfo.AppKey.Equals(AppKeyEnum.cust.ToString(), StringComparison.CurrentCultureIgnoreCase)){
                      permissionBitMask = appInfo.PermissionBitMask;
                     }
            userApp.OperationType = (OperationType)appInfo.OperationType;
                    userApplist.Add(userApp);
                }
                UserAppRelationDTO custSetupApp = new UserAppRelationDTO();
                custSetupApp.AppId = new Guid(AppPortalConstants.CustomerSetUpApplicationId);
                custSetupApp.AppKey = AppKeyEnum.custsetup.ToString();
                userApplist.Add(custSetupApp);

                tenantUserSignUpDTO.UserAppRelationDTOList = userApplist;
                if(userDetail.OperationType == (int)OperationType.Add) {
                    reqeustMethod = "tenantuser/addcustomeruser";
                    List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
                    headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

                    RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                              RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, tenantUserSignUpDTO, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);

                    ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
                    TenantUserSignUpResponseDTO tenantUserSignUpResponseDTO = await httpRequestProcessor.ExecuteAsync<TenantUserSignUpResponseDTO>(requestOptions, false);
                   
            await AddRoleLinkingAndAppPrefrencesForUser(tenantUserSignUpResponseDTO.UserAppRelationDTOs, session.TenantId, tenantUserSignUpResponseDTO.TenantUserDTO.TenantUserId, session.TenantUserId, permissionBitMask, token);
                    _unitOfWork.SaveAll();
                    await GenerateAddUserNotification(tenantUserSignUpResponseDTO, session);
                }
                else if(userDetail.OperationType == (int)OperationType.Update) {
                    reqeustMethod = "tenantuser/updatecustomeruser";
                    tenantUserSignUpDTO.ID = userDetail.TenantUserId;
                    List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
                    headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

                    RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                              RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, tenantUserSignUpDTO, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);

                    ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
                    TenantUserSignUpResponseDTO tenantUserSignUpResponseDTO = await httpRequestProcessor.ExecuteAsync<TenantUserSignUpResponseDTO>(requestOptions, false);
          
          await AddDeleteRoleLinkingAndAppPrefrencesForUser(tenantUserSignUpDTO.UserAppRelationDTOList, session.TenantId, tenantUserSignUpResponseDTO.TenantUserDTO.TenantUserId, session.TenantUserId, token);
                    _unitOfWork.SaveAll();
                }
            }


            return true;
        }

        private async Task AddDeleteRoleLinkingAndAppPrefrencesForUser(List<UserAppRelationDTO> userAppRelationDTOs, Guid tenantId, Guid tenantUserId, Guid createdBy, CancellationToken cancellationToken = default(CancellationToken)) {

            // Get all the added applications
            List<UserAppRelationDTO> addApps = userAppRelationDTOs.Where(u => u.OperationType == OperationType.Add).ToList();
        List<UserAppRelationDTO> updateAppList = userAppRelationDTOs.Where(a => a.OperationType == OperationType.Update).ToList();
        if (addApps != null && addApps.Count > 0) {
                await AddRoleLinkingAndAppPrefrencesForUser(addApps, tenantId, tenantUserId, createdBy,0, cancellationToken);
            }
        if (updateAppList != null && updateAppList.Count > 0)
        {
          bool permissionChange = false;
          foreach (UserAppRelationDTO updateApp in updateAppList)
          {
            if (!permissionChange)
            {
              permissionChange = await UpdateRoleForTenantUser(updateApp, tenantId, tenantUserId, createdBy, cancellationToken);
              //await GenerateAppPermissionChangeNotification(userSession.TenantUserId, tenantUserUpdateRequestDTO.TenantUserId, tenantUserUpdateRequestDTO.TenantId, updateApp.AppKey);
            }
          }
          }
          // Get all the deleted applications
          List<UserAppRelationDTO> deleteApps = userAppRelationDTOs.Where(u => u.OperationType == OperationType.Delete).ToList();
            if(deleteApps != null && deleteApps.Count > 0) {
        await DeleteRoleLinkingAndAppPrefrencesForUser(addApps, tenantId, tenantUserId, createdBy, cancellationToken);

      }

        }

        private async Task AddRoleLinkingAndAppPrefrencesForUser(List<UserAppRelationDTO> userAppRelationDTOs, Guid tenantId, Guid tenantUserId, Guid createdBy, long permissionBitMask , CancellationToken cancellationToken = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();

            foreach(UserAppRelationDTO item in userAppRelationDTOs) {
                // Customer Portal - Customer Setup
                if(item.AppKey.Equals(AppKeyEnum.custsetup.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                    await AddUserRoleAndRoleLinkingAsync(tenantId, tenantUserId, item.AppId, createdBy, (long)CustomerUserCustomerSetupPermissionEnum.All, cancellationToken);
                    // ToDo: Review code to create tenant user preference entry.
                    //await AddTenantUserAppPrefernces(tenantId, tenantUserId, item.AppId, createdBy, (long)BusinessUserEmailPrefrenceEnum.All, (long)BusinessUserSMSPrefrenceEnum.All, cancellationToken);

                    //Old code
                    //await AddTenantUserAppPrefernces(tenantId, tenantUserId, item.AppId, createdBy, (long)CustomerUserCustomerSetupPreferenceEnum.None, (long)CustomerUserCustomerSetupPreferenceEnum.None, (long)CustomerUserCustomerSetupPreferenceEnum.None, cancellationToken);

                    //New code
                    //await AddAPPrefValue(tenantId, tenantUserId, item.AppId, createdBy, (long)CustomerUserCustomerAppPreferenceEnum.AllAPPreference, (long)CustomerUserCustomerAppPreferenceEnum.AllAPPreference, (long)CustomerUserCustomerAppPreferenceEnum.AllAPPreference, cancellationToken);
                    //await AddBEPrefValue(tenantId, tenantUserId, createdBy, userSession.ID.ToString(), (long)CustomerUserCustomerAppPreferenceEnum.AllBEPreference, (long)CustomerUserCustomerAppPreferenceEnum.AllBEPreference, (long)CustomerUserCustomerAppPreferenceEnum.AllBEPreference, cancellationToken);
                    //await AddPaymentPrefValue(tenantId, tenantUserId, createdBy, userSession.ID.ToString(), (long)CustomerUserCustomerAppPreferenceEnum.AllPayPreference, (long)CustomerUserCustomerAppPreferenceEnum.AllPayPreference, (long)CustomerUserCustomerAppPreferenceEnum.AllPayPreference, cancellationToken);
                }
                // Customer Portal - Payment App
                else if(item.AppKey.Equals(AppKeyEnum.pay.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                    TenantUserAppManagmentDTO tenantUserAppManagmentDTO = new TenantUserAppManagmentDTO();
                    tenantUserAppManagmentDTO.AppId = item.AppId;
                    tenantUserAppManagmentDTO.CreatedBy = createdBy;
                    tenantUserAppManagmentDTO.TenantId = tenantId;
                    tenantUserAppManagmentDTO.TenantUserId = tenantUserId;
                    tenantUserAppManagmentDTO.UserType = (int)UserTypeEnum.Customer;
                    tenantUserAppManagmentDTO.Admin = true;

                    ////New code
                    //tenantUserAppManagmentDTO.EmailPreference = (long)CustomerUserPaymentAppPreferenceEnum.AllPayPreference;
                    //tenantUserAppManagmentDTO.SMSPreference = (long)CustomerUserPaymentAppPreferenceEnum.AllPayPreference;
                    //tenantUserAppManagmentDTO.ASPreference = (long)CustomerUserPaymentAppPreferenceEnum.AllPayPreference;

                    tenantUserAppManagmentDTO.EmailPreference = (long)CustomerUserPaymentAppPreferenceEnum.AllEmail;
                    tenantUserAppManagmentDTO.SMSPreference = (long)CustomerUserPaymentAppPreferenceEnum.AllSMS;
                    tenantUserAppManagmentDTO.ASPreference = (long)CustomerUserPaymentAppPreferenceEnum.AllAS;

                    RequestOptions requestOptions = new RequestOptions();
                    requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
                    requestOptions.Method = "customeruser/appassign";
                    requestOptions.MethodData = tenantUserAppManagmentDTO;
                    requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
                    requestOptions.ServiceRequestType = RequestTypeEnum.Post;
                    requestOptions.BearerTokenInfo = new BearerTokenOption();
                    requestOptions.BearerTokenInfo.AppClientName = _appSettings.AppName;
                    requestOptions.BearerTokenInfo.AuthServiceUrl = _appSettings.IdentityServerUrl;
                    List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
                    headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
                    requestOptions.HeaderParameters = headerParams;
                    ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSettings.PaymentApiUrl);
                    await httpRequestProcessor.ExecuteAsync<BusinessTenantSignUpResponseDTO>(requestOptions, false);

                    ////New code
                    //await _custUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId, createdBy, item.AppId, (long)CustomerUserPaymentAppPreferenceEnum.AllAPPreference, (long)CustomerUserPaymentAppPreferenceEnum.AllAPPreference, (long)CustomerUserPaymentAppPreferenceEnum.AllAPPreference, cancellationToken);
                    //await _custUserPreferenceDS.AddBEPrefValue(tenantId, tenantUserId, createdBy, item.AppId, userSession.ID.ToString(), (long)CustomerUserPaymentAppPreferenceEnum.AllBEPreference, (long)CustomerUserPaymentAppPreferenceEnum.AllBEPreference, (long)CustomerUserPaymentAppPreferenceEnum.AllBEPreference, cancellationToken);
                }
                // Customer Portal - Customer App
                else if(item.AppKey.Equals(AppKeyEnum.cust.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                    await AddUserRoleAndRoleLinkingAsync(tenantId, tenantUserId, item.AppId, createdBy, permissionBitMask, cancellationToken);
                    //old code
                    //await AddTenantUserAppPrefernces(tenantId, tenantUserId, item.AppId, createdBy, (long)CustomerUserCustomerAppPreferenceEnum.All, (long)CustomerUserCustomerAppPreferenceEnum.All, (long)CustomerUserCustomerAppPreferenceEnum.All, cancellationToken);

                    //New code
                    await _custUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId, createdBy, item.AppId, (long)CustomerUserCustomerAppPreferenceEnum.AllEmail, (long)CustomerUserCustomerAppPreferenceEnum.AllSMS, (long)CustomerUserCustomerAppPreferenceEnum.AllAS, cancellationToken);
                    //await _custUserPreferenceDS.AddBEPrefValue(tenantId, tenantUserId, createdBy, item.AppId, userSession.ID.ToString(), (long)CustomerUserCustomerAppPreferenceEnum.AllBEPreference, (long)CustomerUserCustomerAppPreferenceEnum.AllBEPreference, (long)CustomerUserCustomerAppPreferenceEnum.AllBEPreference, cancellationToken);
                    //await _custUserPreferenceDS.AddPaymentPrefValue(tenantId, tenantUserId, createdBy, item.AppId, userSession.ID.ToString(), (long)CustomerUserCustomerAppPreferenceEnum.AllPayPreference, (long)CustomerUserCustomerAppPreferenceEnum.AllPayPreference, (long)CustomerUserCustomerAppPreferenceEnum.AllPayPreference, cancellationToken);
                }
                else {
                    //await AddUserRoleAndRoleLinkingAsync(tenantId, tenantUserId, item.AppId, createdBy, 0, cancellationToken);
                    //await AddTenantUserAppPrefernces(tenantId, tenantUserId, item.AppId, createdBy, 0, 0, 0, cancellationToken);
                    throw new InvalidOperationException("Invalid Application - BusCustomerDS.AddRoleLinkingAndAppPreferencesForUser");
                }
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
            Guid roleId = await _roleDS.GetOrCreateRoleAsync(permissionbitMask, appId, (int)UserTypeEnum.Customer, createdBy, cancellationToken);

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
      private async Task<bool> UpdateRoleForTenantUser(UserAppRelationDTO userAppRelationDTO, Guid tenantId, Guid tenantUserId, Guid createdBy, CancellationToken cancellationToken = default(CancellationToken))
      {

        UserSession userSession = _userSessionManager.GetSession();
        bool permissionUpdate = false;
        if (userAppRelationDTO.AppKey.Equals(AppKeyEnum.cust.ToString(), StringComparison.CurrentCultureIgnoreCase))
        
        {
          // Get role/add role and add a entry in rolelinking table  // Add/Update  rolelinking for the user.
          Guid roleId = await _roleDS.GetOrCreateRoleAsync(userAppRelationDTO.PermissionBitMask, userAppRelationDTO.AppId, (int)UserTypeEnum.Customer, tenantUserId);

          RoleLinking roleLinking = await _roleLinkingDS.FindAsync(a => a.TenantUserId == tenantUserId && a.AppId == userAppRelationDTO.AppId && a.TenantId == tenantId && a.Deleted == false);
          if (roleLinking != null && (roleLinking.RoleId != roleId))
          {

            // get role
            Role role = await _roleDS.GetAsync(roleId);

            // Delete user session if permissions are changed
            //await ValidationOnUpdateUser(tenantUserId, tenantId, (int)UserTypeEnum.Customer, userAppRelationDTO.AppId, businessPartnerTenantId, role.RoleKey);
            await _userSessionManager.DeletedByAppUserAndAppId(tenantUserId, userAppRelationDTO.AppId);
            permissionUpdate = true;
            roleLinking.RoleId = roleId;
            _roleLinkingDS.UpdateSystemFieldsByOpType(roleLinking, OperationType.Update);
            await _roleLinkingDS.UpdateAsync(roleLinking, roleLinking.ID);
            //_unitOfWork.SaveAll();
          }
        }
        return permissionUpdate;
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
                Guid appId = tenantUserSignUpResponseDTO.UserAppRelationDTOs.Where(u => u.AppKey == AppKeyEnum.custsetup.ToString()).FirstOrDefault().AppId;

                businessAccountNotificationDTO.PasswordCode = tenantUserSignUpResponseDTO.TenantUserDTO.Code;
                businessAccountNotificationDTO.InvitedUserEmail = tenantUserSignUpResponseDTO.TenantUserDTO.Email;
                businessAccountNotificationDTO.InvitedUserIdentityUserId = tenantUserSignUpResponseDTO.TenantUserDTO.IdentityUserId;
                businessAccountNotificationDTO.TenantId = userSession.TenantId;
                businessAccountNotificationDTO.InvitedUserAppKey = AppKeyEnum.biz.ToString();
                businessAccountNotificationDTO.InvitedUserId = tenantUserSignUpResponseDTO.TenantUserDTO.TenantUserId;
                businessAccountNotificationDTO.InvitedUserAppId = appId;
                businessAccountNotificationDTO.PublisherName = " ";
                businessAccountNotificationDTO.BusinessCompanyName = " ";
                businessAccountNotificationDTO.PartnerType = "Customer";
                businessAccountNotificationDTO.BusinessPartnerCompanyName = " ";
                businessAccountNotificationDTO.InvitorUserName = userSession.UserName;
                businessAccountNotificationDTO.InvitedUserFullName = tenantUserSignUpResponseDTO.TenantUserDTO.FullName;
                businessAccountNotificationDTO.ApplicationName = "Payment";
                businessAccountNotificationDTO.SubDomain = userSession.Subdomain;
                businessAccountNotificationDTO.PortalURL = string.Format(_appSettings.CustomerPortalClientURL, userSession.Subdomain);
                businessAccountNotificationDTO.CopyRightText = " ";
                businessAccountNotificationDTO.UserSession = userSession;
                await _businessNotificationHandler.GenerateBusinessPartnerPrimaryUserNewEmailIdInvitedNotification(businessAccountNotificationDTO);

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


        /// <summary>
        /// Delete customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="businessPartnerTenantId"></param>
        /// <param name="token">Cancellation token for async operations</param>   
        public async Task<bool> DeleteCustomerAsync(Guid customerId, Guid businessPartnerTenantId, CancellationToken token = default(CancellationToken)) {
            List<AppInfoDTO> list = await _qCustomerAppDS.GetAllCustomerApplicationsAsync(businessPartnerTenantId);
            // If application assigned to customer.
            if(list.Count >= 2) {
                EwpError error = new EwpError();
                error.ErrorType = ErrorType.Validation;
                EwpErrorData errorData = new EwpErrorData();
                errorData.ErrorSubType = (int)ValidationErrorSubType.SelfReference;
                errorData.Message = Common.AppPortalConstants.CustomerDeleteException;
                error.EwpErrorDataList.Add(errorData);
                throw new EwpValidationException(Common.AppPortalConstants.CustomerDeleteException, error.EwpErrorDataList);
            }
            Customer cust = await _customerDS.GetCustomerByBusinesPartnerIdAsync(businessPartnerTenantId, token);
            if(cust != null) {

                // Preparing api calling process model.           
                string Method = "BusCustomer/delete/" + customerId.ToString();
                List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
                string clientsessionid = _userSessionManager.GetSession().ID.ToString();
                KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                listHeader.Add(hdr);
                RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, null, _appSettings.AppName, _appSettings.IdentityServerUrl, listHeader);
                ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSettings.BusinessEntityApiUrl);
                await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);

                cust.Deleted = true;
                _customerDS.UpdateSystemFieldsByOpType(cust, OperationType.Update);
                await _customerDS.UpdateAsync(cust, cust.ID, token);
                // Save Data
                _unitOfWork.SaveAll();
            }
            
            return true;
        }
    
  }
}
