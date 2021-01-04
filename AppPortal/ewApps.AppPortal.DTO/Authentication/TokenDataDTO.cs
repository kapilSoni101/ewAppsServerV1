// Response

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ewApps.AppPortal.DTO {

  /// <summary>
  /// This class is a DTO for token data.
  /// </summary>
  public class TokenDataDTO {

    /// <summary>
    /// Publisher name.
    /// </summary>
    public string PublisherName {
      get; set;
    }

    /// <summary>
    /// Publisher portal logo.
    /// </summary>
    public string PublisherLogo {
      get; set;
    }

    /// <summary>
    /// Tenant Name.
    /// </summary>
    public string BusinessName {
      get; set;
    }

    /// <summary>
    /// Tenant logo of the user tenant.
    /// </summary>
    public string BusinessLogo {
      get; set;
    }

    /// <summary>
    /// Sub domain of the user tenant.
    /// </summary>
    public string SubDomain {
      get; set;
    }

    /// <summary>
    /// Tenant Identifier.
    /// </summary>
    public Guid TenantId {
      get; set;
    }

    /// <summary>
    /// Email of the user.
    /// </summary>
    public string Email {
      get; set;
    }

    /// <summary>
    /// Code of the identity user when creating user.
    /// </summary>
    public string Code {
      get; set;
    }

    /// <summary>
    /// Identity user identityfier of the user.
    /// </summary>
    public Guid IdentityUserId {
      get; set;

    }

    /// <summary>
    /// app key for application identitfication.
    /// </summary>
    public string AppKey {
      get; set;
    }

    /// <summary>
    /// Notification url.
    /// </summary>
    public string NotificationUrl {
      get; set;
    }

    /// <summary>
    /// Application copyright text.
    /// </summary>
    public string Copyright {
      get; set;
    }

    /// <summary>
    /// Application name
    /// </summary>
    public string ApplicationName {
      get; set;
    }

    /// <summary>
    /// PublisherWebsite
    /// </summary>
    [NotMapped]
    public string PublisherWebsite {
      get; set;
    }

    /// <summary>
    /// PublisherWebsite
    /// </summary>
    [NotMapped]
    public int UserType {
      get; set;
    }

  }
}