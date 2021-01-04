/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Kuldeep
 * Last Updated On: 10 October 2018
 */



using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppMgmt.DS {

    /// <summary>
    /// This class implements standard business logic and operations for TenantUserAppLinking entity.
    /// </summary>
    public class TenantUserAppLinkingDS:BaseDS<TenantUserAppLinking>, ITenantUserAppLinkingDS {

        #region Local Member  

        ITenantUserAppLinkingRepository _userAppLinkingRepository;
        //ITenantDataService _tenantDS;
        IUserSessionManager _userSessionManager;
        IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor 

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        /// <param name="userAppLinkingRep"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="cacheService"></param>
        /// <param name="userSessionManager"></param>
        public TenantUserAppLinkingDS(ITenantUserAppLinkingRepository userAppLinkingRep,
            IUnitOfWork unitOfWork, IUserSessionManager userSessionManager) : base(userAppLinkingRep) {
            _userAppLinkingRepository = userAppLinkingRep;
            //_tenantDS = tenantDS;
            _userSessionManager = userSessionManager;
            _unitOfWork = unitOfWork;
        }

        #endregion

    }
}

