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
    public class BusBADeliveryDS:IBusBADeliveryDS {

        #region Member Variables

        IBADeliveryDS _bADeliveryDS;
        IBADeliveryItemDS _bADeliveryItemDS;
        IBADeliveryAttachmentDS _bADeliveryAttachmentDS;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusBADeliveryDS"/> class.
        /// </summary>
        /// <param name="bADeliveryDS">The delivery data service instance.</param>
        /// <param name="bADeliveryItemDS">The delivery item data service instance.</param>
        /// <param name="bADeliveryAttachmentDS">The dleivery attachment data service instace.</param>
        public BusBADeliveryDS(IBADeliveryDS bADeliveryDS, IBADeliveryItemDS bADeliveryItemDS, IBADeliveryAttachmentDS bADeliveryAttachmentDS) {
            _bADeliveryDS = bADeliveryDS;
            _bADeliveryItemDS = bADeliveryItemDS;
            _bADeliveryAttachmentDS = bADeliveryAttachmentDS;
        }

        #endregion

        #region Get Methods

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBADeliveryDTO>> GetDeliveryListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _bADeliveryDS.GetDeliveryListByBusinessTenantIdAsync(businessTenantId, listDateFilterDTO, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string>> GetDeliveryItemNameListByDeliveryIdAsync(Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _bADeliveryItemDS.GetDeliveryItemNameListByDeliveryIdAsync(deliveryId, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<BusBADeliveryViewDTO> GetDeliveryDetailByDeliveryIdAsync(Guid businessTenantId, Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken)) {
            BusBADeliveryViewDTO busBADeliverViewDTO = new BusBADeliveryViewDTO();
            busBADeliverViewDTO = await _bADeliveryDS.GetDeliveryDetailByDeliveryIdAsync(businessTenantId, deliveryId, cancellationToken);
            if(busBADeliverViewDTO != null) {
                busBADeliverViewDTO.DeliveryItemList = (await _bADeliveryItemDS.GetDeliveryItemListByDeliveryId(deliveryId, cancellationToken)).ToList();

                busBADeliverViewDTO.DeliveryAttachmentList = (await _bADeliveryAttachmentDS.GetDeliveryAttachmentListByDeliveryIdAsync(deliveryId, cancellationToken)).ToList();
            }
            return busBADeliverViewDTO;
        }

        #endregion
    }
}
