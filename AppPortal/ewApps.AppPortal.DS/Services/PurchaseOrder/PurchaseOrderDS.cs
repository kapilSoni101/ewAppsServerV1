/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 04 Feb 2020
 * 
 * Contributor/s: 
 * Last Updated On: 04 Feb 2020
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
    /// Provide method to add/update/get PurchaseOrder.
    /// </summary>
    public class PurchaseOrderDS: IPurchaseOrderDS {


        #region Local Variables

        IUserSessionManager _userSessionManager;
        AppPortalAppSettings _appSetting;
        INotesDS _notesDS;
        ILogger<PurchaseOrderDS> _log;
        IBusinessDS _businessDS;

        #endregion Local Variables

        #region Constructor

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="userSessionManager"></param>
        /// <param name="appSetting"></param>
        /// <param name="notesDS"></param>
        /// <param name="businessDS"></param>
        /// <param name="log"></param>
        public PurchaseOrderDS( IUserSessionManager userSessionManager,
            IOptions<AppPortalAppSettings> appSetting, INotesDS notesDS, IBusinessDS businessDS, ILogger<PurchaseOrderDS> log) {
           
            _userSessionManager = userSessionManager;
            _appSetting = appSetting.Value;
            _notesDS = notesDS;
            _businessDS = businessDS;
            _log = log;
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get purchase order list by TenantId.
        /// </summary>
        /// <param name="listDateFilterDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BAPurchaseOrderDTO>> GetPurchaseOrderListByBusinessTenantIdAsync(ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            string url = "PurchaseOrder/list";

            List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
            headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, url, listDateFilterDTO, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameterList);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);
            return await httpRequestProcessor.ExecuteAsync<IEnumerable<BAPurchaseOrderDTO>>(requestOptions, false);
            
        }
    /// <summary>
    /// Get purchase order list by TenantId.
    /// </summary>
    /// <param name="listDateFilterDTO"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<BAPurchaseOrderDTO>> GetPurchaseOrderListByBusinessTenantIdAsyncForVend(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken))
    {
      
      string url = string.Format("VendBAPurchaseOrder/list/{0}", businessTenantId);
      List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
      headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, url, listDateFilterDTO, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameterList);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);
      return await httpRequestProcessor.ExecuteAsync<IEnumerable<BAPurchaseOrderDTO>>(requestOptions, false);

    }
    /// <summary>
    /// Get invoice by id.
    /// </summary>
    /// <param name="busTenantId">Business tenantid.</param>
    /// <param name="purchaseorderId">Unique invoice id.</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<BAPurchaseOrderViewDTO> GetPurchaseOrderByPurchaseOrderIdAsync(Guid busTenantId,Guid purchaseorderId, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();            

            string Method = "purchaseorder/"+ busTenantId.ToString() + "/" + purchaseorderId.ToString();
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = userSession.ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);
            BAPurchaseOrderViewDTO bAARInvoiceViewDTO = await httpRequestProcessor.ExecuteAsync<BAPurchaseOrderViewDTO>(requestOptions, false);

            if(bAARInvoiceViewDTO != null) {
                // Get notes list fot he entity.
                bAARInvoiceViewDTO.NotesList = await _notesDS.GetNotesViewListByEntityId(purchaseorderId, userSession.TenantId);
            }

            return bAARInvoiceViewDTO;
        }

        /// <summary>
        /// Get purchase order attachment list.
        /// </summary>
        /// <param name="purchaseOrderId">Unique purchaseOrder id.</param>
        /// <param name="token"></param>
        /// <returns>return attachment list.</returns>
        public async Task<IEnumerable<BAARPurchaseOrderAttachmentDTO>> GetPurchaseOrderAttachmentListByOrderIdAsync(Guid purchaseOrderId, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();

            string Method = "purchaseorder/purchaseorderattachment/" + purchaseOrderId.ToString();
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = userSession.ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
            IEnumerable<BAARPurchaseOrderAttachmentDTO> attachmentList = await httpRequestProcessor.ExecuteAsync<IEnumerable<BAARPurchaseOrderAttachmentDTO>>(requestOptions, false);

            return attachmentList;
        }

        #endregion Get

        #region CRUD

        /// <summary>
        /// Add purchase order.
        /// </summary>
        /// <param name="httpRequest">Contains request object</param>
        /// <param name="request">Contains PruchaseOrder json.</param>
        /// <param name="token">Cancellation token for async operations</param>   
        public async Task<AddPurchaseOrderResponseDTO> AddPurchaseOrderAsync(HttpRequest httpRequest, string request, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "purchaseorder/addpurchaseorder";
            AddBAPurchaseOrderDTO purchaseOrderDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<AddBAPurchaseOrderDTO>(request);
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            //object reqeust = httpRequest.Form.Files;
            KeyValuePair<string, string> hdr2 = new KeyValuePair<string, string>("AddPurchaseOrderModel", request);
            listHeader.Add(hdr2);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);
            RequestOptions requestOptions = null;
            AddPurchaseOrderResponseDTO obj = null;
            if(httpRequest.Form != null && httpRequest.Form.Files != null && httpRequest.Form.Files.Count > 0) {

                using(var mpContent =
                            new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture))) {
                    foreach(FormFile file in httpRequest.Form.Files) {
                        StreamContent stContenant = new StreamContent(file.OpenReadStream());
                        mpContent.Add(stContenant, name: "files", fileName: file.Name);                        
                        mpContent.Headers.Add("ContentType", "application/octet-stream");
                        mpContent.Headers.ContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);                        
                    }
                    requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.MultipartFormData, Method, mpContent, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
                    obj = await httpRequestProcessor.ExecuteAsync<AddPurchaseOrderResponseDTO>(requestOptions, false);
                }
            }
            else {
                //Method = "invoice/add";
                requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.MultipartFormData, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
                obj = await httpRequestProcessor.ExecuteAsync<AddPurchaseOrderResponseDTO>(requestOptions, false);
            }

            try {
                await _notesDS.AddNotesListAsync(purchaseOrderDTO.NotesList, obj.PurchaseOrderId, obj.PurchaseOrderEntityType, token);
            }
            catch(Exception ex) { }
            return obj;
        }

        #endregion CRUD

    }
}
