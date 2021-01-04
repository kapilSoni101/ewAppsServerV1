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
    /// This class is a DTO with consolidate information of <see cref="VendOrderStatusDTO"/> .
    /// </summary>
    public class VendOrderStatusDTO {


        /// <summary>
        /// No of Orders 
        /// </summary>
        public int Orders {
            get;set;
        }

        /// <summary>
        /// Amount of Orders 
        /// </summary>
        public Decimal OrdersAmount {
            get; set;
        }
    }
}
