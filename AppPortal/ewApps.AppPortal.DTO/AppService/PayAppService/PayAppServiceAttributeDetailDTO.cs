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
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class PayAppServiceAttributeDetailDTO {

        public Guid ID {
            get; set;
        }

        public string Name {
            get; set;
        }

        public string AttributeKey {
            get; set;
        }

        public bool Active {
            get; set;
        }

        [NotMapped]
        public List<AppServiceAcctDetailDTO> AppServiceAccountList {
            get; set;
        }

        [NotMapped]
        public BusVCACHPayAttrDTO BusVCACHPayAttrDTO {
            get; set;
        }

        [NotMapped]
        public BusTSYSCreditCardPayAttrDTO BusTSYSCreditCardPayAttrDTO {
            get; set;
        }

    }
}
