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

    /// <summary>
    /// Provide add/update/get method for ERPConnector 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController: ControllerBase {

        #region Local Variables

        IBAARInvoiceDS _baARInvoiceDS;
        private static IHttpContextAccessor _contextAccessor;

        #endregion

        #region Constructor

        /// <summary>
        /// invoice controller.
        /// </summary>
        /// <param name="bAARInvoiceDS"></param>
        public InvoiceController(IBAARInvoiceDS bAARInvoiceDS, IHttpContextAccessor contextAccessor) {
            _baARInvoiceDS = bAARInvoiceDS;
            _contextAccessor = contextAccessor;
    }

        #endregion

        #region Add

        /// <summary>
        /// Add invoice in standalone case.
        /// </summary>
        /// <param name="request">Invoice dto model.</param>
        /// <param name="token"></param>
        /// <returns>return invoice response object.</returns>
        [HttpPost("add")]
        public async Task<InvoiceResponseDTO> AddInvoiceAsync([FromBody] AddBAARInvoiceDTO request, CancellationToken token = default(CancellationToken)) {
            return await _baARInvoiceDS.AddInvoiceAsync(request, token);
        }

        /// <summary>
        /// Add invoice in standalone case.
        /// </summary>
        /// <param name="request">Invoice dto model.</param>
        /// <param name="token"></param>
        /// <returns>return invoice response object.</returns>
        [HttpPost("updateinvoiceamount")]
        public async Task<object> UpdateInvoiceAmountAsync([FromBody] List<BAARInvoiceEntityDTO> request, CancellationToken token = default(CancellationToken)) {
            return await _baARInvoiceDS.UpdateInvoiceAmountAsync(request, token);
        }

        /// <summary>
        /// Add invoice in standalone case.
        /// </summary>
        /// <param name="token"></param>
        /// <returns>return invoice response object.</returns>
        [HttpPost("addinvoice")]
        public async Task<InvoiceResponseDTO> AddInvoiceAsync(CancellationToken token = default(CancellationToken)) {
            HttpRequest httpRequest = _contextAccessor.HttpContext.Request;
            string request = HttpContext.Request.Headers["AddUpdateInvoiceModel"];
            return await _baARInvoiceDS.AddInvoiceAsync(httpRequest, request, token);
        }

        /// <summary>
        /// Delete attahcment.
        /// </summary>
        /// <param name="invoiceId">InvoiceId</param>
        /// <param name="documentid">documentid to delete.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut("deleteattachment/{invoiceId}/{documentid}")]
        public async Task<IEnumerable<BusBAARInvoiceAttachmentDTO>> DeleteInvoiceAttachmentAsync(Guid invoiceId, Guid documentid, CancellationToken token = default(CancellationToken)) {
            await _baARInvoiceDS.DeleteInvoiceAttachment(documentid, token);
            return await _baARInvoiceDS.GetInvoiceAttachmentListByInvoiceIdAsync(invoiceId, token);
        }

        #endregion Add

        #region Update/Delete/Cancel

        /// <summary>
        /// cancel invoice in standalone case.
        /// </summary>
        /// <param name="invoiceid">Invoice id.</param>
        /// <param name="token"></param>
        /// <returns>return invoice response object.</returns>
        [HttpPut("cancelinvoice/{invoiceid}")]
        public async Task<object> CancelInvoiceAsync(Guid invoiceid, CancellationToken token = default(CancellationToken)) {
            return await _baARInvoiceDS.CancelInvoiceAsync(invoiceid, token);
        }

        #endregion Update/Delete/Cancel

        #region Get

        /// <summary>
        /// Get invoice attachment list.
        /// </summary>
        /// <param name="invoiceId">Unique invoiceId id.</param>
        /// <param name="token"></param>
        /// <returns>return attachment list.</returns>
        [Route("invoiceattachment/{invoiceId}")]
        [HttpGet]
        public async Task<IEnumerable<BusBAARInvoiceAttachmentDTO>> GetInvoiceAttachmentListByInvoiceIdAsync([FromRoute]Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            return await _baARInvoiceDS.GetInvoiceAttachmentListByInvoiceIdAsync(invoiceId, token);
        }

        #endregion Get

    }
}
