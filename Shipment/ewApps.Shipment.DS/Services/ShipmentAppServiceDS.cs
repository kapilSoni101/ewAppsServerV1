/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 09 April 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 09 April 2019
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.Shipment.Common;
using ewApps.Shipment.DTO;

namespace ewApps.Shipment.DS {


    /// <summary>
    /// This class performs basic CRUD operation on AppService entity.
    /// </summary>
    public class ShipmentAppServiceDS:IShipmentAppServiceDS {

        #region Local member

        #endregion Local member

        #region Constructor

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        /// <param name="verifiedAddressRepository"></param>
        /// <param name="cacheService"></param>
        public ShipmentAppServiceDS() {
        }


        #endregion Constructor




        //public async Task<List<ShipCarrierServiceDTO>> GetAppServiceListWithAccountDetailByAppAndTenantId(Guid appId, Guid tenantId) {
        //    List<ShipCarrierServiceDTO> shipmentAppServiceDTOList = GetAppServiceListByAppIdAndTenantId(appId, tenantId, false);
        //    return shipmentAppServiceDTOList;
        //}

        //private List<ShipCarrierServiceDTO> GetAppServiceListByAppIdAndTenantId(Guid appId, Guid tenantId, bool includeAttributeAccountDetail) {
        //    List<AppServiceDTO> appServiceDTOList = _appServiceDS.GetAppServiceDetailListByAppAndTenantId(appId, tenantId, false);

        //    List<ShipCarrierServiceDTO> shipmentAppServiceDTOList = new List<ShipCarrierServiceDTO>();

        //    foreach(AppServiceDTO coreServiceDTO in appServiceDTOList) {

        //        ShipCarrierServiceDTO shipAppServiceDTO = new ShipCarrierServiceDTO() {
        //            ID = coreServiceDTO.ID,
        //            Name = coreServiceDTO.Name,
        //            Active = coreServiceDTO.Active,
        //            ServiceKey = coreServiceDTO.ServiceKey
        //        };

        //        //Get Business Acount Detail
        //        List<AppServiceAcctDetailDTO> businessAccountDetailList = _appServiceAccountDetailDS.GetAppServiceAccountDetailByServiceAndAttributeAndEntityIdAndEntityType(tenantId, coreServiceDTO.ID, Guid.Empty, tenantId, (int)Core.Common.CoreEntityTypeEnum.Business);
        //        if(businessAccountDetailList != null) {
        //            if(coreServiceDTO.ServiceKey == Constants.FedExServiceKey) {
        //                List<FedExCarrierServiceAcctDetailDTO> fedExCarrierServiceAcctDetailList = new List<FedExCarrierServiceAcctDetailDTO>();
        //                foreach(AppServiceAcctDetailDTO accDetail in businessAccountDetailList) {

        //                    FedExCarrierServiceAcctDetailDTO fedExCarrierServiceAcctDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<FedExCarrierServiceAcctDetailDTO>(accDetail.AccountJson);
        //                    fedExCarrierServiceAcctDetail.ID = accDetail.ID;
        //                    fedExCarrierServiceAcctDetailList.Add(fedExCarrierServiceAcctDetail);
        //                }
        //                shipAppServiceDTO.FedExCarrierServiceAcctDetailList = fedExCarrierServiceAcctDetailList;

        //            }
        //            if(coreServiceDTO.ServiceKey == Constants.UPSServiceKey) {
        //                List<UpsCarrierServiceAcctDetailDTO> upsCarrierServiceAcctDetailList = new List<UpsCarrierServiceAcctDetailDTO>();
        //                foreach(AppServiceAcctDetailDTO accDetail in businessAccountDetailList) {

        //                    UpsCarrierServiceAcctDetailDTO upsCarrierServiceAcctDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<UpsCarrierServiceAcctDetailDTO>(accDetail.AccountJson);
        //                    upsCarrierServiceAcctDetail.ID = accDetail.ID;
        //                    upsCarrierServiceAcctDetailList.Add(upsCarrierServiceAcctDetail);
        //                }
        //                shipAppServiceDTO.UpsCarrierServiceAcctDetailList = upsCarrierServiceAcctDetailList;

        //            }
        //            if(coreServiceDTO.ServiceKey == Constants.USPSServiceKey) {
        //                List<UspsCarrierServiceAcctDetailDTO> uspsCarrierServiceAcctDetailList = new List<UspsCarrierServiceAcctDetailDTO>();
        //                foreach(AppServiceAcctDetailDTO accDetail in businessAccountDetailList) {

        //                    UspsCarrierServiceAcctDetailDTO uspsCarrierServiceAcctDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<UspsCarrierServiceAcctDetailDTO>(accDetail.AccountJson);
        //                    uspsCarrierServiceAcctDetail.ID = accDetail.ID;
        //                    uspsCarrierServiceAcctDetailList.Add(uspsCarrierServiceAcctDetail);
        //                }
        //                shipAppServiceDTO.UspsCarrierServiceAcctDetailList = uspsCarrierServiceAcctDetailList;

        //            }
        //        }

        //        // AppServiceAttributeDTO coreServiceACHAttribute = coreServiceDTO.AppServiceAttributeList;
        //        List<ShipCarrierServiceAttributeDTO> shipCarrierServiceAttributeList = new List<ShipCarrierServiceAttributeDTO>();
        //        foreach(AppServiceAttributeDTO coreServiceAttribute in coreServiceDTO.AppServiceAttributeList) {

        //            ShipCarrierServiceAttributeDTO shipCarrierServiceAttributeDTO = new ShipCarrierServiceAttributeDTO();
        //            shipCarrierServiceAttributeDTO.ID = coreServiceAttribute.ID;
        //            shipCarrierServiceAttributeDTO.Name = coreServiceAttribute.Name;
        //            shipCarrierServiceAttributeDTO.Active = coreServiceAttribute.Active;
        //            shipCarrierServiceAttributeDTO.AttributeKey = coreServiceAttribute.AttributeKey;

        //            //// by appservice id and attribue id   
        //            //  TenantAppServiceLinking tenantAppServLinking = _tenantAppServiceLinkingDS.Get(shipAppServiceDTO.ID, shipCarrierServiceAttributeDTO.ID, tenantId).FirstOrDefault();
        //            ////TenantAppServiceLinking tenantAppServLinking = _tenantAppServiceLinkingDS.GetEntityListByServiceAndAttributeId(shipAppServiceDTO.ID, shipCarrierServiceAttributeDTO.ID, tenantId).FirstOrDefault();
        //            //if (tenantAppServLinking != null)
        //            //{
        //            //  shipCarrierServiceAttributeDTO.Checked = true;
        //            //  shipAppServiceDTO.Checked = true;
        //            //}

        //            shipCarrierServiceAttributeList.Add(shipCarrierServiceAttributeDTO);

        //        }

        //        shipAppServiceDTO.ShipCarrierServiceAttributeList = shipCarrierServiceAttributeList;
        //        // AppServiceAttributeDTO coreServiceACHAttribute = coreServiceDTO.AppServiceAttributeList.FirstOrDefault(i => i.AttributeKey == Constants.ACHPaymentsAttributeKey && coreServiceDTO.ServiceKey == Constants.VeriCheckServiceKey);
        //        shipmentAppServiceDTOList.Add(shipAppServiceDTO);
        //    }

        //    return shipmentAppServiceDTOList;
        //}

    }
}
