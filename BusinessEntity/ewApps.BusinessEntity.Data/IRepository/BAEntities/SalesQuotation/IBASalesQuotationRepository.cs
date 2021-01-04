// Hari sir comment
/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: Souarbh Agrawal
 * Last Updated On: 26 December 2018
 */

 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Data {

  // <summary>
  /// This interface provides methods to execute all database operations to get Invoice and
  /// related data transfer objects.
  // </summary>
  public interface IBASalesQuotationRepository:IBaseRepository<BASalesQuotation> {

        #region Get

        /// <summary>
        /// Get BASales Quotation list by teanntid.
        /// </summary>
        /// <param name="tenantId">Tenant unique id.</param>
        /// <param name="includeDeleted">return BASalesQuotation with deleted items.</param>        
        /// <returns>return list of BASalesQuotation entity.</returns>
        Task<List<BASalesQuotation>> GetSalesQuotationListByTenantIdAsync(Guid tenantId,ListDateFilterDTO listDateFilterDTO, bool includeDeleted, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get BASales Quotation by ERP unique key.
        /// </summary>
        /// <param name="erpSalesQuotationKey">SalesQuotationKey is a ERP unique key.</param>
        /// <param name="token"></param>
        /// <returns>return BASalesQuotation entity.</returns>
        Task<BASalesQuotation> GetSalesQuotationByERPKeyAsync(string erpSalesQuotationKey, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the SalesQuotation list by business tenant identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <returns>Returns list of <see cref="BusBASalesQuotationDTO"/> that matches provided business tenant id.</returns>
        IQueryable<BusBASalesQuotationDTO> GetSalesQuotationListByBusinessTenantId(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO);

        /// <summary>
        /// Gets the SalesQuotation detail with items by SalesQuotation identifier.
        /// </summary>
        /// <param name="sqID"></param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns delivery detail with items that matches provided delivery id.</returns>
        Task<BusBASalesQuotationViewDTO> GetSalesQuotationDetailBySalesQuotationIdAsync(Guid sqID, CancellationToken cancellationToken);

        IQueryable<CustBASalesQuotationDTO> GetSalesQuotationListByPartnerTenantIdForCust(Guid businessPartnerTenantId,ListDateFilterDTO listDateFilterDTO);

        Task<CustBASalesQuotationViewDTO> GetSalesQuotationDetailBySalesQuotationIdAsyncForCust(Guid salesQuotationId, CancellationToken cancellationToken);

      #endregion Get

    }
}
