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
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Payment.Entity;

namespace ewApps.Payment.Data {

    /// <summary>
    /// This class implements standard database logic and operations for RoleLinking entity.
    /// </summary>
    public class RoleLinkingRepository : BaseRepository<RoleLinking, PaymentDbContext>, IRoleLinkingRepository {

    #region Constructor

    /// <summary>
    /// Constructor initializing the base variables
    /// </summary>
    /// <param name="context">ship Db context reference</param>
    /// <param name="sessionManager">Session manager reference</param>
    public RoleLinkingRepository(PaymentDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
    } 

    #endregion
  }
}
