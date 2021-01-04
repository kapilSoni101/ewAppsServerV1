/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agarwal <sagrawal@ewoorkplaceapps.com>
 * Date: 14 August 2019
 * 
 * Contributor/s: Sourabh agrawal
 * Last Updated On: 14 August 2019
 */


using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
namespace ewApps.AppPortal.Data {

    /// <summary>
    /// Contains supportive method for Address entity.
    /// </summary>
    public class PublisherAddressRepository:BaseRepository<PublisherAddress, AppPortalDbContext>, IPublisherAddressRepository {

        #region Constructor 

        /// <summary>
        ///  Constructor initializing the base variables
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        public PublisherAddressRepository(AppPortalDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor 

        #region Get
        /// <summary>
        /// Get address detail list by publisherid and entityid.
        /// </summary>
        public async Task<List<PublisherAddressDTO>> GetAddressListByPublisherIdAndAddressTypeAsync(Guid publisherId, int addressType, CancellationToken token = default(CancellationToken)) {
            string sql = "SELECT ID,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn,TenantId,Deleted,PublisherId,Label,AddressStreet1,AddressStreet2,AddressStreet3," +
                "City,Country,State,ZipCode,FaxNumber,Phone,AddressType " +
                " FROM ap.PublisherAddress WHERE PublisherId = @publisherId AND AddressType = @addressType  AND Deleted = 0";
            //SqlParameter paramTenantId = new SqlParameter("@tenantId", tenantId);
            SqlParameter paramPublisherId = new SqlParameter("@publisherId", publisherId);
            SqlParameter paramAddressType = new SqlParameter("@addressType", addressType);
            return await GetQueryEntityListAsync<PublisherAddressDTO>(sql, new SqlParameter[] { paramPublisherId, paramAddressType }, token);
        }

        /// <summary>
        /// Get address detail information by parentEntityid.
        /// </summary>
        /// <param name="publisherId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<PublisherAddress>> GetAddressListEntityByParentEntityIdAsync(Guid publisherId, CancellationToken token = default(CancellationToken)) {

            return await _context.PublisherAddress.Where(pa => pa.PublisherId == publisherId && pa.Deleted == false).ToListAsync(token);
        }

        /// <summary>
        /// Get address detail list by parent and entityid.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="publisherId"></param>
        /// <param name="addressType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<PublisherAddress>> GetAddressEntityListByParentEntityIdAndAddressTypeAsync(Guid tenantId, Guid publisherId, int addressType, CancellationToken token = default(CancellationToken)) {
            return await _context.PublisherAddress.Where(pa => pa.TenantId == tenantId && pa.PublisherId == publisherId && pa.AddressType == addressType && pa.Deleted == false).ToListAsync(token);
        }


        #endregion Get

    }
}
