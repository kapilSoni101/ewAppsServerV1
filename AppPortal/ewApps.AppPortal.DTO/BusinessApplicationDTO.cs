/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 04 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class BusinessApplicationDTO {

        public Guid AppId {
            get; set;
        }

        public string Name {
            get; set;
        }

        public Guid ThemeId {
            get; set;
        }

        /// <summary>
        /// Tenant Language
        /// </summary>
        public string Language {
            get; set;
        }

        /// <summary>
        /// Tenant Currency
        /// </summary>
        public string Currency {
            get; set;
        }

        /// <summary>
        /// Tenant TimeZone
        /// </summary>
        public string TimeZone {
            get; set;
        }

        /// <summary>
        /// Tenant DateTimeFormat
        /// </summary>
        public string DateTimeFormat {
            get;
            set;
        }

        [NotMapped]
        /// <summary>
        /// Whether application is active or not
        /// </summary>
        public bool AppActive {
            get;
            set;
        } = true;

        [NotMapped]
        public IEnumerable<AppServiceDTO> appServices;

        //[NotMapped]
        //public IEnumerable<TenantApplicationSubscriptionDQ> appSubscriptions;
        [NotMapped]
        public IEnumerable<SubscriptionPlanInfoDTO> appSubscriptions;
    }
}
