// Hari sir comment
/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: Amit
 * Last Updated On: 26 December 2018
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS
{

  public interface IBASalesQuotationItemDS : IBaseDS<BASalesQuotationItem>
  {

    #region Get

    /// <summary>
    /// Get sales quotration item list by teanntid.
    /// </summary>
    /// <param name="salesQuotationId">salesQuotationId unique id.</param>
    /// <param name="includeDeleted">return all sales quotation item with deleted items if flag is true.</param>        
    /// <returns>return list of sales order entity.</returns>
    Task<List<BASalesQuotationItem>> GetSalesQuotationItemsListBySalesOrderIdAsync(Guid salesQuotationId, bool includeDeleted, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get sales quotation item list by ERP sales quotation key.
    /// </summary>
    /// <param name="erpSalesQuotationKey">erpSalesQuotationKey unique key of connector.</param>
    /// <param name="includeDeleted">return all sales order item with deleted items if flag is true.</param>        
    /// <returns>return list of sales order entity.</returns>
    Task<List<BASalesQuotationItem>> GetSalesQuotationItemsListByERPSalesOrderKeyAsync(string erpSalesQuotationKey, bool includeDeleted, CancellationToken token = default(CancellationToken));

    Task<IEnumerable<CustBASalesQuotationItemDTO>> GetSalesQuotationItemListBySalesQuotationIdForCust(Guid salesQuotationId, CancellationToken cancellationToken = default(CancellationToken));
    Task<IEnumerable<BusBASalesQuotationItemDTO>> GetSalesQuotationItemListBySalesQuotationId(Guid salesQuotationId);

    #endregion Get
    Task AddSalesQuotationItemListAsync(List<BASalesQuotationItemSyncDTO> salesQuotationItemList, Guid tenantId, Guid tenantUserId, Guid salesQuotationId, CancellationToken token = default(CancellationToken));
  }
}
