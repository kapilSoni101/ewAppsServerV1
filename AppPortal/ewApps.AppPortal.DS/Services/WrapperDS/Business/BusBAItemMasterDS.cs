using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {

    public class BusBAItemMasterDS:IBusBAItemMasterDS {

        #region Local Members

        AppPortalAppSettings _appSettings;
        IUserSessionManager _userSessionManager;

        #endregion Local Members 

        #region Constructor

        public BusBAItemMasterDS(IOptions<AppPortalAppSettings> appSettings, IUserSessionManager userSessionManager) {
            _appSettings = appSettings.Value;
            _userSessionManager = userSessionManager;
        }

        #endregion Constructor

        #region Customer Get

        public async Task<IEnumerable<BusBAItemMasterDTO>> GetItemMasterListByBusTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            string baseUrl = _appSettings.BusinessEntityApiUrl;
            string reqeustMethod = "busbaitemmaster/list/" + tenantId;

            UserSession session = _userSessionManager.GetSession();

            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                                     RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
            IEnumerable<BusBAItemMasterDTO> itemMasterList = await httpRequestProcessor.ExecuteAsync<IEnumerable<BusBAItemMasterDTO>>(requestOptions, false);

            for(int i = 0; i < itemMasterList.Count(); i++) {
                //itemMasterList.ElementAt(i).Size = itemMasterList.ElementAt(i).SalesLength + " " + itemMasterList.ElementAt(i).SalesLengthUnitText + " x " + itemMasterList.ElementAt(i).SalesWidth + " " + itemMasterList.ElementAt(i).SalesWidthUnitText + " x " + itemMasterList.ElementAt(i).SalesHeight + " " + itemMasterList.ElementAt(i).SalesHeightUnitText;
                //itemMasterList.ElementAt(i).Size = Math.Truncate(itemMasterList.ElementAt(i).SalesLength) + " " + itemMasterList.ElementAt(i).SalesLengthUnitText + " x " + Math.Truncate(itemMasterList.ElementAt(i).SalesWidth) + " " + itemMasterList.ElementAt(i).SalesWidthUnitText + " x " + Math.Truncate(itemMasterList.ElementAt(i).SalesHeight) + " " + itemMasterList.ElementAt(i).SalesHeightUnitText;
                itemMasterList.ElementAt(i).Size = String.Format("{0:0.00}", itemMasterList.ElementAt(i).SalesLength) + " " + itemMasterList.ElementAt(i).SalesLengthUnitText + " x " + String.Format("{0:0.00}", itemMasterList.ElementAt(i).SalesWidth) + " " + itemMasterList.ElementAt(i).SalesWidthUnitText + " x " + String.Format("{0:0.00}", itemMasterList.ElementAt(i).SalesHeight) + " " + itemMasterList.ElementAt(i).SalesHeightUnitText;
            }

            return itemMasterList;
        }


        public async Task<BusBAItemMasterViewDTO> GetItemMasterByBusTenantIdAndItemidAsync(Guid tenantId, Guid itemId, CancellationToken token = default(CancellationToken)) {
            string baseUrl = _appSettings.BusinessEntityApiUrl;
            string reqeustMethod = "busbaitemmaster/view/" + tenantId + "/" + itemId;

            UserSession session = _userSessionManager.GetSession();

            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                                     RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
            BusBAItemMasterViewDTO itemMaster = await httpRequestProcessor.ExecuteAsync<BusBAItemMasterViewDTO>(requestOptions, false);
            return itemMaster;
        }


        #endregion Customer Get

        #region Vendor Get

        public async Task<IEnumerable<BusBAItemMasterDTO>> GetVendorsItemMasterListByBusTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            string baseUrl = _appSettings.BusinessEntityApiUrl;
            string reqeustMethod = "busbaitemmaster/vendors/list/" + tenantId;

            UserSession session = _userSessionManager.GetSession();

            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                                     RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
            IEnumerable<BusBAItemMasterDTO> itemMasterList = await httpRequestProcessor.ExecuteAsync<IEnumerable<BusBAItemMasterDTO>>(requestOptions, false);

            for(int i = 0; i < itemMasterList.Count(); i++) {
                //itemMasterList.ElementAt(i).Size = itemMasterList.ElementAt(i).SalesLength + " " + itemMasterList.ElementAt(i).SalesLengthUnitText + " x " + itemMasterList.ElementAt(i).SalesWidth + " " + itemMasterList.ElementAt(i).SalesWidthUnitText + " x " + itemMasterList.ElementAt(i).SalesHeight + " " + itemMasterList.ElementAt(i).SalesHeightUnitText;
                //itemMasterList.ElementAt(i).Size = Math.Truncate(itemMasterList.ElementAt(i).SalesLength) + " " + itemMasterList.ElementAt(i).SalesLengthUnitText + " x " + Math.Truncate(itemMasterList.ElementAt(i).SalesWidth) + " " + itemMasterList.ElementAt(i).SalesWidthUnitText + " x " + Math.Truncate(itemMasterList.ElementAt(i).SalesHeight) + " " + itemMasterList.ElementAt(i).SalesHeightUnitText;
                itemMasterList.ElementAt(i).Size = String.Format("{0:0.00}", itemMasterList.ElementAt(i).SalesLength) + " " + itemMasterList.ElementAt(i).SalesLengthUnitText + " x " + String.Format("{0:0.00}", itemMasterList.ElementAt(i).SalesWidth) + " " + itemMasterList.ElementAt(i).SalesWidthUnitText + " x " + String.Format("{0:0.00}", itemMasterList.ElementAt(i).SalesHeight) + " " + itemMasterList.ElementAt(i).SalesHeightUnitText;
            }

            return itemMasterList;
        }

        public async Task<BusBAItemMasterViewDTO> GetVendorsItemMasterByBusTenantIdAndItemidAsync(Guid tenantId, Guid itemId, CancellationToken token = default(CancellationToken)) {
            string baseUrl = _appSettings.BusinessEntityApiUrl;
            string reqeustMethod = "busbaitemmaster/vendors/view/" + tenantId + "/" + itemId;

            UserSession session = _userSessionManager.GetSession();

            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                                     RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
            BusBAItemMasterViewDTO itemMaster = await httpRequestProcessor.ExecuteAsync<BusBAItemMasterViewDTO>(requestOptions, false);
            return itemMaster;
        }

        #endregion Vendor Get

        #region Add Item Master

        /// <summary>
        /// Add Item Master 
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ResponseModelDTO> AddItemMasterByBusinessAsync(HttpRequest httpRequest, string request, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "busbaitemmaster/additemmaster";
            BusBAItemMasterViewDTO busBAItemMasterViewDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<BusBAItemMasterViewDTO>(request);
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            object reqeust = httpRequest.Form.Files;
            KeyValuePair<string, string> hdr2 = new KeyValuePair<string, string>("AddUpdateItemMasterModel", request);
            listHeader.Add(hdr2);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSettings.BusinessEntityApiUrl);
            RequestOptions requestOptions = null;
            ResponseModelDTO obj = null;
            if(httpRequest.Form != null && httpRequest.Form.Files != null && httpRequest.Form.Files.Count > 0) {
                using(var mpContent = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture))) {
                    foreach(FormFile file in httpRequest.Form.Files) {
                        StreamContent stContenant = new StreamContent(file.OpenReadStream());
                        mpContent.Add(stContenant, name: "files", fileName: file.Name);
                        mpContent.Headers.Add("ContentType", "application/octet-stream");
                        mpContent.Headers.ContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                    }
                    requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.MultipartFormData, Method, mpContent, _appSettings.AppName, _appSettings.IdentityServerUrl, listHeader);
                    obj = await httpRequestProcessor.ExecuteAsync<ResponseModelDTO>(requestOptions, false);
                }
            }
            else {
                Method = "busbaitemmaster/add";
                requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, busBAItemMasterViewDTO, _appSettings.AppName, _appSettings.IdentityServerUrl, listHeader);
                obj = await httpRequestProcessor.ExecuteAsync<ResponseModelDTO>(requestOptions, false);
            }
            return obj;
        }

        #endregion Add ItemMaster


        #region Update item master

        /// <summary>
        /// Update item master
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ResponseModelDTO> UpdateItemMasterByBusinessPaymentAppAsync(HttpRequest httpRequest,  string request,Guid itemId, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "busbaitemmaster/updateitemmaster/" + itemId;
            BusBAItemMasterViewDTO busBAItemMasterViewDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<BusBAItemMasterViewDTO>(request);
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            object reqeust = httpRequest.Form.Files;
            KeyValuePair<string, string> hdr2 = new KeyValuePair<string, string>("UpdateItemMasterModel", request);
            listHeader.Add(hdr2);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSettings.BusinessEntityApiUrl);
            RequestOptions requestOptions = null;
            ResponseModelDTO obj = null;
            if(httpRequest.Form != null && httpRequest.Form.Files != null && httpRequest.Form.Files.Count > 0) {
                using(var mpContent = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture))) {
                    foreach(FormFile file in httpRequest.Form.Files) {
                        StreamContent stContenant = new StreamContent(file.OpenReadStream());
                        mpContent.Add(stContenant, name: "files", fileName: file.Name);
                        mpContent.Headers.Add("ContentType", "application/octet-stream");
                        mpContent.Headers.ContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                    }
                    requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.MultipartFormData, Method, mpContent, _appSettings.AppName, _appSettings.IdentityServerUrl, listHeader);
                    obj = await httpRequestProcessor.ExecuteAsync<ResponseModelDTO>(requestOptions, false);
                }
            }
            else {
                Method = "busbaitemmaster/update/" + itemId;
                requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, busBAItemMasterViewDTO, _appSettings.AppName, _appSettings.IdentityServerUrl, listHeader);
                obj = await httpRequestProcessor.ExecuteAsync<ResponseModelDTO>(requestOptions, false);
            }
            return obj;
        }


        #endregion Update item master
    }
}
