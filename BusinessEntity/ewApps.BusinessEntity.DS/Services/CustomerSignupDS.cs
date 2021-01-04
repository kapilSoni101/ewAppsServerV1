using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessEntity.DTO;
using ewApps.BusinessEntity.Common;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.CommonService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;

namespace ewApps.BusinessEntity.DS
{

  /// <summary>
  /// This class implements standard business logic and operations for customer entity data from connectors to local database.
  /// </summary>
  public class CustomerSignupDS : ICustomerSignupDS
  {

    #region Local Member 

    private IBACustomerDS _customerDS;
    private IBACustomerAddressDS _custAddressDS;
    private IBACustomerContactDS _custContactDS;
    private IBACustomerPaymentDetailDS _custPayDetDS;
    private IUnitOfWork _unitOfWork;
    IUserSessionManager _userSessionManager;
    private BusinessEntityAppSettings _appSettings;
    IQNotificationDS _qNotificationDS;
    IBusinessEntityNotificationHandler _businessEntityNotificationHandler;
    IUniqueIdentityGeneratorDS _identityDataService;

    #endregion Local Member 

    #region Constructor

    /// <summary>
    /// initilize constructor
    /// </summary>
    /// <param name="customerDs"></param>
    /// <param name="custAddressDS"></param>
    /// <param name="custContactDS"></param>
    /// <param name="custPayDetDS"></param>
    /// <param name="unitOfWork"></param>
    /// <param name="appSettings"></param>
    public CustomerSignupDS(IBACustomerDS customerDs, IBACustomerAddressDS custAddressDS, IBACustomerContactDS custContactDS,
                        IBACustomerPaymentDetailDS custPayDetDS, IUnitOfWork unitOfWork,
                        IUserSessionManager userSessionManager, IQNotificationDS qNotificationDS,
    IBusinessEntityNotificationHandler businessEntityNotificationHandler, IUniqueIdentityGeneratorDS identityDataService,
    IOptions<BusinessEntityAppSettings> appSettings)
    {
      _customerDS = customerDs;
      _custAddressDS = custAddressDS;
      _custContactDS = custContactDS;
      _custPayDetDS = custPayDetDS;
      _unitOfWork = unitOfWork;
      _userSessionManager = userSessionManager;
      _qNotificationDS = qNotificationDS;
      _businessEntityNotificationHandler = businessEntityNotificationHandler;
      _identityDataService = identityDataService;
      _appSettings = appSettings.Value;
    }

    #endregion Constructor

    ///<inheritdoc/>
    public async Task<bool> AddCustomerListAsync(List<BACustomerSyncDTO> customerDetailList, Guid tenantId, Guid tenantUserId, bool isBulkInsert, CancellationToken token = default(CancellationToken))
    {
      List<BACustomer> customerList = new List<BACustomer>();
      List<string> customerAddERPKeyList = new List<string>();
      List<string> customerUpdateERPKeyList = new List<string>();
      // SingUp Customer On BusinessEntity
      for (int i = 0; i < customerDetailList.Count; i++)
      {
        if (isBulkInsert)
        {
          BACustomer baCustomer = await SignUp(customerDetailList[i], tenantId, tenantUserId);
          if (baCustomer != null)
          {
            customerList.Add(baCustomer);
          }
        }
        else
        {
          if (customerDetailList[i].OpType.Equals("Inserted"))
          {
            BACustomer baCustomer = await AddUpdateCustomerAsync(customerDetailList[i], tenantId, tenantUserId, customerList);
            if (baCustomer != null)
            {
              //customerList.Add(baCustomer);
              customerAddERPKeyList.Add(baCustomer.ERPCustomerKey);
            }
          }
          else if (customerDetailList[i].OpType.Equals("Modified"))
          {
            customerUpdateERPKeyList.Add(customerDetailList[i].ERPCustomerKey);
            await AddUpdateCustomerAsync(customerDetailList[i], tenantId, tenantUserId, customerList);
          }
        }
      }
      // SingUp Customer On App Portal
      try
      {
        await CustomerSingUpOnAppPortalAsync(customerList, token);
      }
      catch (Exception ex)
      {
        StringBuilder exceptionDetail = new StringBuilder();
        exceptionDetail.Append("Exception occurred in InitDB-CustomerSignUpDS:-");
        exceptionDetail.AppendLine();
        exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
        Log.Error(ex, exceptionDetail.ToString());
        // TODO: Logo Error
        // TODO: Rollback transaction
        throw;
      }
      // Save Data
      _unitOfWork.SaveAll();
      if (customerAddERPKeyList.Count > 0)
      {
        OnAddCustomerInIntegratedModeAsync(customerAddERPKeyList, tenantId, (long)BusinessEntityNotificationEventEnum.BizAddCutsomer);
      }
      if (customerUpdateERPKeyList.Count > 0)
      {
        OnUpdateCustomerInIntegratedModeAsync(customerUpdateERPKeyList, tenantId, (long)BusinessEntityNotificationEventEnum.BizUpdateCutsomer);
      }
      return true;
    }


