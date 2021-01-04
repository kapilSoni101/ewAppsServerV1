using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.AppDeeplinkService;
using ewApps.Core.DMService;
using ewApps.Core.UserSessionService;
using ewApps.Payment.Common;
using ewApps.Payment.DS;
using ewApps.Payment.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ewApps.Payment.WebApi.Controllers {

    /// <summary>
    /// Invoice class is used to get invoice list.
    /// Add/delete invoice method.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class QuickPaymentController:ControllerBase {

        #region LocalVariable & Constructor

        IPaymentDataService _paymentDS;
        IPaymentAndInvoiceDS _paymentAndInvoiceDS;
        IAppDeeplinkManager _appDeeplinkManager;
        IUserSessionManager _sessionManager;
        PaymentAppSettings _appSetting;

        /// <summary>
        /// Initilize local variables.
        /// </summary>
        /// <param name="paymentDataService"></param>
        /// <param name="paymentAndInvoiceDS"></param>
        /// <param name="appDeeplinkManager"></param>
        /// <param name="sessionManager"></param>
        /// <param name="appSetting"></param>
        public QuickPaymentController(IPaymentDataService paymentDataService,
                        IPaymentAndInvoiceDS paymentAndInvoiceDS,
                        IAppDeeplinkManager appDeeplinkManager, 
                        IUserSessionManager sessionManager, 
                        IOptions<PaymentAppSettings> appSetting) {
            _paymentDS = paymentDataService;
            _paymentAndInvoiceDS = paymentAndInvoiceDS;
            _appDeeplinkManager = appDeeplinkManager;
            _sessionManager = sessionManager;
            _appSetting = appSetting.Value;
        }

        #endregion LocalVariable & Constructor

        #region GET

        /// <summary>
        /// Generate deeplink of a invoice.
        /// </summary>
        /// <param name="deepLinkRequestModel"></param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        [HttpPut]
        [Route("invoice/generatequickpaylink")]
        public async Task<string> GenerateQuickPaylink([FromBody]AddDeepLinkDTO deepLinkRequestModel, CancellationToken token = default(CancellationToken)) {
            AppDeeplinkPayloadDTO payloadDTO = new AppDeeplinkPayloadDTO();
            payloadDTO.ActionName = "Payment";
            payloadDTO.MaxUseCount = 2;
            UserSession session = _sessionManager.GetSession();
            payloadDTO.UserId = session.TenantId;
            payloadDTO.TenantId = session.TenantId;
            payloadDTO.ActionData = Newtonsoft.Json.JsonConvert.SerializeObject(deepLinkRequestModel);
            string urlKey = await _appDeeplinkManager.GeneratingDeeplink(payloadDTO, true, token);
            if(_appSetting.EnableSubdomain) {
                string subDomainPaymentUrl = URLHelper.GetSubDomainURL(session.Subdomain, _appSetting.PaymentPortalSubDomainBizURL);
                return string.Format("{0}{1}/{2}", subDomainPaymentUrl, "quickpayment", urlKey);
            }
            // Generating the deeplink for quickpayment.
            return string.Format("{0}{1}", _appSetting.QuickPayUrl, urlKey);
        }

        /// <summary>
        /// Get invoice detail by deeplink shorturl.
        /// </summary>
        /// <param name="shortUrlKey"></param>
        /// <param name="machineIPAddress"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("quickpaydetail/{shortUrlKey}/{machineIPAddress}")]
        public async Task<QuickPayInvoiceDetailDTO> GetInvoiceCustomerPaymentDetailAsyncByQuickPaylink(string shortUrlKey, string machineIPAddress, CancellationToken token = default(CancellationToken)) {            
            AppDeeplinkPayloadDTO appDeeplinkPayloadDTO = await _appDeeplinkManager.GetDeeplinkAccessAsync(shortUrlKey, machineIPAddress, token);
            AddDeepLinkDTO addDeepLink = Newtonsoft.Json.JsonConvert.DeserializeObject<AddDeepLinkDTO>(appDeeplinkPayloadDTO.ActionData);
            QuickPayInvoiceDetailDTO detailInfo = new QuickPayInvoiceDetailDTO();
            BAARInvoiceViewDTO InvoiceDTO = await _paymentAndInvoiceDS.GetInvoiceByInvoiceIdAsync(addDeepLink.InvoiceId, token);
            detailInfo.InvoiceDTO = InvoiceDTO;
            detailInfo.PaybleAmount = addDeepLink.PaybleAmount;
            detailInfo.TenantId = InvoiceDTO.TenantId;
           
            return detailInfo;
        }

        #endregion GET

        #region Payment

        /// <summary>
        /// Add muliple invoice payment.
        /// </summary>
        /// <param name="payments">AddPaymentDTO object</param>
        /// <param name="token">Cancellation token for async operations</param>
        // POST api/payment
        [AllowAnonymous]
        [HttpPost]
        [Route("invoice/payment/{shorturl}")]
        public async Task<PaymentDetailDQ> AddMulitplePayment([FromBody] AddPaymentDTO[] payments, [FromRoute]string shorturl, CancellationToken token = default(CancellationToken)) {

            PaymentDetailDQ paymentDTO = await _paymentAndInvoiceDS.AddPaymentsAsync(payments, token);
            if(paymentDTO != null) {
                await _appDeeplinkManager.ExpireDeepLink(shorturl, token);
            }
            //Return new object along with Get URI with status code 201-Created.
            return paymentDTO;
        }

        #endregion Payment

    }
}
