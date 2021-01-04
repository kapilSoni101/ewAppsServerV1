/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agarwal <sagrawal@ewoorkplaceapps.com>
 * Date: 13 August 2019
 * 
 * Contributor/s: Sourabh agrawal
 * Last Updated On: 13 August 2019
 */


using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.Data {

    /// <summary>
    /// Contains supportive method for Address entity.
    /// </summary>
    public class BusinessAddressRepository:BaseRepository<BusinessAddress, AppPortalDbContext>, IBusinessAddressRepository {

        #region Constructor 

        /// <summary>
        ///  Constructor initializing the base variables
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        public BusinessAddressRepository(AppPortalDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor 

        #region Get
        /// <summary>
        /// Get address detail list by parent and entityid.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="businessId"></param>
        /// <param name="addressType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BusinessAddressModelDTO>> GetAddressListByParentEntityIdAndAddressTypeAsync(Guid tenantId, Guid businessId, int addressType, CancellationToken token = default(CancellationToken)) {
            string sql = "SELECT ID,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn,TenantId,Deleted,BusinessId,Label,AddressStreet1,AddressStreet2,AddressStreet3," +
                "City,Country,State,ZipCode,FaxNumber,Phone,AddressType " +
                " FROM ap.BusinessAddress WHERE BusinessId = @businessId AND AddressType = @addressType  AND Deleted = 0";
            SqlParameter paramTenantId = new SqlParameter("@tenantId", tenantId);
            SqlParameter paramBusinessId = new SqlParameter("@businessId", businessId);
            SqlParameter paramAddressType = new SqlParameter("@addressType", addressType);
            return await GetQueryEntityListAsync<BusinessAddressModelDTO>(sql, new SqlParameter[] { paramTenantId, paramBusinessId, paramAddressType }, token);
        }

        /// <summary>
        /// Get address detail information by parentEntityid.
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BusinessAddress>> GetAddressListEntityByParentEntityIdAsync(Guid businessId, CancellationToken token = default(CancellationToken)) {

            return await _context.BusinessAddress.Where(ba => ba.BusinessId == businessId && ba.Deleted == false).ToListAsync(token);
        }

        /// <summary>
        /// Get address detail list by parent and entityid.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="parentEntityId"></param>
        /// <param name="addressType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BusinessAddress>> GetAddressEntityListByParentEntityIdAndAddressTypeAsync(Guid tenantId, Guid parentEntityId, int addressType, CancellationToken token = default(CancellationToken)) {           
            return await _context.BusinessAddress.Where(ts => ts.TenantId == tenantId && ts.BusinessId == parentEntityId && ts.AddressType == addressType && ts.Deleted == false).ToListAsync(token);
        }


        #endregion Get

    }
}
