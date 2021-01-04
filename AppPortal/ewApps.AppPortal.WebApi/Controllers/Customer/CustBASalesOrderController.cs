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
    public class CustBASalesOrderController:ControllerBase {
        ICustBASalesOrderDS _custBASalesOrderDS;

        public CustBASalesOrderController(ICustBASalesOrderDS custBASalesOrderDS) {
            _custBASalesOrderDS = custBASalesOrderDS;
        }

        /// <summary>
        /// Gets the delivery list by business tenant identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of <see cref="BusBADeliveryDTO"/> that matches provided business tenant id.</returns>
        [HttpPut]
        [Route("list/{businessTenantId}")]
        public async Task<IEnumerable<CustBASalesOrderDTO>> GetSalesOrderListByBusinessTenantIdAsync([FromRoute]Guid businessTenantId, [FromBody] ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _custBASalesOrderDS.GetSalesOrderListByBusinessTenantIdAsyncForCust(businessTenantId, listDateFilterDTO, cancellationToken);
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
        public async Task<CustBASalesOrderViewDTO> GetSalesOrderDetailBySOIdAsyncForCust([FromRoute]Guid businessTenantId, [FromRoute]Guid soId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _custBASalesOrderDS.GetSalesOrderDetailBySOIdAsyncForCust(businessTenantId, soId, cancellationToken);
        }

        [HttpPost]
        [Route("salesorderwithitem")]
        public async Task AddSalesOrderWithItem([FromBody] CustBASalesOrderAddDTO dto, CancellationToken cancellationToken = default(CancellationToken))
        {
          await _custBASalesOrderDS.AddSalesOrderWithItemAsync(dto, cancellationToken);
        }

        [HttpPut]
        [Route("salesorderwithitem")]
        public async Task UpdateSalesOrderWithItem([FromBody] CustBASalesOrderUpdateDTO dto, CancellationToken cancellationToken = default(CancellationToken))
        {
          await _custBASalesOrderDS.UpdateSalesOrderWithItemAsync(dto, cancellationToken);
        }

  }
}