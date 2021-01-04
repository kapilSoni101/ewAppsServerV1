using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.QData;
using ewApps.Core.CommonService;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// A wrapper class, contains BACustomer/Customer and its service info.
    /// </summary>
    public class QBACustomerDS:IQBACustomerDS {

        #region Local Variables

        IQBACustomerRepository _qBACustomerRepository;

        #endregion Local Variables


        #region Constructor

        /// <summary>
        /// Initlize local variables.
        /// </summary>
        /// <param name="qBACustomerRepository"></param>
        public QBACustomerDS(IQBACustomerRepository qBACustomerRepository) {
            _qBACustomerRepository = qBACustomerRepository;
        }

        #endregion Constructor


        #region Get

        /// <summary>
        /// Get app services and attrubute list.
        /// </summary>
        /// <param name="appKey">Application key.</param>
        /// <param name="tenantId">Tenant id</param>
        /// <param name="customerId">customer id</param>
        /// <param name="includeAttributeAccountDetail"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BusAppServiceDTO>> GetAppServiceListByAppKeyAndTenantAsync(string appKey, Guid tenantId, Guid customerId, bool includeAttributeAccountDetail, CancellationToken token = default(CancellationToken)) {

            List<BusAppServiceDTO> appServiceDTOList = await _qBACustomerRepository.GetBusinessAppServiceListByAppKeyAndTenantIdAsync(appKey, tenantId, token);
            List<BusAppServiceDTO> businessAppServiceDTOList = new List<BusAppServiceDTO>();

            List<BusAppServiceAttributeDTO> busAllServiceAttributeList = await GetBusinessServiceAttributeListAsync(appServiceDTOList, token);

            foreach(BusAppServiceDTO coreServiceDTO in appServiceDTOList) {
                BusAppServiceDTO busAppServiceDTO = coreServiceDTO;

                // Filter service attribute.
                List<BusAppServiceAttributeDTO> businessServiceAttributeList = busAllServiceAttributeList.FindAll(srvc => srvc.AppServiceId == busAppServiceDTO.ID);

                foreach(BusAppServiceAttributeDTO busServiceAttributeDTO in businessServiceAttributeList) {

                    if(includeAttributeAccountDetail) {
                        busServiceAttributeDTO.CustPayAcctDetail = new CustPayAcctDetailDTO();

                        if(coreServiceDTO.ServiceKey == Core.BaseService.Constants.VeriCheckServiceKey && busServiceAttributeDTO.AttributeKey == Core.BaseService.Constants.ACHPaymentsAttributeKey) {
                            List<CustVCACHPayAttrDTO> custVCACHPayAttrList = new List<CustVCACHPayAttrDTO>();
                            /* foreach(AppServiceAcctDetailDTO accDetail in busServiceAttributeDTO.AppServiceAcctDetailList) {
                                 CustVCACHPayAttrDTO veriCheckAcc = Newtonsoft.Json.JsonConvert.DeserializeObject<CustVCACHPayAttrDTO>(accDetail.AccountJson);
                                 veriCheckAcc.ID = accDetail.ID;
                                 custVCACHPayAttrList.Add(veriCheckAcc);
                             }
                             */
                            busServiceAttributeDTO.CustPayAcctDetail.CustVCACHPayAttrList = custVCACHPayAttrList;
                        }

                        if(coreServiceDTO.ServiceKey == Core.BaseService.Constants.VeriCheckServiceKey && busServiceAttributeDTO.AttributeKey == Core.BaseService.Constants.CreditCardPaymentsAttributeKey) {
                            List<DTO.VCCreditCardPayAttrDTO> vcCreditCardPayAttrList = new List<VCCreditCardPayAttrDTO>();
                            /* for(int i = 0; i < busServiceAttributeDTO.AppServiceAcctDetailList.Count; i++) {
                                DTO.VCCreditCardPayAttrDTO veriCheckCC = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.VCCreditCardPayAttrDTO>(busServiceAttributeDTO.AppServiceAcctDetailList[i].AccountJson);
                                veriCheckCC.ID = busServiceAttributeDTO.AppServiceAcctDetailList[i].ID;
                                vcCreditCardPayAttrList.Add(veriCheckCC);
                            }*/
                            busServiceAttributeDTO.BusPayAcctDetail.VCCreditCardPayAttrList = vcCreditCardPayAttrList;
                        }
                    }

                }
                busAppServiceDTO.BusAppServiceAttributeList = businessServiceAttributeList;
                businessAppServiceDTOList.Add(busAppServiceDTO);
            }

            return businessAppServiceDTOList;
        }

        /// <summary>
        /// Get app services and attrubute list.
        /// </summary>
        /// <param name="appId">Application id.</param>
        /// <param name="tenantId">Tenant id</param>
        /// <param name="customerId">customer id</param>
        /// <param name="includeAttributeAccountDetail"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BusAppServiceDTO>> GetAppServiceListByAppIdAndTenantAsync(Guid appId, Guid tenantId, Guid customerId, bool includeAttributeAccountDetail, CancellationToken token = default(CancellationToken)) {
            List<Guid> listAppId = new List<Guid>();
            listAppId.Add(appId);
            List<BusAppServiceDTO> appServiceDTOList = await GetBusinessAppServiceListByAppIdsAndTenantIdAsync(listAppId, tenantId, token);
            List<BusAppServiceDTO> businessAppServiceDTOList = new List<BusAppServiceDTO>();

            return await GetBusinessServiceAttributeListAsync(appServiceDTOList, appId, tenantId, customerId, includeAttributeAccountDetail, token);

            //foreach(BusAppServiceDTO coreServiceDTO in appServiceDTOList) {
            //    BusAppServiceDTO busAppServiceDTO = coreServiceDTO;

            //    // Filter service attribute.
            //    List<BusAppServiceAttributeDTO> businessServiceAttributeList = busAllServiceAttributeList.FindAll(srvc => srvc.AppServiceId == busAppServiceDTO.ID);

            //    foreach(BusAppServiceAttributeDTO busServiceAttributeDTO in businessServiceAttributeList) {

            //        if(includeAttributeAccountDetail) {
            //            busServiceAttributeDTO.CustPayAcctDetail = new CustPayAcctDetailDTO();

            //            if(coreServiceDTO.ServiceKey == Core.BaseService.Constants.VeriCheckServiceKey && busServiceAttributeDTO.AttributeKey == Core.BaseService.Constants.ACHPaymentsAttributeKey) {
            //                List<CustVCACHPayAttrDTO> custVCACHPayAttrList = new List<CustVCACHPayAttrDTO>();
            //                 foreach(AppServiceAcctDetailDTO accDetail in busServiceAttributeDTO.AppServiceAcctDetailList) {
            //                     CustVCACHPayAttrDTO veriCheckAcc = Newtonsoft.Json.JsonConvert.DeserializeObject<CustVCACHPayAttrDTO>(accDetail.AccountJson);
            //                     veriCheckAcc.ID = accDetail.ID;
            //                     custVCACHPayAttrList.Add(veriCheckAcc);
            //                 }

            //                busServiceAttributeDTO.CustPayAcctDetail.CustVCACHPayAttrList = custVCACHPayAttrList;
            //            }

            //            if(coreServiceDTO.ServiceKey == Core.BaseService.Constants.VeriCheckServiceKey && busServiceAttributeDTO.AttributeKey == Core.BaseService.Constants.CreditCardPaymentsAttributeKey) {
            //                List<DTO.VCCreditCardPayAttrDTO> vcCreditCardPayAttrList = new List<VCCreditCardPayAttrDTO>();
            //                 for(int i = 0; i < busServiceAttributeDTO.AppServiceAcctDetailList.Count; i++) {
            //                    DTO.VCCreditCardPayAttrDTO veriCheckCC = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.VCCreditCardPayAttrDTO>(busServiceAttributeDTO.AppServiceAcctDetailList[i].AccountJson);
            //                    veriCheckCC.ID = busServiceAttributeDTO.AppServiceAcctDetailList[i].ID;
            //                    vcCreditCardPayAttrList.Add(veriCheckCC);
            //                }
            //                busServiceAttributeDTO.BusPayAcctDetail.VCCreditCardPayAttrList = vcCreditCardPayAttrList;
            //            }
            //        }

            //    }
            //    busAppServiceDTO.BusAppServiceAttributeList = businessServiceAttributeList;
            //    businessAppServiceDTOList.Add(busAppServiceDTO);
            //}

            // return businessAppServiceDTOList;
        }

        private async Task<List<BusAppServiceAttributeDTO>> GetBusinessServiceAttributeListAsync(List<BusAppServiceDTO> appServiceDTOList, CancellationToken token) {
            if(appServiceDTOList == null || appServiceDTOList.Count == 0) {
                return new List<BusAppServiceAttributeDTO>();
            }
            List<Guid> listService = new List<Guid>();
            foreach(BusAppServiceDTO coreServiceDTO in appServiceDTOList) {
                listService.Add(coreServiceDTO.ID);
            }

            return await GetBusinessAppServiceAttributeListByServiceIdsAsync(listService, token);
        }

        /// <summary>
        /// Get business app service attribute list.
        /// </summary>
        /// <param name="appServiceDTOList">Collection of services.</param>      
        /// <param name="appId">Application id</param>
        /// <param name="tenantId">tenantid</param>
        /// <param name="customerId">customer uniqueid.</param>
        /// <param name="includeAccountDetail">Include saved account detail.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<List<BusAppServiceDTO>> GetBusinessServiceAttributeListAsync(List<BusAppServiceDTO> appServiceDTOList, Guid appId, Guid tenantId, Guid customerId, bool includeAccountDetail, CancellationToken token) {
            if(appServiceDTOList == null || appServiceDTOList.Count == 0) {
                return appServiceDTOList;
            }
            List<Guid> listService = new List<Guid>();
            foreach(BusAppServiceDTO coreServiceDTO in appServiceDTOList) {
                listService.Add(coreServiceDTO.ID);
            }

            List<BusAppServiceAttributeDTO> busAllServiceAttributeList = await GetBusinessAppServiceAttributeListByServiceIdsAsync(listService, token);

            CryptoHelper cryptoHelper = new CryptoHelper();
            List<CustVCACHPayAttrDTO> custVCACHPayAttrList;
            List<CreditCardDetailDTO> vcCreditCardPayAttrList;
            List<AppServiceAcctDetailDTO> appServiceAcctDetailList = null;
            if(includeAccountDetail) {
                appServiceAcctDetailList = await _qBACustomerRepository.GetAppServiceAccountDetailByCustomerIdAsync(tenantId, customerId, token);
            }
            foreach(BusAppServiceDTO coreServiceDTO in appServiceDTOList) {
                BusAppServiceDTO busAppServiceDTO = coreServiceDTO;
                //    // Filter service attribute.
                List<BusAppServiceAttributeDTO> businessServiceAttributeList = busAllServiceAttributeList.FindAll(srvc => srvc.AppServiceId == busAppServiceDTO.ID);
                for(int i = 0; i < businessServiceAttributeList.Count; i++) {
                    custVCACHPayAttrList = new List<CustVCACHPayAttrDTO>();
                    vcCreditCardPayAttrList = new List<CreditCardDetailDTO>();
                    BusAppServiceAttributeDTO busServiceAttributeDTO = businessServiceAttributeList[i];
                    busServiceAttributeDTO.CustPayAcctDetail = new CustPayAcctDetailDTO();
                    if(includeAccountDetail) {                        
                        foreach(AppServiceAcctDetailDTO appServiceAcctDetail in appServiceAcctDetailList) {
                            string AccountJson = cryptoHelper.Decrypt(appServiceAcctDetail.AccountJson, Constants.DefaultEncryptionAlgo);
                            if(busServiceAttributeDTO.AttributeKey == Constants.ACHPaymentsAttributeKey && appServiceAcctDetail.AccountType == (int)AccountTypeEnum.BankAccount) {
                                CustVCACHPayAttrDTO veriCheckAcc = Newtonsoft.Json.JsonConvert.DeserializeObject<CustVCACHPayAttrDTO>(AccountJson);
                                veriCheckAcc.ID = appServiceAcctDetail.ID;
                                custVCACHPayAttrList.Add(veriCheckAcc);
                            }
                            else if(busServiceAttributeDTO.AttributeKey == Constants.CreditCardPaymentsAttributeKey && appServiceAcctDetail.AccountType == (int)AccountTypeEnum.CreditCard) {
                                
                                CreditCardDetailDTO veriCheckAcc = Newtonsoft.Json.JsonConvert.DeserializeObject<CreditCardDetailDTO>(AccountJson);
                                veriCheckAcc.ID = appServiceAcctDetail.ID;
                                veriCheckAcc.PreAuthPaymentID = appServiceAcctDetail.PreAuthPaymentID;
                                vcCreditCardPayAttrList.Add(veriCheckAcc);
                            }
                        }
                    }
                    busServiceAttributeDTO.CustPayAcctDetail.CustVCACHPayAttrList = custVCACHPayAttrList;
                    busServiceAttributeDTO.CustPayAcctDetail.VCCreditCardPayAttrList = vcCreditCardPayAttrList;
                }
                busAppServiceDTO.BusAppServiceAttributeList = businessServiceAttributeList;
            }
            return appServiceDTOList;
        }

        /// <summary>
        /// Get customer info by business entity customer id.
        /// </summary>
        /// <param name="baCustomerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<CustomerPaymentInfoDTO> GetCustomerInfoAsync(Guid baCustomerId, Guid appId, CancellationToken token = default(CancellationToken)) {
            CustomerPaymentInfoDTO custInfo = await _qBACustomerRepository.GetCustomerInfoAsync(baCustomerId, token);

            return custInfo;
        }

        /// <summary>
        /// Get customer info by businessPartnerTenantId.
        /// </summary>
        /// <param name="businessPartnerTenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<CustomerPaymentInfoDTO> GetCustomerInfoByBusinessPartnerIdAsync(Guid businessPartnerTenantId, CancellationToken token = default(CancellationToken)) {
            CustomerPaymentInfoDTO custInfo = await _qBACustomerRepository.GetCustomerInfoByBusinessPartnerIdAsync(businessPartnerTenantId, token);

            return custInfo;
        }

        /// <summary>
        /// Get app service by appkey and tenantid.
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="tenantId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<BusAppServiceDTO>> GetBusinessAppServiceListByAppKeyAndTenantIdAsync(string appKey, Guid tenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qBACustomerRepository.GetBusinessAppServiceListByAppKeyAndTenantIdAsync(appKey, tenantId, cancellationToken);
        }

        /// <summary>
        /// Get business app service list by application and tenant id.
        /// </summary>
        /// <param name="appIdList">Application id list.</param>
        /// <param name="tenantId">Tenantid.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<List<BusAppServiceDTO>> GetBusinessAppServiceListByAppIdsAndTenantIdAsync(List<Guid> appIdList, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _qBACustomerRepository.GetBusinessAppServiceListByAppIdsAndTenantIdAsync(appIdList, tenantId, token);
        }

        /// <summary>
        /// Get service attribute list by service ids.
        /// </summary>
        /// <param name="serviceIdList">List of AppService Id.</param>               
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<BusAppServiceAttributeDTO>> GetBusinessAppServiceAttributeListByServiceIdsAsync(List<Guid> serviceIdList, CancellationToken cancellationToken = default(CancellationToken)) {
            List<BusAppServiceAttributeDTO> list = await _qBACustomerRepository.GetBusinessAppServiceAttributeListByServiceIdsAsync(serviceIdList, cancellationToken);
            // Decrypt 12-06-2019           

            return list;
        }

        #endregion Get

    }
}
