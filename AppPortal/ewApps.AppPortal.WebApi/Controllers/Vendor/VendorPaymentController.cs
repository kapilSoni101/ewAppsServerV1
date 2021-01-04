using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {
    public class VendorPaymentController {


        #region Local Variables

        IVendorPaymentInvoiceDS _paymentAndInvoiceDS;
        IUserSessionManager _sessionManager;

        #endregion Local Variables

        #region Constructor

        /// <summary>
        /// Initlize local variables.
        /// </summary>
        /// <param name="paymentAndInvoiceDS">Wrapper payment/invoice service class.</param>
        /// <param name="sessionManager">Session manager.</param>
        public VendorPaymentController(IVendorPaymentInvoiceDS paymentAndInvoiceDS, IUserSessionManager sessionManager) {
            _paymentAndInvoiceDS = paymentAndInvoiceDS;
            _sessionManager = sessionManager;
        }

        #endregion Constructor

        #region Vendor

        /// <summary>
        /// Gets list of Payments for a invoice.
        /// </summary>
        /// <param name="invoiceId">invoiceId Guid</param>
        /// <param name="token">For cancellation</param>
        /// <returns>IList of Payment entities</returns>
        [HttpGet]
        [Route("invoicepaymenthistory/{invoiceId}")]
        public async Task<IList<VendorPaymentDetailDTO>> GetVendorPaymentHistoryByInvoiceAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            IList<VendorPaymentDetailDTO> list = await _paymentAndInvoiceDS.GetPaymentHistoryByInvoiceAsync(invoiceId, token);
            return list;
        }

        #endregion Vendor

    }
}
