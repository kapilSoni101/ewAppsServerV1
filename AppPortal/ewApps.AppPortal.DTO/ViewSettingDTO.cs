/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 31 Oct 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 31 Oct 2019
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class ViewSettingDTO {


        public Guid ID {
            get; set;
        }

        public string ViewName {
            get; set;
        }

      
        public string ScreenId {
            get; set;
        }

    
        public bool IsDefault {
            get; set;
        }

      
        public string AppKey {
            get; set;
        }


        public string ViewSettingJson {
            get; set;
        }

        [NotMapped]
        public Guid TenantUserId {
            get; set;
        }


        [NotMapped]
        public Guid TenantId {
            get; set;
        }
    }
}
