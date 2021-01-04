/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar/Amit Mundra <abadgujar@batchmaster.com>
 * Date: 09 April 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 16 April 2019
 */


using ewApps.Core.BaseService;
using ewApps.Shipment.Data;

namespace ewApps.Shipment.DS {

    /// <summary>
    /// This class performs basic CRUD operation on ShipmentPackage entity.
    /// </summary>
    public class ShipmentPkgDS:BaseDS<Entity.ShipmentPkg>, IShipmentPkgDS {

        #region Local member

        IShipmentPkgRepository _shipmentPackageRepository;
        IShipmentUnitOfWork _unitOfWork;
        IShipmentPkgItemDS _shipmentPkgDetailDS;

        #endregion Local member

        #region Constructor

        public ShipmentPkgDS(IShipmentPkgRepository shipmentPackageRepository) : base(shipmentPackageRepository) {
            _shipmentPackageRepository = shipmentPackageRepository;
        }

        #endregion Constructor    


    }
}


