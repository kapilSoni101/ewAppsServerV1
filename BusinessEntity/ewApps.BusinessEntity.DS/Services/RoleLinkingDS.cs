﻿/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using ewApps.BusinessEntity.Entity;
using ewApps.BusinessEntity.Data;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    ///  This class implements standard business logic and operations for roleLinking entity.
    /// </summary>
    public class RoleLinkingDS:BaseDS<RoleLinking>, IRoleLinkingDS {

        #region Local Member 

        IRoleLinkingRepository _roleLinkingRepository;
        //IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor 

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        /// <param name="roleLinkingRep"></param>
        /// <param name="unitOfWork"></param>
        public RoleLinkingDS(IRoleLinkingRepository roleLinkingRep) : base(roleLinkingRep) {
            _roleLinkingRepository = roleLinkingRep;
        }

        #endregion

    }
}
