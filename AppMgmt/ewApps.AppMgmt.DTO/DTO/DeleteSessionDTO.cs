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

namespace ewApps.AppMgmt.DTO {

  /// <summary>
  /// This class is a DTO that contains deatils of the user session information for deleting the session.
  /// </summary>
  public class DeleteSessionDTO {

    /// <summary>
    /// App user identifier.
    /// </summary>
    public Guid TenantUserId {
      get; set;
    }

    /// <summary>
    /// ClientSession ID.
    /// </summary>
    public Guid? ClientSessionId {
      get; set;
    }

    /// <summary>
    /// App key for application identification.
    /// </summary>
    public string AppKey {
      get; set;
    }

    /// <summary>
    /// App key for application identification.
    /// </summary>
    public string Token {
      get; set;
    }

  }
}
