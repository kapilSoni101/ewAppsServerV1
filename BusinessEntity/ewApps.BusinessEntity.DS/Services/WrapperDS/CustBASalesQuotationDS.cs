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
using ewApps.BusinessEntity.Common;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.UserSessionService;

namespace ewApps.BusinessEntity.DS {
    public class CustBASalesQuotationDS:ICustBASalesQuotationDS {

        #region Member Variables

        IBASalesQuotationDS _bASalesQuotationDS;
        IBASalesQuotationItemDS _bASalesQuotationItemDS;
        IBASalesQuotationAttachmentDS _bASalesQuotationAttachmentDS;
    IUserSessionManager _userSessionManager;
    IAppSyncServiceDS _syncServiceDS;

    //ICustBASalesQuotationItemDS _bASalesQuotationItemDS;

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="BusBASalesQuotationDS"/> class.
    /// </summary>
    /// <param name="bASalesQuotationDS">The delivery data service instance.</param>       
    public CustBASalesQuotationDS(IBASalesQuotationDS bASalesQuotationDS, IBASalesQuotationItemDS bASalesQuotationItemDS, IBASalesQuotationAttachmentDS bASalesQuotationAttachmentDS, IAppSyncServiceDS syncServiceDS, IUserSessionManager userSessionManager) {
            _bASalesQuotationDS = bASalesQuotationDS;
            _bASalesQuotationItemDS = bASalesQuotationItemDS;
            _bASalesQuotationAttachmentDS = bASalesQuotationAttachmentDS;
      _userSessionManager = userSessionManager;
      _syncServiceDS = syncServiceDS;
    }

        #endregion

        /// <inheritdoc/>
        public async Task<IEnumerable<CustBASalesQuotationDTO>> GetSalesQuotationListByPartnerTenantIdAsyncForCust(Guid businessPartnerTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _bASalesQuotationDS.GetSalesQuotationListByPartnerTenantIdAsyncForCust(businessPartnerTenantId, listDateFilterDTO, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<CustBASalesQuotationViewDTO> GetSalesQuotationDetailBySalesQuotationIdAsyncForCust(Guid salesQuotationId, CancellationToken cancellationToken = default(CancellationToken)) {
            CustBASalesQuotationViewDTO custBASalesQuotationViewDTO = new CustBASalesQuotationViewDTO();
            custBASalesQuotationViewDTO = await _bASalesQuotationDS.GetSalesQuotationDetailBySalesQuotationIdAsyncForCust(salesQuotationId, cancellationToken);

            if(custBASalesQuotationViewDTO != null) {
                custBASalesQuotationViewDTO.ItemList = (await _bASalesQuotationItemDS.GetSalesQuotationItemListBySalesQuotationId(salesQuotationId)).ToList();
                custBASalesQuotationViewDTO.AttachmentList = (await _bASalesQuotationAttachmentDS.GetSalesQuotationAttachmentListBySalesQuotationIdForCustAsync(salesQuotationId)).ToList();
            }

            return custBASalesQuotationViewDTO;
        }
    /// <inheritdoc/>
    public async Task AddSalesQuotationWithItem(CustBASalesQuotationAddDTO addDTO, CancellationToken cancellationToken = default(CancellationToken))
    {
      //SalesOrder 
      UserSession session = _userSessionManager.GetSession();

      // Map entity to order 
      //await _bASODS.AddSalesOrderAsyncForCust(addDTO, session.TenantId, session.TenantUserId);

      // Now add in SAP.
      BASalesQuotationSyncDTO syncDTO = BASalesQuotationSyncDTO.MapFromCustBASalesQuotationAddDTO(addDTO);
      //async Task<bool> PushSalesOrderDataInERPAsync(BASalesOrderSyncDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
      await _syncServiceDS.PushSalesQuotationDataInERPAsync(syncDTO, session.TenantId);

      //Pull Sales order from ERP
      // Preparing api calling process model.   
      PullERPDataReqDTO request = new PullERPDataReqDTO();
      request.SelectedBAEntityTypes = new List<int>() { (int)BAEntityEnum.SalesQuotation };
      request.TenantId = session.TenantId;
      await _syncServiceDS.PullERPDataAsync(request, cancellationToken);

    }

  }
}
