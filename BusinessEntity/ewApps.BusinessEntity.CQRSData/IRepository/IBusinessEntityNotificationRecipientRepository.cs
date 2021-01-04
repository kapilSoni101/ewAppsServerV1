using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.NotificationService;

namespace ewApps.BusinessEntity.QData {
    public interface IBusinessEntityNotificationRecipientRepository {

        Task<List<NotificationRecipient>> GetARInvoiceNotificationBizRecipientForPayAppAsync(Guid appId, Guid businessTenantId, long invoicePermissionMask, int userType, int userStatus, long invoicePreferenceValue, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<NotificationRecipient>> GetARInvoiceNotificationBizRecipientForCustAppAsync(Guid appId, Guid businessTenantId, long invoicePermissionMask, int userType, int userStatus, long invoicePreferenceValue , CancellationToken cancellationToken = default(CancellationToken));

        Task<List<NotificationRecipient>> GetAddCustomerNotificationRecipientAsync(Guid appId, Guid businessTenantId,Guid loginUserId, int invoicePermissionMask, int userType, int userStatus, long customerPreferenceValue, CancellationToken cancellationToken = default(CancellationToken));

    Task<List<NotificationRecipient>> GetAddCustomerNotificationRecipientForCustAppAsync(Guid appId, Guid businessTenantId, Guid loginUserId, int customerPermissionMask, int userType, int userStatus, long customerPreferenceValue, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<NotificationRecipient>> GetSalesOrderNotificationRecipientAsync(Guid appId, Guid businessTenantId, long permissionBitMask, int userType, int userStatus, long preferenceEnum, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<NotificationRecipient>> GetARInvoiceCustomerRecipientAsync(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, int invoicePreferenceValue, int permissionBitMask, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<NotificationRecipient>> GetSalesQuotationNotificationRecipientAsync(Guid appId, Guid businessTenantId, long permissionBitMask, int userType, int userStatus, long preferenceEnum, CancellationToken cancellationToken = default(CancellationToken));
    }
}
