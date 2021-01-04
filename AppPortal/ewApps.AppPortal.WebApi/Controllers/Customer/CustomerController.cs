using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {

    /// <summary>
    /// Conatins method to signup customer.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController:ControllerBase {

        #region Local Member

        ICustomerSignUpDS _custSignUpDS;
        IBusCustomerDS _busCustomerDS;
        ICustomerDS _customerDS;

        #endregion Local Member

        #region Constructor

        /// <summary>
        /// initilize constructor.
        /// </summary>
        /// <param name="custSingUpDS"></param>
        /// <param name="busCustomerDS"></param>
        /// <param name="customer"></param>
        public CustomerController(ICustomerSignUpDS custSingUpDS, IBusCustomerDS busCustomerDS, ICustomerDS customer) {
            _custSignUpDS = custSingUpDS;
            _busCustomerDS = busCustomerDS;
            _customerDS = customer;
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get customer info.
        /// </summary>
        /// <param name="id">Id of customer.</param>
        /// <param name="appId">Id of application.</param>
        /// <param name="includepaymentservices">Include all payment services subscribed by customer.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("customer/{id}/{appId}/{includepaymentservices}")]
        public async Task<CustomerPaymentInfoDTO> GetCustomerByIdAsync(Guid id, Guid appId, bool includepaymentservices) {
            return await _busCustomerDS.GetCustomerAndPaymentInfoAsync(id, appId, includepaymentservices);
        }

        /// <summary>
        /// Get customer info.
        /// </summary>
        /// <param name="businessPartnerTenantId">custmer businessPartnerTenantId.</param>
        /// <param name="appId">Id of application.</param>
        /// <param name="includepaymentservices">Include all payment services subscribed by customer.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("businesspartner/{businessPartnerTenantId}/{appId}/{includepaymentservices}")]
        public async Task<CustomerPaymentInfoDTO> GetCustomerInfoByBusinessPartnerAsync(Guid businessPartnerTenantId, Guid appId, bool includepaymentservices) {
            return await _busCustomerDS.GetCustomerInfoByBusinessPartnerAsync(businessPartnerTenantId, appId, includepaymentservices);
        }

        #endregion Get

        /// <summary>
        ///SignUp customer .
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("signup")]
        public async Task<CustomerSignUpResDTO> CustomerSignUpAsync([FromBody] List<CustomerSignUpReqDTO> request, CancellationToken token = default(CancellationToken)) {
            CustomerSignUpResDTO result = await _custSignUpDS.CustomerSignUpAsync(request);
            return result;
        }

        /// <summary>
        /// Signup BAcustomer.
        /// Method will call in standalone case.
        /// </summary>
        /// <param name="signupCustDTO">Customer detail object.</param>
        /// <param name="token"></param>
        /// <returns>return business detail  model.</returns>
        [Route("customersignup")]
        [HttpPost]
        public async Task<CustomerSignUpResDTO> CustomerSignupAsync([FromBody]SignUpBACustomerDTO signupCustDTO, CancellationToken token = default(CancellationToken)) {
            return await _custSignUpDS.SignUpCustomerAsync(signupCustDTO, token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buspartnertenantid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getconfiguration/{buspartnertenantid}")]
        public async Task<CustConfigurationViewDTO> GetConfigurationAsync(Guid buspartnertenantid, CancellationToken token = default(CancellationToken)) {
            return await _customerDS.GetConfigurationDetailAsync(buspartnertenantid, token);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="custCreditCardRequestDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("validateCreditCard")]
        public async Task<CustCreditCardResponseDTO> ValidateCreditCard(CustCreditCardRequestDTO custCreditCardRequestDTO, CancellationToken token = default(CancellationToken)) {
            CustCreditCardResponseDTO custCreditCardResponseDTO = await _customerDS.ValidateCreditCardDetailAsync(custCreditCardRequestDTO, token);
            return custCreditCardResponseDTO; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="custConfigurationUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateconfiguration")]
        public async Task<ResponseModelDTO> UpdatetConfigurationAsync(CustConfigurationUpdateDTO custConfigurationUpdateDTO, CancellationToken token = default(CancellationToken)) {
            await _customerDS.UpdatetConfigurationDetailAsync(custConfigurationUpdateDTO, token);
            return new ResponseModelDTO() {
                Id = Guid.NewGuid(),
                IsSuccess = true
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerid">customerid</param>
        /// <param name="customeAccDetail">customer account detail to save.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addaccountdetail/{customerid}")]
        public async Task<ResponseModelDTO> AddCustomerAccountAsync(Guid customerid, CustomeAccDetailDTO customeAccDetail, CancellationToken token = default(CancellationToken)) {
            await _customerDS.AddCustomerAccountDetail(customerid, customeAccDetail, token);
            return new ResponseModelDTO() {
                Id = Guid.NewGuid(),
                IsSuccess = true
            };
        }

        /// <summary>
        /// Get business detail model.
        /// </summary>
        /// <param name="tenantid>Unique businessTenant id.</param>
        /// <param name="token"></param>
        /// <returns>return customer detail  model.</returns>
        [Route("alldetail/{customerid}")]
        [HttpGet]
        public async Task<CustCustomerDetailDTO> GetCustomerDetailByIdAsyncForCust([FromRoute]Guid customerid, CancellationToken token = default(CancellationToken))
        {
          return await _customerDS.GetCustomerDetailByIdAsyncForCust(customerid, token);
        }

  }
}