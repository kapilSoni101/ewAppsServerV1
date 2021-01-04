using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Http;

namespace ewApps.AppPortal.DS {

    public interface IBusBAItemMasterDS {
        #region Customer Get

        Task<IEnumerable<BusBAItemMasterDTO>> GetItemMasterListByBusTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        Task<BusBAItemMasterViewDTO> GetItemMasterByBusTenantIdAndItemidAsync(Guid tenantId, Guid itemId, CancellationToken token = default(CancellationToken));

        #endregion Customer Get

        #region Vendor Get

        Task<IEnumerable<BusBAItemMasterDTO>> GetVendorsItemMasterListByBusTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        Task<BusBAItemMasterViewDTO> GetVendorsItemMasterByBusTenantIdAndItemidAsync(Guid tenantId, Guid itemId, CancellationToken token = default(CancellationToken));

        #endregion Vendor Get

        /// <summary>
        /// Add Item Master
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<ResponseModelDTO> AddItemMasterByBusinessAsync(HttpRequest httpRequest, string request, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Update item master
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<ResponseModelDTO> UpdateItemMasterByBusinessPaymentAppAsync(HttpRequest httpRequest, string request, Guid itemId, CancellationToken token = default(CancellationToken));


    }
}
