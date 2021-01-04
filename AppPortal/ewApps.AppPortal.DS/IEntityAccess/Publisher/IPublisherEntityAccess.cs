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

using System;

namespace ewApps.AppPortal.DS {


  /// <summary>
  /// Manages the all Publisher permission related operations.
  /// </summary>
  public interface IPublisherEntityAccess {

    bool[] AccessList(Guid entityId);

    bool CheckAccess(int operation, Guid entityId);
  }
}