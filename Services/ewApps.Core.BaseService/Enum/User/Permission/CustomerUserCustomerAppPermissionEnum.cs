using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.BaseService {
    [Flags]
    public enum CustomerUserCustomerAppPermissionEnum:long {


        /// <summary>
        /// Dont have any permission.
        /// </summary>
        None = 0,

        AccessItemMaster = 1,
        ViewQuotations = 2,
        ViewOrders = 4,
        ManageOrders = 8,
        ViewAPInvoices = 16,
        ViewDeliveries = 32,
        ViewContractManagement = 64,
        AllowPaymentActivities = 128,
        ManagePaymentInfo = 512,
        AccessTransactionHistory = 1024,
        AccessShipmentHistory = 2048,
        AddNotes = 4096,
        AccessReports = 8192,
        ManageQuotations = 16384,


        /// <summary>
        /// Have all permission.
        /// </summary>
       // All = None | AccessItemMaster | ViewQuotations | ViewOrders | ViewAPInvoices | ViewDeliveries | ViewContractManagement |
       //AccessShipmentHistory | AddNotes | AccessReports
        All = None | AccessItemMaster | ViewQuotations | ViewOrders | ManageOrders | ViewAPInvoices | ViewDeliveries | ViewContractManagement |
        AllowPaymentActivities | ManagePaymentInfo | AccessTransactionHistory | AccessShipmentHistory | AddNotes | AccessReports | ManageQuotations
  }
}
