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
    public class VendBAPurchaseOrderController:ControllerBase {

        #region Member Variables

        IVendBAPurchaseOrderDS _vendBAPurchaseOrderDS;

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="BusBASalesOrderController"/> class.
    /// </summary>
    /// <param name="vendBAPurchaseOrderDS">The business sales order data service instance.</param>
    public VendBAPurchaseOrderController(IVendBAPurchaseOrderDS vendBAPurchaseOrderDS) {
      _vendBAPurchaseOrderDS = vendBAPurchaseOrderDS;
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
        public async Task<IEnumerable<BAPurchaseOrderDTO>> GetPurchaseOrderListByBusinessTenantIdAsyncForVend([FromRoute]Guid businessTenantId, [FromBody] ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _vendBAPurchaseOrderDS.GetPurchaseOrderListByBusinessTenantIdAsyncForVend(businessTenantId, listDateFilterDTO, cancellationToken);
        }


    #endregion

    #endregion

  }
}