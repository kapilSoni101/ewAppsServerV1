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

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.Data {

    /// <summary>
    /// Provides CRUD operations for customer entity.
    /// </summary>
    public class CustomerRepository:BaseRepository<Customer, AppPortalDbContext>, ICustomerRepository {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="sessionManager">The session manager.</param>
        public CustomerRepository(AppPortalDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {

        }

        #endregion

        #region Get

        /// <summary>
        /// Get customer by business partner id.
        /// </summary>
        /// <param name="busPartnerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<Customer> GetCustomerByBusinesPartnerIdAsync(Guid busPartnerId, CancellationToken token = default(CancellationToken)) {
            return await _context.Customer.Where(ba => ba.BusinessPartnerTenantId == busPartnerId && ba.Deleted == false).FirstOrDefaultAsync(token);
        }

        #endregion Get

    }
}

