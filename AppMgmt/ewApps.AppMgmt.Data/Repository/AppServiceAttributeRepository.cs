/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
* Unauthorized copying of this file, via any medium is strictly prohibited
* Proprietary and confidential
* 
* Author: Rajesh Thakur <rthakur@eworkplaceapps.com>
* Date: 24 September 2018
* 
* Contributor/s: Nitin Jain
* Last Updated On: 10 October 2018
* 
*/

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AppManagement.DTO;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppMgmt.Data {

    /// <summary>
    /// This is the repository responsible to perform CRUD operations on <see cref="AppServiceAttribute"/> entity.
    /// </summary>
    public class AppServiceAttributeRepository:BaseRepository<AppServiceAttribute, AppMgmtDbContext>, IAppServiceAttributeRepository {

        #region Constructor

        /// <summary>
        /// Parameter conatins the DBContent and SessionManager, It will be used by a parent class.
        /// </summary>
        public AppServiceAttributeRepository(AppMgmtDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get the Appservice attribute list by service id.
        /// </summary>
        /// <param name="appServiceId">ServiceId</param>
        /// <param name="token"></param>
        public async Task<List<AppServiceAttribute>> GetAppServiceAttributeByAppServiceAsync(Guid appServiceId, CancellationToken token =default(CancellationToken)) {           
            return await _context.AppServiceAttribute.Where(srvc => srvc.Active && srvc.AppServiceId == appServiceId && srvc.Deleted == false).AsNoTracking().ToListAsync(token);
        }

        ///<inheritdoc/>
        public async Task <List<AppServiceAttribute>> GetAppServiceAttributeListByServiceIdAsync(Guid appServiceId, bool skipDeleted, CancellationToken token = default(CancellationToken)) {
            return await  _context.AppServiceAttribute.Where(i => i.AppServiceId.Equals(appServiceId) && i.Deleted != skipDeleted).ToListAsync(token);
        }

        ///<inheritdoc/>
        public async Task<List<AppServiceAttributeDTO>> GetAppServiceAttributeListByServiceIdAsync(Guid appServiceId, bool onlyActive, bool includeDeleted, CancellationToken token = default(CancellationToken)) {
            string sql = @"Select ID, Name, Active, AttributeKey FROM am.AppServiceAttribute Where AppServiceId = @AppServiceId ";

            SqlParameter[] sqlParamList = new SqlParameter[3];
            SqlParameter appServiceIdParam = new SqlParameter("AppServiceId", appServiceId);
            sqlParamList[0] = appServiceIdParam;

            if(onlyActive) {
                sql += " AND Active = @Active ";
                SqlParameter activeParam = new SqlParameter("Active", onlyActive);
                sqlParamList[1] = activeParam;
            }

            if(includeDeleted) {
                sql += " AND Deleted = @Deleted ";
                SqlParameter deletedParam = new SqlParameter("Deleted", includeDeleted);
                sqlParamList[2] = deletedParam;
            }

            return await GetQueryEntityListAsync<AppServiceAttributeDTO>(sql, sqlParamList, token);
        }


        ///<inheritdoc/>
        public async Task<List<AppServiceAttribute>> GetAppServiceAttributeListAsync(List<PubBusinessSubsPlanAppServiceDTO> pubBusinessSubsPlanAppServiceDTO, CancellationToken token = default(CancellationToken)) {

            List<AppServiceAttribute> appservices;
            appservices = await _context.AppServiceAttribute.Where(a => pubBusinessSubsPlanAppServiceDTO.Select(p => p.AppServiceAttributeId).Contains(a.ID)).ToListAsync();
            return appservices;
        }
        #endregion Get

    }
}
