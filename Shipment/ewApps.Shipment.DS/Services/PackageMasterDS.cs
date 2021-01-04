/* Copyright © 2019 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Kuldeep Singh Chauhan <kchauhan@eworkplaceapps.com>
 * Date: 08 Aug 2019
 */


using AutoMapper;
using ewApps.Core.BaseService;
using ewApps.Shipment.Data;
using ewApps.Shipment.Entity;

namespace ewApps.Shipment.DS {

    /// <summary>
    /// This class performs basic CRUD operation on PackageMaster entity.
    /// </summary>
    public class PackageMasterDS:BaseDS<PackageMaster>, IPackageMasterDS {



        #region Local member

        IPackageMasterRepository _packageMasterRepository;
      
        #endregion Local member

        #region Constructor

        public PackageMasterDS(IPackageMasterRepository packageMasterRepository) : base(packageMasterRepository) {
            _packageMasterRepository = packageMasterRepository;
        }

        #endregion Constructor

    }
}
