using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;


namespace ewApps.BusinessEntity.DS
{

  /// <summary>
  /// </summary>
  public interface IBAAPInvoiceItemDS : IBaseDS<BAAPInvoiceItem>
  {


    #region Add/Update/Delete

    Task AddInvoiceItemListAsync(List<BAAPInvoiceItemSyncDTO> invoiceItemList, Guid tenantId, Guid tenantUserId, Guid invoiceId, CancellationToken token = default(CancellationToken));

    #endregion Add/Update/Delete

  }

}