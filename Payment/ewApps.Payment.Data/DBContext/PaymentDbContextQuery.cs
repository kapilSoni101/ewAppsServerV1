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

//using ewApps.Core.NotificationService;
using ewApps.Payment.DTO;
using Microsoft.EntityFrameworkCore;

namespace ewApps.Payment.Data {

    public partial class PaymentDbContext {

        public DbQuery<BAARInvoiceViewDTO> BAARInvoiceDetailDQQuery {
            get; set;
        }

        public DbQuery<PaymentDetailDQ> PaymentDetailDQQuery {
            get; set;
        }

        public DbQuery<PayAppServiceAttributeDetailDTO> PayAppServiceAttributeDetailDTOQuery {
            get; set;
        }

        public DbQuery<PayAppServiceDetailDTO> PayAppServiceDetailDTOQuery {
            get; set;
        }

        public DbQuery<AppServiceAcctDetailDTO> AppServiceAcctDetailDTOQuery {
            get; set;
        }

        public DbQuery<ASNotificationDTO> ASNotificationDTOQuery {
            get; set;
        }

        public DbQuery<PreferenceViewDTO> PreferenceViewDTOQuery {
            get; set;
        }

    }
}
