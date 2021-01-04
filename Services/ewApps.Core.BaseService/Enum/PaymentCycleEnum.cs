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

namespace ewApps.Core.BaseService {

  /// <summary>
  /// Enum used to define the payment cycle for subscription.
  /// </summary>
  [System.Flags]
  public enum PaymentCycleEnum {
    /// <summary>
    /// Monthly.
    /// </summary>
    Monthly = 1,

    /// <summary>
    /// Quaterly.
    /// </summary>
    Quaterly = 2,

    /// <summary>
    /// Half Yearly.
    /// </summary>
    HalfYearly = 3,

    /// <summary>
    /// Yearly.
    /// </summary>
    Yearly = 4
  }
}
