using ewApps.AppPortal.QData;
using ewApps.Core.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.AppPortal.DS {
    public class VendorNotificationRecipientDS:IVendorNotificationRecipientDS {
        IQVendorNotificationRecipientsData _qVendorNotificationRecipientsData;

        public VendorNotificationRecipientDS(IQVendorNotificationRecipientsData qVendorNotificationRecipientsData) {
            _qVendorNotificationRecipientsData = qVendorNotificationRecipientsData;
        }

        /// <inheritdoc/>
        public List<NotificationRecipient> GetVendorUserAppOnboardRecipients(Guid appId, Guid businessTenantId, Guid vendorTenantId, Guid onboardedUserId) {
            return _qVendorNotificationRecipientsData.GetVendorUserAppOnBoardRecipients(appId, businessTenantId, vendorTenantId, onboardedUserId);
        }



    }
}
