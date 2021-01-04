/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author:Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 12 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 12 September 2019
 */

using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.Shipment.DTO {
    public class FedExCarrierServiceAcctDetailDTO : BaseDTO {

        public new Guid ID {
            get; set;
        }

        public string AccountLabel {
            get; set;
        }
        public string UserName {
            get; set;
        }
        public string Password {
            get; set;
        }
        public string AccountNo {
            get; set;
        }
        public string AccessKey {
            get; set;
        }
        public string MeterNo {
            get; set;
        }
        public bool UseCustomerAccount {
            get; set;
        }
    }
}
