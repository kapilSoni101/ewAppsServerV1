using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Common;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.BusinessEntity.QData;
using ewApps.Core.BaseService;
using ewApps.Core.UniqueIdentityGeneratorService;
using Serilog;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    /// Contains sales order required methods to add/update/delete operation.
    /// Also contains method for getting sales order.
    /// </summary>
    public class BASalesOrderDS:BaseDS<BASalesOrder>, IBASalesOrderDS {

        #region Local variables

        IBASalesOrderRepository _baSalesOrderRepository;
        IBASalesOrderItemDS _salesOrderItemDS;
        IBASalesOrderAttachmentDS _salesOrderAttachmentDS;
        IBACustomerDS _customerDS;
        IUnitOfWork _unitOfWork;
        IUniqueIdentityGeneratorDS _uniqueIdentityGeneratorDS;
        IQNotificationDS _qNotificationDS;
        IBusinessEntityNotificationHandler _businessEntityNotificationHandler;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// default constructor.
        /// </summary>
        /// <param name="baSalesOrderRepository"></param>
        public BASalesOrderDS(IQNotificationDS qNotificationDS, IUniqueIdentityGeneratorDS uniqueIdentityGeneratorDS,
        IBASalesOrderRepository baSalesOrderRepository, IBASalesOrderItemDS salesOrderItemDS, IBACustomerDS customerDS, 
        IBASalesOrderAttachmentDS salesOrderAttachmentDS, IUnitOfWork unitOfWork,IBusinessEntityNotificationHandler businessEntityNotificationHandler) : base(baSalesOrderRepository) {
            _baSalesOrderRepository = baSalesOrderRepository;
            _salesOrderItemDS = salesOrderItemDS;
            _salesOrderAttachmentDS = salesOrderAttachmentDS;
            _customerDS = customerDS;
            _unitOfWork = unitOfWork;
            _uniqueIdentityGeneratorDS = uniqueIdentityGeneratorDS;
            _qNotificationDS = qNotificationDS;
            _businessEntityNotificationHandler = businessEntityNotificationHandler;
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get sales order item list by teanntid.
        /// </summary>
        /// <param name="tenantId">Tenant unique id.</param>
        /// <param name="includeDeleted">return sales order with deleted items.</param>        
        /// <returns>return list of sales order entity.</returns>
        public async Task<List<BASalesOrder>> GetSalesOrderListByTenantIdAsync(Guid tenantId, bool includeDeleted, CancellationToken token = default(CancellationToken)) {
            return await _baSalesOrderRepository.GetSalesOrderListByTenantIdAsync(tenantId, includeDeleted, token);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBASalesOrderDTO>> GetSalesOrderListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken) {
            return _baSalesOrderRepository.GetSalesOrderListByBusinessTenantId(businessTenantId, listDateFilterDTO);
        }

        /// <inheritdoc/>
        public Task<BusBASalesOrderViewDTO> GetSalesOrderDetailBySOIdAsync(Guid businessTenantId, Guid soId, CancellationToken cancellationToken = default(CancellationToken)) {
            return _baSalesOrderRepository.GetSalesOrderDetailBySOIdAsync(businessTenantId, soId, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CustBASalesOrderDTO>> GetSalesOrderListByBusinessTenantIdAsyncForCust(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken) {
            return _baSalesOrderRepository.GetSalesOrderListByBusinessTenantIdForCust(businessTenantId, listDateFilterDTO);
        }

        /// <inheritdoc/>
        public Task<CustBASalesOrderViewDTO> GetSalesOrderDetailBySOIdAsyncForCust(Guid businessTenantId, Guid soId, CancellationToken cancellationToken = default(CancellationToken)) {
            return _baSalesOrderRepository.GetSalesOrderDetailBySOIdAsyncForCust(businessTenantId, soId, cancellationToken);
        }


        #endregion Get

        #region Add

        ///<inheritdoc/>
        public async Task AddSalesOrderListAsync(List<BASalesOrderSyncDTO> salesOrderList, Guid tenantId, Guid tenantUserId, bool isBulkInsert, CancellationToken token = default(CancellationToken)) {
            List<string> salesAddERPKeyList = new List<string>();
            List<string> salesUpdateERPKeyList = new List<string>();
            for(int i = 0; i < salesOrderList.Count; i++) {
                if(isBulkInsert) {
                     await AddSalesOrderAsync(salesOrderList[i], tenantId, tenantUserId);
                }
                else {

                    if(salesOrderList[i].OpType.Equals("Inserted")) {
                        bool oldData = await AddUpdateSalesOrderAsync(salesOrderList[i], tenantId, tenantUserId);
                        if(oldData) {
                            salesAddERPKeyList.Add(salesOrderList[i].ERPSalesOrderKey);
                        }
                    }
                    else if(salesOrderList[i].OpType.Equals("Modified")) {
                        salesUpdateERPKeyList.Add(salesOrderList[i].ERPSalesOrderKey);
                        await AddUpdateSalesOrderAsync(salesOrderList[i], tenantId, tenantUserId);
                    }
                }
            }
            //save data
            _unitOfWork.SaveAll();

            if(salesAddERPKeyList.Count > 0) {
             await   OnAddSOInIntegratedModeAsync(salesAddERPKeyList, tenantId, token);
            }
            if(salesUpdateERPKeyList.Count > 0) {
                // Add Update notification method
            }


        }

        /// <summary>
        /// add sales order and its child data .
        /// </summary>
        /// <param name="salesOrderSyncDTO"></param>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<bool> AddSalesOrderAsync(BASalesOrderSyncDTO salesOrderSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken)) {
            BASalesOrder salesOrder = await FindAsync(so => so.ERPSalesOrderKey == salesOrderSyncDTO.ERPSalesOrderKey && so.TenantId == tenantId);
            if(salesOrder != null) {
                return false;
            }
            BACustomer customer = await _customerDS.FindAsync(cust => cust.ERPCustomerKey == salesOrderSyncDTO.ERPCustomerKey && cust.TenantId == tenantId);
      if (customer == null)
      {
        return false;
      }
      salesOrderSyncDTO.CustomerID = customer.ID;
            salesOrder = BASalesOrderSyncDTO.MapToEntity(salesOrderSyncDTO);

            // UpdateSystemFieldsByOpType(salesOrder, OperationType.Add);

            salesOrder.ID = Guid.NewGuid();
            salesOrder.CreatedBy = tenantUserId;//;// Session
            salesOrder.UpdatedBy = tenantUserId;
            salesOrder.CreatedOn = DateTime.UtcNow;
            salesOrder.UpdatedOn = DateTime.UtcNow;
            salesOrder.Deleted = false;
            salesOrder.TenantId = tenantId;

            // add customer detail
            await AddAsync(salesOrder, token);

            //Add customer address detail.
            if(salesOrderSyncDTO.ItemList != null && salesOrderSyncDTO.ItemList.Count > 0) {
                await _salesOrderItemDS.AddSalesOrderItemListAsync(salesOrderSyncDTO.ItemList, tenantId, tenantUserId, salesOrder.ID);
            }

            //Add customer address detail.
            if(salesOrderSyncDTO.Attachments != null && salesOrderSyncDTO.Attachments.Count > 0) {
                await _salesOrderAttachmentDS.AddSalesOrderAttachmentListAsync(salesOrderSyncDTO.Attachments, tenantId, tenantUserId, salesOrder.ID);
            }


            return true;

        }
    /// <summary>
    /// add sales order and its child data .
    /// </summary>
    /// <param name="salesOrderSyncDTO"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task<bool> AddUpdateSalesOrderAsync(BASalesOrderSyncDTO salesOrderSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {
      BASalesOrder salesOrder = await FindAsync(so => so.ERPSalesOrderKey == salesOrderSyncDTO.ERPSalesOrderKey && so.TenantId == tenantId);

      if (salesOrder != null)
      {
        await UpdateSalesOrderAsync(salesOrderSyncDTO, tenantId, tenantUserId);
      }
      else
      {
        await AddSalesOrderAsync(salesOrderSyncDTO, tenantId, tenantUserId);
      }
      return true;
    }

    /// <summary>
    /// add sales order and its child data .
    /// </summary>
    /// <param name="salesOrderSyncDTO"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task<bool> UpdateSalesOrderAsync(BASalesOrderSyncDTO salesOrderSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken)) {
            BASalesOrder salesOrder = await FindAsync(so => so.ERPSalesOrderKey == salesOrderSyncDTO.ERPSalesOrderKey && so.TenantId == tenantId);

      if (salesOrder != null)
      {
        salesOrder = BASalesOrderSyncDTO.MapToEntity(salesOrderSyncDTO, salesOrder);


        UpdateSystemFieldsByOpType(salesOrder, OperationType.Update);

        // add customer detail
        await UpdateAsync(salesOrder, salesOrder.ID, token);

        //Add customer address detail.
        if (salesOrderSyncDTO.ItemList != null && salesOrderSyncDTO.ItemList.Count > 0)
        {
          await _salesOrderItemDS.AddSalesOrderItemListAsync(salesOrderSyncDTO.ItemList, tenantId, tenantUserId, salesOrder.ID);
        }

        //Add customer address detail.
        if (salesOrderSyncDTO.Attachments != null && salesOrderSyncDTO.Attachments.Count > 0)
        {
         // await _salesOrderAttachmentDS.AddSalesOrderAttachmentListAsync(salesOrderSyncDTO.Attachments, tenantId, tenantUserId, salesOrder.ID);
        }

      }
            return true;

        }

        #region CustSalesOrderMethods
        /*
              ///<inheritdoc/>
              public async Task AddSalesOrderListAsyncForCust(List<CustBASalesOrderAddDTO> salesOrderList, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken))
              {
                for (int i = 0; i < salesOrderList.Count; i++)
                {
                  await AddSalesOrderAsyncForCust(salesOrderList[i], tenantId, tenantUserId);
                }
              }
        */
        public async Task AddSalesOrderAsyncForCust(CustBASalesOrderAddDTO dto, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken)) {

            //BACustomer customer = await _customerDS.FindAsync(cust => cust.ERPCustomerKey == dto.ERPCustomerKey && cust.TenantId == tenantId);
            BASalesOrder salesOrder = new BASalesOrder();
            BACustomer customer = await _customerDS.FindAsync(cust => cust.ID == dto.CustomerId && cust.TenantId == tenantId);

            salesOrder = CustBASalesOrderAddDTO.MapToEntity(dto, salesOrder);
            salesOrder.Status = (int)SalesOrderStatusEnum.Open;
            salesOrder.StatusText = SalesOrderStatusEnum.Open.ToString();
            salesOrder.ERPConnectorKey = tenantId.ToString();
            salesOrder.ERPCustomerKey = customer.ERPCustomerKey;
            salesOrder.ERPSalesOrderKey = _uniqueIdentityGeneratorDS.GetIdentityNo(Guid.Empty, (int)BAEntityEnum.SalesOrder, "Ord", 10000).ToString();

            UpdateSystemFieldsByOpType(salesOrder, OperationType.Add);

            // add customer detail
            await AddAsync(salesOrder, token);

            //Add customer address detail.
            if(dto.SalesOrderItemList != null && dto.SalesOrderItemList.Count > 0) {
                await _salesOrderItemDS.AddSalesOrderItemListAsyncForCust(dto.SalesOrderItemList, tenantId, tenantUserId, salesOrder.ID);
            }

            //save data
            _unitOfWork.SaveAll();
        }

        public async Task UpdateSalesOrderAsyncForCust(CustBASalesOrderUpdateDTO dto, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken)) {
            //BACustomer customer = await _customerDS.FindAsync(cust => cust.ERPCustomerKey == dto.ERPCustomerKey && cust.TenantId == tenantId);
            BASalesOrder salesOrder = new BASalesOrder();
            BACustomer customer = await _customerDS.FindAsync(cust => cust.ID == dto.CustomerId && cust.TenantId == tenantId);
            salesOrder = CustBASalesOrderUpdateDTO.MapToEntity(dto, salesOrder);
            //salesOrder.Status = (int)SalesOrderStatusEnum.Open;
            //salesOrder.StatusText = SalesOrderStatusEnum.Open.ToString();
            salesOrder.ERPConnectorKey = tenantId.ToString();
            salesOrder.ERPCustomerKey = customer.ERPCustomerKey;

            UpdateSystemFieldsByOpType(salesOrder, OperationType.Update);

            // add customer detail
            await UpdateAsync(salesOrder, token);

            //Add customer address detail.
            if(dto.SalesOrderItemList != null && dto.SalesOrderItemList.Count > 0) {
                await _salesOrderItemDS.UpdateSalesOrderItemListAsyncForCust(dto.SalesOrderItemList, tenantId, tenantUserId, salesOrder.ID);
            }

            //save data
            _unitOfWork.SaveAll();
        }

        #endregion CustSalesOrderMethods

        #endregion Add

        #region Notification

        private async Task OnAddSOInIntegratedModeAsync(List<string> newSOERPKeyList, Guid businessTenantId, CancellationToken cancellationToken =default(CancellationToken)) {
            for(int i = 0; i < newSOERPKeyList.Count; i++) {
               
                    SONotificationDTO aRInvoiceNotificationDTO = await _qNotificationDS.GetSODetailBySOERPKeyAsync(newSOERPKeyList[i], businessTenantId, cancellationToken);
                if(aRInvoiceNotificationDTO != null) {
                    await _businessEntityNotificationHandler.SendAddSalesOrderToBizUserInIntegratedModeAsync(aRInvoiceNotificationDTO);
                }
                else {
                    Log.Error("Erro", newSOERPKeyList[i].ToString());
                }
               
            }
        }



        #endregion

    }
}

