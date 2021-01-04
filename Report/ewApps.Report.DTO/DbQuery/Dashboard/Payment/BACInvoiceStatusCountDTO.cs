//dbquery

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

using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {

    /// <summary>
    /// This class is a DTO with consolidate information of <see cref="BACInvoiceStatusCountDTO"/> .
    /// </summary>
    public class BACInvoiceStatusCountDTO :BaseDTO{

    /// <summary>
    /// total no of open or unpaid invoices.
    /// </summary>
    public int TotalOpenInvoices
    {
      get; set;
    }

    /// <summary>
    /// total no of partial paid invoices.
    /// </summary>
    public int PartialPaid
    {
      get; set;
    }

    /// <summary>
    /// total no of paid invoices.
    /// </summary>
    public int TotalPaidInvoices
    {
      get; set;
    }

    /// <summary>
    /// total no of last week paid invoices.
    /// </summary>
    public int PaidLastWeek
    {
      get; set;
    }

    /// <summary>
    /// total amount paid last week.
    /// </summary>
    public decimal AmountPaidLaskWeek
    {
      get; set;
    }

    /// <summary>
    /// percentage of partial paid invoices.
    /// </summary>
    [NotMapped]
    public float PartialPaidPercentage {
      get; set;
    }

    /// <summary>
    /// percentage of paid invoices.
    /// </summary>
    [NotMapped]
    public float PaidPercentage {
      get; set;
    }

    /// <summary>
    /// percentage of not paid invoices.
    /// </summary>
    [NotMapped]
    public float NotPaidPercentage {
      get; set;
    }
  }
}
