using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;

namespace ewApps.BusinessEntity.DS {

    public class CustBAItemMasterDS:ICustBAItemMasterDS {

        #region Local Members

        private IBAItemMasterDS _itemMasterDS;

        private IBAItemAttachmentDS _itemAttachmentDS;

        #endregion Local Members 

        #region Constructor

        public CustBAItemMasterDS(IBAItemMasterDS itemMasterDS, IBAItemAttachmentDS itemAttachmentDS) {
            _itemMasterDS = itemMasterDS;
            _itemAttachmentDS = itemAttachmentDS;
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Gets the item master list by bus tenant identifier asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<IEnumerable<CustBAItemMasterDTO>> GetItemMasterListByBusTenantIdAsyncForCust(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            IEnumerable<CustBAItemMasterDTO> itemMasterList = await _itemMasterDS.GetItemMasterListByBusTenantIdAsyncForCust(tenantId, token);
            return itemMasterList;
        }


        /// <summary>
        /// Gets the item master list by bus tenant identifier asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<CustBAItemMasterViewDTO> GetItemMasterByBusTenantIdAndItemIdAsyncForCust(Guid tenantId, Guid itemId, CancellationToken token = default(CancellationToken)) {
            CustBAItemMasterViewDTO itemMaster = await _itemMasterDS.GetItemMasterByBusTenantIdAndItemIdAsyncForCust(tenantId, itemId, token);
            itemMaster.AttachmentList = (await _itemAttachmentDS.GetItemAttachmentListByItemIdForCustAsync(itemId, token)).ToList();
            return itemMaster;
        }

        #endregion Get
    }

}
