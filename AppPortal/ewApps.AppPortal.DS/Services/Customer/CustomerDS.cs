/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agarwal <sagrawal@eworkplaceapps.com>
 * Date: 08 August 2019
 * 
 * Contributor/s: Sourabh agrawal
 * Last Updated On: 22 August 2019
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// This class implements standard business logic and operations for Customer entity.
    /// </summary>
    public class CustomerDS:BaseDS<Customer>, ICustomerDS {

        #region Local Member
        IUserSessionManager _userSessionManager;
        ICustomerRepository _customerRepository;
        IQConfigurationDS _qConfigurationDS;
        IUnitOfWork _unitOfWork;
        ICustomerAccountDetailDS _customerAccountDetailDS;
        AppPortalAppSettings _appSetting;
        ICustomerAccountDetailDS _customerAccountDetail;

        #endregion Local Member

        #region Constructor

        /// <summary>
        /// Initialinzing local variables .
        /// </summary>
        /// <param name="userSessionManager"></param>
        /// <param name="customerRepository"></param>
        /// <param name="qConfigurationDS"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="customerAccountDetailDS"></param>
        public CustomerDS(ICustomerAccountDetailDS customerAccountDetail, IUserSessionManager userSessionManager, ICustomerRepository customerRepository, IQConfigurationDS qConfigurationDS, IUnitOfWork unitOfWork, ICustomerAccountDetailDS customerAccountDetailDS, IOptions<AppPortalAppSettings> appSetting) : base(customerRepository) {
            _userSessionManager = userSessionManager;
            _customerRepository = customerRepository;
            _qConfigurationDS = qConfigurationDS;
            _unitOfWork = unitOfWork;
            _customerAccountDetailDS = customerAccountDetailDS;
            _appSetting = appSetting.Value;
            _customerAccountDetail = customerAccountDetail;
        }

        #endregion Constructor

        #region Add

        ///<inheritdoc/>
        public async Task AddCustomerAsync(CustomerSignUpReqDTO customerSignupDTO, CancellationToken token = default(CancellationToken)) {

            Customer customer = new Customer();
            customer.ID = Guid.NewGuid();
            customer.CreatedBy = customerSignupDTO.BusinesPrimaryUserId;
            customer.UpdatedBy = customerSignupDTO.BusinesPrimaryUserId;
            customer.CreatedOn = DateTime.UtcNow;
            customer.UpdatedOn = DateTime.UtcNow;
            customer.BusinessPartnerTenantId = customerSignupDTO.BusinesPartnerTenantId;
            customer.Deleted = false;
            customer.CanUpdateCurrency = false;
            customer.Configured = false;
            customer.Currency = customerSignupDTO.Currency;
            customer.TenantId = customerSignupDTO.TenantId;

            // add customer detail
            await AddAsync(customer, token);

        }

        #endregion Add


        #region Get configuration

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buspartnertenantid"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CustConfigurationViewDTO> GetConfigurationDetailAsync(Guid buspartnertenantid, CancellationToken cancellationToken = default(CancellationToken)) {
            CustConfigurationViewDTO custConfigurationViewDTO = await _qConfigurationDS.GetConfigurationDetailAsync(buspartnertenantid, cancellationToken);

            return custConfigurationViewDTO;
        }

        #endregion Get configuration

        #region Update configurtion

        /// <summary>
        /// Update customer configuration
        /// </summary>
        /// <param name="custConfigurationUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task UpdatetConfigurationDetailAsync(CustConfigurationUpdateDTO custConfigurationUpdateDTO, CancellationToken token = default(CancellationToken)) {
            UserSession session = _userSessionManager.GetSession();
            Guid customerId = custConfigurationUpdateDTO.CustomerID;
            Guid BusinessPartnerTenantId = custConfigurationUpdateDTO.BusinessPartnerTenantId;

            await _customerAccountDetailDS.AddUpdateCustomerAcctDetail(customerId, custConfigurationUpdateDTO.CustomerAccountDetail, token);

            ResponseModelDTO responseModelDTO = await UpdateBACustomerDetail(custConfigurationUpdateDTO, token);

            // Entity.Customer customer = _customerRepository.Get(custConfigurationUpdateDTO.BusinessPartnerTenantId);

            Entity.Customer customer = await FindAsync(x => x.BusinessPartnerTenantId == custConfigurationUpdateDTO.BusinessPartnerTenantId);

            // Update Field every time when update configuration
            //customer.CanUpdateCurrency = false;
            //customer.Configured = false;

            // START Portal Localization
            customer.Language = custConfigurationUpdateDTO.Language;
            customer.TimeZone = custConfigurationUpdateDTO.TimeZone;
            customer.DateTimeFormat = custConfigurationUpdateDTO.DateTimeFormat;
            customer.Configured = true;

            // START Currency Localization
            customer.Currency = custConfigurationUpdateDTO.CurrencyCode;
            customer.GroupValue = custConfigurationUpdateDTO.GroupValue;
            customer.GroupSeperator = custConfigurationUpdateDTO.GroupSeperator;
            customer.DecimalPrecision = custConfigurationUpdateDTO.DecimalPrecision;

            customer.DecimalSeperator = custConfigurationUpdateDTO.DecimalSeperator;

            UpdateSystemFieldsByOpType(customer, OperationType.Update);
            Update(customer, customer.ID);
            _unitOfWork.SaveAll();


        }



        /// <summary>
        /// Update BA Customer Details
        /// </summary>
        /// <param name="custConfigurationUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ResponseModelDTO> UpdateBACustomerDetail(CustConfigurationUpdateDTO custConfigurationUpdateDTO, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string baseUrl = _appSetting.BusinessEntityApiUrl;
            string reqeustMethod = "BACustomer/update";

            UserSession session = _userSessionManager.GetSession();

            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
           RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, reqeustMethod, custConfigurationUpdateDTO, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

            ResponseModelDTO response = await httpRequestProcessor.ExecuteAsync<ResponseModelDTO>(requestOptions, false);
            return response;
        }
        /// <summary>
        /// Validate CreditCard
        /// </summary>
        /// <param name="custCreditCardRequestDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<CustCreditCardResponseDTO> ValidateCreditCardDetailAsync(CustCreditCardRequestDTO custCreditCardRequestDTO, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "authentication/token";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            UserSession session = _userSessionManager.GetSession();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            KeyValuePair<string, string> hdrTenantID = new KeyValuePair<string, string>("TenantId", session.TenantId.ToString());
            listHeader.Add(hdrTenantID);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, custCreditCardRequestDTO, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.CreditCardApiUrl);
            return await httpRequestProcessor.ExecuteAsync<CustCreditCardResponseDTO>(requestOptions, false);
        }

        #endregion Update configuration

        #region Save Account Detail

        /// <summary>
        /// Save account detail.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customeAccDetail"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task AddCustomerAccountDetail(Guid customerId, CustomeAccDetailDTO customeAccDetail, CancellationToken token = default(CancellationToken)) {
            try {
                for(int i = 0; i < customeAccDetail.BankAcctDetailList.Count; i++) {
                    customeAccDetail.BankAcctDetailList[i].OperationType = (int)OperationType.Add;
                }
                for(int i = 0; i < customeAccDetail.CreditCardDetailList.Count; i++) {
                    if(string.IsNullOrEmpty(customeAccDetail.CreditCardDetailList[i].Token)) {
                        CustCreditCardRequestDTO reqDto = new CustCreditCardRequestDTO();
                        reqDto.CardHolderName = customeAccDetail.CreditCardDetailList[i].NameOnCard;
                        reqDto.CardNumber = customeAccDetail.CreditCardDetailList[i].CardNumber;
                        reqDto.ExpirationDate = customeAccDetail.CreditCardDetailList[i].ExpiryMonth.ToString() + customeAccDetail.CreditCardDetailList[i].ExpiryYear;
                        reqDto.DeveloperID = "ADDS";
                        if(string.IsNullOrEmpty(customeAccDetail.CreditCardDetailList[i].Token)) {
                            CustCreditCardResponseDTO resDto = await ValidateCreditCardDetailAsync(reqDto, token);
                            customeAccDetail.CreditCardDetailList[i].Token = resDto.Response.GetOnusTokenResponse.Token;
                        }
                    }
                    customeAccDetail.CreditCardDetailList[i].OperationType = (int)OperationType.Add;
                }

                await _customerAccountDetailDS.AddUpdateCustomerAcctDetail(customerId, customeAccDetail, token);
            }
            catch { }
        }

        #endregion Save Account Detail

        ///<inheritdoc/>   
        public async Task<CustCustomerDetailDTO> GetCustomerDetailByIdAsyncForCust(Guid customerId, CancellationToken token = default(CancellationToken)) {
            string baseUrl = _appSetting.BusinessEntityApiUrl;
            string reqeustMethod = "bacustomer/customerdetail/" + customerId;

            UserSession session = _userSessionManager.GetSession();

            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
           RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

            CustCustomerDetailDTO response = await httpRequestProcessor.ExecuteAsync<CustCustomerDetailDTO>(requestOptions, false);
            CustomeAccDetailDTO customeAccDetail = await _customerAccountDetail.GetCustomerAccDetailCustomerIdAsync(customerId, token);
            response.CustomerAcctDetail = customeAccDetail;

            return response;
        }

        /// <summary>
        /// Get customer by business partner id.
        /// </summary>
        /// <param name="busPartnerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<Customer> GetCustomerByBusinesPartnerIdAsync(Guid busPartnerId, CancellationToken token = default(CancellationToken)) {
            return await _customerRepository.GetCustomerByBusinesPartnerIdAsync(busPartnerId, token);
        }

    }
}
