using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DS;
using ewApps.BusinessEntity.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.BusinessEntity.WebApi {
    [Route("api/[controller]")]
    [ApiController]

    /// <summary>
    /// This controller contains methods to execute <see cref="BASalesOrder"/> entity related operations.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    public class BusBASalesOrderController:ControllerBase {

        #region Member Variables

        IBusBASalesOrderDS _busBASalesOrderDS;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusBASalesOrderController"/> class.
        /// </summary>
        /// <param name="busBASalesOrderDS">The business sales order data service instance.</param>
        public BusBASalesOrderController(IBusBASalesOrderDS busBASalesOrderDS) {
            _busBASalesOrderDS = busBASalesOrderDS;
        }

        #endregion

        #region Business

        #region Get Methods

        /// <summary>
        /// Gets the delivery list by business tenant identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <param name="listDateFilterDTO">The DTO to filter contract list.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of <see cref="BusBADeliveryDTO"/> that matches provided business tenant id.</returns>
        [HttpPut]
        [Route("list/{businessTenantId}")]
        public async Task<IEnumerable<BusBASalesOrderDTO>> GetSalesOrderListByBusinessTenantIdAsync([FromRoute]Guid businessTenantId, [FromBody] ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _busBASalesOrderDS.GetSalesOrderListByBusinessTenantIdAsync(businessTenantId, listDateFilterDTO, cancellationToken);
        }

        ///// <summary>
        ///// Gets the delivery item name list by delivery id.
        ///// </summary>
        ///// <param name="deliveryId">The delivery to find delivery items.</param>
        ///// <param name="cancellationToken">The cancellation token.</param>
        ///// <returns>Returns list of delivery item name list that matches provided delivery id.</returns>
        //[HttpGet]
        //[Route("items/list/{deliveryId}")]
        //public async Task<IEnumerable<string>> GetDeliveryItemNameListByDeliveryIdAsync(Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken)) {
        //    return await _busBADeliveryDS.GetDeliveryItemNameListByDeliveryIdAsync(deliveryId, cancellationToken);
        //}

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


        #endregion
        #endregion


    }
}