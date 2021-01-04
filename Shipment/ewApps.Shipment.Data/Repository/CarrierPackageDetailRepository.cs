/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 15 May 2019
 * 
 * Contributor/s: Amit Mundra
 * Last Updated On: 15 May 2019
 */

using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Shipment.Entity;

namespace ewApps.Shipment.Data {

    /// <summary>
    /// This class implements standard database logic and operations for CarrierPackageDetail entity.
    /// </summary>
    public class CarrierPackageDetailRepository:BaseRepository<CarrierPackageLinking, ShipmentDbContext>, ICarrierPackageLinkingRepository {

        #region Local member

        //local variables
        IUserSessionManager _sessionManager;

        #endregion Local member

        #region Constructor 

        public CarrierPackageDetailRepository(ShipmentDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
            _sessionManager = sessionManager;
        }

        #endregion Constructor

    }
}
