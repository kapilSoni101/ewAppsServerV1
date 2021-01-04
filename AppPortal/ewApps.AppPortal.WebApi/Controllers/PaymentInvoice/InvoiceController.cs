using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using ewApps.Core.DMService;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Adapter.Internal;

namespace ewApps.AppPortal.WebApi {
    /// <summary>
    /// Invoice class is used to get invoice list.
    /// Add/delete invoice method.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvoiceController:ControllerBase {

        #region Local Variables

        IPaymentAndInvoiceDS _paymentAndInvoiceDS;
        IUserSessionManager _sessionManager;
        private static IHttpContextAccessor _contextAccessor;
        IFileBrowserViewerService _fileBrowserService;

        #endregion Local Variables

        #region Constructor

        /// <summary>
        /// Initlize local variables.
        /// </summary>
        /// <param name="paymentAndInvoiceDS">Wrapper payment/invoice service class.</param>
        /// <param name="sessionManager">Session manager.</param>
        public InvoiceController(IPaymentAndInvoiceDS paymentAndInvoiceDS, IHttpContextAccessor contextAccessor, IFileBrowserViewerService fileBrowserService, IUserSessionManager sessionManager) {
            _paymentAndInvoiceDS = paymentAndInvoiceDS;
            _contextAccessor = contextAccessor;
            _fileBrowserService = fileBrowserService;
            _sessionManager = sessionManager;
        }

        #endregion Constructor

        #region Add

        /// <summary>
        /// Add invoice in standalone case.
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<InvoiceResponseDTO> AddInvoiceAsync([FromBody] AddBAARInvoiceDTO request, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.AddInvoiceAsync(request, token);
        }

        [HttpPost]
        [Route("addinvoice")]
        public async Task<InvoiceResponseDTO> AddInvoiceAndAttachmentAsync(CancellationToken token = default(CancellationToken)) {

            HttpRequest httpRequest = _contextAccessor.HttpContext.Request;

            string request = HttpContext.Request.Headers["AddUpdateInvoiceModel"];
            return await _paymentAndInvoiceDS.AddInvoiceAsync(httpRequest, request, token);
            //_publisherSupportTicketDS.AddPublisherSupportTicket(supportTicketDTO, documentModel, httpRequest);
        }

        /// <summary>
        /// cancel invoice in standalone case.
        /// </summary>
        /// <param name="invoiceid">Invoice id.</param>
        /// <param name="token"></param>
        /// <returns>return invoice response object.</returns>
        [HttpPut("cancelinvoice/{invoiceid}")]
        public async Task<object> CancelInvoiceAsync(Guid invoiceid, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.CancelInvoiceAsync(invoiceid, token);
        }

        #endregion Add

        #region Get

        /// <summary>
        /// Get invoice by id.
        /// </summary>
        /// <param name="invoiceId">invoiceId</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{invoiceId}")]
        public async Task<BAARInvoiceViewDTO> GetInvoiceByInvoiceIdAsync([FromRoute]Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.GetInvoiceByInvoiceIdAsync(invoiceId, token);
        }

        /// <summary>
        /// Get invoice by id.
        /// </summary>        
        /// <param name="invoiceId">invoiceId</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getinvoiceandsync/{erpARInvoiceKey}/{invoiceId}/{tenantid}")]
        public async Task<BAARInvoiceViewDTO> GetInvoiceByInvoiceIdAndSyncInvoiceAsync(string erpARInvoiceKey, [FromRoute]Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.GetInvoiceByInvoiceIdAsync(invoiceId, token);
        }

