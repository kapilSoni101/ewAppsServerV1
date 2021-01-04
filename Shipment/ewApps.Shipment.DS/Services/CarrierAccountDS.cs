/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author:Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 12 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 12 September 2019
 */

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using ewApps.Shipment.Common;
using ewApps.Shipment.DTO;
using Microsoft.Extensions.Options;

namespace ewApps.Shipment.DS{

    //This Class User For Carrier Acount Operation 
    public class CarrierAccountDS : ICarrierAccountDS{

        #region Local Variable
        ShipmentAppSettings _appSetting;
        IUserSessionManager _userSessionManager;
        #endregion

        #region Constructor
        public CarrierAccountDS(IOptions<ShipmentAppSettings> appSetting, IUserSessionManager userSessionManager) {
            _appSetting = appSetting.Value;
            _userSessionManager = userSessionManager;
        }

        #endregion
        #region API Call to save Account number

        /// <summary>
        /// add/update/delete shipper account info in connector.
        /// </summary>
        /// <param name="listAddUpdateAccount"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task AddUpdateCarrierAccountInfoAsync(List<AppServiceAccDetailIdDTO> listAddUpdateAccount, CancellationToken token = default(CancellationToken)) {
            for(int i = 0; i < listAddUpdateAccount.Count; i++) {
                ShipperAccountDTO shipperActDto = GetShipperAccountDTO(listAddUpdateAccount[i]);
                if(listAddUpdateAccount[i].operationType == OperationType.Add || listAddUpdateAccount[i].operationType == OperationType.Update) {
                    bool isAdd = listAddUpdateAccount[i].operationType == OperationType.Add;
                    if(shipperActDto != null) {
                        await AddUpdateShipperAccountDetailAsync(shipperActDto, isAdd, token);
                    }
                }
                else if(listAddUpdateAccount[i].operationType == OperationType.Delete) {
                    if(shipperActDto != null) {
                        await DeleteShipperAccountDetailAsync(shipperActDto, token);
                    }
                }
            }
        }

        private ShipperAccountDTO GetShipperAccountDTO(AppServiceAccDetailIdDTO appSrvcActDtl) {
            ShipperAccountDTO shipperAccountDTO = null;
            //  FedExCarrierServiceAcctDetailDTO fedExCarrierServiceAcctDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<FedExCarrierServiceAcctDetailDTO>(accDetail.AccountJson);
            // UpsCarrierServiceAcctDetailDTO upsCarrierServiceAcctDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<UpsCarrierServiceAcctDetailDTO>(accDetail.AccountJson);
            if(appSrvcActDtl.ServiceId.ToString().ToUpper() == Common.Constants.FedExServiceUniqueId) {
                shipperAccountDTO = new ShipperAccountDTO();
                shipperAccountDTO.ShipperId = appSrvcActDtl.SourceEntityId.ToString();
                shipperAccountDTO.AccountKey = appSrvcActDtl.Id.ToString();
                shipperAccountDTO.CarrierCode = Common.Constants.FedExServiceKey;
                FedExCarrierServiceAcctDetailDTO fedExCarrierServiceAcctDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<FedExCarrierServiceAcctDetailDTO>(appSrvcActDtl.AccountJson);
                if(fedExCarrierServiceAcctDetail != null) {
                    shipperAccountDTO.AccountNumber = fedExCarrierServiceAcctDetail.AccountNo;
                    shipperAccountDTO.AccessKey = fedExCarrierServiceAcctDetail.AccessKey;
                    shipperAccountDTO.MeterNumber = fedExCarrierServiceAcctDetail.MeterNo;
                    shipperAccountDTO.UserName = fedExCarrierServiceAcctDetail.UserName;
                    shipperAccountDTO.Password = fedExCarrierServiceAcctDetail.Password;

                }
            }
            else if(appSrvcActDtl.ServiceId.ToString().ToUpper() == Common.Constants.UPSServiceUniqueId) {
                shipperAccountDTO = new ShipperAccountDTO();
                shipperAccountDTO.ShipperId = appSrvcActDtl.SourceEntityId.ToString();
                shipperAccountDTO.AccountKey = appSrvcActDtl.Id.ToString();
                shipperAccountDTO.CarrierCode = Common.Constants.UPSServiceKey;
                UpsCarrierServiceAcctDetailDTO upsCarrierServiceAcctDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<UpsCarrierServiceAcctDetailDTO>(appSrvcActDtl.AccountJson);
                if(upsCarrierServiceAcctDetail != null) {
                    shipperAccountDTO.AccountNumber = upsCarrierServiceAcctDetail.BillAccountNo;
                    shipperAccountDTO.AccessKey = upsCarrierServiceAcctDetail.AccessLicenseNo;
                    shipperAccountDTO.UserName = upsCarrierServiceAcctDetail.UserName;
                    shipperAccountDTO.Password = upsCarrierServiceAcctDetail.Password;
                }

            }
            else if(appSrvcActDtl.ServiceId.ToString().ToUpper() == Common.Constants.USPSServiceUniqueId) {
                shipperAccountDTO = new ShipperAccountDTO();
                shipperAccountDTO.ShipperId = appSrvcActDtl.SourceEntityId.ToString();
                shipperAccountDTO.AccountKey = appSrvcActDtl.Id.ToString();
                shipperAccountDTO.CarrierCode = Common.Constants.USPSServiceKey;
                UspsCarrierServiceAcctDetailDTO uspsCarrierServiceAcctDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<UspsCarrierServiceAcctDetailDTO>(appSrvcActDtl.AccountJson);
                if(uspsCarrierServiceAcctDetail != null) {
                    shipperAccountDTO.AccountNumber = uspsCarrierServiceAcctDetail.UserId;
                    shipperAccountDTO.UserName = uspsCarrierServiceAcctDetail.UserId;
                    shipperAccountDTO.Password = uspsCarrierServiceAcctDetail.Password;
                }

            }

            return shipperAccountDTO;
        }

