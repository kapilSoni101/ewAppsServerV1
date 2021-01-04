/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agrawal <sagrawal@eworkplaceapps.com>
 * Date: 14 November 2018
 * 
 * Contributor/s: Sourabh Agrawal
 * Last Updated On: 14 November 2018
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.BaseService {
/// <summary>
/// 
/// </summary>
 public  enum TenantType
  {
    /// <summary>
    /// When user is Platform Type.
    /// </summary>
    Platform = 1,

    /// <summary>
    /// When user is publisher Type.
    /// </summary>
    Publisher = 2,

    /// <summary>
    /// When user is buisness Type.
    /// </summary>
    Buisness = 3,

    /// <summary>
    /// When user is buisness partner Type.
    /// </summary>
    BuisnessPartner = 4
  }
}
