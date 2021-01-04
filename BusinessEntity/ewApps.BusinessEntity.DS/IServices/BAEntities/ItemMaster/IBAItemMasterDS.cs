using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using Microsoft.AspNetCore.Http;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    ///  Responsible for exposing all the methods that are intrecting with the DB for retriving the data related to BAItemMaster entity.
    /// </summary>
    public interface IBAItemMasterDS:IBaseDS<BAItemMaster> {

        /// <summary>
        /// Add customer list.
        /// </summary>
        /// <param name="itemMasterList"></param>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task AdditemMasterListAsync(List<BAItemMasterSyncDTO> itemMasterList, Guid tenantId, Guid tenantUserId, bool isBulkInsert, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the item master list by bus tenant identifier asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="token">The token.</param>
        Task<IEnumerable<BusBAItemMasterDTO>> GetItemMasterListByBusTenantIdAsync(Guid tenantId, string itemType, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the item master list by bus tenant identifier and item identifier asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<BusBAItemMasterViewDTO> GetItemMasterByBusTenantIdAndItemIdAsync(Guid tenantId, Guid itemId, string itemType, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Add business Payment App Item Master
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<ResponseModelDTO> AddItemMasterByBusinessAsync(HttpRequest httpRequest, string request, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Add Item Master without attachment
        /// </summary>
        /// <param name="busBAItemMasterViewDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<ResponseModelDTO> AddItemMasterByBusinessPayAppWithoutAttchAsync(BusBAItemMasterViewDTO busBAItemMasterViewDTO, CancellationToken token = default(CancellationToken));

        Task<ResponseModelDTO> UpdateItemMasterByBusinessPaymentAppWithAttchAsync(HttpRequest httpRequest, string request, Guid itemId, CancellationToken token = default(CancellationToken));

        Task<ResponseModelDTO> UpdateItemMasterByBusinessPaymentAppWithoutAttchAppAsync(BusBAItemMasterViewDTO busBAItemMasterViewDTO, Guid itemId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the item master list by bus tenant identifier asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<IEnumerable<CustBAItemMasterDTO>> GetItemMasterListByBusTenantIdAsyncForCust(Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the item master list by bus tenant identifier and item identifier asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<CustBAItemMasterViewDTO> GetItemMasterByBusTenantIdAndItemIdAsyncForCust(Guid tenantId, Guid itemId, CancellationToken token = default(CancellationToken));
    }
}
