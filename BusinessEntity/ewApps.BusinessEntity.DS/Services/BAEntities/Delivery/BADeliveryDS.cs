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
    /// This class manages the CRUD operations related methods and business logics for delviery entity.
    /// </summary>
    public class BADeliveryDS:BaseDS<BADelivery>, IBADeliveryDS {

        IBADeliveryRepository _deliveryRepo;
        IBADeliveryItemDS _deliveryItemDS;
        IBADeliveryAttachmentDS _deliveryAttachmentDS;
        IBACustomerDS _customerDS;
        IUnitOfWork _unitOfWork;
        IQNotificationDS _qNotificationDS;
        IBusinessEntityNotificationHandler _businessEntityNotificationHandler;

        #region Constructor

        /// <summary>
        /// Public constructor for deliveryDS class.
        /// </summary>
        /// <param name="deliveryRepo">Repository class dependancy for delviery.</param>
        /// <param name="deliveryItemDS">data class dependancy for delviery item.</param>
        /// <param name="customerDS">data class dependancy for customer.</param>
        /// <param name="unitOfWork"> dependancy for IUnitOfWork.</param>
        public BADeliveryDS(IBADeliveryRepository deliveryRepo, IBADeliveryItemDS deliveryItemDS, IBADeliveryAttachmentDS deliveryAttachmentDS, IBACustomerDS customerDS, IUnitOfWork unitOfWork, IQNotificationDS qNotificationDS, IBusinessEntityNotificationHandler businessEntityNotificationHandler) : base(deliveryRepo) {
            _deliveryRepo = deliveryRepo;
            _deliveryItemDS = deliveryItemDS;
            _deliveryAttachmentDS = deliveryAttachmentDS;
            _customerDS = customerDS;
            _unitOfWork = unitOfWork;
            _qNotificationDS = qNotificationDS;
            _businessEntityNotificationHandler = businessEntityNotificationHandler;
        }

        #endregion Constructor

        #region Add

        ///<inheritdoc/>
        public async Task AddDeliveryListAsync(List<BADeliverySyncDTO> deliveryList, Guid tenantId, Guid tenantUserId, bool isBulkInsert, CancellationToken token = default(CancellationToken)) {
            List<string> deliveryAddERPKeyList = new List<string>();
            List<string> deliveryUpdateERPKeyList = new List<string>();
            for(int i = 0; i < deliveryList.Count; i++) {
                if(isBulkInsert) {
                    bool oldData = await AddDeliveryListAsync(deliveryList[i], tenantId, tenantUserId);
                }
                else {
                    if(deliveryList[i].OpType.Equals("Inserted")) {
                        bool oldSalesQuotation = await AddUpdateDeliveryListAsync(deliveryList[i], tenantId, tenantUserId);
                        if(oldSalesQuotation == true) {
                            deliveryAddERPKeyList.Add(deliveryList[i].ERPDeliveryKey);
                        }
                    }
                    else if(deliveryList[i].OpType.Equals("Modified")) {
                        deliveryUpdateERPKeyList.Add(deliveryList[i].ERPDeliveryKey);
                        await AddUpdateDeliveryListAsync(deliveryList[i], tenantId, tenantUserId);
                    }
                }
            }

            //save data
            _unitOfWork.SaveAll();

        }

        /// <summary>
        /// add delivery and its child data .
        /// </summary>
        /// <param name="deliverySyncDTO"></param>
        /// <param name="tenantId"></param>
        /// <param name="tenantUserId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<bool> AddDeliveryListAsync(BADeliverySyncDTO deliverySyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken)) {
            // Validate Duplicate delivery.
            BADelivery delivery = await FindAsync(del => del.ERPDeliveryKey == deliverySyncDTO.ERPDeliveryKey && del.TenantId == tenantId);
            if(delivery != null) {
                return false;
            }
            // get customer by ERPCustomer key.
            BACustomer customer = await _customerDS.FindAsync(cust => cust.ERPCustomerKey == deliverySyncDTO.ERPCustomerKey && cust.TenantId == tenantId);
            if(customer != null) {
                deliverySyncDTO.CustomerID = customer.ID;
            }
            delivery = BADeliverySyncDTO.MapToEntity(deliverySyncDTO);

            // UpdateSystemFieldsByOpType(salesOrder, OperationType.Add);
            Guid deliveryId = Guid.NewGuid();
            delivery.ID = deliveryId;
            delivery.CreatedBy = tenantUserId;//;// Session
            delivery.UpdatedBy = tenantUserId;
            delivery.CreatedOn = DateTime.UtcNow;
            delivery.UpdatedOn = DateTime.UtcNow;
            delivery.Deleted = false;
            delivery.TenantId = tenantId;

            // add delivery detail
            await AddAsync(delivery, token);

            //Add delivery item detail.
            if(deliverySyncDTO.DeliveryItemList != null && deliverySyncDTO.DeliveryItemList.Count > 0) {
                await _deliveryItemDS.AddDeliveryItemListAsync(deliverySyncDTO.DeliveryItemList, tenantId, tenantUserId, deliveryId);
            }

            //Add customer address detail.
            if(deliverySyncDTO.Attachments != null && deliverySyncDTO.Attachments.Count > 0) {
                await _deliveryAttachmentDS.AddDeliveryAttachmentListAsync(deliverySyncDTO.Attachments, tenantId, tenantUserId, deliveryId);
            }

            return true;

        }
    private async Task<bool> AddUpdateDeliveryListAsync(BADeliverySyncDTO deliverySyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {
      // Validate Duplicate delivery.
      BADelivery delivery = await FindAsync(del => del.ERPDeliveryKey == deliverySyncDTO.ERPDeliveryKey && del.TenantId == tenantId);

      if (delivery != null)
      {
        await UpdateDeliveryListAsync(deliverySyncDTO, tenantId, tenantUserId);
      }
      else
      {
        await AddDeliveryListAsync(deliverySyncDTO, tenantId, tenantUserId);
      }
      return true;
    }

    #endregion Add
    /// <summary>
    /// Update delivery and its child data .
    /// </summary>
    /// <param name="deliverySyncDTO"></param>
    /// <param name="tenantId"></param>
    /// <param name="tenantUserId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task<bool> UpdateDeliveryListAsync(BADeliverySyncDTO deliverySyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken)) {
            // Validate Duplicate delivery.
            BADelivery delivery = await FindAsync(del => del.ERPDeliveryKey == deliverySyncDTO.ERPDeliveryKey && del.TenantId == tenantId);

      if (delivery != null)
      {
        delivery = BADeliverySyncDTO.MapToEntity(deliverySyncDTO, delivery);

        UpdateSystemFieldsByOpType(delivery, OperationType.Update);

        // add delivery detail
        await UpdateAsync(delivery, delivery.ID, token);

        //Add delivery item detail.
        if (deliverySyncDTO.DeliveryItemList != null && deliverySyncDTO.DeliveryItemList.Count > 0)
        {
          await _deliveryItemDS.AddDeliveryItemListAsync(deliverySyncDTO.DeliveryItemList, tenantId, tenantUserId, delivery.ID);
        }

        //Add customer address detail.
        if (deliverySyncDTO.Attachments != null && deliverySyncDTO.Attachments.Count > 0)
        {
          // await _deliveryAttachmentDS.AddDeliveryAttachmentListAsync(deliverySyncDTO.Attachments, tenantId, tenantUserId, delivery.ID);
        }
      }
            return true;

        }

        #region Business Methods

        #region Get

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBADeliveryDTO>> GetDeliveryListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken) {
            return _deliveryRepo.GetDeliveryListByBusinessTenantId(businessTenantId, listDateFilterDTO);
        }


        /// <summary>
        /// Gets the delivery detail with items by delivery identifier.
        /// </summary>
        /// <param name="deliveryId">The delivery identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns delivery detail with items that matches provided delivery id.</returns>
        public async Task<BusBADeliveryViewDTO> GetDeliveryDetailByDeliveryIdAsync(Guid businessTenantId, Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _deliveryRepo.GetDeliveryDetailByDeliveryIdAsync(businessTenantId, deliveryId, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CustBADeliveryDTO>> GetDeliveryListByBusinessTenantIdAsyncForCust(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken) {
            return _deliveryRepo.GetDeliveryListByBusinessTenantIdForCust(businessTenantId,listDateFilterDTO);
        }

        /// <summary>
        /// Gets the delivery detail with items by delivery identifier.
        /// </summary>
        /// <param name="deliveryId">The delivery identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns delivery detail with items that matches provided delivery id.</returns>
        public async Task<CustBADeliveryViewDTO> GetDeliveryDetailByDeliveryIdAsyncForCust(Guid businessTenantId, Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _deliveryRepo.GetDeliveryDetailByDeliveryIdAsyncForCust(businessTenantId, deliveryId, cancellationToken);
        }


        #endregion

        #endregion

        #region Notification

        private void OnAddASNInIntegratedModeAsync(List<string> deliveryKeyList, Guid businessTenantId) {
            for(int i = 0; i < deliveryKeyList.Count; i++) {
                DeliveryNotificationDTO deliveryNotificationDTO = _qNotificationDS.GetDeliveryDetailByDeliveryERPKeyAsync(deliveryKeyList[i], businessTenantId, AppKeyEnum.cust.ToString()).Result;
                _businessEntityNotificationHandler.SendDeliveryNotificationToBizUser(deliveryNotificationDTO);

            }
        }

        private void OnUpdateASNInIntegratedModeAsync(List<string> deliveryKeyList, Guid businessTenantId) {
            for(int i = 0; i < deliveryKeyList.Count; i++) {
                DeliveryNotificationDTO deliveryNotificationDTO = _qNotificationDS.GetDeliveryDetailByDeliveryERPKeyAsync(deliveryKeyList[i], businessTenantId, AppKeyEnum.cust.ToString()).Result;
                //_businessEntityNotificationHandler.SendUpdateASNNotificationToBizCustomerUserInIntegratedMode(aSNNotificationDTO);
            }
        }

        #endregion

    }
}
