using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    /// This class implements <see cref="BADelivery"/> entity operations for business.
    /// </summary>
    /// <seealso cref="ewApps.BusinessEntity.DS.IBusBADeliveryDS" />
    public class CustBADeliveryDS : ICustBADeliveryDS
  {

        #region Member Variables

        IBADeliveryDS _bADeliveryDS;
        IBADeliveryItemDS _bADeliveryItemDS;
        IBADeliveryAttachmentDS _bABADeliveryAttachmentDS;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusBADeliveryDS"/> class.
        /// </summary>
        /// <param name="bADeliveryDS">The delivery data service instance.</param>
        /// <param name="bADeliveryItemDS">The delivery item data service instance.</param>
        public CustBADeliveryDS(IBADeliveryDS bADeliveryDS, IBADeliveryItemDS bADeliveryItemDS, IBADeliveryAttachmentDS bABADeliveryAttachmentDS) {
            _bADeliveryDS = bADeliveryDS;
            _bADeliveryItemDS = bADeliveryItemDS;
            _bABADeliveryAttachmentDS = bABADeliveryAttachmentDS;
        }

        #endregion

        #region Get Methods

        /// <inheritdoc/>
        public async Task<IEnumerable<CustBADeliveryDTO>> GetDeliveryListByBusinessTenantIdAsyncForCust(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _bADeliveryDS.GetDeliveryListByBusinessTenantIdAsyncForCust(businessTenantId,listDateFilterDTO, cancellationToken);
        }

        ///// <inheritdoc/>
        //public async Task<IEnumerable<string>> GetDeliveryItemNameListByDeliveryIdAsyncForCust(Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken)) {
        //    return await _bADeliveryItemDS.GetDeliveryItemNameListByDeliveryIdAsync(deliveryId, cancellationToken);
        //}

        /// <inheritdoc/>
        public async Task<CustBADeliveryViewDTO> GetDeliveryDetailByDeliveryIdAsyncForCust(Guid businessTenantId, Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken)) {
            CustBADeliveryViewDTO custBADeliverViewDTO = new CustBADeliveryViewDTO();
            custBADeliverViewDTO = await _bADeliveryDS.GetDeliveryDetailByDeliveryIdAsyncForCust(businessTenantId,deliveryId, cancellationToken);
            if(custBADeliverViewDTO != null) {
                custBADeliverViewDTO.DeliveryItemList = (await _bADeliveryItemDS.GetDeliveryItemListByDeliveryIdForCust(deliveryId)).ToList();

                custBADeliverViewDTO.DeliveryAttachmentList = (await _bABADeliveryAttachmentDS.GetDeliveryAttachmentListByDeliveryIdForCustAsync(deliveryId)).ToList();
            }
            return custBADeliverViewDTO;
        }

        #endregion
    }
}
