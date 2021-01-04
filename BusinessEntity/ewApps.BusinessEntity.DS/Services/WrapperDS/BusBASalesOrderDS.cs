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
    /// This class implements <see cref="BASalesOrder"/> entity operations for business.
    /// </summary>
    /// <seealso cref="ewApps.BusinessEntity.DS.IBusBASalesOrderDS" />
    public class BusBASalesOrderDS:IBusBASalesOrderDS {
        #region Member Variables

        IBASalesOrderDS _bASODS;
        IBASalesOrderItemDS _bASalesOrderItemDS;
        IBASalesOrderAttachmentDS _bASalesOrderAttachmentDS;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusBASalesOrderDS"/> class.
        /// </summary>
        /// <param name="bASODS">The sales order data service instance.</param>
        /// <param name="bASalesOrderItemDS">The sales order item data service instance.</param>
        public BusBASalesOrderDS(IBASalesOrderDS bASODS, IBASalesOrderItemDS bASalesOrderItemDS, IBASalesOrderAttachmentDS bASalesOrderAttachmentDS) {
            _bASODS = bASODS;
            _bASalesOrderItemDS = bASalesOrderItemDS;
            _bASalesOrderAttachmentDS = bASalesOrderAttachmentDS;
        }

        #endregion

        #region Get Methods

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBASalesOrderDTO>> GetSalesOrderListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _bASODS.GetSalesOrderListByBusinessTenantIdAsync(businessTenantId, listDateFilterDTO, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<BusBASalesOrderViewDTO> GetSalesOrderDetailBySOIdAsync(Guid businessTenantId, Guid soId, CancellationToken cancellationToken = default(CancellationToken)) {
            BusBASalesOrderViewDTO busBASalesOrderViewDTO = new BusBASalesOrderViewDTO();
            busBASalesOrderViewDTO = await _bASODS.GetSalesOrderDetailBySOIdAsync(businessTenantId, soId, cancellationToken);
            if(busBASalesOrderViewDTO != null) {
                busBASalesOrderViewDTO.SalesOrderItemList = await _bASalesOrderItemDS.GetSalesOrderItemListBySOIdAsync(soId, cancellationToken);
                busBASalesOrderViewDTO.AttachmentList = (await _bASalesOrderAttachmentDS.GetSalesOrderAttachmentListByIdAsync(soId, cancellationToken)).ToList();
            }
            return busBASalesOrderViewDTO;
        }

        #endregion


    }
}
