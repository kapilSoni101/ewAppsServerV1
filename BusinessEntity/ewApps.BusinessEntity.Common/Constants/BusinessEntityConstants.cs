/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 26 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 26 August 2019
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.Common {


    /// <summary>
    /// This class provides all the constants used in the application.
    /// </summary>
    public class BusinessEntityConstants {

        public const string DefaultLanguage = "en";

        public const string InvoicePrefix = "ARINV";

        public const string ItemMasterPrefix = "IMS";

        public const string CustomerPrefix = "CUST";

        public const string PurchaseOrderPrefix = "PO";

        public const string PurchaseOrderItemPrefix = "POI";

        #region Application Guid

        /// <summary>
        /// Publisher application Guid.
        /// </summary>
        public const string PublisherApplicationId = "67D09A6F-CE95-498C-BF69-33C7D38F9041";
        public const string BizSetUpApplicationId = "F4952EF3-F1BD-4621-A5F9-290FD09BC81B";

    public const string CustomerDeleteException = "You can not delete customer, its reference exist.";

        #endregion Application Guid        

        public const string ClosedStatus = "Closed";

    }

    public class ItemTypeConstants {
        public const string CustomerItemType = "Customer";
        public const string VendorItemType = "Vendor";
    }

    public class ASNTypeConstants {
        public const string CustomerASNType = "Customer";
        public const string VendorASNType = "Vendor";
    }
}
