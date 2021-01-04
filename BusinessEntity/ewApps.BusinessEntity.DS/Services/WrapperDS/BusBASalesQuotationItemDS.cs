using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {
  public class BusBASalesQuotationItemDS : BaseDS<BASalesQuotationItem>, IBusBASalesQuotationItemDS {

        IBASalesQuotationItemRepository _bASalesQuotationItemRepository;

        #region Constructor

        /// <summary>
        /// Public constructor for DeliveryItemDS class.
        /// </summary>
        /// <param name="bASalesQuotationItemRepository">Repository class dependancy for delviery item.</param>
        public BusBASalesQuotationItemDS(IBASalesQuotationItemRepository bASalesQuotationItemRepository) : base(bASalesQuotationItemRepository) {
            _bASalesQuotationItemRepository = bASalesQuotationItemRepository;
        }

        #endregion Constructor

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBASalesQuotationItemDTO>> GetSalesQuotationItemListBySalesQuotationId(Guid salesQuotationId, CancellationToken cancellationToken = default(CancellationToken)) {
            return _bASalesQuotationItemRepository.GetSalesQuotationItemListBySalesQuotationId(salesQuotationId);
        }

    }
}
