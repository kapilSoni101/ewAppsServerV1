using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.BaseService {

    [Flags]
    public enum BusinessUserPaymentAppPermissionEnum:long {
        /// <summary>
        /// Dont have any permission.
        /// </summary>
        None = 0,

        /// <summary>
        /// View Customers permission.
        /// </summary>
        ViewCustomers = 1,

        /// <summary>
        /// Manage Customers permission.
        /// </summary>
        ManageCustomers = 2,

        /// <summary>
        /// View Invoices permission.
        /// </summary>
        ViewInvoices = 4,

        /// <summary>
        /// Manage Invoices permission.
        /// </summary>
        ManageInvoices = 8,

        /// <summary>
        /// Manage Customer Payment Info permissions.
        /// </summary>
        ManageCustomerPaymentInfo = 16,

        /// <summary>
        /// Manage Business Payment Info permissions.
        /// </summary>
        ManageBusinessPaymentInfo = 32,

        /// <summary>
        /// Payment Activities permisssions.
        /// </summary>
        PaymentActivities = 64,

        /// <summary>
        /// Access Transaction History
        /// </summary>
        AccessTransactionHistory = 128,

        /// <summary>
        /// Access Reports.
        /// </summary>
        AccessReports = 256,

        /// <summary>
        /// Manage Portal Settings permission.
        /// </summary>
        ManageBusinessPortalSettings = 512,

        /// <summary>
        /// View Ticket.
        /// </summary>
        ViewTickets = 1024,

        /// <summary>
        /// Manage Ticket.
        /// </summary>
        ManageTickets = 2048,

        /// <summary>
        /// Have all permission.
        /// </summary>
        All = None | ViewCustomers | ManageCustomers | ViewInvoices | ManageInvoices
                   | ManageCustomerPaymentInfo | ManageBusinessPaymentInfo | PaymentActivities | AccessTransactionHistory
                   | AccessReports | ManageBusinessPortalSettings | ViewTickets | ManageTickets
    }
}
