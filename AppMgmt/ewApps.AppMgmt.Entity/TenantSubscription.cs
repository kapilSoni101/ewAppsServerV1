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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.Entity {

    [Table("TenantSubscription", Schema = "am")]
    public class TenantSubscription:BaseEntity {

        /// <summary>
        /// The entity name.
        /// </summary>
        public const string EntityName = "TenantSubscription";

        [Required]
        public Guid TenantId {
            get; set;
        }

        [Required]
        public Guid AppId {
            get; set;
        }

        public Guid SystemConfId {
            get; set;
        }

        public int Term {
            get; set;
        }

        [Required]
        public DateTime SubscriptionStartDate {
            get; set;
        }

        [Required]
        public DateTime SubscriptionStartEnd {
            get; set;
        }

        [Required]
        public int BusinessUserCount {
            get; set;
        }

        [Required]
        public Guid SubscriptionPlanId {
            get; set;
        }

        /// <summary>
        /// The PriceInDollar.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,5)")]
        public decimal PriceInDollar {
            get; set;
        }
       
        public int PaymentCycle {
            get; set;
        }

        [MaxLength(4000)]
        /// <summary>
        /// InactiveComment for application in-active.
        /// </summary>    
        public string InactiveComment {
            get;
            set;
        }

        public int Status {
            get; set;
        }

        [Required]
        public bool CustomizeSubscription {
            get; set;
        }

        public Guid LogoThumbnailId {
            get; set;
        }
        
        /// <summary>
        ///The Id of Theme 
        /// </summary>
        [Required]
        public Guid ThemeId {
            get; set;
        }

        /*
         CreditCardNo	
         PrimaryAccountNo	
         */

        public bool AutoRenewal {
            get; set;
        }

        [Required]
        public bool OneTimePlan {
            get; set;
        }

        public int? UserPerCustomerCount {
            get; set;
        }

        public int? CustomerUserCount {
            get; set;
        }

        public int? ShipmentCount {
            get; set;
        }

        public int? ShipmentUnit {
            get; set;
        }

    }
}
