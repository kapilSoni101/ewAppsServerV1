/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 08 Aug -2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.Data {
    /// <summary>
    /// This class contains methods to perform all database operations related to business information (like Data Transfer Object).
    /// </summary>
    public class BusinessRepository:BaseRepository<Business, AppPortalDbContext>, IBusinessRepository {

        #region Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessRepository"/> class.
        /// </summary>
        /// <param name="context">Instance of BusinessDbContext to executes database operations.</param>
        /// <param name="sessionManager">User session manager instance to get login user details.</param>
        public BusinessRepository(AppPortalDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion

        #region Get

        /// <summary>
        /// Get business by tenantid.
        /// </summary>
        /// <returns></returns>
        public async Task<Business> GetBusinessByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            //string sql = "SELECT TOP 1 * FROm Business WHERE TenantId = @tenantId ";
            //SqlParameter param = new SqlParameter("@tenantId", tenantId);
            //return await GetEntityAsync(sql, new SqlParameter[] { param }, token);
            return await _context.Business.Where(b => b.TenantId == tenantId).AsNoTracking().FirstOrDefaultAsync(token);
        }

        ///<inheritdoc/>
        public async Task<UpdateBusinessTenantModelDQ> GetBusinessTenantDetailModelDTOAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {           
            return null;
        }

       

        #endregion Get        

    }
}
