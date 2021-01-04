using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.Common {
  public class BusinessEntityAppSettings {

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
    public string ShipmentApiUrl {
      get; set;
    }

    public string AppMgmtApiUrl {
      get; set;
    }

    public string AppPortalApiUrl {
      get; set;
    }

    public string BusinessConnectorApiUrl {
      get; set;
    }

    public string[] CrossOriginsUrls {
      get; set;
    }

        public string BizPayAppUrl {
            get; set;
        }
    public string BusinessPortalAppUrl
    {
      get; set;
    }
    public string CustomerPortalAppUrl
    {
      get; set;
    }

  }
}
