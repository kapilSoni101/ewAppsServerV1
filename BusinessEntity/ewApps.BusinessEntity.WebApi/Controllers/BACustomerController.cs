using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusinessEntity.DTO;
using ewApps.BusinessEntity.DS;
using ewApps.BusinessEntity.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.BusinessEntity.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class BACustomerController:ControllerBase {
        #region Local Members

        IBACustomerDS _customerDS;
        ICustomerSignupDS _customerSignupDS;

        #endregion Local Members

        #region Constructor

        /// <summary>
        /// initalize constructor.
        /// </summary>
        /// <param name="customerDS"></param>
        /// <param name="customerSignupDS">Customer signup service.</param>
        public BACustomerController(IBACustomerDS customerDS, ICustomerSignupDS customerSignupDS) {
            _customerDS = customerDS;
            _customerSignupDS = customerSignupDS;
        }

        #endregion Constructor

        #region Post

        /// <summary>
        /// Signup BAcustomer.
        /// </summary>
        /// <param name="signupCustDTO">Customer detail object.</param>
        /// <param name="token"></param>
        /// <returns>return business detail  model.</returns>
        [Route("signupcustomer")]
        [HttpPost]
        public async Task<CustomerSignUpDTO> CustomerSignupAsync([FromBody]SignUpBACustomerDTO signupCustDTO, CancellationToken token = default(CancellationToken)) {
            return await _customerSignupDS.SignUpCustomerAsync(signupCustDTO, token);
        }

        #endregion Post

        #region Get

        /// <summary>
        /// Get Customer list by tenant Id.
        /// </summary>
        /// <returns></returns>
        [HttpGet("list/{tenantid}")]
        public async Task<List<BACustomerDTO>> GetCustomerListByTenantIdAsync([FromRoute] Guid tenantid, CancellationToken token = default(CancellationToken)) {
            return await _customerDS.GetCustomerListByTenantIdAsync(tenantid);
        }

        /// <summary>
        /// Get Customer list by tenant Id.
        /// </summary>
        /// <returns></returns>
        [HttpGet("customerlist/{tenantid}/{status}")]
        public async Task<List<BACustomerDTO>> GetCustomerListByStatusAndTenantIdAsync([FromRoute] Guid tenantid, [FromRoute] int status, CancellationToken token = default(CancellationToken)) {
            return await _customerDS.GetCustomerListByStatusAndTenantIdAsync(tenantid, status, token);
        }

        /// <summary>
        /// Get business detail model.
        /// </summary>
        /// <param name="tenantid>Unique businessTenant id.</param>
        /// <param name="token"></param>
        /// <returns>return business detail  model.</returns>
        [Route("detail/{customerid}")]
        [HttpGet]
        public async Task<BusBACustomerDetailDTO> GetCustomerDetailByIdAsync([FromRoute]Guid customerid, CancellationToken token = default(CancellationToken)) {
            return await _customerDS.GetCustomerDetailByIdAsync(customerid, token);
        }

        /// <summary>
        /// Get Customer list by tenant Id.
        /// </summary>
        /// <returns></returns>
        [HttpGet("setupapp/customerlist/{tenantid}/{isDeleted}")]
        public async Task<List<BusCustomerSetUpAppDTO>> GetCustomerListForBizSetupApp([FromRoute] Guid tenantid, [FromRoute] bool isDeleted, CancellationToken token = default(CancellationToken)) {
            return await _customerDS.GetCustomerListForBizSetupApp(tenantid, isDeleted);
        }

        /// <summary>
        /// Get business detail model.
        /// </summary>
        /// <param name="tenantid>Unique businessTenant id.</param>
        /// <param name="token"></param>
        /// <returns>return business detail  model.</returns>
        [Route("setupapp/customerdetail/{customerid}")]
        [HttpGet]
        public async Task<BusCustomerSetUpAppViewDTO> GetCustomerDetailForBizSetupApp([FromRoute]Guid customerid, CancellationToken token = default(CancellationToken)) {
            return await _customerDS.GetCustomerDetailForBizSetupApp(customerid, token);
        }

        /// <summary>
        /// Get businecustss detail model.
        /// </summary>
        /// <param name="tenantid>Unique businessTenant id.</param>
        /// <param name="token"></param>
        /// <returns>return business detail  model.</returns>
        [Route("customerdetail/{customerid}")]
        [HttpGet]
        public async Task<CustBACustomerDetailDTO> GetCustomerDetailByIdAsyncForCust([FromRoute]Guid customerid, CancellationToken token = default(CancellationToken)) {
            return await _customerDS.GetCustomerDetailByIdAsyncForCust(customerid, token);
        }

        #endregion Get

        #region PUT


        /// <summary>
        /// Update Customer 
        /// </summary>
        /// <param name="custConfigurationUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [Route("update")]
        [HttpPut]
        public async Task<ResponseModelDTO> BACustomerUpdateAsync([FromBody]CustConfigurationUpdateDTO custConfigurationUpdateDTO, CancellationToken token = default(CancellationToken)) {
             return await _customerDS.BACustomerUpdateAsync(custConfigurationUpdateDTO, token);
        }

        #endregion PUT

    }
}