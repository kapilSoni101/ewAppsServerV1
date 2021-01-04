using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.BusinessEntity.DTO {
    public class ASNNotificationDTO {

        public string PublisherName {
            get; set;
        }

        public string BusinessName {
            get; set;
        }

        public Guid BusinessTenantId {
            get; set;
        }

        public Guid PublisherTenantId {
            get; set;
        }

        public string CustomerName {
            get; set;
        }

        public string ERPCustomerKey {
            get; set;
        }

        public string TotalAmount {
            get; set;
        }

        public Guid ID {
            get; set;
        }

        public string ERPASNKey {
            get; set;
        }

        public string TrackingNo {
            get; set;
        }

        [NotMapped]
        public string Address {
            get; set;
        }

        public string PackagingSlipNo {
            get; set;
        }

        public string ShippingTypeText {
            get; set;
        }

        public string ShipmentPlan {
            get; set;
        }


        public string UserIdentityNo {
            get; set;
        }

        public string UserName {
            get; set;
        }

        public string SubDomainName {
            get; set;
        }

        [NotMapped]
        public string PortalUrl {
            get; set;
        }

        public string Copyright {
            get; set;
        }

        public string AppKey {
            get; set;
        }

        public Guid AppId {
            get; set;
        }

        [NotMapped]
        public DateTime DateTime {
            get;set;
        }

        public string TimeZone {
            get; set;
        }

        public string DateTimeFormat {
            get; set;
        }

        public DateTime ShipDate {
            get;set;
        }

        public DateTime ExpectedDate {
            get; set;
        }

    }
}