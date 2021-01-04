/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 26 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 26 August 2019
 */

using System;

namespace ewApps.AppPortal.DTO {
  /// <summary>
  /// Represents a request DTO with to request support ticket list
  /// </summary>
  public class SupportMyTicketListRequestDTO {
    /// <summary>
    /// Gets or sets the application key.
    /// </summary>
    /// <value>
    /// The application key to get specific application support tickets.
    /// </value>
    public string AppKey {
      get; set;
    }

    /// <summary>
    /// Gets or sets the tenant id.
    /// </summary>
    /// <value>
    /// The tenant id to get specific tenant's support tickets.
    /// </value>
    public Guid TenantId {
      get; set;
    }

    /// <summary>
    /// Gets or sets the generation level.
    /// </summary>
    /// <value>
    /// The generation level.
    /// </value>
    /// <remarks>It should be any value of <see cref="SupportLevelEnum"/>.</remarks>
    public int GenerationLevel {
      get; set;
    }

    /// <summary>
    /// Gets or sets the customer identifier.
    /// </summary>
    /// <value>
    /// The customer id to filter specific customer support ticket.
    /// </value>
    public Guid? CustomerId {
      get; set;
    }

    /// <summary>
    /// Gets or sets the creator user id.
    /// </summary>
    /// <value>
    /// The user id of support ticket creator.
    /// </value>
    public Guid CreatorId {
      get; set;
    }


    public bool OnlyDeleted
    {
      get; set;
    } = false;

  }
}
