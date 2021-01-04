using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DS;
using ewApps.BusinessEntity.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.BusinessEntity.WebApi.Controllers.Customer
{
  [Route("api/[controller]")]
  [ApiController]
  public class CustBAItemMasterController : ControllerBase
  {

    #region Local Members

    ICustBAItemMasterDS _custItemMasterDS;

    #endregion Local Members

    #region Constructor 

    public CustBAItemMasterController(ICustBAItemMasterDS custItemMasterDS)
    {
      _custItemMasterDS = custItemMasterDS;
    }

    #endregion Constructor

    #region Get

    [HttpGet]
    [Route("list/{tenantId}")]
    public async Task<IEnumerable<CustBAItemMasterDTO>> GetItemMasterListByBusinessTenantIdAsyncForCust([FromRoute] Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      return await _custItemMasterDS.GetItemMasterListByBusTenantIdAsyncForCust(tenantId, token);
    }

    [HttpGet]
    [Route("view/{tenantId}/{itemId}")]
    public async Task<CustBAItemMasterViewDTO> GetItemMasterByBusinessTenantIdAndItemIdAsyncForCust([FromRoute] Guid tenantId, [FromRoute] Guid itemId, CancellationToken token = default(CancellationToken))
    {
      return await _custItemMasterDS.GetItemMasterByBusTenantIdAndItemIdAsyncForCust(tenantId, itemId, token);
    }

    #endregion Get
  }
}