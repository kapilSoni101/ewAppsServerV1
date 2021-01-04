using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.BusinessEntity.DTO {
    public class ContractNotificationDTO {

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

        public Guid ID {
            get; set;
        }

        public string ERPContractKey {
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

        public DateTime StartDate {
            get; set;
        }

        public DateTime EndDate {
            get; set;
        }

        public DateTime TerminationDate {
            get; set;
        }

        public DateTime SigningDate {
            get; set;
        }

        public string Description {
            get; set;
        }

        public string TimeZone {
            get; set;
        }

        public string DateTimeFormat {
            get; set;
        }

        [NotMapped]
        public DateTime DateTime {
            get; set;
        }

    }
}
