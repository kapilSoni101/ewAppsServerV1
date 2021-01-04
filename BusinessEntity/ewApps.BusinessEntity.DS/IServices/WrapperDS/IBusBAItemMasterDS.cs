using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using Microsoft.AspNetCore.Http;

namespace ewApps.BusinessEntity.DS {

    public interface IBusBAItemMasterDS {

        /// <summary>
        /// Gets the item master list by bus tenant identifier asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<IEnumerable<BusBAItemMasterDTO>> GetItemMasterListByBusTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the item master list by bus tenant identifier and item identifier asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<BusBAItemMasterViewDTO> GetItemMasterByBusTenantIdAndItemIdAsync(Guid tenantId, Guid itemId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Add Item Master.
        /// </summary>
        /// <param name="httpRequest">httpRequest object</param>
        /// <param name="request"></param>
        /// <param name="token">Cancellation token for async operations</param>   
        Task<ResponseModelDTO> AddItemMasterByBusinessAsync(HttpRequest httpRequest, string request, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Add item Master Without Attechment
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<ResponseModelDTO> AddItemMasterByBusinessPayAppWithoutAttchAsync(BusBAItemMasterViewDTO busBAItemMasterViewDTO, CancellationToken token = default(CancellationToken));

        Task<ResponseModelDTO> UpdateItemMasterByBusinessPaymentAppWithAttchAsync(HttpRequest httpRequest, string request, Guid itemId, CancellationToken token = default(CancellationToken));

        Task<ResponseModelDTO> UpdateItemMasterByBusinessPaymentAppWithoutAttchAppAsync(BusBAItemMasterViewDTO busBAItemMasterViewDTO, Guid itemId, CancellationToken token = default(CancellationToken));

        #region Vendor Methods

        /// <summary>
        /// Gets the item master list by bus tenant identifier asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<IEnumerable<BusBAItemMasterDTO>> GetVendorsItemMasterListByBusTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the item master list by bus tenant identifier and item identifier asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<BusBAItemMasterViewDTO> GetVendorsItemMasterByBusTenantIdAndItemIdAsync(Guid tenantId, Guid itemId, CancellationToken token = default(CancellationToken));


        #endregion

    }
}