        /// <summary>
        /// Save carrier account detail for shipper.
        /// </summary>
        /// <param name="shipperAccountRequestDTO"> entity to be added</param>
        /// <param name="isAdd">Whether add/update account detail.</param>
        /// <returns>return account detail for shipper.</returns>
        public async Task<ShipperAccountDTO> AddUpdateShipperAccountDetailAsync(ShipperAccountDTO shipperAccountRequestDTO, bool isAdd, CancellationToken token = default(CancellationToken)) {
            UserSession session = _userSessionManager.GetSession();
            HttpClient client = new HttpClient();
            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> tenantKey = new KeyValuePair<string, string>("tenantid", session.TenantId.ToString());
            headerParameters.Add(tenantKey);

            KeyValuePair<string, string> appUserIdKey = new KeyValuePair<string, string>("appuserid", session.TenantUserId.ToString());
            headerParameters.Add(appUserIdKey);

            KeyValuePair<string, string> appidKey = new KeyValuePair<string, string>("appid", session.AppId.ToString());
            headerParameters.Add(appidKey);
            string api = isAdd ? "Shipper/add" : "Shipper/update";
            string url = _appSetting.ShipmentConnectorUrl + api;
            HttpRequestProcessor reqProc = new HttpRequestProcessor(client);

            string bearerToken = await reqProc.GetBearerTokenAsync(_appSetting.AppName, _appSetting.IdentityServerUrl);
            client.SetBearerToken(bearerToken);
            ShipperAccountDTO obj = null;
            try {
                obj = await reqProc.ExecutePOSTRequestAsync<ShipperAccountDTO, ShipperAccountDTO>(url, "", AcceptMediaTypeEnum.JSON, headerParameters, null, null, shipperAccountRequestDTO);
            }
            catch(Exception ex) {
                EwpError ewp = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(ex.Message);
                if(ewp == null)
                    throw ex;
                else {
                    RaiseException(ewp, ex);
                }
                return obj;
            }

            return obj;
        }

        /// <summary>
        /// Delete shipper account detail.
        /// </summary>
        /// <param name="shipperAccountRequestDTO"> shipper account to delete</param>
        public async Task DeleteShipperAccountDetailAsync(ShipperAccountDTO shipperAccountRequestDTO, CancellationToken token = default(CancellationToken)) {
            UserSession session = _userSessionManager.GetSession();
            HttpClient client = new HttpClient();
            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> tenantKey = new KeyValuePair<string, string>("tenantid", session.TenantId.ToString());
            headerParameters.Add(tenantKey);

            KeyValuePair<string, string> appUserIdKey = new KeyValuePair<string, string>("appuserid", session.TenantUserId.ToString());
            headerParameters.Add(appUserIdKey);

            KeyValuePair<string, string> appidKey = new KeyValuePair<string, string>("appid", session.AppId.ToString());
            headerParameters.Add(appidKey);
            string api = "Shipper/delete";
            string url = _appSetting.ShipmentConnectorUrl + api;
            HttpRequestProcessor reqProc = new HttpRequestProcessor(client);

            string bearerToken = await reqProc.GetBearerTokenAsync(_appSetting.AppName, _appSetting.IdentityServerUrl);
            client.SetBearerToken(bearerToken);
            try {
                reqProc.ExecuteDELETERequest(url, "", AcceptMediaTypeEnum.JSON, headerParameters, null, null, shipperAccountRequestDTO);
            }
            catch(Exception ex) {
                EwpError ewp = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(ex.Message);
                if(ewp == null)
                    throw ex;
                else {
                    RaiseException(ewp, ex);
                }
            }

        }

