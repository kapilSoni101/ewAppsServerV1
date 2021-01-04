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

namespace ewApps.Report.DTO {

    /// <summary>
    /// This class is a DTO with consolidate information of <see cref="VendOpenLinesListDTO"/> .
    /// </summary>
    public class VendOpenLinesListDTO {


        /// <summary>
        /// Open line item Id 
        /// </summary>
        public Guid ID {
            get; set;
        }

        public string ERPPurchaseOrderKey {
            get;set;
            }

        /// <summary>
        /// Date Of Document 
        /// </summary>
        public DateTime DocumentDate {
            get; set;
        }

        /// <summary>
        /// Name of Items
        /// </summary>
        public string ItemNames {
            get; set;
        }


        /// <summary>
        /// Name of Items
        /// </summary>
        public string VendorNames {
            get; set;
        }


        //Quantity of Open Items 
        public int OpenQuantity {
            get; set;
        }

    }
}
