/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar<abadgujar@batchmaster>
 * Date: 22 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 22 August 2019
 */

using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.Core.DMService {
    public class DMDocumentUserRepository : BaseRepository<DMDocumentUser, DMDBContext>, IDMDocumentUserRepository {

    #region Constructor 

    /// <summary>
    ///  Constructor initializing the base variables
    /// </summary>
    /// <param name="context"></param>
    /// <param name="sessionManager"></param>
    /// <param name="connectionManager"></param>
    public DMDocumentUserRepository(DMDBContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
    }

    #endregion Constructor
  }
}
