/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
* Unauthorized copying of this file, via any medium is strictly prohibited
* Proprietary and confidential
* 
* Author: Rajesh Thakur <rthakur@eworkplaceapps.com>
* Date: 24 September 2018
* 
* Contributor/s: Nitin Jain
* Last Updated On: 10 October 2018
*/


using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppMgmt.Data {

    /// <summary>
    /// This is the repository responsible to perform CRUD operations on service entity.
    /// </summary>
    public class CustomerAppServiceLinkingRepository:BaseRepository<CustomerAppServiceLinking, AppMgmtDbContext>, ICustomerAppServiceLinkingRepository {

        #region Constructor

        /// <summary>
        /// Parameter conatins the DBContent and SessionManager, It will be used by a parent class.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        /// <param name="connectionManager"></param>
        public CustomerAppServiceLinkingRepository(AppMgmtDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor

    }
}
