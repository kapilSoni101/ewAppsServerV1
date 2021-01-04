using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.QData;

namespace ewApps.BusinessEntity.DS {
    public class QNotificationDS:IQNotificationDS {

        private IQNotificationData _qNotificationData;

        public QNotificationDS(IQNotificationData qNotificationData) {
            _qNotificationData = qNotificationData;
        }


        public async Task<ARInvoiceNotificationDTO> GetARInvoiceDetailByInvoiceERPKeyAsync(string invoiceERPKey, Guid businessTenantId, string appKey, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qNotificationData.GetARInvoiceDetailByInvoiceERPKeyAsync(invoiceERPKey, businessTenantId, appKey, cancellationToken);
        }

        public async Task<ASNNotificationDTO> GetASNDetailByASNERPKeyAsync(string aSNERPKey, Guid businessTenantId, string appKey, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qNotificationData.GetASNDetailByASNERPKeyAsync(aSNERPKey, businessTenantId, appKey, cancellationToken);
        }

        public async Task<ContractNotificationDTO> GetContractDetailByContractERPKeyAsync(string contractERPKey, Guid businessTenantId, string appKey, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qNotificationData.GetContractDetailByContractERPKeyAsync(contractERPKey, businessTenantId, appKey, cancellationToken);
        }

        public async Task<DeliveryNotificationDTO> GetDeliveryDetailByDeliveryERPKeyAsync(string DeliveryERPKey, Guid businessTenantId, string appKey, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qNotificationData.GetDeliveryDetailByDeliveryERPKeyAsync(DeliveryERPKey, businessTenantId, appKey, cancellationToken);
        }

        public async Task<SONotificationDTO> GetSODetailBySOERPKeyAsync(string soERPKey, Guid businessTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qNotificationData.GetSODetailBySOERPKeyAsync(soERPKey, businessTenantId, cancellationToken);
        }

        public async Task<CustomerNotificationDTO> GetCustomerDetailByCustomerERPKeyAsync(string customerERPKey, Guid businessTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qNotificationData.GetCustomerDetailByCustomerERPKeyAsync(customerERPKey, businessTenantId, cancellationToken);
        }
    }
}
