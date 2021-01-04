using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {
    /// <summary>
    /// Contains sales quotation required methods to add/update/delete operation.
    /// Also contains method for getting sales quotation.
    /// </summary>
    public class BASalesQuotationDS:BaseDS<BASalesQuotation>, IBASalesQuotationDS {

        #region Local variables

        IBASalesQuotationRepository _baSalesQuotationRepository;
        IBASalesQuotationItemDS _salesQuotationItemDS;
        IBASalesQuotationAttachmentDS _salesQuotationAttachmentDS;
        IBACustomerDS _customerDS;
        IUnitOfWork _unitOfWork;
        //   ICustBASalesQuotationDS _custSalesQuotationDS;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// default constructor.
        /// </summary>
        /// <param name="baSalesQuotationRepository"></param>
        /// <param name="salesQuotationItemDS"></param>
        /// <param name="unitOfWork"></param>
        public BASalesQuotationDS(IBASalesQuotationRepository baSalesQuotationRepository, IBASalesQuotationItemDS salesQuotationItemDS, IBASalesQuotationAttachmentDS salesQuotationAttachmentDS, IBACustomerDS customerDS, IUnitOfWork unitOfWork) : base(baSalesQuotationRepository) {
            _baSalesQuotationRepository = baSalesQuotationRepository;
            _salesQuotationItemDS = salesQuotationItemDS;
            _salesQuotationAttachmentDS = salesQuotationAttachmentDS;
            _customerDS = customerDS;
            _unitOfWork = unitOfWork;
            //   _custSalesQuotationDS = custSalesQuotationDS;
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get BASales Quotation list by teanntid.
        /// </summary>
        /// <param name="tenantId">Tenant unique id.</param>
        /// <param name="includeDeleted">return BASalesQuotation with deleted items.</param>        
        /// <returns>return list of BASalesQuotation entity.</returns>
        public async Task<List<BASalesQuotation>> GetSalesQuotationListByTenantIdAsync(Guid tenantId, ListDateFilterDTO listDateFilterDTO, bool includeDeleted, CancellationToken token = default(CancellationToken)) {
            return await _baSalesQuotationRepository.GetSalesQuotationListByTenantIdAsync(tenantId, listDateFilterDTO, includeDeleted, token);
        }

        /// <summary>
        /// Get BASales Quotation by ERP unique key.
        /// </summary>
        /// <param name="erpSalesQuotationKey">SalesQuotationKey is a ERP unique key.</param>
        /// <param name="token"></param>
        /// <returns>return BASalesQuotation entity.</returns>
        public async Task<BASalesQuotation> GetSalesQuotationByERPKeyAsync(string erpSalesQuotationKey, CancellationToken token = default(CancellationToken)) {
            return await _baSalesQuotationRepository.GetSalesQuotationByERPKeyAsync(erpSalesQuotationKey, token);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBASalesQuotationDTO>> GetSalesQuotationListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken) {
            return _baSalesQuotationRepository.GetSalesQuotationListByBusinessTenantId(businessTenantId, listDateFilterDTO);
        }

        /// <inheritdoc/>
        public async Task<BusBASalesQuotationViewDTO> GetSalesQuotationDetailBySalesQuotationIdAsync(Guid salesQuotationId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _baSalesQuotationRepository.GetSalesQuotationDetailBySalesQuotationIdAsync(salesQuotationId, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CustBASalesQuotationDTO>> GetSalesQuotationListByPartnerTenantIdAsyncForCust(Guid businessPartnerTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken) {
            return _baSalesQuotationRepository.GetSalesQuotationListByPartnerTenantIdForCust(businessPartnerTenantId, listDateFilterDTO);

            //GetSalesQuotationListByBusinessTenantIdAsyncForCust(businessTenantId);
        }

        /// <inheritdoc/>
        public async Task<CustBASalesQuotationViewDTO> GetSalesQuotationDetailBySalesQuotationIdAsyncForCust(Guid salesQuotationId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _baSalesQuotationRepository.GetSalesQuotationDetailBySalesQuotationIdAsyncForCust(salesQuotationId, cancellationToken);
        }

        #endregion Get

        #region Add

        ///<inheritdoc/>
        public async Task AddSalesQuotationListAsync(List<BASalesQuotationSyncDTO> salesQuotationList, Guid tenantId, Guid tenantUserId, bool isBulkInsert, CancellationToken token = default(CancellationToken)) {
            List<string> salesQuotationAddERPKeyList = new List<string>();
            List<string> salesQuotationUpdateERPKeyList = new List<string>();
            for(int i = 0; i < salesQuotationList.Count; i++) {
               // await AddSalesQuotationAsync(salesQuotationList[i], tenantId, tenantUserId);
                if(isBulkInsert) {
                    bool oldSalesQuotation = await AddSalesQuotationAsync(salesQuotationList[i], tenantId, tenantUserId);
                }
                else {
                    if(salesQuotationList[i].OpType.Equals("Inserted")) {
                        bool oldSalesQuotation = await AddUpdateSalesQuotationAsync(salesQuotationList[i], tenantId, tenantUserId);
                        if(oldSalesQuotation == true) {
                            salesQuotationAddERPKeyList.Add(salesQuotationList[i].ERPSalesQuotationKey);
                        }
                    }
                    else if(salesQuotationList[i].OpType.Equals("Modified")) {
                        salesQuotationUpdateERPKeyList.Add(salesQuotationList[i].ERPSalesQuotationKey);
                        await AddUpdateSalesQuotationAsync(salesQuotationList[i], tenantId, tenantUserId);
                    }
                }

            }
        }

        /// <summary>
        /// add sales quotation and its child data .
        /// </summary>
        /// <param name="salesQuotationSyncDTO"></param>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<bool> AddSalesQuotationAsync(BASalesQuotationSyncDTO salesQuotationSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken)) {
            BASalesQuotation salesQuotation = await FindAsync(sq => sq.ERPSalesQuotationKey == salesQuotationSyncDTO.ERPSalesQuotationKey && sq.TenantId == tenantId);
            if(salesQuotation != null) {
                return false;
            }
            BACustomer customer = await _customerDS.FindAsync(cust => cust.ERPCustomerKey == salesQuotationSyncDTO.ERPCustomerKey && cust.TenantId == tenantId);
      if (customer == null)
      {
        return false;
      }
      salesQuotationSyncDTO.CustomerId = customer.ID;
            salesQuotation = BASalesQuotationSyncDTO.MapToEntity(salesQuotationSyncDTO);

            // UpdateSystemFieldsByOpType(salesOrder, OperationType.Add);
            Guid salesQuotationId = Guid.NewGuid();
            salesQuotation.ID = salesQuotationId;
            salesQuotation.CreatedBy = tenantUserId;//;// Session
            salesQuotation.UpdatedBy = tenantUserId;
            salesQuotation.CreatedOn = DateTime.UtcNow;
            salesQuotation.UpdatedOn = DateTime.UtcNow;
            salesQuotation.Deleted = false;
            salesQuotation.TenantId = tenantId;

            // add sales quotation detail
            await AddAsync(salesQuotation, token);

            //Add sales quotation item detail.
            if(salesQuotationSyncDTO.ItemList != null && salesQuotationSyncDTO.ItemList.Count > 0) {
                await _salesQuotationItemDS.AddSalesQuotationItemListAsync(salesQuotationSyncDTO.ItemList, tenantId, tenantUserId, salesQuotationId);
            }

            //Add customer address detail.
            if(salesQuotationSyncDTO.Attachments != null && salesQuotationSyncDTO.Attachments.Count > 0) {
                await _salesQuotationAttachmentDS.AddSalesQuotationAttachmentListAsync(salesQuotationSyncDTO.Attachments, tenantId, tenantUserId, salesQuotationId);
            }
            //save data
            _unitOfWork.SaveAll();

            return true;

        }
    private async Task<bool> AddUpdateSalesQuotationAsync(BASalesQuotationSyncDTO salesQuotationSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {
      BASalesQuotation salesQuotation = await FindAsync(sq => sq.ERPSalesQuotationKey == salesQuotationSyncDTO.ERPSalesQuotationKey && sq.TenantId == tenantId);
      if (salesQuotation != null)
      {
        await UpdateSalesQuotationAsync(salesQuotationSyncDTO, tenantId, tenantUserId);
      }
      else
      {
        await AddSalesQuotationAsync(salesQuotationSyncDTO, tenantId, tenantUserId);
      }
      return true;
    }

    #endregion Add


    /// <summary>
    /// add sales quotation and its child data .
    /// </summary>
    /// <param name="salesQuotationSyncDTO"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task<bool> UpdateSalesQuotationAsync(BASalesQuotationSyncDTO salesQuotationSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken)) {
            BASalesQuotation salesQuotation = await FindAsync(sq => sq.ERPSalesQuotationKey == salesQuotationSyncDTO.ERPSalesQuotationKey && sq.TenantId == tenantId);
      if (salesQuotation != null)
      {
        salesQuotation = BASalesQuotationSyncDTO.MapToEntity(salesQuotationSyncDTO, salesQuotation);

        UpdateSystemFieldsByOpType(salesQuotation, OperationType.Update);


        // add sales quotation detail
        await UpdateAsync(salesQuotation, salesQuotation.ID, token);

        //Add sales quotation item detail.
        if (salesQuotationSyncDTO.ItemList != null && salesQuotationSyncDTO.ItemList.Count > 0)
        {
          await _salesQuotationItemDS.AddSalesQuotationItemListAsync(salesQuotationSyncDTO.ItemList, tenantId, tenantUserId, salesQuotation.ID);
        }

        //Add customer address detail.
        if (salesQuotationSyncDTO.Attachments != null && salesQuotationSyncDTO.Attachments.Count > 0)
        {
          //   await _salesQuotationAttachmentDS.AddSalesQuotationAttachmentListAsync(salesQuotationSyncDTO.Attachments, tenantId, tenantUserId, salesQuotation.ID);
        }
        //save data
        _unitOfWork.SaveAll();
      }
            return true;

        }

    }
}
