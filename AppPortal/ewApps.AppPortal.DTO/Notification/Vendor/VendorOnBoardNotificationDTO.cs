using System;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.DTO {
    public class VendorOnBoardNotificationDTO {

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

        public Guid BusinessPartnerTenantId {
            get; set;
        }

        // BusinessName
        public string VendorName {
            get; set;
        }

        // CopyrightText
        public string Copyright {
            get; set;
        }

        // SubDomain
        public string SubDomain {
            get; set;
        }

        // CopyrightText
        public string PortalURL {
            get; set;
        }

        public Guid OnBoardedUserId {
            get; set;
        }

        [NotMapped]
        public UserSession UserSessionInfo {
            get; set;
        }

        public string DateTimeFormat {
            get; set;
        }

    }
}
