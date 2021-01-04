using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;


namespace ewApps.BusinessEntity.DTO {
    public class BusBAASNDTO {
        public Guid ID {
            get; set;
        }

        public string ERPConnectorKey {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ERPCustomerKey {
            get; set;
        }


        /// <summary>
        /// 
        /// </summary>
        public Guid CustomerId {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string CustomerName {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ERPASNKey {
            get; set;
        }

        public string DeliveryNo {
            get; set;
        }

        public DateTime ShipDate {
            get; set;
        }

        public DateTime ExpectedDate {
            get; set;
        }

        public string TrackingNo {
            get; set;
        }


        public int ShipmentType {
            get; set;
        }

        public string ShipmentTypeText {
            get; set;
        }

        public string ShipmentPlan {
            get; set;
        }

        public string PackagingSlipNo {
            get; set;
        }

        public decimal? TotalAmount {
            get; set;
        }


        public decimal? Discount {
            get; set;
        }

        public decimal? Freight {
            get; set;
        }

        public decimal? Tax {
            get; set;
        }

        public string ERPDocNum {
            get; set;
        }

        public string LocalCurrency {
            get; set;
        }

    }
}
