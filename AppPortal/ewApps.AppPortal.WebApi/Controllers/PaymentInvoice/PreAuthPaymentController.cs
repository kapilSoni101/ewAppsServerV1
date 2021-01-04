/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 29 October 2019
 * 
 * Contributor/s: 
 * Last Updated On: 29 October 2019
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {
    /// <summary>
    /// Invoice class is used to get invoice list.
    /// Add/delete invoice method.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PreAuthPaymentController:ControllerBase {

        #region Local Variables

        IPaymentAndInvoiceDS _paymentAndInvoiceDS;
        IUserSessionManager _sessionManager;

        #endregion Local Variables

        #region Constructor

        /// <summary>
        /// Initlize local variables.
        /// </summary>
        /// <param name="paymentAndInvoiceDS">Wrapper payment/invoice service class.</param>
        /// <param name="sessionManager">Session manager.</param>
        public PreAuthPaymentController(IPaymentAndInvoiceDS paymentAndInvoiceDS, IUserSessionManager sessionManager) {
            _paymentAndInvoiceDS = paymentAndInvoiceDS;
            _sessionManager = sessionManager;
        }

        #endregion Constructor

        #region Authorize/Block Amount

        /// <summary>
        /// Authorize/Block Amount from Credit card for future use.
        /// </summary>
        /// <param name="payment">Payment authorize object</param>
        /// <param name="token">Cancellation token for async operations</param>
        // POST api/payment
        [HttpPost]
        [Route("authorization/payment")]
        public async Task<object> AuthorizePayment([FromBody] AddPreAuthPaymentDTO payment, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.AuthorizeCardPaymentAsync(payment, token);
        }

        #endregion Authorize/Block Amount

        #region Get

        [HttpPut]
        [Route("authpayhistory")]
        /// <summary>
        /// Get card preauthorized payment history.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IList<PreAuthPaymentDetailDTO>> GetFilterTenantPreAuthPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.GetFilterTenantPreAuthPaymentHistoryAsync(filter, token);
        }

        #endregion Get

        /// <summary>
        /// UnAuthorize/UnBlock Amount from Credit card for future use.
        /// </summary>        
        /// <param name="id">Pre Authorization PaymentId</param>        
        /// <param name="token">Cancellation token for async operations</param>
        // POST api/payment
        [HttpPost("void/{id}")]
        public async Task<IActionResult> VoidPreAuthPayment(Guid id, CancellationToken token = default(CancellationToken)) {
            await _paymentAndInvoiceDS.VoidPreAuthPaymentAsync(id, token);
            return Ok();
        }

    }
}
