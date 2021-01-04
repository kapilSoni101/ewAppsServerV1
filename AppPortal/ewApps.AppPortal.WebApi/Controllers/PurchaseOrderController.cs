using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.DS;

namespace ewApps.AppPortal.WebApi {
    /// <summary>
    /// Provide add/update/get method for PurchaseOrder.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController:ControllerBase {

        #region Local Variables

        IPurchaseOrderDS _purchaseOrderDS;
        private static IHttpContextAccessor _contextAccessor;

        #endregion Local Variables

        #region Constructors

        /// <summary>
        /// To initilize initial local variables.
        /// </summary>
        /// <param name="purchaseOrderDS"></param>
        /// <param name="contextAccessor"></param>
        public PurchaseOrderController(IPurchaseOrderDS purchaseOrderDS, IHttpContextAccessor contextAccessor) {
            _purchaseOrderDS = purchaseOrderDS;
            _contextAccessor = contextAccessor;
        }

        #endregion Constructors

        #region Add/Update/Delete


        /// <summary>
        /// Add invoice in standalone case.
        /// </summary>
        /// <param name="token"></param>
        /// <returns>return invoice response object.</returns>
        [HttpPost("addpurchaseorder")]
        public async Task<AddPurchaseOrderResponseDTO> AddPurchaseOrderAsync(CancellationToken token = default(CancellationToken)) {
            HttpRequest httpRequest = _contextAccessor.HttpContext.Request;
            string request = HttpContext.Request.Headers["AddPurchaseOrderModel"];
            return await _purchaseOrderDS.AddPurchaseOrderAsync(httpRequest, request, token);
        }

        #endregion Add/Update/Delete

        #region Get

        /// <summary>
        /// Get purchase order list by TenantId.
        /// </summary>
        /// <param name="listDateFilterDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("list")]
        public async Task<IEnumerable<BAPurchaseOrderDTO>> GetPurchaseOrderListByBusinessTenantIdAsync([FromBody] ListDateFilterDTO listDateFilterDTO, CancellationToken token = default(CancellationToken)) {
            return await _purchaseOrderDS.GetPurchaseOrderListByBusinessTenantIdAsync(listDateFilterDTO, token);
        }

        /// <summary>
        /// Get purchase order by id.
        /// </summary>
        /// <param name="businessTenantId">businessTenantId</param>
        /// <param name="purchaseOrderId">PurchaseOrderId</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{businessTenantId}/{purchaseOrderId}")]
        public async Task<BAPurchaseOrderViewDTO> GetPurcahseDetailByPOIdAsync([FromRoute]Guid businessTenantId, [FromRoute]Guid purchaseOrderId, CancellationToken token = default(CancellationToken)) {
            return await _purchaseOrderDS.GetPurchaseOrderByPurchaseOrderIdAsync(businessTenantId, purchaseOrderId, token);
        }

        /// <summary>
        /// Get purchase order attachment list.
        /// </summary>
        /// <param name="purchaseOrderId">Unique purchaseOrder id.</param>
        /// <param name="token"></param>
        /// <returns>return attachment list.</returns>
        [Route("purchaseorderattachment/{purchaseOrderId}")]
        [HttpGet]
        public async Task<IEnumerable<BAARPurchaseOrderAttachmentDTO>> GetPurchaseOrderAttachmentListByOrderIdAsync([FromRoute]Guid purchaseOrderId, CancellationToken token = default(CancellationToken)) {
            return await _purchaseOrderDS.GetPurchaseOrderAttachmentListByOrderIdAsync(purchaseOrderId, token);
        }
    /// <summary>
    /// Gets the delivery list by business tenant identifier.
    /// </summary>
    /// <param name="businessTenantId">The business tenant identifier.</param>
    /// <param name="listDateFilterDTO">The DTO to filter contract list.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns list of <see cref="BusBADeliveryDTO"/> that matches provided business tenant id.</returns>
    [HttpPut]
    [Route("vendlist/{businessTenantId}")]
    public async Task<IEnumerable<BAPurchaseOrderDTO>> GetPurchaseOrderListByBusinessTenantIdAsyncForVend([FromRoute]Guid businessTenantId, [FromBody] ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await _purchaseOrderDS.GetPurchaseOrderListByBusinessTenantIdAsyncForVend(businessTenantId, listDateFilterDTO, cancellationToken);
    }

    #endregion Get

  }
}
