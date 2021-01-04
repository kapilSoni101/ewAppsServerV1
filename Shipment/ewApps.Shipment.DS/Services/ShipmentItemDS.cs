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
using ewApps.Shipment.Data;
using ewApps.Shipment.Entity;

namespace ewApps.Shipment.DS {

    /// <summary>
    /// This class performs basic CRUD operation on ShipmentItem entity.
    /// </summary>
    public class ShipmentItemDS:BaseDS<ShipmentItem>, IShipmentItemDS {


        #region Local member
        IShipmentItemRepository _shipmentItemRepository;

        #endregion Local member

        #region Constructor

        public ShipmentItemDS(IShipmentUnitOfWork unitOfWork,
        IShipmentItemRepository shipmentItemRepository) : base(shipmentItemRepository) {
            _shipmentItemRepository = shipmentItemRepository;
        }

        #endregion Constructor

    }
}