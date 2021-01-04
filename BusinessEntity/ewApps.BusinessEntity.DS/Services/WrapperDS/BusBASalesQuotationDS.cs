/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 30 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 30 September 2019
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DS {
  public class BusBASalesQuotationDS : IBusBASalesQuotationDS {

        #region Member Variables

        IBASalesQuotationDS _bASalesQuotationDS;
        IBASalesQuotationItemDS _bASalesQuotationItemDS;
        IBASalesQuotationAttachmentDS _bASalesQuotationAttachmentDS;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusBASalesQuotationDS"/> class.
        /// </summary>
        /// <param name="bASalesQuotationDS">The delivery data service instance.</param>       
        public BusBASalesQuotationDS(IBASalesQuotationDS bASalesQuotationDS, IBASalesQuotationItemDS bASalesQuotationItemDS, IBASalesQuotationAttachmentDS bASalesQuotationAttachmentDS) {
            _bASalesQuotationDS = bASalesQuotationDS;
            _bASalesQuotationItemDS = bASalesQuotationItemDS;
            _bASalesQuotationAttachmentDS = bASalesQuotationAttachmentDS;
        }

        #endregion

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBASalesQuotationDTO>> GetSalesQuotationListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _bASalesQuotationDS.GetSalesQuotationListByBusinessTenantIdAsync(businessTenantId, listDateFilterDTO, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<BusBASalesQuotationViewDTO> GetSalesQuotationDetailBySalesQuotationIdAsync(Guid salesQuotationId, CancellationToken cancellationToken = default(CancellationToken)) {
            BusBASalesQuotationViewDTO busBASalesQuotationViewDTO = new BusBASalesQuotationViewDTO();
            busBASalesQuotationViewDTO = await _bASalesQuotationDS.GetSalesQuotationDetailBySalesQuotationIdAsync(salesQuotationId, cancellationToken);
            if(busBASalesQuotationViewDTO != null) {
                busBASalesQuotationViewDTO.ItemList = (await _bASalesQuotationItemDS.GetSalesQuotationItemListBySalesQuotationId(salesQuotationId)).ToList();
                busBASalesQuotationViewDTO.AttachmentList = (await _bASalesQuotationAttachmentDS.GetSalesQuotationAttachmentListBySalesQuotationIdAsync(salesQuotationId, cancellationToken)).ToList();
            }
            return busBASalesQuotationViewDTO;
        }

    }
}
