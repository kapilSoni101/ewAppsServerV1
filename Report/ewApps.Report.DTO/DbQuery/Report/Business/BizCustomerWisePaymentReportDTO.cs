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
    /// This class is a DTO with consolidate information of <see cref="BizCustomerWisePaymentReportDTO"/>.
    /// </summary>
    public class BizCustomerWisePaymentReportDTO :BaseDTO {

    /// <summary>
    /// System generated unique Payment id.
    /// </summary>
    public new Guid ID {
      get; set;
    }

    /// <summary>
    /// System generated CustomerId id.
    /// </summary>
    public string CustomerID {
      get; set;
    }

    /// <summary>
    /// Amount Paid BY Customer 
    /// </summary> 
    public decimal CreditAmount {
      get; set;
    }


    /// <summary>
    /// calulate credit amount with excahge rate Amount.
    /// </summary>
    [NotMapped]
    public decimal CalculateCreditAmount
    {
      get; set;
    }

    /// <summary>
    /// customer currency code.
    /// </summary>
    public int DocumentCurrencyCode
    {
      get; set;
    }

    /// <summary>
    /// Currency conversion rate FromCurrency to ToCurrency.
    /// </summary>
    public decimal FinalConversionRate
    {
      get; set;
    }

    /// <summary>
    /// The CustomerName.
    /// </summary>
    /// <remarks>It's empty if not applicable.</remarks>
    public string CustomerName {
      get; set;
    }

    /// <summary>
    /// Total Number of Paid Invoices By Customer
    /// </summary>
    public int PaidInvoices {
      get; set;
    }

    /// <summary>
    /// Total Number of Partially Paid Invoices By Customer
    /// </summary>
    public int PartialPaidInvoices {
      get; set;
    }

    /// <summary>
    /// Total Number of UnPaid Invoices By Customer
    /// </summary>
    public int OpenInvoices {
      get; set;
    }   

  }
}
