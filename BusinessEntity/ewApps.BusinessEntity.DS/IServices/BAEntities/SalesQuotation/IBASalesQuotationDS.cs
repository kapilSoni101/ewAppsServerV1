
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    /// Contains all required sales order methods.
    /// </summary>
    public interface IBASalesQuotationDS:IBaseDS<BASalesQuotation> {

        #region Get

        /// <summary>
        /// Get BASales Quotation list by teanntid.
        /// </summary>
        /// <param name="tenantId">Tenant unique id.</param>
        /// <param name="includeDeleted">return BASalesQuotation with deleted items.</param>        
        /// <returns>return list of BASalesQuotation entity.</returns>
        Task<List<BASalesQuotation>> GetSalesQuotationListByTenantIdAsync(Guid tenantId, ListDateFilterDTO listDateFilterDTO, bool includeDeleted, CancellationToken token = default(CancellationToken));

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
        /// <param name="listDateFilterDTO">The date filter values to filter sales quatation data on Document Date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of <see cref="BusBADeliveryDTO"/> that matches provided business tenant id.</returns>
        Task<IEnumerable<BusBASalesQuotationDTO>> GetSalesQuotationListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the salesQuotation detail with items by salesQuotation identifier.
        /// </summary>
        /// <param name="salesQuotationId">The delivery identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns salesQuotation detail with items that matches provided salesQuotationId .</returns>
        Task<BusBASalesQuotationViewDTO> GetSalesQuotationDetailBySalesQuotationIdAsync(Guid salesQuotationId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the SalesQuotation list by business tenant identifier.
        /// </summary>
        /// <param name="businessPartnerTenantId">The business tenant identifier.</param>
        /// <param name="listDateFilterDTO">The date filter values to filter sales quatation data on Document Date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of <see cref="BusBADeliveryDTO"/> that matches provided business tenant id.</returns>
        Task<IEnumerable<CustBASalesQuotationDTO>> GetSalesQuotationListByPartnerTenantIdAsyncForCust(Guid businessPartnerTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken);

        Task<CustBASalesQuotationViewDTO> GetSalesQuotationDetailBySalesQuotationIdAsyncForCust(Guid salesQuotationId, CancellationToken cancellationToken = default(CancellationToken));

        #endregion Get

        #region Add

        /// <summary>
        /// add sales quotation list.
        /// </summary>
        /// <param name="salesQuotationList"></param>
        /// <param name="tenantId"></param>
        /// <param name="tenantUserId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task AddSalesQuotationListAsync(List<BASalesQuotationSyncDTO> salesQuotationList, Guid tenantId, Guid tenantUserId, bool isBulkInsert, CancellationToken token = default(CancellationToken));

        #endregion Add
    }

}

