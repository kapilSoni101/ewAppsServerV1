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

using ewApps.AppPortal.Data;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DS {
    /// <summary>
    /// This class implements standard business logic and operations for support group.
    /// </summary>
    public class SupportGroupDS:BaseDS<SupportGroup>, ISupportGroupDS {

    #region Local member

    ISupportGroupRepository _supportGroupRepository;



    #endregion Local member

    #region Constructor
    /// <summary>
    /// Initializing local variables
    /// </summary>
    /// /// <param name="supportGroupRepository"></param>
    /// <param name="cacheService"></param>

    public SupportGroupDS(ISupportGroupRepository supportGroupRepository) : base(supportGroupRepository) {
      _supportGroupRepository = supportGroupRepository;
 }
    #endregion Constructor
  }
}
