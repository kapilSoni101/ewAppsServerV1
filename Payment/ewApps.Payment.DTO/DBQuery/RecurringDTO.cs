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

namespace ewApps.Payment.DTO {

  /// <summary>
  /// Contains recurring defination, it contains all the information of a scheduled payment.
  /// </summary>
  public class RecurringDTO {

    /// <summary>
    /// 
    /// </summary>
    public Guid ID
    {
      get; set;
    }

    public string JobName
    {
      get; set;
    }

    /// <summary>
    /// When scheduled event occured at first time. 
    /// </summary>
    public DateTime StartDate
    {
      get; set;
    }

    /// <summary>
    /// When scheduled event occured at last time. 
    /// </summary>
    public DateTime EndDate
    {
      get; set;
    }

    /// <summary>
    /// ewApps recurring period 
    /// </summary>
    public int RecurringPeriod
    {
      get; set;
    }

    /// <summary>
    /// ewApps recurring terms
    /// </summary>
    public int RecurringTerms
    {
      get; set;
    }

    /// <summary>
    /// Active/Inactive
    /// </summary>
    public int Status
    {
      get; set;
    }

    public decimal TermAmount
    {
      get; set;
    }

    /// <summary>
    /// Total amount to pay
    /// </summary>
    public decimal TotalAmount
    {
      get; set;
    }

    public string SourceCallback
    {
      get; set;
    }

    public string SourceEventName
    {
      get; set;
    }

    /// <summary>
    /// Contains detail of a payment object.
    /// </summary>
    public RecurringPaymentDTO RecurringPaymentDetail
    {
      get; set;
    }

    /// <summary>
    /// Customer Account detail id.
    /// </summary>
    public Guid CustomerAccountId
    {
      get; set;
    }

    /// <summary>
    /// Created by user.
    /// </summary>
    public Guid CreatedBy
    {
      get; set;
    }

    /// <summary>
    /// Created by user name.
    /// </summary>
    public string CreatedByName
    {
      get; set;
    }

    /// <summary>
    /// Recurring Creation date.
    /// </summary>
    public DateTime? CreatedOn
    {
      get; set;
    }

    /// <summary>
    ///Remaining pay count.
    /// </summary>
    public int RemainingTermCount
    {
      get; set;
    }

    /// <summary>
    /// Business currency code.
    /// </summary>    
    public int FromCurrencyCode
    {
      get; set;
    }

    /// <summary>
    /// customer currency code.
    /// </summary>    
    public int ToCurrencyCode
    {
      get; set;
    }

    /// <summary>
    /// Currency conversion rate FromCurrency to ToCurrency.
    /// </summary>    
    public decimal ExchangeRate
    {
      get; set;
    }

  }

}
