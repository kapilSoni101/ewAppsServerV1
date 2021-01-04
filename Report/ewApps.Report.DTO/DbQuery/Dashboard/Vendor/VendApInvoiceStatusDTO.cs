/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 08 Nov 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 08 Nov 2019
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Report.DTO{

    /// <summary>
    /// This class is a DTO with consolidate information of <see cref="VendApInvoiceStatusDTO"/> .
    /// </summary>
    public class VendApInvoiceStatusDTO {

        /// <summary>
        /// total no of open or unpaid invoices.
        /// </summary>
        public int TotalOpenInvoices {
            get; set;
        }

        /// <summary>
        /// Amount Of Total OPen Invoices
        /// </summary>
        public Decimal TotalOpenInvoicesAmount {
            get; set;
        }

        /// <summary>
        /// Amount Of Total Closed Invoices
        /// </summary>
        public int  ClosedInvoices {
            get; set;
        }

        /// <summary>
        /// Amount Of Total OPen Invoices
        /// </summary>
        public Decimal TotalClosedInvoicesAmount {
            get; set;
        }

    }
}
