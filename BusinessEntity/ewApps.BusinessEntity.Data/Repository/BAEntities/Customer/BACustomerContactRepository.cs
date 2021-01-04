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
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.BusinessEntity.Data {

    /// <summary>
    /// Provides CRUD operations for customer entity.
    /// </summary>
    public class BACustomerContactRepository:BaseRepository<BACustomerContact, BusinessEntityDbContext>, IBACustomerContactRepository {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BACustomerRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="sessionManager">The session manager.</param>
        public BACustomerContactRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {

        }

        #endregion

        #region Public Methods
        #region Get
        ///<inheritdoc/>
        public async Task<List<CustomerContactDTO>> GetCustomerContactListByIdAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {

            string query = string.Format(@"SELECT custcontact.ID, custcontact.TenantID,custcontact.FirstName,custcontact.CustomerId,
                                    custcontact.LastName, custcontact.Title,custcontact.Position,custcontact.ERPContactKey, 
                                    custcontact.Address,custcontact.Telephone,custcontact.Email
                                    FROM be.BACustomerContact as custcontact where custcontact.CustomerId  = @customerId And custcontact.Deleted = @deleted   
                                    ORDER BY custcontact.CreatedOn DESC ");

            SqlParameter parameterS = new SqlParameter("@customerId", customerId);
            SqlParameter parameterDeleted = new SqlParameter("@deleted", false);

            List<CustomerContactDTO> customerContactDTOs = await GetQueryEntityListAsync<CustomerContactDTO>(query, new object[] { parameterS, parameterDeleted });

            return customerContactDTOs;
        }

        ///<inheritdoc/>
        public async Task<List<BACustomerContact>> GetCustomerContactListByCustomerIdAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _context.BACustomerContact.Where(ba => ba.CustomerId == customerId).ToListAsync(token);
        }



        #endregion Get

        #endregion Public Methods

    }
}

