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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Entity {


    //This class contain information about grid view settings
    [Table("ViewSettings", Schema = "ap")]
    public class ViewSettings:BaseEntity {

        [MaxLength(200)]
        public string ViewName {
            get; set;
        }

        [MaxLength(200)]
        public string ScreenId {
            get; set;
        }

        
        public bool IsDefault {
            get; set;
        }

        [MaxLength(200)]
        public string AppKey {
            get; set;
        }


        public string ViewSettingJson {
            get; set;
        }


        public Guid TenantUserId {
            get; set;
        }
    }
}
