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
  public interface IBAAPInvoiceDS : IBaseDS<BAAPInvoice> {


    #region Add/Update/Delete

    Task AddAPInvoiceListAsync(List<BAAPInvoiceSyncDTO> invoiceList, Guid tenantId, Guid tenantUserId, bool isBulkInsert, CancellationToken token = default(CancellationToken));

    #endregion Add/Update/Delete

  }

}