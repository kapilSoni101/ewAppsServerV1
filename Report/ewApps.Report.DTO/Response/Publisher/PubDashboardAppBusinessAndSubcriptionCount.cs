//Response
// Move to publisher

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ewApps.Report.DTO {
    public class PubDashboardAppBusinessAndSubcriptionCount {

        [NotMapped]
        public AppAndBusinessCountDTO AppAndBusinessCountDTO {
            get; set;
        }
        [NotMapped]
        public BusinessAndSubscriptionCountDTO BusinessAndSubscriptionCountDTO {
            get; set;
        }
        [NotMapped]
        public ShipmentBusinessAndSubscriptionCountDTO ShipmentBusinessAndSubscriptionCountDTO {
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
        public List<BusinessNameAndSumCount> BusinessNameAndSumCount {
            get; set;
        }

        [NotMapped]
        public List<ShipmentBusinessNameAndSumCount> ShipmentBusinessNameAndSumCount {
            get; set;
        }

        [NotMapped]
        public List<ShipmentServiceNameAndCountDTO> ShipmentServiceNameAndCountDTO {
            get; set;
        }


    }
}
