using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DS
{
  public interface IBusinessEntityNotificationHandler
  {
    void SendAddARInvoiceToBizUser(ARInvoiceNotificationDTO aRInvoiceNotificationDTO);
    void SendAddARInvoiceToCustomerUserInIntegratedMode(ARInvoiceNotificationDTO aRInvoiceNotificationDTO);

    void SendBulkAddARInvoiceToBizPaymentUserInIntegratedMode(NotificationCommonDetailDTO notificationCommonDetailDTO, int newInvoiceCount, Guid businessTenantId, string updatedByName, string updatedByUserNo, DateTime updatedDate);

    void SendUpdateARInvoiceToBizPaymentUserInIntegratedMode(ARInvoiceNotificationDTO aRInvoiceNotificationDTO);

    void SendAddCustomerInIntegratedMode(CustomerNotificationDTO customerNotificationDTO, long bizNotificationEnum);

    void SendUpdateCustomerInIntegratedMode(CustomerNotificationDTO customerNotificationDTO, long bizNotificationEnum);

    Task SendAddSalesOrderToBizUserInIntegratedModeAsync(SONotificationDTO soNotificationDTO);

      void SendASNToBizUser(ASNNotificationDTO aSNNotificationDTO);

        void SendContractNotificationToBizUser(ContractNotificationDTO contractNotificationDTO);

        void SendUpdateASNNotificationToBizCustomerUserInIntegratedMode(ASNNotificationDTO aSNNotificationDTO);

        void SendUpdateContractToBizCustomerUserInIntegratedMode(ContractNotificationDTO contractNotificationDTO);

        void SendDeliveryNotificationToBizUser(DeliveryNotificationDTO deliveryNotificationDTO);
  }
}
