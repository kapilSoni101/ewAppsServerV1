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


using AutoMapper;
using ewApps.Core.BaseService;
using ewApps.Shipment.Data;

namespace ewApps.Shipment.DS {

    /// <summary>
    /// This class performs basic CRUD operation on Shipment entity.
    /// </summary>
    public class ShipmentDS:BaseDS<ewApps.Shipment.Entity.Shipment>, IShipmentDS {

        #region Local member

        IShipmentRepository _shipmentRepo;

        #endregion Local member

        #region Constructor

        public ShipmentDS(IShipmentRepository shipmentRepo) : base(shipmentRepo) {
            _shipmentRepo = shipmentRepo;
        }

        #endregion Constructor


    }
}