        /// <summary>
        /// Get shipper account list by shipperid, shippertype and carriercode.
        /// If carriercode is empty then return all supportive carriers account.
        /// </summary>
        /// <param name="shipperId">Shipperid, for example TenantId or CustomerId.</param>
        /// <param name="shipperType">shipper entity type</param>
        /// <param name="carrierCode">CarrierCode for example FedEx, UPS, empty etc.</param>
        /// <param name="includeDeleted">Include deleted accounts.</param>        
        /// <param name="token"></param>
        /// <returns>return all accounts list configure for carriers.</returns>
        //public async Task<List<ShipperAccountDTO>> GetShipperAccountListAsync(Guid shipperId, int shipperType, string carrierCode, bool includeDeleted, CancellationToken token = default(CancellationToken)) {
        //    UserSession uSession = _userSessionManager.GetSession();
        //    List<ShipperAccountDTO> listShipperActDto = new List<ShipperAccountDTO>();
        //    ShipperAccountDTO shipperActDTO;
        //    if(carrierCode == Common.Constants.FedExServiceKey) {
        //        List<AppServiceAcctDetailDTO> listActDto = _serviceAccountDetailDS.GetAppServiceAccountDetailByServiceAndAttributeAndEntityIdAndEntityType(uSession.TenantId, new Guid(Common.Constants.FedExServiceUniqueId), Guid.Empty, shipperId, shipperType);
        //        for(int i = 0; i < listActDto.Count; i++) {
        //            shipperActDTO = GetShipperAccountFromService(listActDto[i], Common.Constants.FedExServiceUniqueId, shipperId.ToString());
        //            if(shipperActDTO != null) {
        //                listShipperActDto.Add(shipperActDTO);
        //            }
        //        }
        //    }
        //    else if(carrierCode == Common.Constants.UPSServiceKey) {
        //        List<AppServiceAcctDetailDTO> listActDto = _serviceAccountDetailDS.GetAppServiceAccountDetailByServiceAndAttributeAndEntityIdAndEntityType(uSession.TenantId, new Guid(Common.Constants.UPSServiceUniqueId), Guid.Empty, shipperId, shipperType);
        //        for(int i = 0; i < listActDto.Count; i++) {
        //            shipperActDTO = GetShipperAccountFromService(listActDto[i], Common.Constants.UPSServiceUniqueId, shipperId.ToString());
        //            if(shipperActDTO != null) {
        //                listShipperActDto.Add(shipperActDTO);
        //            }
        //        }
        //    }
        //    else {
        //        List<AppServiceAcctDetailDTO> listActDto = _serviceAccountDetailDS.GetAppServiceAccountDetailByServiceAndAttributeAndEntityIdAndEntityType(uSession.TenantId, new Guid(Common.Constants.FedExServiceUniqueId), Guid.Empty, shipperId, shipperType);
        //        for(int i = 0; i < listActDto.Count; i++) {
        //            shipperActDTO = GetShipperAccountFromService(listActDto[i], Common.Constants.FedExServiceUniqueId, shipperId.ToString());
        //            if(shipperActDTO != null) {
        //                listShipperActDto.Add(shipperActDTO);
        //            }
        //        }
        //        listActDto = _serviceAccountDetailDS.GetAppServiceAccountDetailByServiceAndAttributeAndEntityIdAndEntityType(uSession.TenantId, new Guid(Common.Constants.UPSServiceUniqueId), Guid.Empty, shipperId, shipperType);
        //        for(int i = 0; i < listActDto.Count; i++) {
        //            shipperActDTO = GetShipperAccountFromService(listActDto[i], Common.Constants.UPSServiceUniqueId, shipperId.ToString());
        //            if(shipperActDTO != null) {
        //                listShipperActDto.Add(shipperActDTO);
        //            }
        //        }
        //    }

        //    return listShipperActDto;
        //}

