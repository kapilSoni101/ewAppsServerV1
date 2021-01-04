using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {

    [Route("api/[controller]")]
    [ApiController]
    public class CustBAItemMasterController:ControllerBase {

        #region Local Members

        ICustBAItemMasterDS _busItemMasterDS;

        #endregion Local Members

        #region Constructor 

        public CustBAItemMasterController(ICustBAItemMasterDS busItemMasterDS) {
            _busItemMasterDS = busItemMasterDS;
        }

        #endregion Constructor

        #region Get

        [HttpGet]
        [Route("list/{tenantId}")]
        public async Task<IEnumerable<CustBAItemMasterDTO>> GetItemMasterListByBusinessTenantIdAsyncForCust([FromRoute] Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _busItemMasterDS.GetItemMasterListByBusTenantIdAsyncForCust(tenantId, token);
        }


        [HttpGet]
        [Route("view/{tenantId}/{itemId}")]
        public async Task<CustBAItemMasterViewDTO> GetItemMasterByBusinessTenantIdAndItemIdAsyncForCust([FromRoute] Guid tenantId, [FromRoute] Guid itemId, CancellationToken token = default(CancellationToken)) {
            return await _busItemMasterDS.GetItemMasterByBusTenantIdAndItemidAsyncForCust(tenantId, itemId, token);
        }

        [HttpPut]
        [Route("itemprice")]
        public async Task<BASyncItemPriceDTO> PullItemPriceAsync([FromBody] PullItemPriceReqDTO request, CancellationToken token = default(CancellationToken)) {
          return await _busItemMasterDS.PullItemPriceAsync(request, token);
        }

    #endregion Get

  }
}