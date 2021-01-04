using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {

  /// <summary>
  /// This class implements standard business logic and operations for customer entity data from connectors to local database.
  /// </summary>
  public class CustomerSignUpDS : ICustomerSignUpDS {

    #region Local Member

    ICustomerDS _customerDS;
    IBusinessDS _businessDS;
    IUnitOfWork _unitOfWork;
    IUserSessionManager _userSessionManager;
    private AppPortalAppSettings _appSettings;

    #endregion Local Member

    #region Constructor

    /// <summary>
    /// Initialinzing local variables .
    /// </summary>
    /// <param name="customerDS"></param>
    /// <param name="unitOfWork"></param>
    /// <param name="appSettings"></param>
    public CustomerSignUpDS(ICustomerDS customerDS, IBusinessDS businessDS, IUnitOfWork unitOfWork, IUserSessionManager userSessionManager, IOptions<AppPortalAppSettings> appSettings) {
      _customerDS = customerDS;
      _businessDS = businessDS;
      _unitOfWork = unitOfWork;
      _userSessionManager = userSessionManager;
      _appSettings = appSettings.Value;

    }

    #endregion Constructor

    #region Signup Customer

    /// <summary>
    /// Signup Customer .
    /// <summary>
    /// </summary>
    /// <param name="customerSignUpDTO"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<CustomerSignUpResDTO> CustomerSignUpAsync(List<CustomerSignUpReqDTO> customerSignUpDTOs, CancellationToken token = default(CancellationToken)) {

      CustomerSignUpResDTO response;

      // Add Customer Ext;
      for (int i = 0; i < customerSignUpDTOs.Count; i++) {
        await AddCustomerExt(customerSignUpDTOs[i], false);
      }
      // Call Singup on App Mgmt 
      try {
        response = await TenantSignUpInAppMgmt(customerSignUpDTOs, token);
      }
      catch (Exception ex) {
        // ToDo: Log Error Here.
        // ToDo: Also call Api to rollback transactions.
        throw;
      }

      // Call Send Notification 

      // Save Data
      _unitOfWork.SaveAll();

      return response;
    }

    /// <summary>
    /// Signup customer.
    /// Method will use in standalone case.
    /// </summary>
    /// <param name="signupCustDTO"></param>
    /// <param name="token">Cancellation token for async operations</param>   
    public async Task<CustomerSignUpResDTO> SignUpCustomerAsync(SignUpBACustomerDTO signupCustDTO, CancellationToken token = default(CancellationToken)) {
      // Preparing api calling process model.           
      string Method = "BACustomer/signupcustomer";
      List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
      string clientsessionid = _userSessionManager.GetSession().ID.ToString();
      KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
      listHeader.Add(hdr);
      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, signupCustDTO, _appSettings.AppName, _appSettings.IdentityServerUrl, listHeader);
      ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSettings.BusinessEntityApiUrl);
      CustomerSignUpReqDTO obj = await httpRequestProcessor.ExecuteAsync<CustomerSignUpReqDTO>(requestOptions, false);
      await AddCustomerExt(signupCustDTO, obj, true);
      CustomerSignUpResDTO response;
      // Call Singup on App Mgmt 
      try {
        List<CustomerSignUpReqDTO> list = new List<CustomerSignUpReqDTO>();
        list.Add(obj);
        response = await TenantSignUpInAppMgmt(list, token);
      }
      catch (Exception ex) {
        // ToDo: Log Error Here.
        // ToDo: Also call Api to rollback transactions.
        throw;
      }

      // Call Send Notification 

      // Save Data
      _unitOfWork.SaveAll();
      return response;
    }

    #endregion Signup Customer

    #region Private Methods

    private async Task AddCustomerExt(CustomerSignUpReqDTO customerSignUpDTO, bool CanUpdateCurrency, CancellationToken token = default(CancellationToken)) {

      Business business = await _businessDS.FindAsync(bus => bus.TenantId == customerSignUpDTO.TenantId);
      int decimalPrecision = 0;
      string timeZone = "";
      string dateTimeFormat = "DD/MM/YYYY";
      if (business != null) {
        decimalPrecision = business.DecimalPrecision;
        timeZone = business.TimeZone;
        dateTimeFormat = business.DateTimeFormat;
      }
      // Add in Customer Extention 
      Customer customer = new Customer();
      customer.ID = Guid.NewGuid();
      customer.CreatedBy = customerSignUpDTO.BusinesPrimaryUserId;
      customer.UpdatedBy = customerSignUpDTO.BusinesPrimaryUserId;
      customer.CreatedOn = DateTime.UtcNow;
      customer.UpdatedOn = DateTime.UtcNow;
      customer.BusinessPartnerTenantId = customerSignUpDTO.BusinesPartnerTenantId;
      customer.Deleted = false;
      customer.CanUpdateCurrency = CanUpdateCurrency;
      customer.Configured = false;
      customer.Currency = customerSignUpDTO.Currency;
      customer.DecimalPrecision = decimalPrecision;
      customer.TimeZone = timeZone;
      customer.TenantId = customerSignUpDTO.TenantId;
      customer.DateTimeFormat = dateTimeFormat;
      await _customerDS.AddAsync(customer, token);
    }

    private async Task AddCustomerExt(SignUpBACustomerDTO signUpCustDTO, CustomerSignUpReqDTO customerSignUpDTO, bool CanUpdateCurrency, CancellationToken token = default(CancellationToken)) {
      // Add in Customer Extention 
      Customer customer = new Customer();
      customer.ID = Guid.NewGuid();
      customer.CreatedBy = customerSignUpDTO.BusinesPrimaryUserId;
      customer.UpdatedBy = customerSignUpDTO.BusinesPrimaryUserId;
      customer.CreatedOn = DateTime.UtcNow;
      customer.UpdatedOn = DateTime.UtcNow;
      customer.BusinessPartnerTenantId = customerSignUpDTO.BusinesPartnerTenantId;
      customer.Deleted = false;
      customer.CanUpdateCurrency = CanUpdateCurrency;
      customer.Configured = false;
      customer.Currency = customerSignUpDTO.Currency;
      customer.Language = signUpCustDTO.Language;
      customer.TimeZone = signUpCustDTO.TimeZone;
      customer.DateTimeFormat = signUpCustDTO.DateTimeFormat;
      customer.DecimalPrecision = signUpCustDTO.DecimalPrecision;
      customer.DecimalSeperator = signUpCustDTO.DecimalSeperator;
      customer.GroupValue = signUpCustDTO.GroupValue;
      customer.GroupSeperator = signUpCustDTO.GroupSeperator;
      customer.TenantId = customerSignUpDTO.TenantId;
      await _customerDS.AddAsync(customer, token);
    }

    private async Task<CustomerSignUpResDTO> TenantSignUpInAppMgmt(List<CustomerSignUpReqDTO> customerSignUpDTOs, CancellationToken token = default(CancellationToken)) {
      string baseUrl = _appSettings.AppMgmtApiUrl;
      string reqeustMethod = "tenant/customersignup";

      //UserSession session = _userSessionManager.GetSession();

      //List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      //headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, customerSignUpDTOs, _appSettings.AppName, _appSettings.IdentityServerUrl);
      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
      var response = await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);
      return new CustomerSignUpResDTO();
    }

    #endregion Private Methods
  }
}
