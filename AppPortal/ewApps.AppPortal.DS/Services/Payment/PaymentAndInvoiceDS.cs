/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 02 Aug 2019
 * 
 * Contributor/s: 
 * Last Updated On: 29 October 2019
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.DMService;
using ewApps.Core.Money;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// A wrapper class contains method to get payment and invoice list.    
    /// </summary>
    public class PaymentAndInvoiceDS:IPaymentAndInvoiceDS {

        #region Local Variables

        IUserSessionManager _userSessionManager;
        AppPortalAppSettings _appSetting;
        IBusinessDS _businessDS;
        IBusCustomerDS _busCustomerDS;
        ICustomerDS _customerDS;
        INotesDS _notesDS;
        IEntityThumbnailDS _thumbnailDS;
        ILogger<PaymentAndInvoiceDS> _log;

        #endregion Local Variables

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PaymentAndInvoiceDS(IBusinessDS businessDS, IBusCustomerDS busCustomerDS, ICustomerDS customerDS, IUserSessionManager userSessionManager,
            IOptions<AppPortalAppSettings> appSetting, INotesDS notesDS, IEntityThumbnailDS thumbnailDS, ILogger<PaymentAndInvoiceDS> log) {
            _businessDS = businessDS;
            _busCustomerDS = busCustomerDS;
            _userSessionManager = userSessionManager;
            _customerDS = customerDS;
            _appSetting = appSetting.Value;
            _notesDS = notesDS;
            _thumbnailDS = thumbnailDS;
            _log = log;
        }

        #endregion Constructor

        #region Add

        #endregion Add

        #region Get Invoice

        /// <summary>
        /// Get invoice list by tenant id. allcancelinvoices
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceDTO>> GetInvoiceListByTenantAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "Invoice/allinvoices";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, filter, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            return await httpRequestProcessor.ExecuteAsync<List<BAARInvoiceDTO>>(requestOptions, false);
        }

        /// <summary>
        /// Get cancel invoice list by tenant id. 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceDTO>> GetCancelInvoiceListByTenantAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "Invoice/allcancelinvoices";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, filter, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            return await httpRequestProcessor.ExecuteAsync<List<BAARInvoiceDTO>>(requestOptions, false);
        }

        /// <summary>
        /// Get customer invoices by customer partner tenantid and filter by date.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns>retun list of invoices.</returns>
        public async Task<List<BAARInvoiceDTO>> GetInvoiceListByCustomerAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "Invoice/customerinvoices";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, filter, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            return await httpRequestProcessor.ExecuteAsync<List<BAARInvoiceDTO>>(requestOptions, false);
        }

        /// <summary>
        /// Get quick pay invoice list.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceDTO>> GetQuickPayInvoicesByTenantAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            string Method = "Invoice/allquickpayinvoices";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, filter, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            return await httpRequestProcessor.ExecuteAsync<List<BAARInvoiceDTO>>(requestOptions, false);
        }

        /// <summary>
        /// Get customer payble invoices list.
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceDTO>> GetCustomerPaybleinvoicesByCustomerIdAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            string Method = "Invoice/paybleinvoices/" + customerId.ToString();
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            return await httpRequestProcessor.ExecuteAsync<List<BAARInvoiceDTO>>(requestOptions, false);
        }

        /// <summary>
        /// Get invoice by id.
        /// </summary>
        /// <param name="invoiceId">Unique invoice id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<BAARInvoiceViewDTO> GetInvoiceByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {

            UserSession userSession = _userSessionManager.GetSession();

            string Method = "Invoice/" + invoiceId.ToString();
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = userSession.ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            BAARInvoiceViewDTO bAARInvoiceViewDTO = await httpRequestProcessor.ExecuteAsync<BAARInvoiceViewDTO>(requestOptions, false);

            if(bAARInvoiceViewDTO != null) {
                // Get notes list fot he entity.
                bAARInvoiceViewDTO.NotesList = await _notesDS.GetNotesViewListByEntityId(invoiceId, userSession.TenantId);
            }

            return bAARInvoiceViewDTO;
        }

        /// <summary>
        /// Get invoice by id.
        /// </summary>
        /// <param name="invoiceId">Unique invoice id.</param>
        /// <param name="erpARInvoiceKey">ERPARInvoiceKey</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<BAARInvoiceViewDTO> GetInvoiceByInvoiceIdAndSyncInvoiceAsync(Guid invoiceId, string erpARInvoiceKey, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();
            Business bus = await _businessDS.GetAsync(tenantId, token);
            if(bus != null && bus.IntegratedMode) {
                //await PullInvoiceFromERPAsync(erpARInvoiceKey, tenantId, token);
            }

            string Method = "Invoice/" + invoiceId.ToString();
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = userSession.ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            BAARInvoiceViewDTO bAARInvoiceViewDTO = await httpRequestProcessor.ExecuteAsync<BAARInvoiceViewDTO>(requestOptions, false);

            if(bAARInvoiceViewDTO != null) {
                // Get notes list fot he entity.
                bAARInvoiceViewDTO.NotesList = await _notesDS.GetNotesViewListByEntityId(invoiceId, userSession.TenantId);
            }

            return bAARInvoiceViewDTO;
        }


        /// <summary>
        /// Get invoices customer and its payment info.
        /// </summary>
        /// <param name="reqDto">invoice list and customerid.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<InvoiceCustomerResponseDTO> GetInvoiceAndCustomerPayInfoByInvoiceIdAndSyncInvoiceAsync(InvoiceCustomerRequestDTO reqDto, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();
            Business bus = await _businessDS.GetBusinessByTenantIdAsync(reqDto.TenantId, token);
            if(bus != null && bus.IntegratedMode) {
                await PullInvoiceFromERPAsync(reqDto, token);
            }
            List<BAARInvoiceDTO> listInvoice = await GetUpdatedARInvoicesAsync(reqDto, token);
            CustomerPaymentInfoDTO payInfo = await _busCustomerDS.GetCustomerAndPaymentInfoAsync(reqDto.CustomerId, reqDto.AppId, true);
            InvoiceCustomerResponseDTO responseDTO = new InvoiceCustomerResponseDTO();
            responseDTO.listInvoice = listInvoice;
            responseDTO.customerPaymentInfoDTO = payInfo;

            return responseDTO;
        }

        #endregion Get Invoice

        #region Get     

        /// <summary>
        /// Get invoice attachment list.
        /// </summary>
        /// <param name="invoiceId">Unique invoiceId id.</param>
        /// <param name="token"></param>
        /// <returns>return attachment list.</returns>
        public async Task<object> GetInvoiceAttachmentListByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = string.Format("invoice/invoiceattachment/{0}", invoiceId.ToString());
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);
            return await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);
        }

        /// <summary>
        /// Gets Payment DTO given its  id and inventoryid.
        /// </summary>
        /// <param name="id">  Id, unique key</param>
        /// <param name="invoiceId">  invoiceId</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        public async Task<PaymentDetailModelDTO> GetPaymentDTOAsync(Guid id, Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = string.Format("payment/invoice/paymentdetail/{0}/{1}", id.ToString(), invoiceId.ToString());
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            return await httpRequestProcessor.ExecuteAsync<PaymentDetailModelDTO>(requestOptions, false);
        }

        /// <summary>
        /// Get payment detail by paymentid.
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public async Task<PaymentDetailModelDTO> GetPaymentDTOAsyncByPaymentIdAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            //return await _paInvoiceDS.GetPaymentDTOAsyncByPaymentIdAsync(paymentId, token);
            // Preparing api calling process model.           
            string Method = string.Format("payment/paymentdetail/{0}", paymentId.ToString());
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            UserSession session = _userSessionManager.GetSession();
            if(session != null) {
                string clientsessionid = _userSessionManager.GetSession().ID.ToString();
                KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                listHeader.Add(hdr);
            }
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            return await httpRequestProcessor.ExecuteAsync<PaymentDetailModelDTO>(requestOptions, false);
        }

        /// <summary>
        /// Gets advance Payments detail by given Payment id.
        /// </summary>
        /// <param name="paymentId">  Id, unique key</param>        
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>        
        //[Route("advancepaymentdetail/{paymentId}")]
        public async Task<AdvancePaymentDetailModelDTO> GetAdvancePaymentDTOAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            //return await _paInvoiceDS.GetPaymentDTOAsyncByPaymentIdAsync(paymentId, token);
            // Preparing api calling process model.           
            string Method = string.Format("payment/advancepaymentdetail/{0}", paymentId.ToString());
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            UserSession session = _userSessionManager.GetSession();
            if(session != null) {
                string clientsessionid = _userSessionManager.GetSession().ID.ToString();
                KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                listHeader.Add(hdr);
            }
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            return await httpRequestProcessor.ExecuteAsync<AdvancePaymentDetailModelDTO>(requestOptions, false);
        }

        /// <summary>
        /// Get the list of invoices paid in a single transection(payment).
        /// </summary>  
        /// <param name="paymentId">PaymentId</param>
        /// <param name="token"></param>
        public async Task<List<InvoiceInfoDTO>> GetInvoicesByPaymentIdAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = string.Format("payment/invoicelist/{0}", paymentId.ToString());
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            UserSession session = _userSessionManager.GetSession();
            if(session != null) {
                string clientsessionid = _userSessionManager.GetSession().ID.ToString();
                KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                listHeader.Add(hdr);
            }
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            return await httpRequestProcessor.ExecuteAsync<List<InvoiceInfoDTO>>(requestOptions, false);
        }

        /// <summary>
        /// Gets Payment history by tenant and from/todate for a invoice by partnerid.
        /// </summary>
        /// <param name="filter">filter for getting tenant payment list.</param>
        /// <param name="token">For cancellation</param>
        /// <returns>IList of Payment entities</returns>
        public async Task<List<PaymentDetailDTO>> GetFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "payment/invoice/tenantpaymenthistory";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            UserSession session = _userSessionManager.GetSession();
            if(session != null) {
                string clientsessionid = _userSessionManager.GetSession().ID.ToString();
                KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                listHeader.Add(hdr);
            }
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, filter, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            return await httpRequestProcessor.ExecuteAsync<List<PaymentDetailDTO>>(requestOptions, false);
        }

        /// <summary>
        /// Gets Payment history by invoice.
        /// </summary>
        /// <param name="invoiceId">filter by invoice.</param>
        /// <param name="token">For cancellation</param>
        /// <returns>IList of Payment entities</returns>
        public async Task<List<PaymentDetailDTO>> GetPaymentHistoryByInvoiceAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = string.Format("payment/invoice/invoicepaymenthistory/{0}", invoiceId.ToString());
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            UserSession session = _userSessionManager.GetSession();
            if(session != null) {
                string clientsessionid = _userSessionManager.GetSession().ID.ToString();
                KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                listHeader.Add(hdr);
            }
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            return await httpRequestProcessor.ExecuteAsync<List<PaymentDetailDTO>>(requestOptions, false);
        }

        /// <summary>
        /// Gets Payment history by tenant and from/todate for a invoice by partnerid.
        /// </summary>
        /// <param name="filter">filter for getting tenant payment list.</param>
        /// <param name="token">For cancellation</param>
        /// <returns>IList of Payment entities</returns>
        public async Task<List<PaymentDetailDTO>> GetCustomerFilterPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "payment/invoice/customerpaymenthistory";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            UserSession session = _userSessionManager.GetSession();
            if(session != null) {
                string clientsessionid = _userSessionManager.GetSession().ID.ToString();
                KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                listHeader.Add(hdr);
            }
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, filter, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            return await httpRequestProcessor.ExecuteAsync<List<PaymentDetailDTO>>(requestOptions, false);
        }

        /// <summary>
        /// Gets Payment history by tenant and from/todate for a invoice by partnerid.
        /// </summary>
        /// <param name="filter">filter for getting tenant payment list.</param>
        /// <param name="token">For cancellation</param>
        /// <returns>IList of Payment entities</returns>
        public async Task<List<PaymentDetailDTO>> GetVoidPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "payment/invoice/voidpaymenthistory";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            UserSession session = _userSessionManager.GetSession();
            if(session != null) {
                string clientsessionid = _userSessionManager.GetSession().ID.ToString();
                KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                listHeader.Add(hdr);
            }
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, filter, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            return await httpRequestProcessor.ExecuteAsync<List<PaymentDetailDTO>>(requestOptions, false);
        }

        /// <summary>
        /// Gets Payment history by tenant and from/todate for a invoice by partnerid.
        /// </summary>
        /// <param name="filter">filter for getting tenant payment list.</param>
        /// <param name="token">For cancellation</param>
        /// <returns>IList of Payment entities</returns>
        public async Task<List<PaymentDetailDTO>> GetSattledFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "payment/invoice/sattledpaymenthistory";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            UserSession session = _userSessionManager.GetSession();
            if(session != null) {
                string clientsessionid = _userSessionManager.GetSession().ID.ToString();
                KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                listHeader.Add(hdr);
            }
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, filter, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            return await httpRequestProcessor.ExecuteAsync<List<PaymentDetailDTO>>(requestOptions, false);
        }


        /// <summary>
        /// Get payment transection status flow.
        /// </summary>
        /// <param name="paymentId">Unique paymentId</param>
        /// <param name="paymentMode">Mode of payment Vericheck or card</param>
        /// <param name="token"></param>
        /// <returns>retun payment status.</returns>
        public async Task<List<PaymentTransectionStatusDTO>> GetPaymentStatusFlowListAsync(Guid paymentId, string paymentMode, CancellationToken token = default(CancellationToken)) {
            string Method = string.Format("payment/allstatus/{0}/{1}", paymentId.ToString(), paymentMode);
            try {
                UserSession session = _userSessionManager.GetSession();
                string clientsessionid = session == null ? "" : session.ID.ToString();
                List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
                KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                listHeader.Add(hdr);
                RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
                ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
                return await httpRequestProcessor.ExecuteAsync<List<PaymentTransectionStatusDTO>>(requestOptions, false);

            }
            catch(Exception ex) {
                return new List<PaymentTransectionStatusDTO>();
            }
        }

        #endregion Get

        #region Get PreAuth payment

        /// <summary>
        /// Get preauthorized payment history.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IList<PreAuthPaymentDetailDTO>> GetFilterTenantPreAuthPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "preauthpayment/authpayhistory";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            UserSession session = _userSessionManager.GetSession();
            if(session != null) {
                string clientsessionid = _userSessionManager.GetSession().ID.ToString();
                KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                listHeader.Add(hdr);
            }
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, filter, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            return await httpRequestProcessor.ExecuteAsync<List<PreAuthPaymentDetailDTO>>(requestOptions, false);
        }

        #endregion Get PreAuth Payment

        #region Get Advance Payment


        /// <summary>
        /// Get advance payment list by tenantid.
        /// </summary>
        /// <param name="filter">Filter object contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        //[Route("advance/tenantpaymenthistory")]
        public async Task<IList<PaymentDetailDTO>> GetFilterTenantAdvancePaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "payment/advance/tenantpaymenthistory";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            UserSession session = _userSessionManager.GetSession();
            if(session != null) {
                string clientsessionid = _userSessionManager.GetSession().ID.ToString();
                KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                listHeader.Add(hdr);
            }
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, filter, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            return await httpRequestProcessor.ExecuteAsync<List<PaymentDetailDTO>>(requestOptions, false);
        }

        /// <summary>
        /// Get customer advance payment list by partnerid.
        /// </summary>
        /// <returns></returns>        
        public async Task<IList<PaymentDetailDTO>> GetCustomerAdvanceFilterPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "payment/advance/customerpaymenthistory";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            UserSession session = _userSessionManager.GetSession();
            if(session != null) {
                string clientsessionid = _userSessionManager.GetSession().ID.ToString();
                KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                listHeader.Add(hdr);
            }
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, filter, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            return await httpRequestProcessor.ExecuteAsync<List<PaymentDetailDTO>>(requestOptions, false);
        }

        #endregion Get Advance Payment

        #region Make Payment

        /// <summary>
        /// Get invoice list by tenant id.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceDTO>> AddPaymentAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "Invoice/allinvoices";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, filter, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            return await httpRequestProcessor.ExecuteAsync<List<BAARInvoiceDTO>>(requestOptions, false);
        }

        #endregion Make Payment

        #region Pre Auth Payment

        /// <summary>
        /// Authorize/Block Amount from Credit card for future use.
        /// </summary>
        /// <param name="payments">authorized payment object</param>
        /// <param name="token">Cancellation token for async operations</param>   
        public async Task<object> AuthorizeCardPaymentAsync(AddPreAuthPaymentDTO payments, CancellationToken token = default(CancellationToken)) {
            // saving the card before authrorizing amount.
            await SavePreAuthAccountDetailAsync(payments, token);
            // Preparing api calling process model.           
            string Method = "preauthpayment/authorization/payment";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, payments, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            object obj = await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);

            return obj;
        }

        /// <summary>
        /// UnAuthorize/UnBlock Amount from Credit card for future use.
        /// </summary>
        /// <param name="preAuthPaymentId"></param>        
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task VoidPreAuthPaymentAsync(Guid preAuthPaymentId, CancellationToken token = default(CancellationToken)) {
            string Method = "PreAuthPayment/void/" + preAuthPaymentId.ToString();
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);
        }

        #endregion Pre Auth Payment

        #region Quick Payment

        /// <summary>
        /// Generate deeplink of a invoice.
        /// </summary>
        /// <param name="deepLinkRequestModel"></param>
        /// <param name="token">Cancellation token</param>
        /// <returns>return link url.</returns>
        public async Task<string> GenerateQuickPaylink(AddDeepLinkDTO deepLinkRequestModel, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "QuickPayment/invoice/generatequickpaylink";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, deepLinkRequestModel, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            return await httpRequestProcessor.ExecuteAsync<string>(requestOptions, false);
        }

        /// <summary>
        /// Get invoice detail by deeplink shorturl.
        /// </summary>
        /// <param name="shortUrlKey"></param>
        /// <param name="machineIPAddress"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<QuickPayInvoiceDetailDTO> GetInvoiceCustomerPaymentDetailAsyncByQuickPaylink(string shortUrlKey, string machineIPAddress, CancellationToken token = default(CancellationToken)) {
            string Method = string.Format("QuickPayment/quickpaydetail/{0}/{1}", shortUrlKey, machineIPAddress);
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            UserSession session = _userSessionManager.GetSession();
            if(session != null) {
                string clientsessionid = _userSessionManager.GetSession().ID.ToString();
                KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                listHeader.Add(hdr);
            }
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            QuickPayInvoiceDetailDTO qPayDtl = await httpRequestProcessor.ExecuteAsync<QuickPayInvoiceDetailDTO>(requestOptions, false);
            if(qPayDtl != null) {
                qPayDtl.CustomerDTO = await _busCustomerDS.GetCustomerAndPaymentAppKeyInfoAsync(qPayDtl.InvoiceDTO.CustomerId, "pay", true);
                Entity.Business bus = await _businessDS.GetBusinessByTenantIdAsync(qPayDtl.TenantId, token);
                if(bus != null) {
                    qPayDtl.TenantName = bus.Name;
                    qPayDtl.Language = bus.Language;
                    qPayDtl.TimeZone = bus.TimeZone;
                    qPayDtl.DateTimeFormat = bus.DateTimeFormat;
                    ThumbnailAddAndUpdateDTO th = _thumbnailDS.GetThumbnailInfoByOwnerEntityId(bus.TenantId);
                    if(th != null) {
                        qPayDtl.TenantLogUrl = th.ThumbnailUrl;
                    }
                    CurrencyCultureInfoTable tbl = new CurrencyCultureInfoTable(null);
                    tbl.GetCultureInfo((CurrencyISOCode)bus.CurrencyCode);
                    CurrencyCultureInfo culInfo = tbl.GetCultureInfo((CurrencyISOCode)bus.CurrencyCode);
                    if(culInfo != null)
                        qPayDtl.Currency = culInfo.Symbol;
                }
            }
            return qPayDtl;
        }

        /// <summary>
        /// Add muliple invoice payment.
        /// </summary>
        /// <param name="payments">AddPaymentDTO object</param>
        /// <param name="shorturl">a key generated for making payment.</param>
        /// <param name="token">Cancellation token for async operations</param>   
        public async Task<object> QuickPaymentsAsync(AddPaymentDTO[] payments, string shorturl, CancellationToken token = default(CancellationToken)) {
            if(payments.Length > 0) {
                _log.LogInformation("QuickPayment initiated for invoiceid " + payments[0].InvoiceId.ToString(), null);
            }
            // Preparing api calling process model.           
            string Method = string.Format("QuickPayment/invoice/payment/{0}", shorturl);
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            UserSession session = _userSessionManager.GetSession();
            if(session != null) {
                string clientsessionid = _userSessionManager.GetSession().ID.ToString();
                KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                listHeader.Add(hdr);
            }

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, payments, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            object payHis = await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);
            try {
                PaymentDetailDQ obj = payHis as PaymentDetailDQ;
                if(obj != null) {

                    if(obj.IntegratedMode) {
                        await PullInvoiceFromERPAsync(payments, token);
                    }

                    if(payments.Length > 0) {
                        string formatLog = string.Format("QuickPayment initiated success for invoiceid {0} and Paymentid {1} ", payments[0].InvoiceId.ToString(), obj.PaymentId.ToString());
                        _log.LogInformation("QuickPayment initiated success for invoiceid " + payments[0].InvoiceId.ToString(), null);
                    }
                }

            }
            catch(Exception) { }

            return payHis;
        }

        #endregion Quick Payment

        #region Add Payment

        /// <summary>
        /// Add muliple invoice payment.
        /// </summary>
        /// <param name="payments">AddPaymentDTO object</param>
        /// <param name="token">Cancellation token for async operations</param>   
        public async Task<object> AddPaymentsAsync(AddPaymentDTO[] payments, CancellationToken token = default(CancellationToken)) {
            if(payments.Length > 0) {
                _log.LogInformation("Payment initiated for invoiceid " + payments[0].InvoiceId.ToString(), null);
            }

            // Preparing api calling process model.           
            string Method = "payment/invoice/payment";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, payments, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            PaymentDetailDQ obj = await httpRequestProcessor.ExecuteAsync<PaymentDetailDQ>(requestOptions, false);
            //_customerDS.AddCustomerAccountDetail()

            try {
                await SaveAccountDetailAsync(payments, token);
                if(obj.IntegratedMode) {
                    await PullInvoiceFromERPAsync(payments, token);
                }

                if(payments.Length > 0) {
                    string formatLog = string.Format("Payment initiated success for invoiceid {0} and Paymentid {1} ", payments[0].InvoiceId.ToString(), obj.PaymentId.ToString());
                    _log.LogInformation("Payment initiated success for invoiceid " + payments[0].InvoiceId.ToString(), null);
                }
            }
            catch(Exception e) {
            }

            return obj;
        }

        /// <summary>
        /// Pull invoice data after payment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<object> PullInvoiceFromERPAsync(AddPaymentDTO[] e, CancellationToken token = default(CancellationToken)) {
           
            string Method = "BASync/pullerpdata";
            //List<PullERPDataReqDTO> listPullERPDataReqDTO = new List<PullERPDataReqDTO>();
            PullERPDataReqDTO reqDto = new PullERPDataReqDTO();
            int busInvoiceEntityEnum = 6;
            reqDto.SelectedBAEntityTypes = new List<int>() { busInvoiceEntityEnum };
            reqDto.ERPEntityKey = new List<string>();
            for(int i = 0; i < e.Length; i++) {
                
                reqDto.ERPEntityKey.Add( e[i].ERPARInvoiceKey);
                reqDto.TenantId = e[0].TenantId;
                //listPullERPDataReqDTO.Add(reqDto);
          
            }

            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            UserSession uSession = _userSessionManager.GetSession();
            if(uSession != null) {
                string clientsessionid = uSession.ID.ToString();
                KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                listHeader.Add(hdr);
            }
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, reqDto, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);
            await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);

            return null;
        }

        /// <summary>
        /// Pull invoice before payment.
        /// </summary>
        /// <param name="erpARInvoiceKey"></param>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task PullInvoiceFromERPAsync(InvoiceCustomerRequestDTO requestDTO, CancellationToken token = default(CancellationToken)) {
            try {
                //List<PullERPDataReqDTO> listPullERPDataReqDTO = new List<PullERPDataReqDTO>();
                string Method = "BASync/pullerpdata";
                PullERPDataReqDTO reqDto = new PullERPDataReqDTO();
                int busInvoiceEntityEnum = 6;
                reqDto.SelectedBAEntityTypes = new List<int>();
                reqDto.ERPEntityKey = new List<string>();
                reqDto.SelectedBAEntityTypes = new List<int>() { busInvoiceEntityEnum };
                for(int i = 0; i < requestDTO.listInvoiceId.Count; i++) {                    
                    reqDto.ERPEntityKey.Add(requestDTO.erpARInvoiceKeys[i]);
                    reqDto.TenantId = requestDTO.TenantId;
                }
                
                List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
                UserSession uSession = _userSessionManager.GetSession();
                if(uSession != null) {
                    string clientsessionid = uSession.ID.ToString();
                    KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                    listHeader.Add(hdr);
                }
                RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, reqDto, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
                ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);
                await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);
            }
            catch (Exception ex) {
                _log.LogError(ex,"Error in getting invoice from ERP "  , null);
            }
        }

        /// <summary>
        /// Pull invoice before payment.
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<List<BAARInvoiceDTO>> GetUpdatedARInvoicesAsync(InvoiceCustomerRequestDTO requestDTO, CancellationToken token = default(CancellationToken)) {
            try {
                string Method = "invoice/getbaarinvoicesbyids";
                List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
                UserSession uSession = _userSessionManager.GetSession();
                if(uSession != null) {
                    string clientsessionid = uSession.ID.ToString();
                    KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                    listHeader.Add(hdr);
                }
                RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, requestDTO.listInvoiceId, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
                ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
                return await httpRequestProcessor.ExecuteAsync<List<BAARInvoiceDTO>>(requestOptions, false);
            }
            catch(Exception ex) {
                _log.LogError(ex, "Error in getting invoice from ERP ", null);
            }

            return null;
        }


        /// <summary>
        /// Pay advance payment.
        /// </summary>
        /// <param name="payment">AddPaymentDTO object</param>
        /// <param name="token">Cancellation token for async operations</param>   
        public async Task<object> AddAdvancePaymentsAsync(AddPaymentDTO payment, CancellationToken token = default(CancellationToken)) {

            if(payment.userPaymentInfoModel != null) {
                _log.LogInformation("Advance Payment initiated for customer " + payment.userPaymentInfoModel.CustomerId.ToString(), null);
            }
            // Preparing api calling process model.           
            string Method = "payment/invoice/advancepayment";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, payment, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            object obj = await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);
            //_customerDS.AddCustomerAccountDetail()

            try {
                await SaveAccountDetailAsync(new AddPaymentDTO[] { payment }, token);
                if(payment.userPaymentInfoModel != null) {
                    _log.LogInformation("Advance payment initiated sucess for customer " + payment.userPaymentInfoModel.CustomerId.ToString(), null);
                }
            }
            catch(Exception e) {
            }

            return obj;
        }

        /// <summary>
        /// Save customer account info.
        /// </summary>
        /// <param name="payments"></param>
        /// <returns></returns>
        private async Task SaveAccountDetailAsync(AddPaymentDTO[] payments, CancellationToken token = default(CancellationToken)) {
            if(payments.Length > 0 && payments[0].SaveAccountInfo) {
                UserPaymentInfoModel payInfoModel = payments[0].userPaymentInfoModel;
                CustomeAccDetailDTO customeAccDetail = new CustomeAccDetailDTO();
                if(payInfoModel.selectedVCCreditCardPayAttr != null) {
                    if(payInfoModel.selectedVCCreditCardPayAttr.ID != Guid.Empty || !string.IsNullOrEmpty(payInfoModel.selectedVCCreditCardPayAttr.Token)) {
                        return;
                    }
                    customeAccDetail.BankAcctDetailList = new List<BankAcctDetailDTO>();
                    customeAccDetail.CreditCardDetailList = new List<CreditCardDetailDTO>();
                    CreditCardDetailDTO card = new CreditCardDetailDTO();
                    card.CardNumber = payInfoModel.selectedVCCreditCardPayAttr.CardNumber;
                    card.CardType = payInfoModel.selectedVCCreditCardPayAttr.CardType;
                    card.ExpiryMonth = Convert.ToInt16(payInfoModel.selectedVCCreditCardPayAttr.ExpiryMonth);
                    card.ExpiryYear = Convert.ToInt16(payInfoModel.selectedVCCreditCardPayAttr.ExpiryYear);
                    card.NameOnCard = payInfoModel.selectedVCCreditCardPayAttr.NameOnCard;

                    customeAccDetail.CreditCardDetailList.Add(card);
                }
                if(payInfoModel.SelectedCustVCACHPayAttr != null) {
                    if(payInfoModel.SelectedCustVCACHPayAttr.ID != Guid.Empty) {
                        return;
                    }
                    customeAccDetail.BankAcctDetailList = new List<BankAcctDetailDTO>();
                    customeAccDetail.CreditCardDetailList = new List<CreditCardDetailDTO>();

                    BankAcctDetailDTO bank = new BankAcctDetailDTO();
                    bank.ABARounting = payInfoModel.SelectedCustVCACHPayAttr.ABARounting;
                    bank.AccountNo = payInfoModel.SelectedCustVCACHPayAttr.AccountNo;
                    bank.BankName = payInfoModel.SelectedCustVCACHPayAttr.BankName;
                    bank.NameInBank = payInfoModel.SelectedCustVCACHPayAttr.NameInBank;
                    bank.AccountType = payInfoModel.SelectedCustVCACHPayAttr.AccountType;
                    customeAccDetail.BankAcctDetailList.Add(bank);
                }

                await _customerDS.AddCustomerAccountDetail(payInfoModel.BACustomerId, customeAccDetail, token);
            }
        }

        /// <summary>
        /// Save customer account info.
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        private async Task SavePreAuthAccountDetailAsync(AddPreAuthPaymentDTO payment, CancellationToken token = default(CancellationToken)) {
            if(payment != null && payment.SaveAccountInfo) {
                UserPreAuthPaymentInfoModel payInfoModel = payment.userPaymentInfoModel;
                CustomeAccDetailDTO customeAccDetail = new CustomeAccDetailDTO();
                if(payInfoModel.selectedVCCreditCardPayAttr != null) {
                    if(payInfoModel.selectedVCCreditCardPayAttr.ID != Guid.Empty) {
                        return;
                    }
                    customeAccDetail.BankAcctDetailList = new List<BankAcctDetailDTO>();
                    customeAccDetail.CreditCardDetailList = new List<CreditCardDetailDTO>();
                    CreditCardDetailDTO card = new CreditCardDetailDTO();
                    card.CardNumber = payInfoModel.selectedVCCreditCardPayAttr.CardNumber;
                    card.CardType = payInfoModel.selectedVCCreditCardPayAttr.CardType;
                    card.ExpiryMonth = Convert.ToInt16(payInfoModel.selectedVCCreditCardPayAttr.ExpiryMonth);
                    card.ExpiryYear = Convert.ToInt16(payInfoModel.selectedVCCreditCardPayAttr.ExpiryYear);
                    card.NameOnCard = payInfoModel.selectedVCCreditCardPayAttr.NameOnCard;
                    card.Token = payInfoModel.selectedVCCreditCardPayAttr.Token;

                    customeAccDetail.CreditCardDetailList.Add(card);
                }

                await _customerDS.AddCustomerAccountDetail(payInfoModel.BACustomerId, customeAccDetail, token);
                if(customeAccDetail.CreditCardDetailList.Count > 0) {
                    // Assigning the id to use for saving the account id with pre auth.
                    payInfoModel.selectedVCCreditCardPayAttr.ID = customeAccDetail.CreditCardDetailList[0].ID;
                }
            }
        }

        #endregion Add Payment

        #region  AddInvoice

        /// <summary>
        /// Add invoice.
        /// </summary>
        /// <param name="invoiceDTO">Add invoice object</param>
        /// <param name="token">Cancellation token for async operations</param>   
        public async Task<InvoiceResponseDTO> AddInvoiceAsync(AddBAARInvoiceDTO invoiceDTO, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "invoice/add";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, invoiceDTO, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);
            InvoiceResponseDTO obj = await httpRequestProcessor.ExecuteAsync<InvoiceResponseDTO>(requestOptions, false);
            try {
                await _notesDS.AddNotesListAsync(invoiceDTO.NotesList, obj.InvoiceId, obj.InvoiceEntityType, token);
            }
            catch(Exception ex) { }
            return obj;
        }

        /// <summary>
        /// Add invoice.
        /// </summary>
        /// <param name="invoiceDTO">Add invoice object</param>
        /// <param name="token">Cancellation token for async operations</param>   
        public async Task<InvoiceResponseDTO> AddInvoiceAsync(HttpRequest httpRequest, string request, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "invoice/addinvoice";
            AddBAARInvoiceDTO invoiceDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<AddBAARInvoiceDTO>(request);
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            object reqeust = httpRequest.Form.Files;
            KeyValuePair<string, string> hdr2 = new KeyValuePair<string, string>("AddUpdateInvoiceModel", request);
            listHeader.Add(hdr2);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);
            RequestOptions requestOptions = null;
            InvoiceResponseDTO obj = null;
            if(httpRequest.Form != null && httpRequest.Form.Files != null && httpRequest.Form.Files.Count > 0) {

                using(var mpContent =
                            new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture))) {
                    foreach(FormFile file in httpRequest.Form.Files) {
                        StreamContent stContenant = new StreamContent(file.OpenReadStream());
                        mpContent.Add(stContenant, name: "files", fileName: file.Name);

                        //mpContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data;boundary:"+ "----" + DateTime.Now.ToString());
                        mpContent.Headers.Add("ContentType", "application/octet-stream");
                        mpContent.Headers.ContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                        //MediaTypeHeaderValue.Parse("multipart/form-data");
                        //mpContent.Headers.Add("boundary", "----" +  DateTime.Now.ToString());
                    }
                    requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.MultipartFormData, Method, mpContent, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
                    obj = await httpRequestProcessor.ExecuteAsync<InvoiceResponseDTO>(requestOptions, false);
                }
            }
            else {
                Method = "invoice/add";
                requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, invoiceDTO, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
                obj = await httpRequestProcessor.ExecuteAsync<InvoiceResponseDTO>(requestOptions, false);
            }

            try {
                await _notesDS.AddNotesListAsync(invoiceDTO.NotesList, obj.InvoiceId, obj.InvoiceEntityType, token);
            }
            catch(Exception ex) { }
            return obj;
        }

        /// <summary>
        /// Cancel invoice.
        /// </summary>
        /// <param name="invoiceId">invoice id</param>
        /// <param name="token">Cancellation token for async operations</param>   
        public async Task<object> CancelInvoiceAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "invoice/cancelinvoice/" + invoiceId.ToString();
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);
            object obj = await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);
            return obj;
        }

        #endregion AddInvoice

        #region Payment Status

        /// <summary>
        /// Refund payment transaction.
        /// </summary>
        /// <param name="paymentId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task RefundPaymentAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            string Method = "payment/refund/" + paymentId.ToString();
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);
        }

        /// <summary>
        /// Void payment transaction.
        /// </summary>
        /// <param name="paymentId"></param>
        /// <param name="serviceName">Name of service to pay invoice.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task VoidPaymentAsync(Guid paymentId, string serviceName, CancellationToken token = default(CancellationToken)) {
            string Method = "payment/void/" + paymentId.ToString() + "/" + serviceName;
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);
        }

        #endregion Payment Status

        #region Delete

        /// <summary>
        /// delete invoice attachment.
        /// </summary>
        /// <param name="invoiceId">invoiceId</param>
        /// <param name="documentId">Unique document id.</param>
        /// <param name="token"></param>
        /// <returns>return attachment list.</returns>
        public async Task<object> DeleteInvoiceAttachmentByDocumentIdAsync(Guid invoiceId, Guid documentId, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = string.Format("invoice/deleteattachment/{0}/{1}", invoiceId.ToString(), documentId.ToString());
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);
            return await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);
        }

        #endregion Delete

    }
}
