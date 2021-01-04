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

namespace ewApps.AppMgmt.Common {
  /// <summary>
  /// This enum describes the status of the user.
  /// </summary>
  public enum AppScopeEnum {

    /// <summary>
    /// Internal application for internal user only.
    /// </summary>
    Public = 1,

    /// <summary>
    /// External application to be provided to user .
    /// </summary>
    Private = 2
  }
}
