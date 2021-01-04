/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 09 April 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 09 April 2019
 */

using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Shipment.Entity;

namespace ewApps.Shipment.Data {

    /// <summary>
    /// This class implements standard database logic and operations for ShipmentItem entity.
    /// </summary>
    public class ShipmentItemRepository:BaseRepository<ShipmentItem, ShipmentDbContext>, IShipmentItemRepository {

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
        public ShipmentItemRepository(ShipmentDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
            _sessionManager = sessionManager;
        }

        #endregion Constructor

    }
}
