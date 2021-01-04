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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Payment.DTO;

namespace ewApps.Payment.DS {
    public class PayAppServiceDS :IPayAppServiceDS {

        #region Local Variable
        IQPayAppServiceDS _qPayAppServiceDS;
        #endregion

        #region Constructor
        public PayAppServiceDS(IQPayAppServiceDS qPayAppServiceDS) {
            _qPayAppServiceDS = qPayAppServiceDS;
        }
        #endregion

        public async Task<List<PayAppServiceDetailDTO>> GetBusinessAppServiceListByAppIdsAndTenantIdAsync(Guid appId, Guid tenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            List<PayAppServiceDetailDTO> payAppServiceDetailListDTOs = new List<PayAppServiceDetailDTO>();
            payAppServiceDetailListDTOs = await _qPayAppServiceDS.GetBusinessAppServiceListByAppIdsAndTenantIdAsync(appId, tenantId, cancellationToken);
            foreach(PayAppServiceDetailDTO req in payAppServiceDetailListDTOs) {
                req.AppServiceAttributeList = await GetBusinessAppServiceAttributeAndAccountListByAppIdsAndTenantIdAsync(appId, tenantId, req.ID, cancellationToken);
            }
            if(payAppServiceDetailListDTOs != null) {
                foreach(PayAppServiceDetailDTO req in payAppServiceDetailListDTOs) {
                    foreach(PayAppServiceAttributeDetailDTO req2 in req.AppServiceAttributeList) {
                        if(req.ServiceKey == Common.Constants.VeriCheckServiceKey && req2.AttributeKey == Common.Constants.ACHPaymentsAttributeKey && req2.AppServiceAccountList.Count > 0) {
                            // decrypt accoutjson and assign t
                            BusVCACHPayAttrDTO veriCheckAcc = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.BusVCACHPayAttrDTO>(req2.AppServiceAccountList[0].AccountJson);                            
                            req2.BusVCACHPayAttrDTO = veriCheckAcc;
                            req.Active = true;
                            req2.Active = true;

                        }
                        else if(req.ServiceKey == Common.Constants.VeriCheckServiceKey && req2.AttributeKey == Common.Constants.ACHPaymentsAttributeKey && req2.AppServiceAccountList.Count == 0){
                            BusVCACHPayAttrDTO veriCheckAcc = new BusVCACHPayAttrDTO();
                            veriCheckAcc.ID = Guid.Empty;
                            veriCheckAcc.VeriCheckID = null;
                            req2.BusVCACHPayAttrDTO = veriCheckAcc;
                            req.Active = false;
                            req2.Active = false;
                            //veriCheckAcc.ID = Guid.Empty;
                        }
                        if(req.ServiceKey == Common.Constants.TSYSCheckServiceKey && req2.AttributeKey == Common.Constants.CreditCardPaymentsAttributeKey && req2.AppServiceAccountList.Count > 0) {
                            // decrypt accoutjson and assign t
                            BusTSYSCreditCardPayAttrDTO tsysCheckAcc = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.BusTSYSCreditCardPayAttrDTO>(req2.AppServiceAccountList[0].AccountJson);                            
                            req.Active = true;
                            req2.Active = true;
                            req2.BusTSYSCreditCardPayAttrDTO = tsysCheckAcc;

                        }
                        else if(req.ServiceKey == Common.Constants.TSYSCheckServiceKey && req2.AttributeKey == Common.Constants.CreditCardPaymentsAttributeKey && req2.AppServiceAccountList.Count == 0) {
                           
                            BusTSYSCreditCardPayAttrDTO tsysCheckAcc = new BusTSYSCreditCardPayAttrDTO();
                            tsysCheckAcc.ID = Guid.Empty;
                            tsysCheckAcc.MID = null;
                            tsysCheckAcc.UserId = null;
                            tsysCheckAcc.Password = null;
                            req.Active = false;
                            req2.Active = false;
                            req2.BusTSYSCreditCardPayAttrDTO = tsysCheckAcc;

                        }
                    }
                }
            }
            return payAppServiceDetailListDTOs;
        }

        public async Task<List<PayAppServiceAttributeDetailDTO>> GetBusinessAppServiceAttributeAndAccountListByAppIdsAndTenantIdAsync(Guid appId, Guid tenantId, Guid serviceId, CancellationToken cancellationToken = default(CancellationToken)) {

            //Initialize Local Variable
            List<PayAppServiceAttributeDetailDTO> payAppServiceAttributeDetailDTO = new List<PayAppServiceAttributeDetailDTO>();

            //Get AppService Attribute List By App and TenantId 
            payAppServiceAttributeDetailDTO = await _qPayAppServiceDS.GetBusinessAppServiceAttributeAndAccountListByAppIdsAndTenantIdAsync(appId, tenantId, serviceId, cancellationToken);

            //Initialize Task Array
            Task<List<AppServiceAcctDetailDTO>>[] taskArray = new Task<List<AppServiceAcctDetailDTO>>[payAppServiceAttributeDetailDTO.Count];
            //foreach(PayAppServiceAttributeDetailDTO req in payAppServiceAttributeDetailDTO) {
            //    req.AppServiceAccountList = await GetBusinessAppServiceAccountListByAppIdsAndTenantIdAsync(appId, tenantId, req.ID, cancellationToken);
            //}
            for(int i = 0; i < payAppServiceAttributeDetailDTO.Count; i++) {
                taskArray[i] = GetBusinessAppServiceAccountListByAppIdsAndTenantIdAsync(appId, tenantId, payAppServiceAttributeDetailDTO[i].ID, cancellationToken);
            }

            // Task.WaitAll(taskArray);

            for(int t = 0; t < taskArray.Length; t++) {
                taskArray[t].Wait();
                payAppServiceAttributeDetailDTO[t].AppServiceAccountList = taskArray[t].Result;
            }

            return payAppServiceAttributeDetailDTO;
        }

        public async Task<List<AppServiceAcctDetailDTO>> GetBusinessAppServiceAccountListByAppIdsAndTenantIdAsync(Guid appId, Guid tenantId, Guid serviceAttributeId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qPayAppServiceDS.GetBusinessAppServiceAccountListByAppIdsAndTenantIdAsync(appId, tenantId, serviceAttributeId, cancellationToken);
        }

    }
}
