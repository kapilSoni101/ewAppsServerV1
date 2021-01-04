/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 10 Oct 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 Oct 2019
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.Report.DTO {
    public class SalesOrdersItemsCountFCDTO {

        /// <summary>
        /// No of Items in Sales Orders 
        /// </summary>
        public int NoOfItems {
            get; set;
        }
        /// <summary>
        /// Name of the Item
        /// </summary>
        public string SalesOrderName {
            get; set;
        }
        /// <summary>
        /// Percentage of User In Items
        /// </summary>
        [NotMapped]
        public float PercentageOfItem {
            get; set;
        }
    }
}
