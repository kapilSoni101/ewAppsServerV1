/* Copyright © 2018 eWorkplace Apps(https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 *
 * Author: Sourabh Agrawal<sagrawal @eworkplaceapps.com>
 * Date:05 september 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {
    public class BAASNDS:BaseDS<BAASN>, IBAASNDS {

        #region Local variables

        IBAASNRepositorty _asnRepositorty;
        IBAASNItemDS _asnItemDS;
        IBACustomerDS _customerDS;
        IUnitOfWork _unitOfWork;
        IQNotificationDS _qNotificationDS;
        IBusinessEntityNotificationHandler _businessEntityNotificationHandler;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractRepositorty"></param>
        public BAASNDS(IBAASNRepositorty asnRepositorty, IBAASNItemDS asnItemDS, IBACustomerDS customerDS, IUnitOfWork unitOfWork, IQNotificationDS qNotificationDS, IBusinessEntityNotificationHandler businessEntityNotificationHandler) : base(asnRepositorty) {
            _asnRepositorty = asnRepositorty;
            _asnItemDS = asnItemDS;
            _customerDS = customerDS;
            _unitOfWork = unitOfWork;
            _qNotificationDS = qNotificationDS;
            _businessEntityNotificationHandler = businessEntityNotificationHandler;
        }

        #endregion Constructor


        #region Add

        ///<inheritdoc/>
        public async Task AddASNListAsync(List<BAASNSyncDTO> asnList, Guid tenantId, Guid tenantUserId, bool isBulkInsert, CancellationToken token = default(CancellationToken)) {
            List<string> asnAddERPKeyList = new List<string>();
            List<string> asnUpdateERPKeyList = new List<string>();
            for(int i = 0; i < asnList.Count; i++) {
                if(isBulkInsert) {
                    bool oldSalesQuotation = await AddASNAsync(asnList[i], tenantId, tenantUserId);
                }
                else {
                    if(asnList[i].OpType.Equals("Inserted")) {
                        bool oldSalesQuotation = await AddUpdateASNAsync(asnList[i], tenantId, tenantUserId);
                        if(oldSalesQuotation == true) {
                            asnAddERPKeyList.Add(asnList[i].ERPASNKey);
                        }
                    }
                    else if(asnList[i].OpType.Equals("Modified")) {
                        asnUpdateERPKeyList.Add(asnList[i].ERPASNKey);
                        await AddUpdateASNAsync(asnList[i], tenantId, tenantUserId);
                    }
                }
            }

            //save data
            _unitOfWork.SaveAll();

        }

        /// <summary>
        /// add sales quotation and its child data .
        /// </summary>
        /// <param name="contractSyncDTO"></param>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<bool> AddASNAsync(BAASNSyncDTO asnSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken)) {
            BAASN asn = await FindAsync(con => con.ERPASNKey == asnSyncDTO.ERPASNKey && con.TenantId == tenantId);
            if(asn != null) {
                return false;
            }
            BACustomer customer = await _customerDS.FindAsync(cust => cust.ERPCustomerKey == asnSyncDTO.ERPCustomerKey && cust.TenantId == tenantId);
      if (customer == null)
      {
        return false;
      }
      asnSyncDTO.CustomerId = customer.ID;
            asn = BAASNSyncDTO.MapToEntity(asnSyncDTO);

            // UpdateSystemFieldsByOpType(salesOrder, OperationType.Add);
            Guid asnId = Guid.NewGuid();
            asn.ID = asnId;
            asn.CreatedBy = tenantUserId;//;// Session
            asn.UpdatedBy = tenantUserId;
            asn.CreatedOn = DateTime.UtcNow;
            asn.UpdatedOn = DateTime.UtcNow;
            asn.Deleted = false;
            asn.TenantId = tenantId;

            // add sales quotation detail
            await AddAsync(asn, token);

            //Add sales quotation item detail.
            if(asnSyncDTO.ItemList != null && asnSyncDTO.ItemList.Count > 0) {
                await _asnItemDS.AddASNItemListAsync(asnSyncDTO.ItemList, tenantId, tenantUserId, asnId);
            }

            return true;

        }
    private async Task<bool> AddUpdateASNAsync(BAASNSyncDTO asnSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {
      BAASN asn = await FindAsync(con => con.ERPASNKey == asnSyncDTO.ERPASNKey && con.TenantId == tenantId);
      if (asn != null)
      {
        await UpdateASNAsync(asnSyncDTO, tenantId, tenantUserId);
      }
      else
      {
        await AddASNAsync(asnSyncDTO, tenantId, tenantUserId);
      }
      return true;
    }

    #endregion Add

    /// <summary>
    /// add sales quotation and its child data .
    /// </summary>
    /// <param name="asnSyncDTO"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task<bool> UpdateASNAsync(BAASNSyncDTO asnSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken)) {
            BAASN asn = await FindAsync(con => con.ERPASNKey == asnSyncDTO.ERPASNKey && con.TenantId == tenantId);
      if (asn != null)
      {
        asn = BAASNSyncDTO.MapToEntity(asnSyncDTO, asn);

        UpdateSystemFieldsByOpType(asn, OperationType.Update);


        // add sales quotation detail
        await UpdateAsync(asn, asn.ID, token);

        //Add sales quotation item detail.
        if (asnSyncDTO.ItemList != null && asnSyncDTO.ItemList.Count > 0)
        {
          await _asnItemDS.AddASNItemListAsync(asnSyncDTO.ItemList, tenantId, tenantUserId, asn.ID);
        }
      }
            return true;

        }


        #region Business Methods

        #region Get Methods

        /// <inhritdoc/>
        public async Task<IEnumerable<BusBAASNDTO>> GetASNListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, string asnType, CancellationToken cancellationToken = default(CancellationToken)) {
            return _asnRepositorty.GetASNListByBusinessTenantId(businessTenantId, listDateFilterDTO, asnType);
        }


        /// <inhritdoc/>
        public async Task<BusBAASNViewDTO> GetASNDetailByASNIdAsync(Guid asnId, string asnType, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _asnRepositorty.GetASNDetailByASNIdAsync(asnId, asnType, cancellationToken);
        }

        /// <inhritdoc/>
        public async Task<IEnumerable<CustBAASNDTO>> GetASNListByBusinessTenantIdAsyncForCust(Guid businessPartnerTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return _asnRepositorty.GetASNListByBusinessTenantIdForCust(businessPartnerTenantId, listDateFilterDTO);
        }


        /// <inhritdoc/>
        public async Task<CustBAASNViewDTO> GetASNDetailByASNIdAsyncForCust(Guid asnId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _asnRepositorty.GetASNDetailByASNIdAsyncForCust(asnId, cancellationToken);
        }

        #endregion

        #endregion

        #region Notification

        private void OnAddASNInIntegratedModeAsync(List<string> aSNERPKeyList, Guid businessTenantId) {
            for(int i = 0; i < aSNERPKeyList.Count; i++) {
                ASNNotificationDTO aSNNotificationDTO = _qNotificationDS.GetASNDetailByASNERPKeyAsync(aSNERPKeyList[i], businessTenantId, AppKeyEnum.cust.ToString()).Result;
                _businessEntityNotificationHandler.SendASNToBizUser(aSNNotificationDTO);

            }
        }

        private void OnUpdateASNInIntegratedModeAsync(List<string> aSNERPKeyList, Guid businessTenantId) {
            for(int i = 0; i < aSNERPKeyList.Count; i++) {
                ASNNotificationDTO aSNNotificationDTO = _qNotificationDS.GetASNDetailByASNERPKeyAsync(aSNERPKeyList[i], businessTenantId, AppKeyEnum.cust.ToString()).Result;
                _businessEntityNotificationHandler.SendUpdateASNNotificationToBizCustomerUserInIntegratedMode(aSNNotificationDTO);
            }
        }

        #endregion
    }
}
