using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.Common {

  public enum PartnerAccessEnum {
    ViewPartners = 0,
    ManagePartners = 1
  }

  public enum PartnerPaymentAccessEnum {
    ViewInvoices = 0,
    ManageInvoices = 1
  }

  public enum PortalSettingAccessEnum {
    ViewPortalSettings = 0,
    ManagePortalSettings = 1 
  }

  public enum PartnerHelpAndSupportAccessEnum {
    ViewCustomerTicket = 0,
    ManageCustomerTicket = 1 
  }

  public enum PartnerAlertsAccessEnum {
    ViewAndManageAlerts = 0 
  }

}
