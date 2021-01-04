using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    /// This interface defines <see cref="BASalesQuotation"/> entity operations for business.
    /// </summary>
    public interface IBusBASalesQuotationDS {

        /// <summary>
        /// Gets the SalesQuotation list by business tenant identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <param name="listDateFilterDTO">The date filter values to filter sales quatation data on Document Date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of <see cref="BusBADeliveryDTO"/> that matches provided business tenant id.</returns>
        Task<IEnumerable<BusBASalesQuotationDTO>> GetSalesQuotationListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get Sales Quatation Detail By Sales Quotation Id 
        /// </summary>
        /// <param name="salesQuotationId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<BusBASalesQuotationViewDTO> GetSalesQuotationDetailBySalesQuotationIdAsync(Guid salesQuotationId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
