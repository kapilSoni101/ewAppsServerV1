/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author:Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 25 Feb 2019
 * 
 * Contributor/s: Amit Mundra
 * Last Updated On: 25 Feb 2019
 */
using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.Payment.DTO {

    public class RecurringPaymentDTO {

        public Guid CustomerId {
            get; set;
        }

        public decimal Amount {
            get; set;
        }

        public decimal Tax {
            get; set;
        }

        public Guid CreatedBy {
            get; set;
        }

        /// <summary>
        /// order Id 
        /// </summary>
        public string OrderId {
            get; set;
        }

        public string Description {
            get; set;
        }

        /* Begin: Payment Information */



        /// <summary>
        /// Account type:Saving,Checking
        /// </summary>
        public BankAccountTypeEnum AccountType {
            get; set;
        }

        /// <summary>
        ///  Its CCD, SSD
        /// </summary>
        public string PaymentEntryType {
            get; set;
        }

        /* End: Payment Information */

        /* Begin Billing Address Info*/

        public string Street1 {
            get; set;
        }

        public string Street2 {
            get; set;
        }

        public string Street3 {
            get; set;
        }

        public string State {
            get; set;
        }

        public string City {
            get; set;
        }

        public string ZipCode {
            get; set;
        }

        public string Phone {
            get; set;
        }
        public string Country {
            get; set;
        }
        public string FaxNo {
            get; set;
        }

        public string Website {
            get; set;
        }

        public string ContactPhoneNo {
            get; set;
        }

        public string ContactName {
            get; set;
        }

        /*END : Address Info */

        /// <summary>
        /// Payment information.
        /// </summary>
        public UserPaymentInfoModel UserPaymentInfoModel {
            get; set;
        }

    }

}
