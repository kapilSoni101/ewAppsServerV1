/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using ewApps.Core.NotificationService;
using ewApps.Payment.DTO;
using Microsoft.EntityFrameworkCore;

namespace ewApps.Payment.QData {

    public partial class QPaymentDBContext {

        public DbQuery<PayAppServiceAttributeDetailDTO> PayAppServiceAttributeDetailDTOQuery {
            get; set;
        }

        public DbQuery<PayAppServiceDetailDTO> PayAppServiceDetailDTOQuery {
            get; set;
        }

        public DbQuery<AppServiceAcctDetailDTO> AppServiceAcctDetailDTOQuery {
            get; set;
        }

        public DbQuery<InvoiceInfoDTO> InvoiceInfoDTOQuery {
            get;set;
        }

        public DbQuery<PaymentDetailModelDTO> PaymentDetailModelDTOQuery {
            get;set;
        }

        public DbQuery<PaymentAdvanceDetailModelDTO> PaymentAdvanceDetailModelDTOQuery {
            get;set;
        }

        public DbQuery<PaymentInfoDTO> PaymentInfoDTOQuery {
            get; set;
        }
        public DbQuery<NotificationRecipient> NotificationRecipientQuery {
            get; set;
        }
        public DbQuery<PaymentRelatedDataDTO> PaymentRelatedDataDTOQuery {
            get; set;
        }

        public DbQuery<PreAuthPaymentRelatedDataDTO> PreAuthPaymentRelatedDataDTOQuery {
            get;set;
        }

        public DbQuery<TenantInfo> TenantInfoQuery {
            get;set;
        }

        public DbQuery<UserTenantLinkingEntityDTO> UserTenantLinkingEntityDTOQuery {
            get;set;
        }
    }
}
