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

    /// </summary>
    public interface IBAAPInvoiceAttachmentDS:IBaseDS<BAAPInvoiceAttachment> {


    #region Add/Update/Delete

    Task AddAPInvoiceAttachmentListAsync(List<BAAPInvoiceAttachmentSyncDTO> aPInvoiceAttachmentList, Guid tenantId, Guid tenantUserId, Guid apInvoiceId, CancellationToken token = default(CancellationToken));

        #endregion Add/Update/Delete

  }

}