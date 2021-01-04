using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.NotificationService;

namespace ewApps.AppPortal.DS {
    public interface IVendorNotificationRecipientDS {

        #region Vendor Onboard Notification

        List<NotificationRecipient> GetVendorUserAppOnboardRecipients(Guid appId, Guid businessTenantId, Guid vendorTenantId, Guid onboardedUserId);

        #endregion
    }
}
