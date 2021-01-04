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
    /// Invoice class is used to get invoice list.
    /// Add/delete invoice method.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    
    public class PaymentController:ControllerBase {


        #region Local Variables

        IPaymentAndInvoiceDS _paInvoiceDS;
        IUserSessionManager _sessionManager;

        #endregion Local Variables

        #region Constructor

        /// <summary>
        /// Initlize local variables.
        /// </summary>
        /// <param name="paInvoiceDS">Queried invoice service.</param>
        /// <param name="sessionManager">Session manager.</param>
        public PaymentController(IPaymentAndInvoiceDS paInvoiceDS, IUserSessionManager sessionManager) {
            _paInvoiceDS = paInvoiceDS;
            _sessionManager = sessionManager;
        }

        #endregion Constructor

        #region Add

        /// <summary>
        /// Add muliple invoice payment.
        /// </summary>
        /// <param name="payments">AddPaymentDTO object</param>
        /// <param name="token">Cancellation token for async operations</param>
        // POST api/payment
        [HttpPost]
        [Route("invoice/payment")]
        public async Task<PaymentDetailDQ> AddMulitplePayment([FromBody] AddPaymentDTO[] payments, CancellationToken token = default(CancellationToken)) {
            PaymentDetailDQ paymentDTO = await _paInvoiceDS.AddPaymentsAsync(payments, token);

            //MessagePayload payload = new MessagePayload(PayLiveUpdateConstants.AddPaymentEvent, payments);
            string groupName = payments[0].TenantId.ToString();
            //_realtimeUpdate.SendMessageAsync(PayLiveUpdateConstants.PaymentLiveUpdateHandler, payload, groupName);

            //Return new object along with Get URI with status code 201-Created.
            return paymentDTO;

        }

        /// <summary>
        /// Add muliple invoice payment.
        /// </summary>
        /// <param name="payments">AddPaymentDTO object</param>
        /// <param name="token">Cancellation token for async operations</param>
        // POST api/payment
        [HttpPost]
        [Route("invoice/advancepayment")]
        public async Task<PaymentDetailDQ> AddAdvancePayment([FromBody] AddAdvancedPaymentDTO payment, CancellationToken token = default(CancellationToken)) {
            PaymentDetailDQ paymentDTO = await _paInvoiceDS.AddAdvancePaymentsAsync(payment, token);

            //Return new object along with Get URI with status code 201-Created.
            return paymentDTO;

        }

        #endregion Add

        #region Get

        /// <summary>
        /// Gets Payment DTO given its  id and inventoryid.
        /// </summary>
        /// <param name="paymentId">  paymentId, unique key</param>
        /// <param name="invoiceId">  invoiceId</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        [HttpGet]
        [Route("invoice/paymentdetail/{paymentId}/{invoiceId}")]
        public async Task<PaymentDetailModelDTO> GetPaymentDTOAsync(Guid paymentId, Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            return await _paInvoiceDS.GetPaymentDTOAsync(paymentId, invoiceId, token);
        }

        /// <summary>
        /// Get payment detail by paymentid.
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("paymentdetail/{paymentId}")]
        public async Task<PaymentDetailModelDTO> GetPaymentDTOAsyncByPaymentIdAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            return await _paInvoiceDS.GetPaymentDTOAsyncByPaymentIdAsync(paymentId, token);
        }

        /// <summary>
        /// Gets advance Payments detail by given Payment id.
        /// </summary>
        /// <param name="paymentId">  Id, unique key</param>        
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        [HttpGet]
        [Route("advancepaymentdetail/{paymentId}")]
        public async Task<PaymentAdvanceDetailModelDTO> GetAdvancePaymentDTOAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            return await _paInvoiceDS.GetAdvancePaymentDTOAsync(paymentId, token);
        }

        /// <summary>
        /// Get the list of invoices paid in a single transection(payment).
        /// <param name="paymentId">  paymentId, unique key</param>
        /// </summary>        
        [AllowAnonymous]
        [HttpGet]
        [Route("invoicelist/{paymentId}")]
        public async Task<List<InvoiceInfoDTO>> GetInvoicesByPaymentId([FromRoute]Guid paymentId, CancellationToken token = default(CancellationToken)) {
            return await _paInvoiceDS.GetInvoicesByPaymentIdAsync(paymentId, token);
        }

        /// <summary>
        /// Gets Payment history by tenant and from/todate for a invoice by partnerid.
        /// </summary>
        /// <param name="filter">filter for getting tenant payment list.</param>
        /// <param name="token">For cancellation</param>
        /// <returns>IList of Payment entities</returns>
        [HttpPut]
        [Route("invoice/tenantpaymenthistory")]

        
        public async Task<IList<PaymentDetailDQ>> GetTenantPaymentHistoryAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            IList<PaymentDetailDQ> list = await _paInvoiceDS.GetFilterTenantPaymentHistoryAsync(filter, token);
            return list;
        }

        /// <summary>
        /// Gets list of Payments for a invoice by partnerid.
        /// </summary>
        /// <param name="filter">filter for getting customer payment list.</param>
        /// <param name="token">For cancellation</param>
        /// <returns>IList of Payment entities</returns>
        [HttpPut]
        [Route("invoice/customerpaymenthistory")]
        // public async Task<IList<PaymentDetailDTO>> GetPaymentHistoryAsync([FromBody]DateTimeFilterDTO filter, CancellationToken token = default(CancellationToken)) {
        public async Task<IList<PaymentDetailDQ>> GetPaymentHistoryAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            IList<PaymentDetailDQ> list = await _paInvoiceDS.GetCustomerFilterPaymentHistoryAsync(filter, token);
            return list;
        }

        /// <summary>
        /// Gets Payment history by tenant and from/todate.
        /// Method return all payyment with the status void.
        /// </summary>
        /// <param name="filter">filter for getting tenant payment list.</param>
        /// <param name="token">For cancellation</param>
        /// <returns>IList of Payment entities</returns>
        [HttpPut]
        [Route("invoice/voidpaymenthistory")]

        //public async Task<IList<PaymentDetailDTO>> GetVoidPaymentHistoryAsync([FromBody]DateTimeFilterDTO filter, CancellationToken token = default(CancellationToken)) {
        public async Task<IList<PaymentDetailDQ>> GetVoidPaymentHistoryAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            IList<PaymentDetailDQ> list = await _paInvoiceDS.GetVoidFilterTenantPaymentHistoryAsync(filter, token);
            return list;
        }

        /// <summary>
        /// Gets Payment history by tenant and from/todate.
        /// Method return all payyment with the status Sattled.
        /// </summary>voidpay
        /// <param name="filter">filter for getting tenant payment list.</param>
        /// <param name="token">For cancellation</param>
        /// <returns>IList of Payment entities</returns>
        [HttpPut]
        [Route("invoice/sattledpaymenthistory")]
        //public async Task<IList<PaymentDetailDTO>> GetSattledPaymentHistoryAsync([FromBody]DateTimeFilterDTO filter, CancellationToken token = default(CancellationToken)) {
        public async Task<IList<PaymentDetailDQ>> GetSattledPaymentHistoryAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            IList<PaymentDetailDQ> list = await _paInvoiceDS.GetSattledFilterTenantPaymentHistoryAsync(filter, token);
            return list;
        }

        /// <summary>
        /// Gets list of Payments for a invoice.
        /// </summary>
        /// <param name="invoiceId">invoiceId Guid</param>
        /// <param name="token">For cancellation</param>
        /// <returns>IList of Payment entities</returns>
        [HttpGet]
        [Route("invoice/invoicepaymenthistory/{invoiceId}")]
        public async Task<IList<PaymentDetailDQ>> GetPaymentHistoryByInvoiceAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            IList<PaymentDetailDQ> list = await _paInvoiceDS.GetPaymentHistoryByInvoiceAsync(invoiceId, token);
            return list;
        }

        /// <summary>
        /// Get advance payment list by tenantid.
        /// </summary>
        /// <param name="filter">Filter object contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>      
        [HttpPut]
        [Route("advance/tenantpaymenthistory")]
        public async Task<IList<PaymentDetailDQ>> GetFilterTenantAdvancePaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _paInvoiceDS.GetFilterTenantAdvancePaymentHistoryAsync(filter, token);
        }

        /// <summary>
        /// Get customer advance payment list by partnerid.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("advance/customerpaymenthistory")]
        public async Task<IList<PaymentDetailDQ>> GetCustomerAdvanceFilterPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _paInvoiceDS.GetCustomerAdvanceFilterPaymentHistoryAsync(filter, token);
        }


        /// <summary>
        /// Get payment transection status flow.
        /// </summary>
        /// <param name="id">Unique paymentId</param>
        /// <param name="paymentmode">Mode of payment Vericheck or card</param>
        /// <param name="token"></param>
        /// <returns>retun payment status.</returns>
        // GET api/Payment/id
        [HttpGet("allstatus/{id}/{paymentmode}")]
        public async Task<List<PaymentTransectionStatusDTO>> GetStatusListAsync(Guid id, string paymentmode, CancellationToken token = default(CancellationToken)) {            
            return await _paInvoiceDS.GetPaymentStatusFlowListAsync(id, paymentmode, token);            
        }

        #endregion Get

        #region void/refund payment

        /// <summary>
        /// Void the payment/transaction
        /// </summary>        
        /// <param name="id">PaymentId</param>
        /// <param name="serviceName">service used for paying invoice</param>
        /// <param name="token">Cancellation token for async operations</param>
        // POST api/payment
        [HttpPost("void/{id}/{serviceName}")]
        public async Task<IActionResult> VoidPayment(Guid id, string serviceName, CancellationToken token = default(CancellationToken)) {
            await _paInvoiceDS.VoidPaymentAsync(id, serviceName, token);
            return Ok();
        }

        /// <summary>
        /// Void the payment/transaction
        /// </summary>        
        /// <param name="token">Cancellation token for async operations</param>
        /// <param name="id">PaymentId</param>
        // POST api/payment
        [HttpPost("refund/{id}")]
        public async Task<IActionResult> RefundPayment(Guid id, CancellationToken token = default(CancellationToken)) {
            await _paInvoiceDS.RefundPaymentAsync(id, token);
            return Ok();
        }

        #endregion void/refund payment

    }
}
