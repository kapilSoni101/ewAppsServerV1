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
    /// VendorInvoice class is used to get invoice list.
    /// Add/delete invoice method.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VendorInvoiceController {

        #region Local Variables

        IVendorPaymentInvoiceDS _paymentAndInvoiceDS;
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
        public VendorInvoiceController(IVendorPaymentInvoiceDS paymentAndInvoiceDS, IHttpContextAccessor contextAccessor, IFileBrowserViewerService fileBrowserService, IUserSessionManager sessionManager) {
            _paymentAndInvoiceDS = paymentAndInvoiceDS;
            _contextAccessor = contextAccessor;
            _fileBrowserService = fileBrowserService;
            _sessionManager = sessionManager;
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get vendor invoice by id.
        /// </summary>
        /// <param name="invoiceId">invoiceId</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{invoiceId}")]
        public async Task<VendorAPInvoiceViewDTO> GetInvoiceByInvoiceIdAsync([FromRoute]Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.GetInvoiceByInvoiceIdAsync(invoiceId, token);
        }

        /// <summary>
        /// Get vendor invoice list by tenant id.
        /// </summary>
        /// <param name="filter">Filter object to filter invoice by date.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("allinvoices")]
        public async Task<List<VendorAPInvoiceDTO>> GetInvoiceByTenantAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.GetInvoiceListByTenantAsync(filter, token);
        }
    /// <summary>
    /// Get customer invoices by customer tenantid and filter by date.
    /// </summary>
    /// <param name="filter">Contains filter criteria.</param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("list")]
    public async Task<List<VendorAPInvoiceDTO>> GetInvoiceListByVendorAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken))
    {
      return await _paymentAndInvoiceDS.GetInvoiceListByVendorAsync(filter, token);
    }

    /// <summary>
    /// Get vendor invoice attachment list.
    /// </summary>
    /// <param name="invoiceId">Unique invoiceId id.</param>
    /// <param name="token"></param>
    /// <returns>return attachment list.</returns>
    [Route("invoiceattachment/{invoiceId}")]
        [HttpGet]
        public async Task<object> GetInvoiceAttachmentListByInvoiceIdAsync([FromRoute]Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.GetInvoiceAttachmentListByInvoiceIdAsync(invoiceId, token);
        }

        #endregion Get

    }
}
