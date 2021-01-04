using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DS;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.BusinessEntity.WebApi.Controllers {

    /// <summary>
    /// Provide add/update/get method for PurchaseOrder.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController:ControllerBase {

        #region Local Variables

        IBAPurchaseOrderDS _purchaseOrderDS;
        private static IHttpContextAccessor _contextAccessor;

        #endregion Local Variables

        #region Constructors

        /// <summary>
        /// To initilize initial local variables.
        /// </summary>
        /// <param name="purchaseOrderDS"></param>
        /// <param name="contextAccessor"></param>
        public PurchaseOrderController(IBAPurchaseOrderDS purchaseOrderDS, IHttpContextAccessor contextAccessor) {
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
            return await _purchaseOrderDS.AddPurchaseOrder(httpRequest, request, token);
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
            return await _purchaseOrderDS.GetPurcahseDetailByPOIdAsync(businessTenantId, purchaseOrderId, token);
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

        #endregion Get

    }
}