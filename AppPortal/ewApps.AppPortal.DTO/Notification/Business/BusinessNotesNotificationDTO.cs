using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.DTO {
    public class BusinessNotesNotificationDTO {

       

        // PaymentAppName
        public string AppName {
            get; set;
        }

        // App Key
        public string AppKey {
            get; set;
        }

        public Guid AppId {
            get; set;
        }

        // UserName
        public string UserName {
            get; set;
        }

        // PublisherName
        public string PublisherName {
            get; set;
        }

        public Guid BusinessTenantId {
            get; set;
        }

        // BusinessName
        public string BusinessName {
            get; set;
        }

        // BusinessName
        public string ERPDocNum {
            get; set;
        }

        // CustomerName
        public string CustomerName {
            get; set;
        }

        // CustomerName
        public string CustomerId {
            get; set;
        }

        // CopyrightText
        public string Copyright {
            get; set;
        }

       //SubDomain
        public string SubDomainName {
            get; set;
        }

        [NotMapped]
        public string Comment {
            get; set;
        }

        public Guid UserId {
            get; set;
        }       

        public int UserType {
            get; set;
        }

        public string TimeZone {
            get; set;
        }

        public string DateTimeFormat {
            get; set;
        }

        public DateTime CreatedOn {
            get; set;
        }

        [NotMapped]
        public Guid EntityId {
            get; set;
        }

        [NotMapped]
        public int EntityType {
            get; set;
        }

        [NotMapped]
        public UserSession UserSessionInfo {
            get; set;
        }
    }
}
