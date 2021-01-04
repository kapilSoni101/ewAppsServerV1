//response

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


using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {
    /// <summary>
    /// class Contain Application Name And Business Count 
    /// </summary>
    public class ShipmentAapNameAndBusinessCountDTO:BaseDTO {
        /// <summary>
        /// No of Buiness 
        /// </summary>
        public int TotalBusiness {
            get; set;
        }
        /// <summary>
        /// Total No of Business User 
        /// </summary>
        public int TotalBusinessUser {
            get; set;
        }
        /// <summary>
        /// Name Of Applciation 
        /// </summary>
        public string AppName {
            get; set;
        }
        /// <summary>
        /// NO of Active Business 
        /// </summary>
        public int ActiveBusiness {
            get; set;
        }
        /// <summary>
        /// no of InActive Business 
        /// </summary>
        public int InActiveBusiness {
            get; set;
        }
        /// <summary>
        /// No of Business Added Lask Week
        /// </summary>
        public int BusinessAddedLaskWeek {
            get; set;
        }
    }
}



