/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Rajesh Thakur <rthakur@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */




using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.BusinessEntity.Data {

    /// <summary>
    /// Provides CRUD operations for customer entity.
    /// </summary>
    public class BACustomerPaymentDetailRepository: BaseRepository<BACustomerPaymentDetail, BusinessEntityDbContext>, IBACustomerPaymentDetailRepository {

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="BACustomerRepository"/> class.
    /// </summary>
    public BACustomerPaymentDetailRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {

    }

    #endregion

    #region Public Methods

    #endregion Public Methods

  }
}

