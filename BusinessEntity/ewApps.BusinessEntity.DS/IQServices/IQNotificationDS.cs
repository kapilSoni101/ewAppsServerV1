using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;

namespace ewApps.BusinessEntity {
    public interface IQNotificationDS {
        Task<ARInvoiceNotificationDTO> GetARInvoiceDetailByInvoiceERPKeyAsync(string invoiceERPKey, Guid businessTenantId, string appKey, CancellationToken cancellationToken = default(CancellationToken));

        Task<SONotificationDTO> GetSODetailBySOERPKeyAsync(string soERPKey, Guid businessTenantId, CancellationToken cancellationToken = default(CancellationToken));

        Task<CustomerNotificationDTO> GetCustomerDetailByCustomerERPKeyAsync(string customerERPKey, Guid businessTenantId, CancellationToken cancellationToken = default(CancellationToken));

        Task<ASNNotificationDTO> GetASNDetailByASNERPKeyAsync(string aSNERPKey, Guid businessTenantId, string appKey, CancellationToken cancellationToken = default(CancellationToken));


        Task<ContractNotificationDTO> GetContractDetailByContractERPKeyAsync(string contractERPKey, Guid businessTenantId, string appKey, CancellationToken cancellationToken = default(CancellationToken));

        Task<DeliveryNotificationDTO> GetDeliveryDetailByDeliveryERPKeyAsync(string DeliveryERPKey, Guid businessTenantId, string appKey, CancellationToken cancellationToken = default(CancellationToken));
    }
}
