using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.NotificationService;

namespace ewApps.AppPortal.QData {
    public interface IQVendorNotificationRecipientsData {

        List<NotificationRecipient> GetVendorUserAppOnBoardRecipients(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, Guid onboardedUserId);

    }
}
