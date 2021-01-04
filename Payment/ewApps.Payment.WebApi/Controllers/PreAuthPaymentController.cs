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

namespace ewApps.Payment.WebApi {

    /// <summary>
    /// Invoice class is used to get invoice list.
    /// Add/delete invoice method.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PreAuthPaymentController:ControllerBase {

        #region Local Variables

        IPreAuthPaymentDS _preAuthPaymentDS;
        IUserSessionManager _sessionManager;

        #endregion Local Variables

        #region Constructor

        /// <summary>
        /// Initlize local variables.
        /// </summary>
        /// <param name="preAuthPaymentDS">Preauthrization Payment service.</param>
        /// <param name="sessionManager">Session manager.</param>
        public PreAuthPaymentController(IPreAuthPaymentDS preAuthPaymentDS, IUserSessionManager sessionManager) {
            _preAuthPaymentDS = preAuthPaymentDS;
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
        public async Task<PaymentDQ> AuthorizePayment([FromBody] AddPreAuthPaymentDTO payment, CancellationToken token = default(CancellationToken)) {
            return await _preAuthPaymentDS.AddPreAuthPaymentAsync(payment, token);         
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
        public async Task<IList<PreAuthPaymentDetailDQ>> GetFilterTenantPreAuthPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _preAuthPaymentDS.GetFilterTenantPreAuthPaymentHistoryAsync(filter, token);
        }

        #endregion Get

        #region Void Pre Auth

        /// <summary>
        /// Void the pre auth transaction
        /// </summary>        
        /// <param name="id">Pre Authorization PaymentId</param>        
        /// <param name="token">Cancellation token for async operations</param>
        // POST api/payment
        [HttpPost("void/{id}")]
        public async Task<IActionResult> VoidPreAuthPayment(Guid id, CancellationToken token = default(CancellationToken)) {
            await _preAuthPaymentDS.voidPreAuthPaymentAsync(id, token);
            return Ok();
        }

        #endregion Void Pre Auth

    }
}
