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
    /// <summary>
    /// Invoice class is used to get invoice list.
    /// Add/delete invoice method.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController {

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
        public PaymentController(IPaymentAndInvoiceDS paymentAndInvoiceDS, IUserSessionManager sessionManager) {
            _paymentAndInvoiceDS = paymentAndInvoiceDS;
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
        public async Task<object> AddMulitplePayment([FromBody] AddPaymentDTO[] payments, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.AddPaymentsAsync(payments, token);
        }

        /// <summary>
        /// To pay advance payment.
        /// </summary>
        /// <param name="payment">AddPaymentDTO object</param>
        /// <param name="token">Cancellation token for async operations</param>
        // POST api/payment
        [HttpPost]
        [Route("invoice/advancepayment")]
        public async Task<object> AddAdvancePayment([FromBody] AddPaymentDTO payment, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.AddAdvancePaymentsAsync(payment, token);
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
            return await _paymentAndInvoiceDS.GetPaymentDTOAsync(paymentId, invoiceId, token);
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
            return await _paymentAndInvoiceDS.GetPaymentDTOAsyncByPaymentIdAsync(paymentId, token);
        }

        /// <summary>
        /// Gets advance Payments detail by given Payment id.
        /// </summary>
        /// <param name="paymentId">  Id, unique key</param>        
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>       
        [HttpGet] 
        [Route("advancepaymentdetail/{paymentId}")]
        public async Task<AdvancePaymentDetailModelDTO> GetAdvancePaymentDTOAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.GetAdvancePaymentDTOAsync(paymentId, token);
        }

        /// <summary>
        /// Get the list of invoices paid in a single transection(payment).
        /// <param name="paymentId">  paymentId, unique key</param>
        /// </summary>        
        [AllowAnonymous]
        [HttpGet]
        [Route("invoicelist/{paymentId}")]
        public async Task<List<InvoiceInfoDTO>> GetInvoicesByPaymentId([FromRoute]Guid paymentId, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.GetInvoicesByPaymentIdAsync(paymentId, token);
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
            return await _paymentAndInvoiceDS.GetPaymentStatusFlowListAsync(id, paymentmode, token);
        }

        #endregion Get

        #region Quick Payment

        /// <summary>
        /// Generate deeplink of a invoice.
        /// </summary>
        /// <param name="deepLinkRequestModel"></param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        [HttpPut]
        [Route("invoice/generatequickpaylink")]
        public async Task<string> GenerateQuickPaylink([FromBody]AddDeepLinkDTO deepLinkRequestModel, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.GenerateQuickPaylink(deepLinkRequestModel, token);
        }

        /// <summary>
        /// Get invoice detail by deeplink shorturl.
        /// </summary>
        /// <param name="shortUrlKey">Shorturl key</param>
        /// <param name="machineIPAddress">Machine IP address.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("quickpaydetail/{shortUrlKey}/{machineIPAddress}")]
        public async Task<QuickPayInvoiceDetailDTO> GetInvoiceCustomerPaymentDetailAsyncByQuickPaylink(string shortUrlKey, string machineIPAddress, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.GetInvoiceCustomerPaymentDetailAsyncByQuickPaylink(shortUrlKey,machineIPAddress, token);
        }

        /// </summary>
        /// <param name="payments">AddPaymentDTO object</param>
        /// <param name="token">Cancellation token for async operations</param>
        // POST api/payment
        [AllowAnonymous]
        [HttpPost]
        [Route("invoice/payment/{shorturl}")]
        public async Task<object> AddMulitplePayment([FromBody] AddPaymentDTO[] payments, [FromRoute]string shorturl, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.QuickPaymentsAsync(payments, shorturl, token);            
        }

        #endregion Quick Payment

        #region Payment History


        /// <summary>
        /// Gets Payment history by tenant and from/todate for a invoice by partnerid.
        /// </summary>
        /// <param name="filter">filter for getting tenant payment list.</param>
        /// <param name="token">For cancellation</param>
        /// <returns>IList of Payment entities</returns>
        [HttpPut]
        [Route("invoice/tenantpaymenthistory")]

        //public async Task<IList<PaymentDetailDTO>> GetTenantPaymentHistoryAsync([FromBody]DateTimeFilterDTO filter, CancellationToken token = default(CancellationToken)) {
        public async Task<IList<PaymentDetailDTO>> GetTenantPaymentHistoryAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            IList<PaymentDetailDTO> list = await _paymentAndInvoiceDS.GetFilterTenantPaymentHistoryAsync(filter, token);
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
        public async Task<IList<PaymentDetailDTO>> GetPaymentHistoryAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            IList<PaymentDetailDTO> list = await _paymentAndInvoiceDS.GetCustomerFilterPaymentHistoryAsync(filter, token);
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
        public async Task<IList<PaymentDetailDTO>> GetPaymentHistoryByInvoiceAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            IList<PaymentDetailDTO> list = await _paymentAndInvoiceDS.GetPaymentHistoryByInvoiceAsync(invoiceId, token);
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
        public async Task<IList<PaymentDetailDTO>> GetVoidPaymentHistoryAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            IList<PaymentDetailDTO> list = await _paymentAndInvoiceDS.GetVoidPaymentHistoryAsync(filter, token);
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
        public async Task<IList<PaymentDetailDTO>> GetSattledPaymentHistoryAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            IList<PaymentDetailDTO> list = await _paymentAndInvoiceDS.GetSattledFilterTenantPaymentHistoryAsync(filter, token);
            return list;
        }

        #endregion Payment History

        #region Advance Payment

        /// <summary>
        /// Get advance payment list by tenantid.
        /// </summary>
        /// <param name="filter">Filter object contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>      
        [HttpPut]
        [Route("advance/tenantpaymenthistory")]
        public async Task<IList<PaymentDetailDTO>> GetFilterTenantAdvancePaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.GetFilterTenantAdvancePaymentHistoryAsync(filter, token);
        }

        /// <summary>
        /// Get customer advance payment list by partnerid.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("advance/customerpaymenthistory")]
        public async Task<IList<PaymentDetailDTO>> GetCustomerAdvanceFilterPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _paymentAndInvoiceDS.GetCustomerAdvanceFilterPaymentHistoryAsync(filter, token);
        }

        #endregion Advance Payment

        #region void/refund payment

        /// <summary>
        /// Void the payment/transaction
        /// </summary>        
        /// <param name="id">PaymentId</param>
        /// <param name="serviceName">Service used to pay the invoice.</param>
        /// <param name="token">Cancellation token for async operations</param>
        // POST api/payment
        [HttpPost("void/{id}/{serviceName}")]
        public async Task<bool> VoidPayment(Guid id, string serviceName, CancellationToken token = default(CancellationToken)) {
            await _paymentAndInvoiceDS.VoidPaymentAsync(id, serviceName, token);
            return true;
        }

        /// <summary>
        /// Refund the payment/transaction
        /// </summary>        
        /// <param name="token">Cancellation token for async operations</param>
        /// <param name="id">PaymentId</param>
        // POST api/payment
        [HttpPost("refund/{id}")]
        public async Task<bool> RefundPayment(Guid id, CancellationToken token = default(CancellationToken)) {
            await _paymentAndInvoiceDS.RefundPaymentAsync(id, token);
            return true;
        }

        #endregion void/refund payment

        #region Vendor

        /// <summary>
        /// Gets list of Payments for a invoice.
        /// </summary>
        /// <param name="invoiceId">invoiceId Guid</param>
        /// <param name="token">For cancellation</param>
        /// <returns>IList of Payment entities</returns>
        [HttpGet]
        [Route("vendor/invoicepaymenthistory/{invoiceId}")]
        public async Task<IList<PaymentDetailDTO>> GetVendorPaymentHistoryByInvoiceAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            IList<PaymentDetailDTO> list = await _paymentAndInvoiceDS.GetPaymentHistoryByInvoiceAsync(invoiceId, token);
            return list;
        }

        #endregion Vendor

    }
}
