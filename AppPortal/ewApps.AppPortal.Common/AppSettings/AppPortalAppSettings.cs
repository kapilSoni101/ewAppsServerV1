using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.Common {

  public class AppPortalAppSettings {

        public string AppName {
            get; set;
        }

        public string AppVersion {
            get; set;
        }

        public string Deployment {
            get; set;
        }

        public string MinimumLoggingLevel {
            get; set;
        }

        public bool EnableSubdomain {
            get; set;
        }

        public string IdentityServerUrl {
            get; set;
        }

        public string ConnectionString {
            get; set;
        }

        public string QConnectionString {
            get; set;
        }

        public string LogPortalUrl {
            get; set;
        }

        public string PaymentApiUrl {
            get; set;
        }
        public string CreditCardApiUrl {
            get; set;
        }
        public string ShipmentApiUrl {
            get; set;
        }

        public string AppMgmtApiUrl {
            get; set;
        }

        public string AppPortalApiUrl {
            get; set;
        }
        public string BusinessEntityApiUrl {
            get; set;
        }

        public string PaymentConnectorApiUrl {
            get; set;
        }

        public string ShipmentConnectorApiUrl {
            get; set;
        }

        public string[] CrossOriginsUrls {
            get; set;
        }

        public string PlatformPortalClientURL {
            get; set;
        }

        public string PublisherPortalClientURL {
            get; set;
        }

        public string BusinessPortalClientURL {
            get; set;
        }

        public string CustomerPortalClientURL {
            get; set;
        }

    }
}