    /// <summary>
    /// Signs up.
    /// </summary>
    /// <param name="customerSyncDTO">The customer synchronize dto.</param>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="tenantUserId">The tenant user identifier.</param>
    /// <param name="token">The token.</param>
    /// <returns></returns>
    public async Task<BACustomer> SignUp(BACustomerSyncDTO customerSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {

      try
      {


        // Validate Duplicate Customer
        BACustomer customer = await _customerDS.FindAsync(cust => cust.ERPCustomerKey == customerSyncDTO.ERPCustomerKey && cust.TenantId == tenantId);
        if (customer != null)
        {
          return null;
        }

        // Add Customer
        Guid businessPartnerTenantId = Guid.NewGuid();
        Guid CustomerId = Guid.NewGuid();
        customerSyncDTO.BusinessPartnerTenantId = businessPartnerTenantId;
        customer = BACustomerSyncDTO.MapToEntity(customerSyncDTO);
        customer.ID = CustomerId;
        customer.CreatedBy = tenantUserId;//;// Session
        customer.UpdatedBy = tenantUserId;
        customer.CreatedOn = DateTime.UtcNow;
        customer.UpdatedOn = DateTime.UtcNow;
        customer.Deleted = false;
        customer.TenantId = tenantId;
        await _customerDS.AddAsync(customer, token);

        //customerList.Add(customer);

        // Add Customer Address
        if (customerSyncDTO.CustomerAddressList != null && customerSyncDTO.CustomerAddressList.Count > 0)
        {
          foreach (var item in customerSyncDTO.CustomerAddressList)
          {
            item.CustomerId = CustomerId;
            BACustomerAddress customerAddress = BACustomerAddressSyncDTO.MapToEntity(item);
            customerAddress.ID = Guid.NewGuid();
            customerAddress.CreatedBy = tenantUserId;//;// Session
            customerAddress.UpdatedBy = tenantUserId;
            customerAddress.CreatedOn = DateTime.UtcNow;
            customerAddress.UpdatedOn = DateTime.UtcNow;
            customerAddress.Deleted = false;
            customerAddress.TenantId = tenantId;
            await _custAddressDS.AddAsync(customerAddress);
          }
        }

        // Add Customer Contact
        if (customerSyncDTO.CustomerContactList != null && customerSyncDTO.CustomerContactList.Count > 0)
        {
          foreach (var item in customerSyncDTO.CustomerContactList)
          {
            item.CustomerId = CustomerId;
            BACustomerContact customerContact = BACustomerContactSyncDTO.MapToEntity(item);
            customerContact.ID = Guid.NewGuid();
            customerContact.CreatedBy = tenantUserId;//;// Session
            customerContact.UpdatedBy = tenantUserId;
            customerContact.CreatedOn = DateTime.UtcNow;
            customerContact.UpdatedOn = DateTime.UtcNow;
            customerContact.Deleted = false;
            customerContact.TenantId = tenantId;
            await _custContactDS.AddAsync(customerContact);
          }
        }

        // Add Customer Payment
        if (customerSyncDTO.CustomerPaymentDetailList != null && customerSyncDTO.CustomerPaymentDetailList.Count > 0)
        {
          foreach (var item in customerSyncDTO.CustomerPaymentDetailList)
          {
            item.CustomerId = CustomerId;
            BACustomerPaymentDetail customerPayment = BACustomerPaymentDetailSyncDTO.MapToEntity(item);
            customer.ID = Guid.NewGuid();
            customerPayment.CreatedBy = tenantUserId;//;// Session
            customerPayment.UpdatedBy = tenantUserId;
            customerPayment.CreatedOn = DateTime.UtcNow;
            customerPayment.UpdatedOn = DateTime.UtcNow;
            customerPayment.Deleted = false;
            customerPayment.TenantId = tenantId;
            await _custPayDetDS.AddAsync(customerPayment);
          }
        }
        return customer;
      }
      catch (Exception ex)
      {
        return null;
        // throw;
      }
    }
    public async Task<BACustomer> AddUpdateCustomerAsync(BACustomerSyncDTO customerSyncDTO, Guid tenantId, Guid tenantUserId, List<BACustomer> customerList, CancellationToken token = default(CancellationToken))
    {
      BACustomer customer = await _customerDS.FindAsync(cust => cust.ERPCustomerKey == customerSyncDTO.ERPCustomerKey && cust.TenantId == tenantId);
      if (customer != null)
      {
        await UpdateCustomerAsync(customerSyncDTO, tenantId, tenantUserId);
      }
      else
      {
        customer = await SignUp(customerSyncDTO, tenantId, tenantUserId);
        if (customer != null)
        {
          customerList.Add(customer);
        }
      }
      return customer;
    }
    /// <summary>
    /// Add Update Customer Address 
    /// </summary>
    /// <param name="customerAddSyncDTO"></param>
    /// <param name="tenantId"></param>
    /// <param name="tenantUserId"></param>
    /// <param name="customerId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<bool> AddUpdateCustomerAddresAsync(BACustomerAddressSyncDTO customerAddSyncDTO, Guid tenantId, Guid tenantUserId, Guid customerId, CancellationToken token = default(CancellationToken))
    {
      BACustomerAddress customerAddress = await _custAddressDS.FindAsync(custAdd => custAdd.ERPCustomerKey == customerAddSyncDTO.ERPCustomerKey && custAdd.Label ==
                                                                  customerAddSyncDTO.Label && custAdd.TenantId == tenantId);
      if (customerAddress != null)
      {
        customerAddress = BACustomerAddressSyncDTO.MapToEntity(customerAddSyncDTO);
        _custAddressDS.UpdateSystemFieldsByOpType(customerAddress, OperationType.Update);
        await _custAddressDS.UpdateAsync(customerAddress, customerAddress.ID, token);
      }
      else
      {
        customerAddSyncDTO.CustomerId = customerId;
        customerAddress = BACustomerAddressSyncDTO.MapToEntity(customerAddSyncDTO);
        customerAddress.ID = Guid.NewGuid();
        customerAddress.CreatedBy = tenantUserId;//;// Session
        customerAddress.UpdatedBy = tenantUserId;
        customerAddress.CreatedOn = DateTime.UtcNow;
        customerAddress.UpdatedOn = DateTime.UtcNow;
        customerAddress.Deleted = false;
        customerAddress.TenantId = tenantId;
        await _custAddressDS.AddAsync(customerAddress);
      }
      return true;
    }
    /// <summary>
    /// Add Update Customer Contact
    /// </summary>
    /// <param name="customerContactSyncDTO"></param>
    /// <param name="tenantId"></param>
    /// <param name="tenantUserId"></param>
    /// <param name="customerId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<bool> AddUpdateCustomerContactAsync(BACustomerContactSyncDTO customerContactSyncDTO, Guid tenantId, Guid tenantUserId, Guid customerId, CancellationToken token = default(CancellationToken))
    {
      BACustomerContact customerContact = await _custContactDS.FindAsync(custContact => custContact.ERPCustomerKey == customerContactSyncDTO.ERPCustomerKey && custContact.ERPContactKey ==
                                                                  customerContactSyncDTO.ERPContactKey && custContact.TenantId == tenantId);
      if (customerContact != null)
      {
        customerContact = BACustomerContactSyncDTO.MapToEntity(customerContactSyncDTO);
        _custContactDS.UpdateSystemFieldsByOpType(customerContact, OperationType.Update);
        await _custContactDS.UpdateAsync(customerContact, customerContact.ID, token);
      }
      else
      {
        customerContactSyncDTO.CustomerId = customerId;
        customerContact = BACustomerContactSyncDTO.MapToEntity(customerContactSyncDTO);
        customerContact.ID = Guid.NewGuid();
        customerContact.CreatedBy = tenantUserId;//;// Session
        customerContact.UpdatedBy = tenantUserId;
        customerContact.CreatedOn = DateTime.UtcNow;
        customerContact.UpdatedOn = DateTime.UtcNow;
        customerContact.Deleted = false;
        customerContact.TenantId = tenantId;
        await _custContactDS.AddAsync(customerContact);
      }
      return true;
    }
    /// <summary>
    /// Signs up.
    /// </summary>
    /// <param name="customerSyncDTO">The customer synchronize dto.</param>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="tenantUserId">The tenant user identifier.</param>
    /// <param name="token">The token.</param>
    /// <returns></returns>
    public async Task<BACustomer> UpdateCustomerAsync(BACustomerSyncDTO customerSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {

      try
      {

        BACustomer customer = await _customerDS.FindAsync(cust => cust.ERPCustomerKey == customerSyncDTO.ERPCustomerKey && cust.TenantId == tenantId);

        customer = BACustomerSyncDTO.MapToEntity(customerSyncDTO, customer);
        _customerDS.UpdateSystemFieldsByOpType(customer, OperationType.Update);


        // update invoice detail
        await _customerDS.UpdateAsync(customer, customer.ID, token);
        //customerList.Add(customer);

        // Add Customer Address
        if (customerSyncDTO.CustomerAddressList != null && customerSyncDTO.CustomerAddressList.Count > 0)
        {
          foreach (var item in customerSyncDTO.CustomerAddressList)
          {
            await AddUpdateCustomerAddresAsync(item, tenantId, tenantUserId, customer.ID);
          }
        }

        // Add Customer Contact
        if (customerSyncDTO.CustomerContactList != null && customerSyncDTO.CustomerContactList.Count > 0)
        {
          foreach (var item in customerSyncDTO.CustomerContactList)
          {
            await AddUpdateCustomerContactAsync(item, tenantId, tenantUserId, customer.ID);
          }
        }

        // Add Customer Payment
        //if(customerSyncDTO.CustomerPaymentDetailList != null && customerSyncDTO.CustomerPaymentDetailList.Count > 0) {
        //    foreach(var item in customerSyncDTO.CustomerPaymentDetailList) {
        //        item.CustomerId = customer.ID;
        //        BACustomerPaymentDetail customerPayment = BACustomerPaymentDetailSyncDTO.MapToEntity(item);
        //        customer.ID = Guid.NewGuid();
        //        customerPayment.CreatedBy = tenantUserId;//;// Session
        //        customerPayment.UpdatedBy = tenantUserId;
        //        customerPayment.CreatedOn = DateTime.UtcNow;
        //        customerPayment.UpdatedOn = DateTime.UtcNow;
        //        customerPayment.Deleted = false;
        //        customerPayment.TenantId = tenantId;
        //        await _custPayDetDS.AddAsync(customerPayment);
        //    }
        //}

        //try {
        //    List<string> customerAddERPKeyList = new List<string>();
        //    customerAddERPKeyList.Add(customer.ERPCustomerKey);
        //    OnAddCustomerInIntegratedModeAsync(customerAddERPKeyList, customer.TenantId, (long)BusinessEntityNotificationEventEnum.BizUpdateCutsomer);
        //}
        //catch { } 
        return customer;
      }
      catch (Exception ex)
      {
        return null;
        // throw;
      }

    }

    #region Private Methods

    private async Task CustomerSingUpOnAppPortalAsync(List<BACustomer> customerList, CancellationToken token = default(CancellationToken))
    {
      if (customerList.Count <= 0)
      {
        return;
      }

      List<CustomerSignUpDTO> CustomerSignUpDTOs = new List<CustomerSignUpDTO>();
      for (int i = 0; i < customerList.Count; i++)
      {
        CustomerSignUpDTO request = new CustomerSignUpDTO();
        request.TenantId = customerList[i].TenantId;
        request.BusinesPartnerTenantId = customerList[i].BusinessPartnerTenantId;
        request.Currency = Convert.ToString(PicklistHelper.GetCurrencyIdBySymbol(customerList[i].Currency));
        request.CutomerName = customerList[i].CustomerName;
        request.BusinesPrimaryUserId = customerList[i].CreatedBy;
        CustomerSignUpDTOs.Add(request);
      }

      string baseUrl = _appSettings.AppPortalApiUrl;
      string relativeUrl = "customer/signup";

      UserSession session = _userSessionManager.GetSession();

      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, relativeUrl, CustomerSignUpDTOs, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);
      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
      var response = await httpRequestProcessor.ExecuteAsync<CustomerSignUpResDTO>(requestOptions, false);
    }

    #endregion Private Methods

    #region Signup Customer in Standalone Mode

    /// <summary>
    /// Signs up customer.
    /// </summary>
    /// <param name="customerSyncDTO">customer dto.</param>
    /// <param name="token">The token.</param>
    /// <returns></returns>
    public async Task<CustomerSignUpDTO> SignUpCustomerAsync(SignUpBACustomerDTO customerSyncDTO, CancellationToken token = default(CancellationToken))
    {

      UserSession session = _userSessionManager.GetSession();
      Guid tenantId = session.TenantId;
      Guid tenantUserId = session.TenantUserId;

      // Add Customer
      Guid businessPartnerTenantId = Guid.NewGuid();
      Guid CustomerId = Guid.NewGuid();
      customerSyncDTO.BusinessPartnerTenantId = businessPartnerTenantId;
      BACustomer customer = SignUpBACustomerDTO.MapToEntity(customerSyncDTO);
      int identity = _identityDataService.GetIdentityNo(Guid.Empty, (int)EntityTypeEnum.BACustomer, BusinessEntityConstants.CustomerPrefix, 1000);
      customer.ERPCustomerKey = BusinessEntityConstants.CustomerPrefix + identity;
      // For temp purpose
      customer.ERPConnectorKey = tenantId.ToString();
      customer.ID = CustomerId;
      customer.CreatedBy = tenantUserId;//;// Session
      customer.UpdatedBy = tenantUserId;
      customer.CreatedOn = DateTime.UtcNow;
      customer.UpdatedOn = DateTime.UtcNow;
      customer.Deleted = false;
      customer.TenantId = tenantId;
      await _customerDS.AddAsync(customer, token);

      // Add Customer Address
      if (customerSyncDTO.CustomerAddressList != null && customerSyncDTO.CustomerAddressList.Count > 0)
      {
        foreach (var item in customerSyncDTO.CustomerAddressList)
        {
          item.CustomerId = CustomerId;
          item.ERPCustomerKey = customer.ERPCustomerKey;
          BACustomerAddress customerAddress = SignUpBACustomerAddressDTO.MapToEntity(item);
          customerAddress.ID = Guid.NewGuid();
          customerAddress.ERPConnectorKey = customer.ERPConnectorKey;
          customerAddress.CreatedBy = tenantUserId;//;// Session
          customerAddress.UpdatedBy = tenantUserId;
          customerAddress.CreatedOn = DateTime.UtcNow;
          customerAddress.UpdatedOn = DateTime.UtcNow;
          customerAddress.Deleted = false;
          customerAddress.TenantId = tenantId;
          await _custAddressDS.AddAsync(customerAddress);
        }
      }

      // Add Customer Contact
      if (customerSyncDTO.CustomerContactList != null && customerSyncDTO.CustomerContactList.Count > 0)
      {
        foreach (var item in customerSyncDTO.CustomerContactList)
        {
          int contactKeyidentity = _identityDataService.GetIdentityNo(tenantId, (int)EntityTypeEnum.BACustomerContact, BusinessEntityConstants.CustomerPrefix, 1000);
          item.CustomerId = CustomerId;
          item.ERPCustomerKey = customer.ERPCustomerKey;
          item.ERPContactKey = contactKeyidentity.ToString();
          BACustomerContact customerContact = SignUpBACustomerContactDTO.MapToEntity(item);
          customerContact.ID = Guid.NewGuid();
          customerContact.ERPConnectorKey = customer.ERPConnectorKey;
          customerContact.CreatedBy = tenantUserId;//;// Session
          customerContact.UpdatedBy = tenantUserId;
          customerContact.CreatedOn = DateTime.UtcNow;
          customerContact.UpdatedOn = DateTime.UtcNow;
          customerContact.Deleted = false;
          customerContact.TenantId = tenantId;
          await _custContactDS.AddAsync(customerContact);
        }
      }

      // Add Customer Payment
      if (customerSyncDTO.CustomerPaymentDetailList != null && customerSyncDTO.CustomerPaymentDetailList.Count > 0)
      {
        foreach (var item in customerSyncDTO.CustomerPaymentDetailList)
        {
          item.CustomerId = CustomerId;
          item.ERPCustomerKey = customer.ERPCustomerKey;
          BACustomerPaymentDetail customerPayment = BACustomerPaymentDetailSyncDTO.MapToEntity(item);
          customer.ID = Guid.NewGuid();
          customerPayment.CreatedBy = tenantUserId;//;// Session
          customerPayment.UpdatedBy = tenantUserId;
          customerPayment.CreatedOn = DateTime.UtcNow;
          customerPayment.UpdatedOn = DateTime.UtcNow;
          customerPayment.Deleted = false;
          customerPayment.TenantId = tenantId;
          await _custPayDetDS.AddAsync(customerPayment);
        }
      }
      // SingUp Customer On App Portal
      CustomerSignUpDTO customerSignUpDTO = this.GenerateCustomerSignUpDTOFromBACustomer(customer, customerSyncDTO);
      // Save Data
      _unitOfWork.SaveAll();

      try
      {
        List<string> customerAddERPKeyList = new List<string>();
        customerAddERPKeyList.Add(customer.ERPCustomerKey);
        OnAddCustomerInIntegratedModeAsync(customerAddERPKeyList, customer.TenantId, (long)BusinessEntityNotificationEventEnum.BizAddCutsomer);
      }
      catch { }



      return customerSignUpDTO;
    }

    private CustomerSignUpDTO GenerateCustomerSignUpDTOFromBACustomer(BACustomer customer, SignUpBACustomerDTO customerDTO)
    {
      CustomerSignUpDTO request = new CustomerSignUpDTO();
      request.TenantId = customer.TenantId;
      request.BusinesPartnerTenantId = customer.BusinessPartnerTenantId;
      request.Currency = customerDTO.CurrencyCode.ToString();
      request.CutomerName = customer.CustomerName;
      request.BusinesPrimaryUserId = customer.CreatedBy;

      return request;
    }

    #endregion Signup Customer in Standalone Mode

    #region Notification

    private void OnAddCustomerInIntegratedModeAsync(List<string> customerErpKeyList, Guid businessTenantId, long bizNotificationEnum)
    {
      for (int i = 0; i < customerErpKeyList.Count; i++)
      {
        CustomerNotificationDTO customerNotificationDTO = _qNotificationDS.GetCustomerDetailByCustomerERPKeyAsync(customerErpKeyList[i], businessTenantId).Result;
        _businessEntityNotificationHandler.SendAddCustomerInIntegratedMode(customerNotificationDTO, bizNotificationEnum);
      }
    }

    private void OnUpdateCustomerInIntegratedModeAsync(List<string> customerErpKeyList, Guid businessTenantId, long bizNotificationEnum)
    {
      for (int i = 0; i < customerErpKeyList.Count; i++)
      {
        CustomerNotificationDTO customerNotificationDTO = _qNotificationDS.GetCustomerDetailByCustomerERPKeyAsync(customerErpKeyList[i], businessTenantId).Result;
        _businessEntityNotificationHandler.SendUpdateCustomerInIntegratedMode(customerNotificationDTO, bizNotificationEnum);
      }
    }

    #endregion

    #region Delete
    #endregion Delete
  }

}