

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.BaseService
{
  [Flags]
  public enum CustomerUserPaymentAppPermissionEnum : long
  {
    /// <summary>
    /// Dont have any permission
    /// </summary>
    None = 0,

    /// <summary>
    /// view invoices permission
    /// </summary>
    ViewInvoices = 1,

    /// <summary>
    /// trans history
    /// </summary>
    AccessTransactionHistory = 2,

    AllowPaymentActivities = 4,

    ManagePaymentInfo = 8,

    AddNotes = 16,

    AccessReports = 32,

    All = None | ViewInvoices | AccessTransactionHistory | AllowPaymentActivities | ManagePaymentInfo | AddNotes | AccessReports
  }
}

