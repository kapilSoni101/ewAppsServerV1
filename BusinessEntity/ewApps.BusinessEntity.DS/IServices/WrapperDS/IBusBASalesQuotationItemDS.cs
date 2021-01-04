using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;

namespace ewApps.BusinessEntity.DS {
  public interface IBusBASalesQuotationItemDS {

        /// <summary>
        /// Gets the salesQuotation item list by salesQuotation identifier.
        /// </summary>
        /// <param name="salesQuotationId">The sales Quotation identifier to find items.</param>
        /// <param name="cancellationToken">The async task cancellation token.</param>
        /// <returns>Returns list of salesQuotation items that matches provided salesQuotation id.</returns>
        Task<IEnumerable<BusBASalesQuotationItemDTO>> GetSalesQuotationItemListBySalesQuotationId(Guid salesQuotationId, CancellationToken cancellationToken = default(CancellationToken));

    }
}
