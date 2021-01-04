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
    public class BAPurchaseOrderController:ControllerBase {
        IBAPurchaseOrderDS _busBAPurchaseOrderDS;

        public BAPurchaseOrderController(IBAPurchaseOrderDS busBAOrderDS) {
            _busBAPurchaseOrderDS = busBAOrderDS;
        }

        /// <summary>
        /// Gets the purchase order list by business tenant identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of <see cref="BAPurchaseOrderDTO"/> that matches provided business tenant id.</returns>
        [HttpPut]
        [Route("list/{businessTenantId}")]
        public async Task<IEnumerable<BAPurchaseOrderDTO>> GetPurchaseOrderListByBusinessTenantIdAsync([FromRoute]Guid businessTenantId, [FromBody] ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _busBAPurchaseOrderDS.GetPurchaseOrderListByBusinessTenantIdAsync(businessTenantId, listDateFilterDTO, cancellationToken);
        }

        /// <summary>
        /// Gets purchase order detail that matches the provided business tenant id and purchase order id.
        /// </summary>
        /// <param name="businessTenantId">The business tenant id.</param>
        /// <param name="poId">The purchase order identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns <see cref="BAPurchaseOrderViewDTO"/> that matches provided business tenant and sales order id.</returns>
        [HttpGet]
        [Route("view/{businessTenantId}/{soId}")]
        public async Task<BAPurchaseOrderViewDTO> GetPurchaseOrderDetailByPOIdAsync([FromRoute]Guid businessTenantId, [FromRoute]Guid poId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _busBAPurchaseOrderDS.GetPurchaseOrderDetailByPOIdAsync(businessTenantId, poId, cancellationToken);
        }

    }
}