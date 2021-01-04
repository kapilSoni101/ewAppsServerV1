using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    public interface IBusBASalesQuotationDS {

        /// <summary>
        /// Gets the SalesQuotation list by business tenant identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <param name="listDateFilterDTO">The date filter values to filter sales quatation data on Document Date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of <see cref="BusBADeliveryDTO"/> that matches provided business tenant id.</returns>
        Task<IEnumerable<BusBASalesQuotationDTO>> GetSalesQuotationListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken);


        /// <summary>
        /// Gets the SalesQuotation  by SalesQuotation id.
        /// </summary>
        /// <param name="salesQuotationId">The delivery to find SalesQuotation items.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of SalesQuotation item name list that matches provided SalesQuotation id.</returns>
        Task<BusBASalesQuotationViewDTO> GetSalesQuotationDetailBySalesQuotationIdAsync(Guid salesQuotationId, CancellationToken cancellationToken = default(CancellationToken));

    }
}
