///* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
// * Unauthorized copying of this file, via any medium is strictly prohibited
// * Proprietary and confidential
// * 
// * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
// * Date: 24 September 2018
// * 
// * Contributor/s: Nitin Jain
// * Last Updated On: 20 June 2019
// */

//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace ewApps.Payment.Common {

//    /// <summary>
//    /// Enum containing all the permissions for the business user for payment Application
//    /// </summary>
//    [System.Flags]
//    public enum PaymentAppBusinessUserPermissionEnum:long {

//        /// <summary>
//        /// Dont have any permission.
//        /// </summary>
//        None = 0,

//        /// <summary>
//        /// View Customers permission.
//        /// </summary>
//        ViewCustomers = 1,

//        /// <summary>
//        /// Manage Customers permission.
//        /// </summary>
//        ManageCustomers = 2,

//        /// <summary>
//        /// View Invoices permission.
//        /// </summary>
//        ViewInvoices = 4,

//        /// <summary>
//        /// Manage Invoices permission.
//        /// </summary>
//        ManageInvoices = 8,

//        /// <summary>
//        /// Manage Customer Payment Info permissions.
//        /// </summary>
//        ManageCustomerPaymentInfo = 16,

//        /// <summary>
//        /// Manage Business Payment Info permissions.
//        /// </summary>
//        ManageBusinessPaymentInfo = 32,

//        /// <summary>
//        /// Payment Activities permisssions.
//        /// </summary>
//        PaymentActivities = 64,

//        /// <summary>
//        /// Access Transaction History
//        /// </summary>
//        AccessTransactionHistory = 128,

//        /// <summary>
//        /// Access Reports.
//        /// </summary>
//        AccessReports = 256,

//        /// <summary>
//        /// Manage Portal Settings permission.
//        /// </summary>
//        ManageBusinessPortalSettings = 512,

//        /// <summary>
//        /// View Ticket.
//        /// </summary>
//        ViewTickets = 1024,

//        /// <summary>
//        /// Manage Ticket.
//        /// </summary>
//        ManageTickets = 2048,

//        /// <summary>
//        /// Have all permission.
//        /// </summary>
//        All = None | ViewCustomers | ManageCustomers | ViewInvoices | ManageInvoices
//                   | ManageCustomerPaymentInfo | ManageBusinessPaymentInfo | PaymentActivities | AccessTransactionHistory
//                   | AccessReports | ManageBusinessPortalSettings | ViewTickets | ManageTickets

//    }
//}



////1. ViewCustomers 
////2. ManageCustomers
////3. ViewInvoices 
////4. ManageInvoices
////5. ManageCustomerPaymentInfo
////6. ManageBusinessPaymentInfo	
////7. PaymentActivities
////8. AccessTransactionHistory 
////9. AccessReports
////10. ManageBusinessPortalSettings
////11. ViewTickets
////12. ManageTickets