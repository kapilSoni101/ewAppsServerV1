using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {

  /// <summary>
  /// Detail of a recurring payment.
  /// </summary>
  public class RecurringPaymentViewDTO {

    public Guid ID
    {
      get; set;
    }

    public string OrderId
    {
      get; set;
    }

    public string CustomerName
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
    /// When scheduled event occured at first time. 
    /// </summary>
    public DateTime EndDate
    {
      get; set;
    }

    /// <summary>
    /// Next payment date.
    /// Upcoming Pay On: date of the upcoming payment.
    /// </summary>
    public DateTime NextPaymentdate
    {
      get; set;
    }

    /// <summary>
    /// ewApps recurring period(TermPeriod, Month, Quater, yearly)
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
    /// Payment for each pay.
    /// </summary>
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

    public decimal InvoiceTax
    {
      get; set;
    }

    /// <summary>
    /// remaining term count
    /// </summary>
    public int RemainingTermCount
    {
      get; set;
    }

    public int Status
    {
      get; set;
    }

  }
}