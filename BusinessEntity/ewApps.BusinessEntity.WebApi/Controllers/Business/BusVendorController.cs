using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DS;
using ewApps.BusinessEntity.DTO;
using ewApps.Core.ExceptionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.BusinessEntity.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BusVendorController : ControllerBase
  {

    #region Local Members

    IBAVendorDS _vendorDS;

    #endregion Local Members

    #region Constructor

    /// <summary>
    /// initalize constructor.
    /// </summary>
    /// <param name="vendorDS"></param>
    public BusVendorController(IBAVendorDS vendorDS)
    {
      _vendorDS = vendorDS;
    }

    #endregion Constructor
    #region Get
    /// <summary>
    /// Get Customer list by tenant Id.
    /// </summary>
    /// <returns></returns>
    [Route("list/{tenantid}")]
    [HttpGet]
    public async Task<List<BusVendorDTO>> GetVendorListByTenantIdAsync([FromRoute]Guid tenantid, CancellationToken token = default(CancellationToken))
    {
      return await _vendorDS.GetVendorListByTenantIdAsync(tenantid);
    }

    /// <summary>
    /// Get Customer list by tenant Id.
    /// </summary>
    /// <returns></returns>
    [HttpGet("vendorlist/{tenantid}/{status}")]
    public async Task<List<BusVendorDTO>> GetVendorListByStatusAndTenantIdAsync([FromRoute] Guid tenantid, [FromRoute] int status, CancellationToken token = default(CancellationToken))
    {
      return await _vendorDS.GetVendorListByStatusAndTenantIdAsync(tenantid, status, token);
    }

    /// <summary>
    /// Get business detail model.
    /// </summary>
    /// <param name="tenantid>Unique businessTenant id.</param>
    /// <param name="token"></param>
    /// <returns>return business detail  model.</returns>
    [Route("detail/{vendorid}")]
    [HttpGet]
    public async Task<BusVendorDetailDTO> GetVendorDetailByIdAsync([FromRoute]Guid vendorid, CancellationToken token = default(CancellationToken))
    {
      return await _vendorDS.GetVendorDetailByIdAsync(vendorid, token);
    }
    /// <summary>
    /// Get Customer list by tenant Id.
    /// </summary>
    /// <returns></returns>
    [HttpGet("setupapp/vendorlist/{tenantid}/{isDeleted}")]
    public async Task<List<BusVendorSetUpAppDTO>> GetVendorListForBizSetupApp([FromRoute] Guid tenantid, [FromRoute] bool isDeleted, CancellationToken token = default(CancellationToken))
    {
      return await _vendorDS.GetVendorListForBizSetupApp(tenantid, isDeleted);
    }

    /// <summary>
    /// Get business detail model.
    /// </summary>
    /// <param name="tenantid>Unique businessTenant id.</param>
    /// <param name="token"></param>
    /// <returns>return business detail  model.</returns>
    [Route("setupapp/vendordetail/{vendorid}")]
    [HttpGet]
    public async Task<BusVendorSetUpAppViewDTO> GetVendorDetailForBizSetupApp([FromRoute]Guid vendorid, CancellationToken token = default(CancellationToken))
    {
      return await _vendorDS.GetVendorDetailForBizSetupApp(vendorid, token);
    }
    #endregion Get

    #region Put 
    /// <summary>
    /// Update BAcustomer.
    /// </summary>
    /// <param name="custDTO">Customer detail object.</param>
    /// <param name="token"></param>
    /// <returns>return business detail  model.</returns>
    [Route("setupapp/updatevendordetail")]
    [HttpPut]
    public async Task<bool> UpdateVendorDetailForBizSetupApp([FromBody]BusVendorUpdateDTO vendDTO, CancellationToken token = default(CancellationToken))
    {
      return await _vendorDS.UpdateVendorDetailForBizSetupApp(vendDTO, token);
    }
    #endregion Put
  }
}