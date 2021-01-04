using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusCustomerController : ControllerBase
    {
        #region Local variables

        IBusCustomerDS _businessCustomerDS;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// Initilize constructor for Business Tenant.
        /// </summary>
        /// <param name="businessCustomerDS">Business customer data service.</param>
        public BusCustomerController(IBusCustomerDS businessCustomerDS) {
            _businessCustomerDS = businessCustomerDS;
            
        }

        #endregion Constructor

        #region Get
        
        /// <summary>
        /// Get business detail model.
        /// </summary>
        /// <param name="tenantid>Unique businessTenant id.</param>
        /// <param name="token"></param>
        /// <returns>return business detail  model.</returns>
        [Route("list/{tenantid}")]
        [HttpGet]
        public async Task<List<BusCustomerDTO>> GetCustomerListByTenantIdAsync([FromRoute]Guid tenantid, CancellationToken token = default(CancellationToken)) {
            return await _businessCustomerDS.GetCustomerListByTenantIdAsync(tenantid, token);
        }

        /// <summary>
        /// Get Customer list by tenant Id.
        /// </summary>
        /// <returns></returns>
        [HttpGet("customerlist/{tenantid}/{status}")]
        public async Task<List<BusCustomerDTO>> GetCustomerListByStatusAndTenantIdAsync([FromRoute] Guid tenantid, [FromRoute] int status, CancellationToken token = default(CancellationToken)) {
            return await _businessCustomerDS.GetCustomerListByStatusAndTenantIdAsync(tenantid, status, token);
        }

        /// <summary>
        /// Get business detail model.
        /// </summary>
        /// <param name="tenantid>Unique businessTenant id.</param>
        /// <param name="token"></param>
        /// <returns>return customer detail  model.</returns>
        [Route("detail/{customerid}")]
        [HttpGet]
        public async Task<BusCustomerDetailDTO> GetCustomerDetailByIdAsync([FromRoute]Guid customerid, CancellationToken token = default(CancellationToken)) {
            return await _businessCustomerDS.GetCustomerDetailByIdAsync(customerid, token);
        }

        /// <summary>
        /// Get business detail model.
        /// </summary>
        /// <param name="tenantid>Unique businessTenant id.</param>
        /// <param name="token"></param>
        /// <returns>return business detail  model.</returns>
        [Route("setupapp/customerlist/{tenantid}/{isDeleted}")]
        [HttpGet]
        public async Task<List<BusCustomerSetUpAppDTO>> GetCustomerListForBizSetupApp([FromRoute] Guid tenantid, [FromRoute] bool isDeleted, CancellationToken token = default(CancellationToken)) {
            return await _businessCustomerDS.GetCustomerListForBizSetupApp(tenantid, isDeleted, token);
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
            return await _businessCustomerDS.GetCustomerDetailForBizSetupApp(customerid, token);
        }
        #endregion Get

        #region Put

        /// <summary>
        /// Method is used to update customer with all child entities.
        /// </summary>
        /// <param name="dto">customer detail model.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [Route("detail/update")]
        [HttpPut]
        public async Task<bool> UpdateCustomerDetail([FromBody]BusCustomerDetailDTO dto, CancellationToken token = default(CancellationToken)) {
            await _businessCustomerDS.UpdateCustomerDetail(dto, token);
            return true;
        }
        /// <summary>
        /// Method is used to update customer with all child entities.
        /// </summary>
        /// <param name="dto">customer detail model.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [Route("setupapp/updatecustomerDetail")]
        [HttpPut]
        public async Task<bool> UpdateCustomerDetailForBizSetupApp([FromBody]BusCustomerUpdateDTO dto, CancellationToken token = default(CancellationToken)) {
            await _businessCustomerDS.UpdateCustomerDetailForBizSetupApp(dto, token);
            return true;
        }

        /// <summary>
        /// Delete BAcustomer and associtaed data.
        /// </summary>
        /// <param name="bacustomerId">BACustomerId.</param>
        /// <param name="businessPartnerTenantId">Business partner tenant id.</param>
        /// <param name="token"></param>
        /// <returns>return business detail  model.</returns>
        [Route("delete/{bacustomerId}/{businessPartnerTenantId}")]
        [HttpPut]
        public async Task<bool> DeleteCustomer([FromRoute]Guid bacustomerId, Guid businessPartnerTenantId, CancellationToken token = default(CancellationToken)) {
            return await _businessCustomerDS.DeleteCustomerAsync(bacustomerId, businessPartnerTenantId, token);            
        }

        #endregion Put

    }
}