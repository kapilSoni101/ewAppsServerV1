using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO
{
 public class BusVendorApplicationDTO
  {
    /// <summary>
    /// App Identifier.
    /// </summary>
    public Guid ID
    {
      get; set;
    }

    /// <summary>
    /// AppKey of the application
    /// </summary>
    public string AppKey
    {
      get; set;
    }

    /// <summary>
    /// Application name of the application
    /// </summary>
    public string AppName
    {
      get; set;
    }

    /// <summary>
    /// Subscription plan name of the plan given to the business.
    /// </summary>
    public string SubscriptionPlanName
    {
      get; set;
    }

    /// <summary>
    /// Expiry date of the subscription given to the business.
    /// </summary>
    public DateTime SubscriptionExpiry
    {
      get; set;
    }

    /// <summary>
    /// Subscription given by
    /// </summary>
    public string SubscribedBy
    {
      get; set;
    }

    /// <summary>
    /// Subscription date.
    /// </summary>
    public DateTime SubscribedOn
    {
      get; set;
    }

    /// <summary>
    /// Subscription auto renewal key.
    /// </summary>
    public bool AutoRenewal
    {
      get; set;
    }

    /// <summary>
    /// Is application assigned to the customer.
    /// </summary>
    public bool Assigned
    {
      get; set;
    }
  }
}