        private ShipperAccountDTO GetShipperAccountFromService(AppServiceAcctDetailDTO appSrvcActDtl, string serviceId, string shipperId) {
            ShipperAccountDTO shipperAccountDTO = null;
            //  FedExCarrierServiceAcctDetailDTO fedExCarrierServiceAcctDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<FedExCarrierServiceAcctDetailDTO>(accDetail.AccountJson);
            // UpsCarrierServiceAcctDetailDTO upsCarrierServiceAcctDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<UpsCarrierServiceAcctDetailDTO>(accDetail.AccountJson);
            if(serviceId.ToUpper() == Common.Constants.FedExServiceUniqueId) {
                shipperAccountDTO = new ShipperAccountDTO();
                shipperAccountDTO.ShipperId = shipperId;
                shipperAccountDTO.AccountKey = appSrvcActDtl.ID.ToString();
                shipperAccountDTO.CarrierCode = Common.Constants.FedExServiceKey;
                FedExCarrierServiceAcctDetailDTO fedExCarrierServiceAcctDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<FedExCarrierServiceAcctDetailDTO>(appSrvcActDtl.AccountJson);
                if(fedExCarrierServiceAcctDetail != null) {
                    shipperAccountDTO.AccountNumber = fedExCarrierServiceAcctDetail.AccountNo;
                    shipperAccountDTO.AccessKey = fedExCarrierServiceAcctDetail.AccessKey;
                    shipperAccountDTO.MeterNumber = fedExCarrierServiceAcctDetail.MeterNo;
                    shipperAccountDTO.UserName = fedExCarrierServiceAcctDetail.UserName;
                    shipperAccountDTO.Password = fedExCarrierServiceAcctDetail.Password;

                }
            }
            else if(serviceId.ToUpper() == Common.Constants.UPSServiceUniqueId) {
                shipperAccountDTO = new ShipperAccountDTO();
                shipperAccountDTO.ShipperId = shipperId;
                shipperAccountDTO.AccountKey = appSrvcActDtl.ID.ToString();
                shipperAccountDTO.CarrierCode = Common.Constants.UPSServiceKey;
                UpsCarrierServiceAcctDetailDTO upsCarrierServiceAcctDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<UpsCarrierServiceAcctDetailDTO>(appSrvcActDtl.AccountJson);
                if(upsCarrierServiceAcctDetail != null) {
                    shipperAccountDTO.AccountNumber = upsCarrierServiceAcctDetail.BillAccountNo;
                    shipperAccountDTO.AccessKey = upsCarrierServiceAcctDetail.AccessLicenseNo;
                    shipperAccountDTO.UserName = upsCarrierServiceAcctDetail.UserName;
                    shipperAccountDTO.Password = upsCarrierServiceAcctDetail.Password;
                }

            }

            return shipperAccountDTO;
        }

        /// <summary>
        /// Get shipper account list by shipperid and carriercode.
        /// </summary>
        /// <param name="shipperId">Shipperid, for example TenantId or CustomerId.</param>
        /// <param name="carrierCode">CarrierCode for example FedEx, UPS etc.</param>
        /// <returns>Added entity</returns>
        public async Task<List<ShipperAccountDTO>> GetShipperAccountListAsync(string shipperId, string carrierCode, CancellationToken token = default(CancellationToken)) {
            UserSession session = _userSessionManager.GetSession();
            HttpClient client = new System.Net.Http.HttpClient();
            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> tenantKey = new KeyValuePair<string, string>("tenantid", session.TenantId.ToString());
            headerParameters.Add(tenantKey);

            KeyValuePair<string, string> appUserIdKey = new KeyValuePair<string, string>("appuserid", session.TenantUserId.ToString());
            headerParameters.Add(appUserIdKey);

            KeyValuePair<string, string> appidKey = new KeyValuePair<string, string>("appid", session.AppId.ToString());
            headerParameters.Add(appidKey);

            string url = _appSetting.ShipmentConnectorUrl + "Shipment/get/" + shipperId + "/" + carrierCode;
            HttpRequestProcessor reqProc = new HttpRequestProcessor(client);

            string bearerToken = await reqProc.GetBearerTokenAsync(_appSetting.AppName, _appSetting.IdentityServerUrl);
            client.SetBearerToken(bearerToken);
            List<ShipperAccountDTO> obj = null;
            try {
                obj = reqProc.ExecuteGetRequest<List<ShipperAccountDTO>>(url, "", AcceptMediaTypeEnum.JSON, headerParameters, null, null);
            }
            catch(Exception ex) {
                EwpError ewp = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(ex.Message);
                if(ewp == null)
                    throw ex;
                else {
                    RaiseException(ewp, ex);
                }
                return obj;
            }

            return obj;
        }

        private void RaiseException(EwpError ewp, Exception ex) {
            switch(ewp.ErrorType) {
                case ErrorType.Validation:
                    string message = ewp.MessageList.Count > 0 ? ewp.MessageList[0] : "";
                    EwpValidationException valid = new EwpValidationException(message, ewp.EwpErrorDataList);
                    throw valid;
                default:
                    throw ex;
            }
        }

        #endregion API Call to save Account number

    }
}
