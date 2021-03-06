﻿/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;

namespace ewApps.AppPortal.Entity {

    /// <summary>
    /// Publisher table represting all the Publisher.
    /// </summary>
    [Table("TenantAppLinking", Schema = "ap")]
    public class TenantAppLinking:BaseEntity {

        /// <summary>
        /// The entity name publisher.
        /// </summary>
        public const string EntityName = "TenantAppLinking";


        public Guid AppId {
            get; set;
        }

        public Guid ThemeId {
            get; set;
        }

    }
}
