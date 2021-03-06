﻿/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System.Collections.Generic;

namespace ewApps.AppPortal.DS {

  /// <summary>
  /// This DTO is collection of the data about deeplink.
  /// </summary>
  public class DeeplinkActionParameterAndBranchKeyDTO {

    /// <summary>
    /// Branch key.
    /// </summary>
    public string BranchKey {
      get; set;
    }

    /// <summary>
    /// Deeplink action string to be set in deeplink.
    /// </summary>
    public string DeepLinkAction {
      get; set;
    }

    /// <summary>
    /// Deeplink parameter string list to be set in deeplink.
    /// </summary>
    public List<string> DeeplinkParametersList {
      get; set;
    }
  }
}
