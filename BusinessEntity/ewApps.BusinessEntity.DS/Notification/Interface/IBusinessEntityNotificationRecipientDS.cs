using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.NotificationService;

namespace ewApps.BusinessEntity.DS
{
  public interface IBusinessEntityNotificationRecipientDS
  {

    Task<List<NotificationRecipient>> GetAddARInvoiceNotificationBizRecipientAsync(Guid publisherTenantId, Guid businessTenantId, int userType, int userStatus, CancellationToken cancellationToken = default(CancellationToken));

    Task<List<NotificationRecipient>> GetUpdateARInvoiceNotificationBizRecipientAsync(Guid publisherTenantId, Guid businessTenantId, int userType, int userStatus, CancellationToken cancellationToken = default(CancellationToken));


    Task<List<NotificationRecipient>> GetAddCustomerNotificationRecipientAsync(Guid publisherTenantId, Guid businessTenantId, Guid loginUserId, int userType, int userStatus, CancellationToken cancellationToken = default(CancellationToken));

    Task<List<NotificationRecipient>> GetUpdateCustomerNotificationRecipientAsync(Guid publisherTenantId, Guid businessTenantId, Guid loginUserId, int userType, int userStatus, CancellationToken cancellationToken = default(CancellationToken));

    Task<List<NotificationRecipient>> GetAddSalesOrderNotificationRecipientAsync(Guid publisherTenantId, Guid businessTenantId, int userType, int userStatus, CancellationToken cancellationToken = default(CancellationToken));

    Task<List<NotificationRecipient>> GetARInvoiceNotificationCustomerUserRecipientAsync(Guid publisherTenantId, Guid businessTenantId, Guid businessPartnerTenantId, CancellationToken cancellationToken = default(CancellationToken));

    Task<List<NotificationRecipient>> GetNotificationBizRecipientAsync(Guid publisherTenantId, Guid businessTenantId, int userType, int userStatus, long permissionBitMask, long preferenceBitMask, CancellationToken cancellationToken = default(CancellationToken));

    Task<List<NotificationRecipient>> GetAddSalesQuotationNotificationRecipientAsync(Guid publisherTenantId, Guid businessTenantId, int userType, int userStatus, CancellationToken cancellationToken = default(CancellationToken));
  }
}
