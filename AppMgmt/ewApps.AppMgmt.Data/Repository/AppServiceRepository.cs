/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
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
using AppManagement.DTO;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppMgmt.Data {
    public class AppServiceRepository:BaseRepository<AppService, AppMgmtDbContext>, IAppServiceRepository {

        #region Constructor
        public AppServiceRepository(AppMgmtDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {

        }
        #endregion Constructor

        #region Get

        ///<inheritdoc/>
        public async Task<List<AppService>> GetAppServicesDetailsAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            List<AppService> appservices;
            appservices = await _context.AppService.Where(a => a.AppId == appId).AsNoTracking().ToListAsync(token);
            return appservices;
        }

        public async Task<List<AppServiceDTO>> GetAppServiceListByAppIdAsync(Guid appId, bool onlyActive, bool includeDeleted, CancellationToken token = default(CancellationToken)) {
            string sql = "Select ID, Name, Active, ServiceKey FROM am.AppService Where AppId = @AppId ";

            SqlParameter[] sqlParamList = new SqlParameter[3];

            SqlParameter appIdParam = new SqlParameter("AppId", appId);
            sqlParamList[0] = appIdParam;

            if(onlyActive) {
                sql += " AND Active = @Active ";
                SqlParameter activeParam = new SqlParameter("Active", onlyActive);
                sqlParamList[1] = activeParam;
            }

            if(includeDeleted) {
                sql += " AND Deleted = @Deleted";
                SqlParameter deletedParam = new SqlParameter("Deleted", includeDeleted);
                sqlParamList[2] = deletedParam;
            }

            return await GetQueryEntityListAsync<AppServiceDTO>(sql, sqlParamList, token);
        }

        ///<inheritdoc/>
        public async Task<List<AppService>> GetAppServiceAsync(List<PubBusinessSubsPlanAppServiceDTO> pubBusinessSubsPlanAppServiceDTO, CancellationToken token = default(CancellationToken)) {          

            List<AppService> appservices;
            appservices = await _context.AppService.Where(a => pubBusinessSubsPlanAppServiceDTO.Select(p => p.AppServiceId).Contains(a.ID)).ToListAsync();
            return appservices;
        }


        #endregion Get

    }
}