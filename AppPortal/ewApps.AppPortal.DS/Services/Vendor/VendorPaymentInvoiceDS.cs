/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 11 February 2020
 * 
 * Contributor/s: 
 * Last Updated On: 11 February 2020
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
    /// A wrapper class contains method to get payment and vendor invoice list.    
    /// </summary>
    public class VendorPaymentInvoiceDS: IVendorPaymentInvoiceDS {

        #region Local Variables

        IUserSessionManager _userSessionManager;
        AppPortalAppSettings _appSetting;
        IBusinessDS _businessDS;
        INotesDS _notesDS;
        IEntityThumbnailDS _thumbnailDS;
        ILogger<VendorPaymentInvoiceDS> _log;

        #endregion Local Variables

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public VendorPaymentInvoiceDS(IBusinessDS businessDS,  IUserSessionManager userSessionManager,
            IOptions<AppPortalAppSettings> appSetting, INotesDS notesDS, IEntityThumbnailDS thumbnailDS, ILogger<VendorPaymentInvoiceDS> log) {
            _businessDS = businessDS;
            _userSessionManager = userSessionManager;
            _appSetting = appSetting.Value;
            _notesDS = notesDS;
            _thumbnailDS = thumbnailDS;
            _log = log;
        }

        #endregion Constructor

        #region Get Invoice

        /// <summary>
        /// Get invoice list by tenant id. allcancelinvoices
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<VendorAPInvoiceDTO>> GetInvoiceListByTenantAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "vendorInvoice/allinvoices";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, filter, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            return await httpRequestProcessor.ExecuteAsync<List<VendorAPInvoiceDTO>>(requestOptions, false);
        }

        /// <summary>
        /// Get invoice by id.
        /// </summary>
        /// <param name="invoiceId">Unique invoice id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<VendorAPInvoiceViewDTO> GetInvoiceByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {

            UserSession userSession = _userSessionManager.GetSession();

            string Method = "vendorInvoice/" + invoiceId.ToString();
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = userSession.ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            VendorAPInvoiceViewDTO bAARInvoiceViewDTO = await httpRequestProcessor.ExecuteAsync<VendorAPInvoiceViewDTO>(requestOptions, false);

            if(bAARInvoiceViewDTO != null) {
                // Get notes list fot he entity.
                bAARInvoiceViewDTO.NotesList = await _notesDS.GetNotesViewListByEntityId(invoiceId, userSession.TenantId);
            }

            return bAARInvoiceViewDTO;
        }
    /// <summary>
    /// Get customer invoices by customer partner tenantid and filter by date.
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="token"></param>
    /// <returns>retun list of invoices.</returns>
    public async Task<List<VendorAPInvoiceDTO>> GetInvoiceListByVendorAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken))
    {
      // Preparing api calling process model.           
      string Method = "vendorInvoice/vendorinvoices";
      List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
      string clientsessionid = _userSessionManager.GetSession().ID.ToString();
      KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
      listHeader.Add(hdr);
      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, filter, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
      ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
      return await httpRequestProcessor.ExecuteAsync<List<VendorAPInvoiceDTO>>(requestOptions, false);
    }
    /// <summary>
    /// Get invoice attachment list.
    /// </summary>
    /// <param name="invoiceId">Unique invoiceId id.</param>
    /// <param name="token"></param>
    /// <returns>return attachment list.</returns>
    public async Task<object> GetInvoiceAttachmentListByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = string.Format("vendorInvoice/invoiceattachment/{0}", invoiceId.ToString());
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);
            return await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);
        }

        /// <summary>
        /// Gets Payment history by invoice.
        /// </summary>
        /// <param name="invoiceId">filter by invoice.</param>
        /// <param name="token">For cancellation</param>
        /// <returns>IList of Payment entities</returns>
        public async Task<List<VendorPaymentDetailDTO>> GetPaymentHistoryByInvoiceAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = string.Format("VendorPayment/invoice/invoicepaymenthistory/{0}", invoiceId.ToString());
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            UserSession session = _userSessionManager.GetSession();
            if(session != null) {
                string clientsessionid = _userSessionManager.GetSession().ID.ToString();
                KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                listHeader.Add(hdr);
            }
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            return await httpRequestProcessor.ExecuteAsync<List<VendorPaymentDetailDTO>>(requestOptions, false);
        }

        #endregion Get Invoice

    }
}
