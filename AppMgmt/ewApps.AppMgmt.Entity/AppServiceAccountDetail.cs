﻿/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Rajesh Thakur<rthakur@eworkplaceapps.com>
 * Date: 10 January 2019
 * 
 * Contributor/s: Sourabh Agrawal 
 * Last Updated On: 08 August 2019
 */


using System;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.Entity {

    [Table("AppServiceAccountDetail", Schema = "am")]
    public class AppServiceAccountDetail:BaseEntity {

        public Guid ServiceId {
            get; set;
        }

        public Guid ServiceAttributeId {
            get; set;
        }

        public Guid EntityId {
            get; set;
        }

        public int EntityType {
            get; set;
        }

        public string AccountJson {
            get; set;
        }

        public Guid AppId {
            get; set;
        }

    }
}


