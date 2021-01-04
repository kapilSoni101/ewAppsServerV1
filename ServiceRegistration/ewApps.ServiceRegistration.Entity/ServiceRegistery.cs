/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Hari Dudani, Asha Sharda
 * Date: 25 Jun 2018
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.ServiceRegistration.Entity
{
  [Table("ServiceRegistry")]
  public class ServiceRegistry
  {
    /// <summary>
    /// Internal Id
    /// </summary>
    [Key]
    public Guid ID
    {
      get; set;
    }
    [Required]

    /// <summary>
    /// Unique reference Id to be used to access or delete that service registration inog , Generated if not passed by client
    /// </summary>
    public string ServiceRefId
    {
      get; set;
    }

    /// <summary>
    /// Name of the Service
    /// </summary>
    public string ServiceName
    {
      get; set;
    }

    /// <summary>
    /// Preference criteriacan be set at the time of Registering aservice, thiswill help to access a service with that preference criteria
    /// Preference criteriaand Service Name are Join Unique Key
    /// </summary>
    public string PreferenceCriteria
    {
      get; set;
    }
    /// <summary>
    /// Base URL to access the service
    /// </summary>
    public string ServiceBaseURL
    {
      get; set;
    }
    /// <summary>
    /// Any other info required to access the service,JSON object
    /// </summary>
    public string ServiceInfo
    {
      get; set;
    }

    /// <summary>
    /// Comma seperated list of supported communication protocols Like REST,XMPP etc
    /// </summary>
    [Required]
    public string SupportedProtocol
    {
      get; set;
    }

    /// <summary>
    /// Just used it for Active/Inactive purpose
    /// Not used right now
    /// </summary>
    public string Status
    {
      get; set;
    }

  }
}
