using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    /// This interface provides the CRUD operations related methods for delviery entity.
    /// </summary>
    public interface IBAARInvoiceItemDS:IBaseDS<BAARInvoiceItem> {

        #region Get

        /// <summary>
        /// Get invoice items by invoice id.
        /// </summary>
        /// <param name="invoiceId">InvoiceID</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BAARInvoiceItemDQ>> GetInvoiceItemListByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

        #endregion Get

        #region Add

        /// <summary>
        /// add invoice item list.
        /// </summary>
        /// <param name="invoiceItemList"></param>
        /// <param name="tenantId"></param>
        /// <param name="tenantUserId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task AddInvoiceItemListAsync(List<BAARInvoiceItemSyncDTO> invoiceItemList, Guid tenantId, Guid tenantUserId, Guid invoiceId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Add invoice items on adding invoice.
        /// </summary>
        /// <param name="invoiceItemList">Invoice items list.</param>
        /// <param name="tenantId">Tenant id</param>
        /// <param name="tenantUserId">tenant user id.</param>
        /// <param name="invoiceId">added invoice id</param>
        /// <param name="erpArInvoiceKey">invoice key.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task AddBAARInvoiceItemListAsync(List<AddBAARInvoiceItemDTO> invoiceItemList, Guid tenantId, Guid tenantUserId, Guid invoiceId, string erpArInvoiceKey, CancellationToken token = default(CancellationToken));

        #endregion Add
    }
}