        /// <summary>
        /// Get invoice list by tenant id.
        /// </summary>
        /// <param name="filter">Filter object to filter invoice by date.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("allinvoices")]
        public async Task<List<BAARInvoiceDTO>> GetInvoiceByTenantAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.GetInvoiceListByTenantAsync(filter, token);
        }

        /// <summary>
        /// Get cancel invoice list by tenant id.
        /// </summary>
        /// <param name="filter">Filter object to filter invoice by date.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("allcancelinvoices")]
        public async Task<List<BAARInvoiceDTO>> GetCancelInvoiceByTenantAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.GetCancelInvoiceListByTenantAsync(filter, token);
        }

        /// <summary>
        /// Get customer invoices by customer tenantid and filter by date.
        /// </summary>
        /// <param name="filter">Contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("customerinvoices")]
        public async Task<List<BAARInvoiceDTO>> GetInvoiceListByCustomerAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.GetInvoiceListByCustomerAsync(filter, token);
        }

        /// <summary>
        /// Get quick pay invoice list.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("allquickpayinvoices")]
        public async Task<List<BAARInvoiceDTO>> GetQuickPayInvoicesByTenantAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.GetQuickPayInvoicesByTenantAsync(filter, token);
        }

        /// <summary>
        /// Get customer payble invoices list.
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("paybleinvoices/{customerId}")]
        public async Task<List<BAARInvoiceDTO>> GetPaybleInvoiceByCustomerAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.GetCustomerPaybleinvoicesByCustomerIdAsync(customerId, token);
        }

        /// <summary>
        /// show the document by document by storage id.
        /// </summary>
        /// <returns></returns>
        [Route("filebrowserview/{storageId:Guid}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetFileIntoBrowserView(Guid storageId) {
            HttpResponseMessage result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            try {
                //IStorageService service = StorageServiceFactory.GetStorageService(this.GetFileStorageType());
                IFileBrowserViewerService service = this._fileBrowserService;
                FileResultData fileInfo = service.GetFileInfo(storageId, null);

                string mimeType = FileSupport.GetFileMimeType(fileInfo.FileName);
                result.Headers.Add("x-mimetype", mimeType);
                result.Headers.Add("access-control-expose-headers", new[] { "x-mimetype" });
                result.Headers.Add("x-filepath", fileInfo.FileUrl);
                result.Headers.Add("access-control-expose-headers", new[] { "x-filepath" });
                if(fileInfo.FileStream != null) {

                    //result.Content = new StreamContent(fileInfo.FileStream);
                    string base64 = Convert.ToBase64String(await new StreamContent(fileInfo.FileStream).ReadAsByteArrayAsync());
                    result.Headers.Add("x-base64", base64);
                    result.Headers.Add("access-control-expose-headers", new[] { "x-base64" });
                    //result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") {
                    //    FileName = fileInfo.FileName
                    //};

                    //result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                }
                return result;
            }
            catch(Exception ex) {
                result = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                //ServiceExceptionHandler.HandleException(ref ex);
                result.Headers.Add("ErrorMessage", ex.Message);
                result.Headers.Add("access-control-expose-headers", new[] { "ErrorMessage" });
                return result;
            }
        }

        /// <summary>
        /// Get invoice attachment list.
        /// </summary>
        /// <param name="invoiceId">Unique invoiceId id.</param>
        /// <param name="token"></param>
        /// <returns>return attachment list.</returns>
        [Route("invoiceattachment/{invoiceId}")]
        [HttpGet]
        public async Task<object> GetInvoiceAttachmentListByInvoiceIdAsync([FromRoute]Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.GetInvoiceAttachmentListByInvoiceIdAsync(invoiceId, token);
        }

        /// <summary>
        /// Get invoice list and customer payment info by invoice and customerid.
        /// </summary>
        /// <param name="requestDTO">Ids of invoice.</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("payment/getinvoicesandcustomer")]
        public async Task<InvoiceCustomerResponseDTO> GetBAARInvoiceDTOByInvoiceIdAsync([FromBody]InvoiceCustomerRequestDTO requestDTO, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.GetInvoiceAndCustomerPayInfoByInvoiceIdAndSyncInvoiceAsync(requestDTO, token);
        }

        #endregion Get

        #region Delete

        /// <summary>
        /// Delete attahcment.
        /// </summary>
        /// <param name="invoiceId">invoiceId.</param>
        /// <param name="documentid">documentid to delete.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [Route("deleteattachment/{invoiceId:Guid}/{documentid:Guid}")]
        [HttpPut]
        public async Task<object> DeleteInvoiceAttachmentAsync(Guid invoiceId, Guid documentid, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.DeleteInvoiceAttachmentByDocumentIdAsync(invoiceId, documentid, token);
        }

        #endregion Delete

    }
}
