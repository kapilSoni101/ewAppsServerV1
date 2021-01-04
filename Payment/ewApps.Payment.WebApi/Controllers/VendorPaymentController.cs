using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.UserSessionService;
using ewApps.Payment.DS;
using ewApps.Payment.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.Payment.WebApi.Controllers {

    /// <summary>
    /// APInvoice class is used to get invoice list.
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class VendorPaymentController:ControllerBase {

        #region Local Variables

        IVendorPaymentAndInvoiceDS _paInvoiceDS;
        IUserSessionManager _sessionManager;

        #endregion Local Variables

        #region Constructor

        /// <summary>
        /// Initlize local variables.
        /// </summary>
        /// <param name="paInvoiceDS">Queried invoice service.</param>
        /// <param name="sessionManager">Session manager.</param>
        public VendorPaymentController(IVendorPaymentAndInvoiceDS paInvoiceDS, IUserSessionManager sessionManager) {
            _paInvoiceDS = paInvoiceDS;
            _sessionManager = sessionManager;
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Gets Payment history by tenant and from/todate for a invoice by partnerid.
        /// </summary>
        /// <param name="filter">filter for getting tenant payment list.</param>
        /// <param name="token">For cancellation</param>
        /// <returns>IList of Payment entities</returns>
        [HttpPut]
        [Route("invoice/tenantpaymenthistory")]

        public async Task<IList<VendorPaymentDetailDQ>> GetTenantPaymentHistoryAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            IList<VendorPaymentDetailDQ> list = await _paInvoiceDS.GetAPInvoiceFilterTenantPaymentHistoryAsync(filter, token);
            return list;
        }

        /// <summary>
        /// Gets list of Payments for vendor invoice.
        /// </summary>
        /// <param name="invoiceId">invoiceId Guid</param>
        /// <param name="token">For cancellation</param>
        /// <returns>IList of Payment entities</returns>
        [HttpGet]
        [Route("invoice/invoicepaymenthistory/{invoiceId}")]
        public async Task<IList<VendorPaymentDetailDQ>> GetPaymentHistoryByInvoiceAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            IList<VendorPaymentDetailDQ> list = await _paInvoiceDS.GetVendorInvoicePaymentHistoryByInvoiceAsync(invoiceId, token);
            return list;
        }

        #endregion Get

    }
}
