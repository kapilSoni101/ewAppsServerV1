using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.BusinessEntity.DTO {
    public class CustomerNotificationDTO {

        public Guid CustomerId {
            get; set;
        }

        public string CustomerName {
            get; set;
        }

        public string ERPCustomerKey {
            get; set;
        }

       
        public DateTime UpdatedOn {
            get; set;
        }

        public string UpdatedByName {
            get; set;
        }

        public string UpdatedByNo {
            get; set;
        }

        public string CreatedByName {
            get; set;
        }

        public string CreatedByNo {
            get; set;
        }

        public DateTime CreatedOn {
            get; set;
        }

        public string SubDomainName {
            get; set;
        }
     
        public string PublisherName {
            get; set;
        }

        public string BusinessName {
            get; set;
        }

        public string Copyright {
            get; set;
        }

        public string DateTimeFormat {
            get; set;
        }

        //public Guid AppId {
        //    get; set;
        //}

        //public string AppKey {
        //    get; set;
        //}

        //public string AppName {
        //    get; set;
        //}

        public Guid BusinessTenantId {
            get; set;
        }

        public Guid PublisherTenantId {
            get; set;
        }
    public string TimeZone
    {
      get; set;
    }



  }
}
