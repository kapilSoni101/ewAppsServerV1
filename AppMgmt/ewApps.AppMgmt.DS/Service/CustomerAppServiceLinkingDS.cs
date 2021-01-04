
/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Rajesh Thakur <rthakur@eworkplaceapps.com>
 * Date: 02 January 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 02 January 2019
 */


using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppMgmt.DS {

    /// <summary>
    ///  This class implements standard business logic and operations for class CustomerAppServiceLinking entity.
    /// </summary>
    public class CustomerAppServiceLinkingDS:BaseDS<CustomerAppServiceLinking>, ICustomerAppServiceLinkingDS {

        #region Local Member 
        
        IUnitOfWork _unitOfWork;
        ICustomerAppServiceLinkingRepository _customerAppServiceLinkingRepository;
        IUserSessionManager _sessionmanager;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        /// <param name="customerAppServiceLinkingRepository"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <param name="cacheService"></param>
        public CustomerAppServiceLinkingDS(ICustomerAppServiceLinkingRepository customerAppServiceLinkingRepository, IUnitOfWork unitOfWork, IUserSessionManager sessionmanager) : base(customerAppServiceLinkingRepository) {
           
            _unitOfWork = unitOfWork;
            _customerAppServiceLinkingRepository = customerAppServiceLinkingRepository;
            _sessionmanager = sessionmanager;
        }

        #endregion



    }
}
