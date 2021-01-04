//dbquery

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 20 November 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 20 November 2018
 */

using System;
using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {
    /// <summary>
    /// This class Contain Business Name and Sum of invoice 
    /// </summary>
    public class ShipmentBusinessNameAndSumCount:BaseDTO {
        /// <summary>
        /// Business Name 
        /// </summary>
        public string BusinessName {
            get; set;
        }
        /// <summary>
        /// Sum of Invoice for Business
        /// </summary>
        public int SalesOrders {
            get; set;
        }

        /// <summary>
        /// Business Name 
        /// </summary>
        public string ServiceName {
            get; set;
        }

        /// <summary>
        /// UniqueIdentifier of Business.
        /// </summary>
        public new Guid ID {
            get; set;
        }
    }
}

