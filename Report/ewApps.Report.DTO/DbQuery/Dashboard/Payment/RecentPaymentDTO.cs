/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 19 December 2018
 */

using System;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {

    /// <summary>
    /// This class is a DTO with consolidate information of <see cref="RecentPaymentDTO"/> .
    /// </summary>
    public class RecentPaymentDTO : BaseDTO {

    /// <summary>
    /// system generated payment Id .
    /// </summary>
    public new Guid Id {
      get; set;
    }

    /// <summary>
    /// system generated invoice no.
    /// </summary>
    public string PaymentNo {
      get; set;
        }

        /// <summary>
        /// system generated invoice no.
        /// </summary>
        public string ErparInvoiceKey {
            get; set;
        }

        

    /// <summary>
    /// name of customer.
    /// </summary>
    public string CustomerName {
      get; set;
    }

    /// <summary>
    /// Payment creation date and time (in UTC).
    /// </summary>
    public DateTime CreatedOn {
      get; set;
    }

    /// <summary>
    /// Payment status.
    /// </summary>
    public string Status {
      get; set;
    }

    ///// <summary>
    ///// Payment Amount.
    ///// </summary>
    //public Decimal Amount {
    //  get; set;
    //}

    /// <summary>
    /// Paid Payment Amount.
    /// </summary>
    public Decimal CreditAmount {
      get; set;
    }


    ///// <summary>
    ///// calulated excahge rate Amount.
    ///// </summary>
    //[NotMapped]
    //public decimal CalculateAmount
    //{
    //  get; set;
    //}

    ///// <summary>
    ///// Calulated Amountpaid as per exchangerate.
    ///// </summary>
    //[NotMapped]
    //public decimal CalculateCreditAmount
    //{
    //  get; set;
    //}


    ///// <summary>
    ///// customer currency code.
    ///// </summary>
    //public int DocumentCurrencyCode
    //{
    //  get; set;
    //}

    ///// <summary>
    ///// Currency conversion rate FromCurrency to ToCurrency.
    ///// </summary>
    //public decimal FinalConversionRate
    //{
    //  get; set;
    //}


  }
}
