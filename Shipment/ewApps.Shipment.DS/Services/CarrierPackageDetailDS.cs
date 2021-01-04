/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Kuldeep Singh Chauhan <kchauhan@eworkplaceapps.com>
 * Date: 08 Aug 2019
 */

using ewApps.Core.BaseService;
using ewApps.Shipment.Data;
using ewApps.Shipment.Entity;

namespace ewApps.Shipment.DS {

    /// <summary>
    /// This class performs basic CRUD operation on CarrierPackageDetail entity.
    /// </summary>
    public class CarrierPackageDetailDS:BaseDS<CarrierPackageLinking>, ICarrierPackageDetailDS {


        #region Local member

        ICarrierPackageLinkingRepository _carrierPackageDetailRepository;
        #endregion Local member

        #region Constructor

        /// <summary>
        /// Initialinzing local variables.
        /// </summary>
        public CarrierPackageDetailDS(ICarrierPackageLinkingRepository carrierPackageDetailRepository) : base(carrierPackageDetailRepository) {
            _carrierPackageDetailRepository = carrierPackageDetailRepository;            
        }

        #endregion Constructor


    }
}
