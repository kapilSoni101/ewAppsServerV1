using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Payment.DTO;

namespace ewApps.Payment.QData {
    public interface IQInvoiceItemRepository {

        #region Get

        /// <summary>
        /// Get invoice detail by invoice id.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BAARInvoiceItemViewDTO>> GetInvoiceItemsByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

        #endregion Get        

    }
}
