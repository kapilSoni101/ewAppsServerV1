using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.Common {

    /// <summary>
    /// Permissions for the business partner user on payment aplication.
    /// </summary>
    [System.Flags]
    public enum PaymentAppCustomerUserPermissionEnum:long {

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


//1. View Invoices 
//2. Manage Invoices 
//3. Access Transaction History 
//4. Access Reports 
//5. Manage Customer Portal Settings