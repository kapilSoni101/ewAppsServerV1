/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 02 July 2019 
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 02 July 2019 
 */


using ewApps.Core.BaseService;

using ewApps.Shipment.Data;
using ewApps.Shipment.Entity;

namespace ewApps.Shipment.DS {

    /// <summary>
    /// This class performs basic CRUD operation on FavourateShipmentPkgSetting entity.
    /// </summary>
    public class FavouriteShipmentPkgSettingDS:BaseDS<FavouriteShipmentPkgSetting>, IFavouriteShipmentPkgSettingDS {


        #region Local member
        IFavourateShipmentPkgSettingRepository _favourateShipmentPkgSettingRepository;
        #endregion Local member

        #region Constructor

        public FavouriteShipmentPkgSettingDS(IFavourateShipmentPkgSettingRepository favourateShipmentPkgSettingRepository) : base(favourateShipmentPkgSettingRepository) {
            _favourateShipmentPkgSettingRepository = favourateShipmentPkgSettingRepository;

        }
        #endregion


    }
}

