using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;

namespace ewApps.BusinessEntity.QData {
    public interface IQNotificationData  {
        Task<ARInvoiceNotificationDTO> GetARInvoiceDetailByInvoiceERPKeyAsync(string invoiceERPKey, Guid businessTenantId, string appKey, CancellationToken cancellationToken = default(CancellationToken));

        Task<ARInvoiceNotificationDTO> GetARInvoiceDetailByInvoiceIdAsync(Guid invoiceId, Guid businessTenantId, string appKey, CancellationToken cancellationToken = default(CancellationToken));

        Task<NotificationCommonDetailDTO> GetNotificationCommonDetailDTOAsync(Guid businessTenantId, string appKey , CancellationToken cancellationToken = default(CancellationToken));

        Task<List<AppInfoDTO>> GetAppListByBusinessTenantIdAsync(Guid publisherTenantId, Guid businessTenantId, CancellationToken cancellationToken = default(CancellationToken));

        Task<CustomerNotificationDTO> GetCustomerDetailByCustomerERPKeyAsync(string customerERPKey, Guid businessTenantId, CancellationToken cancellationToken = default(CancellationToken));

        Task<SONotificationDTO> GetSODetailBySOERPKeyAsync(string soERPKey, Guid businessTenantId, CancellationToken cancellationToken = default(CancellationToken));
        Task<List<AppInfoDTO>> GetAppListByCustomerTenantIdAsync(Guid publisherTenantId, Guid customerTenantId, CancellationToken cancellationToken = default(CancellationToken));

        Task<ASNNotificationDTO> GetASNDetailByASNERPKeyAsync(string aSNERPKey, Guid businessTenantId, string appKey, CancellationToken cancellationToken = default(CancellationToken));

        Task<ContractNotificationDTO> GetContractDetailByContractERPKeyAsync(string contractERPKey, Guid businessTenantId, string appKey, CancellationToken cancellationToken = default(CancellationToken));

        Task<DeliveryNotificationDTO> GetDeliveryDetailByDeliveryERPKeyAsync(string deliveryERPKey, Guid businessTenantId, string appKey, CancellationToken cancellationToken = default(CancellationToken));

    }
}
