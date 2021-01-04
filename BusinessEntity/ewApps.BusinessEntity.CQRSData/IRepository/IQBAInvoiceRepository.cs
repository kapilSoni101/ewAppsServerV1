using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;

namespace ewApps.BusinessEntity.QData {
    public  interface IQBAInvoiceRepository {

        /// <summary>
        /// Get invoice attachment list.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<BusBAARInvoiceAttachmentDTO>> GetInvoiceAttachmentListByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
