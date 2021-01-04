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

    /// <summary>
    ///  This class implements standard business logic and operations for BAContract entity.
    /// </summary>
    public class BAContractDS:BaseDS<BAContract>, IBAContractDS {

        #region Local variables

        IBAContractRepository _contractRepositorty;
        IBAContractItemDS _contractItemDS;
        IBAContractAttachmentDS _contractAttachmentDS;
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
        public BAContractDS(IBAContractRepository contractRepositorty, IBAContractItemDS contractItemDS, IBAContractAttachmentDS contractAttachmentDS, IBACustomerDS customerDS, IUnitOfWork unitOfWork, IQNotificationDS qNotificationDS, IBusinessEntityNotificationHandler businessEntityNotificationHandler) : base(contractRepositorty) {
            _contractRepositorty = contractRepositorty;
            _contractItemDS = contractItemDS;
            _contractAttachmentDS = contractAttachmentDS;
            _customerDS = customerDS;
            _unitOfWork = unitOfWork;
            _qNotificationDS = qNotificationDS;
            _businessEntityNotificationHandler = businessEntityNotificationHandler;
        }

        #endregion Constructor

        #region Get Methods

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBAContractDTO>> GetContractListByTenantIdAsync(Guid tenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return _contractRepositorty.GetContractListByTenantId(tenantId, listDateFilterDTO);
        }

        /// <summary>
        /// Gets the contract detail by business tenant id and contract identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <param name="contractId">The contract identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns contract detail that matches given business tenant  and contract id.</returns>
        public async Task<BusBAContractViewDTO> GetContractDetailByContractIdAsync(Guid businessTenantId, Guid contractId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _contractRepositorty.GetContractDetailByContractIdAsync(businessTenantId, contractId, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CustBAContractDTO>> GetContractListByTenantIdAsyncForCust(Guid tenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return _contractRepositorty.GetContractListByTenantIdForCust(tenantId, listDateFilterDTO);
        }

        /// <summary>
        /// Gets the contract detail by business tenant id and contract identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <param name="contractId">The contract identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns contract detail that matches given business tenant  and contract id.</returns>
        public async Task<CustBAContractViewDTO> GetContractDetailByContractIdAsyncForCust(Guid businessTenantId, Guid contractId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _contractRepositorty.GetContractDetailByContractIdAsyncForCust(businessTenantId, contractId, cancellationToken);
        }

     

        #endregion

        #region Add

        ///<inheritdoc/>
        public async Task AddContractListAsync(List<BAContractSyncDTO> contractList, Guid tenantId, Guid tenantUserId, bool isBulkInsert, CancellationToken token = default(CancellationToken)) {
            List<string> contractAddERPKeyList = new List<string>();
            List<string> contractUpdateERPKeyList = new List<string>();
            for(int i = 0; i < contractList.Count; i++) {
                if(isBulkInsert) {
                    bool oldData = await AddContractAsync(contractList[i], tenantId, tenantUserId);
                }
                else {
                    if(contractList[i].OpType.Equals("Inserted")) {
                        bool oldSalesQuotation = await AddUpdateContractAsync(contractList[i], tenantId, tenantUserId);
                        if(oldSalesQuotation == true) {
                            contractAddERPKeyList.Add(contractList[i].ERPContractKey);
                        }
                    }
                    else if(contractList[i].OpType.Equals("Modified")) {
                        contractUpdateERPKeyList.Add(contractList[i].ERPContractKey);
                        await AddUpdateContractAsync(contractList[i], tenantId, tenantUserId);
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
        private async Task<bool> AddContractAsync(BAContractSyncDTO contractSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken)) {
            BAContract contract = await FindAsync(con => con.ERPContractKey == contractSyncDTO.ERPContractKey && con.TenantId == tenantId);
            if(contract != null) {
                return false;
            }
            BACustomer customer = await _customerDS.FindAsync(cust => cust.ERPCustomerKey == contractSyncDTO.ERPCustomerKey && cust.TenantId == tenantId);
      if (customer == null)
      {
        return false;
      }
      contractSyncDTO.CustomerId = customer.ID;
            contract = BAContractSyncDTO.MapToEntity(contractSyncDTO);

            // UpdateSystemFieldsByOpType(salesOrder, OperationType.Add);
            Guid ContractId = Guid.NewGuid();
            contract.ID = ContractId;
            contract.CreatedBy = tenantUserId;//;// Session
            contract.UpdatedBy = tenantUserId;
            contract.CreatedOn = DateTime.UtcNow;
            contract.UpdatedOn = DateTime.UtcNow;
            contract.Deleted = false;
            contract.TenantId = tenantId;

            // add sales quotation detail
            await AddAsync(contract, token);

            //Add sales quotation item detail.
            if(contractSyncDTO.LineItems != null && contractSyncDTO.LineItems.Count > 0) {
                await _contractItemDS.AddContractItemListAsync(contractSyncDTO.LineItems, tenantId, tenantUserId, ContractId);
            }

            //Add customer address detail.
            if(contractSyncDTO.Attachments != null && contractSyncDTO.Attachments.Count > 0) {
                await _contractAttachmentDS.AddContractAttachmentListAsync(contractSyncDTO.Attachments, tenantId, tenantUserId, contract.ID);
            }


            return true;

        }
    private async Task<bool> AddUpdateContractAsync(BAContractSyncDTO contractSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {
      BAContract contract = await FindAsync(con => con.ERPContractKey == contractSyncDTO.ERPContractKey && con.TenantId == tenantId);
      if (contract != null)
      {
        await UpdateContractAsync(contractSyncDTO, tenantId, tenantUserId);
      }
      else
      {
        await AddContractAsync(contractSyncDTO, tenantId, tenantUserId);
      }
      return true;
    }

    #endregion Add

    /// <summary>
    /// Update Draft Delivery and its child data .
    /// </summary>
    /// <param name="contractSyncDTO"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task<bool> UpdateContractAsync(BAContractSyncDTO contractSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken)) {
            BAContract contract = await FindAsync(con => con.ERPContractKey == contractSyncDTO.ERPContractKey && con.TenantId == tenantId);
      if (contract != null)
      {
        contract = BAContractSyncDTO.MapToEntity(contractSyncDTO, contract);

        UpdateSystemFieldsByOpType(contract, OperationType.Update);


        // add sales quotation detail
        await UpdateAsync(contract, contract.ID, token);

        //Add sales quotation item detail.
        if (contractSyncDTO.LineItems != null && contractSyncDTO.LineItems.Count > 0)
        {
          await _contractItemDS.AddContractItemListAsync(contractSyncDTO.LineItems, tenantId, tenantUserId, contract.ID);
        }

        //Add customer address detail.
        if (contractSyncDTO.Attachments != null && contractSyncDTO.Attachments.Count > 0)
        {
          //await _contractAttachmentDS.AddContractAttachmentListAsync(contractSyncDTO.Attachments, tenantId, tenantUserId, contract.ID);
        }

      }
            return true;

        }

        #region Notification
        private void OnAddContractInIntegratedModeAsync(List<string> contractERPKeyList, Guid businessTenantId) {
            for(int i = 0; i < contractERPKeyList.Count; i++) {
                ContractNotificationDTO contractNotificationDTO = _qNotificationDS.GetContractDetailByContractERPKeyAsync(contractERPKeyList[i], businessTenantId, AppKeyEnum.cust.ToString()).Result;
                _businessEntityNotificationHandler.SendContractNotificationToBizUser(contractNotificationDTO);

            }
        }

        private void OnUpdateContractInIntegratedModeAsync(List<string> contractERPKeyList, Guid businessTenantId) {
            for(int i = 0; i < contractERPKeyList.Count; i++) {
                ContractNotificationDTO contractNotificationDTO = _qNotificationDS.GetContractDetailByContractERPKeyAsync(contractERPKeyList[i], businessTenantId, AppKeyEnum.cust.ToString()).Result;
                _businessEntityNotificationHandler.SendUpdateContractToBizCustomerUserInIntegratedMode(contractNotificationDTO);
            }
        }
        #endregion
    }
}
