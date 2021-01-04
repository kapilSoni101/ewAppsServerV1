using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.DTO {

    public class BusinessOnBoardNotificationDTO {

        // OnBoardedUserName
        public string UserName {
            get; set;
        }

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


        // OnBoardingDateTime
        public DateTime? JoinedDate {
            get; set;
        }

        // OnBoardingTimeZone
        public string TimeZone {
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

        // Customer Name
        [NotMapped]
        public string CustomerName {
            get; set;
        }

        // CopyrightText
        public string Copyright {
            get; set;
        }

        public Guid OnBoardedUserId {
            get; set;
        }

        [NotMapped]
        public UserSession UserSessionInfo {
            get; set;
        }

        public string SubDomain {
            get; set;
        }

        public string DateTimeFormat {
            get; set;
        }
        
    }
}
