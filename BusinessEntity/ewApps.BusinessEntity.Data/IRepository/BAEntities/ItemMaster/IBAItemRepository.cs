// Hari sir comment
/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Souarbh Agrawal <sagrawal@eworkplaceapps.com>
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
    public interface IBAItemMasterRepository:IBaseRepository<BAItemMaster> {

        /// <summary>
        /// Gets the item master list by business tenant id and item type asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant id to filter item master list.</param>    
        /// <param name="itemType">The item type to filter item master list.</param>
        /// <returns>Returns list of item that matches the given tenant id and item type.</returns>
        IQueryable<BusBAItemMasterDTO> GetItemMasterListByBusTenantId(Guid tenantId, string itemType);

        /// <summary>
        /// Gets the item master list by bus tenant identifier and item identifier.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <returns></returns>
        BusBAItemMasterViewDTO GetItemMasterByBusTenantIdAndItemId(Guid tenantId, Guid itemId);

        /// <summary>
        /// Gets the item master list by bus tenant identifier asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>    
        /// <returns></returns>
        IQueryable<CustBAItemMasterDTO> GetItemMasterListByBusTenantIdForCust(Guid tenantId);

        /// <summary>
        /// Gets the item master list by bus tenant identifier and item identifier.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <returns></returns>
        CustBAItemMasterViewDTO GetItemMasterByBusTenantIdAndItemIdForCust(Guid tenantId, Guid itemId);

    }
}
