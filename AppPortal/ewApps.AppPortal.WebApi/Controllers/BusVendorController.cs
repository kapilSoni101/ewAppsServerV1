using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class BusVendorController:ControllerBase {
        #region Local variables

        IBusVendorDS _businessVendorDS;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// Initilize constructor for Business Tenant.
        /// </summary>
        /// <param name="businessVendorDS">Business customer data service.</param>
        public BusVendorController(IBusVendorDS businessVendorDS) {
            _businessVendorDS = businessVendorDS;

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
    public async Task<List<BusVendorDTO>> GetVendorListByTenantIdAsync([FromRoute]Guid tenantid, CancellationToken token = default(CancellationToken))
    {
      return await _businessVendorDS.GetVendorListByTenantIdAsync(tenantid, token);
    }

    /// <summary>
    /// Get Customer list by tenant Id.
    /// </summary>
    /// <returns></returns>
    [HttpGet("vendorlist/{tenantid}/{status}")]
    public async Task<List<BusVendorDTO>> GetVendorListByStatusAndTenantIdAsync([FromRoute] Guid tenantid, [FromRoute] int status, CancellationToken token = default(CancellationToken))
    {
      return await _businessVendorDS.GetVendorListByStatusAndTenantIdAsync(tenantid, status, token);
    }

    /// <summary>
    /// Get business detail model.
    /// </summary>
    /// <param name="tenantid>Unique businessTenant id.</param>
    /// <param name="token"></param>
    /// <returns>return customer detail  model.</returns>
    [Route("detail/{vendorid}")]
    [HttpGet]
    public async Task<BusVendorDetailDTO> GetVendorDetailByIdAsync([FromRoute]Guid vendorid, CancellationToken token = default(CancellationToken))
    {
      return await _businessVendorDS.GetVendorDetailByIdAsync(vendorid, token);
    }

    /// <summary>
    /// Get business detail model.
    /// </summary>
    /// <param name="tenantid>Unique businessTenant id.</param>
    /// <param name="token"></param>
    /// <returns>return business detail  model.</returns>
    [Route("setupapp/vendorlist/{tenantid}/{isDeleted}")]
        [HttpGet]
        public async Task<List<BusVendorSetUpAppDTO>> GetVendorListForBizSetupApp([FromRoute] Guid tenantid, [FromRoute] bool isDeleted, CancellationToken token = default(CancellationToken)) {
            return await _businessVendorDS.GetVendorListForBizSetupApp(tenantid, isDeleted, token);
        }

        /// <summary>
        /// Get business detail model.
        /// </summary>
        /// <param name="tenantid>Unique businessTenant id.</param>
        /// <param name="token"></param>
        /// <returns>return business detail  model.</returns>
        [Route("setupapp/vendordetail/{vendorid}")]
        [HttpGet]
        public async Task<BusVendorSetUpAppViewDTO> GetVendorDetailForBizSetupApp([FromRoute]Guid vendorid, CancellationToken token = default(CancellationToken)) {
            return await _businessVendorDS.GetVendorDetailForBizSetupApp(vendorid, token);
        }

        /// <summary>
        /// Get businessVendor detail model.
        /// </summary>
        /// <param name="vendorid">VendorId</param>
        /// <param name="tenantId">Unique businessTenant id.</param>
        /// <param name="token"></param>
        /// <returns>return business detail  model.</returns>
        [Route("setupapp/vendordetail/{vendorid}/{tenantId}")]
        [HttpGet]
        public async Task<BusinessVendorResponse> GetVendorDetailForBizSetupApp([FromRoute]Guid vendorid, [FromRoute]Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _businessVendorDS.GetBusinessVendorDetail(vendorid, tenantId, token);
        }

        #endregion Get

        #region Put


        /// <summary>
        /// Method is used to update customer with all child entities.
        /// </summary>
        /// <param name="dto">customer detail model.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [Route("setupapp/updatevendorDetail")]
        [HttpPut]
        public async Task<bool> UpdateVendorDetailForBizSetupApp([FromBody]BusVendorUpdateDTO dto, CancellationToken token = default(CancellationToken)) {
            await _businessVendorDS.UpdateVendorDetailForBizSetupApp(dto, token);
            return true;
        }



        #endregion Put
    }
}