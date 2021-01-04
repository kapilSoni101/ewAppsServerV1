using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi.Controllers.Business {
    [Route("api/[controller]")]
    [ApiController]
    public class BusBASalesOrderController:ControllerBase {
        IBusBASalesOrderDS _busBASalesOrderDS;

        public BusBASalesOrderController(IBusBASalesOrderDS busBASalesOrderDS) {
            _busBASalesOrderDS = busBASalesOrderDS;
        }

        /// <summary>
        /// Gets the delivery list by business tenant identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of <see cref="BusBADeliveryDTO"/> that matches provided business tenant id.</returns>
        [HttpPut]
        [Route("list/{businessTenantId}")]
        public async Task<IEnumerable<BusBASalesOrderDTO>> GetSalesOrderListByBusinessTenantIdAsync([FromRoute]Guid businessTenantId, [FromBody] ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _busBASalesOrderDS.GetSalesOrderListByBusinessTenantIdAsync(businessTenantId, listDateFilterDTO, cancellationToken);
        }

        /// <summary>
        /// Gets sales order detail that matches the provided business tenant id and sales order id.
        /// </summary>
        /// <param name="businessTenantId">The business tenant id.</param>
        /// <param name="soId">The sales order identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns <see cref="BusBASalesOrderViewDTO"/> that matches provided business tenant and sales order id.</returns>
        [HttpGet]
        [Route("view/{businessTenantId}/{soId}")]
        public async Task<BusBASalesOrderViewDTO> GetSalesOrderDetailBySOIdAsync([FromRoute]Guid businessTenantId, [FromRoute]Guid soId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _busBASalesOrderDS.GetSalesOrderDetailBySOIdAsync(businessTenantId, soId, cancellationToken);
        }

    }
}