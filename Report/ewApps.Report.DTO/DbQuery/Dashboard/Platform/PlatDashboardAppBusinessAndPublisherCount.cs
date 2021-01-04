//dbquery

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 06 February 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 06 February 2019
 */

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {

    /// <summary>
    /// This class contain all platform dashboard DTO count 
    /// </summary>
    public class PlatDashboardAppBusinessAndPublisherCount:BaseDTO {
        [NotMapped]
        public PlatAppAndBusinessCountDTO PlatAppAndBusinessCountDTO {
            get; set;
        }
        [NotMapped]
        public AapNameAndBusinessCountDTO AapNameAndBusinessCountDTO {
            get; set;
        }
        [NotMapped]
        public ShipmentAapNameAndBusinessCountDTO ShipmentAapNameAndBusinessCountDTO {
            get; set;
        }
        [NotMapped]
        public List<ApplicationUserCountDTO> ApplicationUserCountDTO {
            get; set;
        }
        [NotMapped]
        public List<BusinessCountDTO> BusinessCountDTO {
            get; set;
        }
        [NotMapped]
        public List<BusinessAddedCountAndMonthDTO> BusinessAddedCountAndMonthDTO {
            get; set;
        }
        [NotMapped]
        public List<ShipmentBusinessAddedCountAndMonthDTO> ShipmentBusinessAddedCountAndMonthDTO {
            get; set;
        }
        [NotMapped]
        public List<ShipmentBusinessNameAndSumCount> ShipmentBusinessNameAndSumCount {
            get; set;
        }
        [NotMapped]
        public List<BusinessNameAndSumCount> BusinessNameAndSumCount {
            get; set;
        }
        [NotMapped]
        public List<PublisherTenantCountDTO> PublisherTenantCountDTO {
            get; set;
        }
        [NotMapped]
        public List<ApplicationPublisherCountDTO> ApplicationPublisherCountDTO {
            get; set;
        }
        [NotMapped]
        public List<ShipmentServiceNameAndCountDTO> ShipmentServiceNameAndCountDTO {
            get; set;
        }
        
    }
}
