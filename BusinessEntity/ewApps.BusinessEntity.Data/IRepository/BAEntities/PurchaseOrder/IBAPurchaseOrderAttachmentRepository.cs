 using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Data {

	public interface IBAPurchaseOrderAttachmentRepository: IBaseRepository<BAPurchaseOrderAttachment> {

        /// <summary>
        /// Get purchase order attachment list.
        /// </summary>
        /// <param name="pOrderId">Purchase orderid.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<BAARPurchaseOrderAttachmentDTO>> GetPurchaseOrderAttachmentListByOrderIdAsync(Guid pOrderId, CancellationToken cancellationToken = default(CancellationToken));


    }

}