/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 29 October 2019
 * 
 * Contributor/s: 
 * Last Updated On: 29 October 2019
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class AddPreAuthPaymentDTO {

        public Guid ID {
            get; set;
        }

        public Guid BusinessId {
            get; set;
        }

        public Guid TenantId {
            get; set;
        }

        public decimal AmountPaid {
            get; set;
        }

        public decimal AmountPaidFC {
            get; set;
        }


        // Business currency
        public string LocalCurrency {
            get; set;
        }

        public string CustomerCurrency {
            get; set;
        }

        /// <summary>
        /// Number of times can be use for payment.
        /// </summary>
        public int MaxTotalPaymentCount {
            get; set;
        }

        /// <summary>
        /// Payment currency.
        /// </summary>
        public string PaymentTransectionCurrency {
            get; set;
        }

        public string Note {
            get; set;
        }

        public DateTime OriginationDate {
            get; set;
        }

        /// <summary>
        /// Adding customer DTO to get customer detail 
        /// from application when actual payment is made intead of creating it when it is added to application.
        /// COnnector has responsibility to add and update it whenever required.
        /// </summary>
        public UserPreAuthPaymentInfoModel userPaymentInfoModel {
            get; set;
        }

        /// <summary>
        /// Payment account id.
        /// </summary>
        public Guid CustomerAccountId {
            get; set;
        }

        /// <summary>
        /// Save customer account info.
        /// </summary>
        public bool SaveAccountInfo {
            get; set;
        } = false;

        public string PayeeName {
            get; set;
        }

        /// <summary>
        /// Client Ip address where Payment done 
        /// </summary>
        public string ClientIP {
            get; set;
        }

        /// <summary>
        /// Client Browser Information where Payment Done 
        /// </summary>
        public string ClientBrowser {
            get; set;
        }


        /// <summary>
        /// Client Operating System Information where Payment Done 
        /// </summary>
        public string ClientOS {
            get; set;
        }

    }
}
