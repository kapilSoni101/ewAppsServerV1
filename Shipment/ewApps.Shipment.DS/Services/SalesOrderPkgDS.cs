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
using ewApps.Shipment.Entity;

namespace ewApps.Shipment.DS {

    /// <summary>
    /// This class performs basic CRUD operation on SalesOrderPackage entity.
    /// </summary>
    public class SalesOrderPkgDS:BaseDS<SalesOrderPkg>, ISalesOrderPkgDS {


        #region Local member

        ISalesOrderPkgRepository _salesOrderPakageRepository;

        #endregion Local member

        #region Constructor

        public SalesOrderPkgDS(ISalesOrderPkgRepository salesOrderPkgRepository) : base(salesOrderPkgRepository) {
            _salesOrderPakageRepository = salesOrderPkgRepository;
        }

        #endregion Constructor



    }
}


