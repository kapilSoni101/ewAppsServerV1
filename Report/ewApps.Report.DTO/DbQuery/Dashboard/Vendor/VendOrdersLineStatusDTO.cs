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
    /// This class is a DTO with consolidate information of <see cref="VendOrdersLineStatusDTO"/> .
    /// </summary>
    public class VendOrdersLineStatusDTO {

        /// <summary>
        /// Open Orders 
        /// </summary>
        public int OpenOrders {
            get; set;
        }


        /// <summary>
        /// Open Orders lines 
        /// </summary>
        public int OpenLines {
            get; set;
        }
    }
}
