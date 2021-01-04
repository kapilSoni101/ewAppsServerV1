/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Kuldeep Singh Chauhan <kchauhan@eworkplaceapps.com>
 * Date: 08 Aug 2019
 */

using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Shipment.Entity;

namespace ewApps.Shipment.Data {

    /// <summary>
    /// This class implements standard database logic and operations for PackageMaster entity.
    /// </summary>
    public class PackageMasterRepository:BaseRepository<PackageMaster, ShipmentDbContext>, IPackageMasterRepository {

        #region Local member

        //local variables
        IUserSessionManager _sessionManager;

        #endregion Local member

        #region Constructor 

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        public PackageMasterRepository(ShipmentDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
            _sessionManager = sessionManager;
        }

        #endregion Constructor


    }
}
