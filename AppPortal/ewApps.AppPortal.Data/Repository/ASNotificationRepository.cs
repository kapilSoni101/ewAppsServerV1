/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil nigam <anigam@eworkplaceapps.com>
 * Date: 06 Nov 2019

 */

using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.AppPortal.Data {
    public class ASNotificationRepository:BaseRepository<ASNotification, AppPortalDbContext>, IASNotificationRepository {

        public ASNotificationRepository(AppPortalDbContext context, IUserSessionManager userSession) : base(context, userSession) {

        }

        public async Task<List<ASNotificationDTO>> GetASNotificationList(Guid appId, Guid tenantId, Guid TenantUserId, int fromCount, int toCount, CancellationToken token = default(CancellationToken)) {

            if(toCount == 0) {
                string query = @"SELECT  Id, TextContent, TenantId, AppId, RecipientTenantUserId,ReadState, EntityType, EntityId, CreatedOn, AdditionalInfo FROM ap.ASNotification             
             WHERE TenantId = @TenantId AND AppId = @AppId And RecipientTenantUserId = @TenantUserId  ORDER BY createdon desc";
                SqlParameter paramAppId = new SqlParameter("@AppId", appId);
                SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
                SqlParameter paramTenantUserId = new SqlParameter("@TenantUserId", TenantUserId);

                return await GetQueryEntityListAsync<ASNotificationDTO>(query, new SqlParameter[] { paramAppId, paramTenantId, paramTenantUserId }, token);
            }
            else {
                string query = @"SELECT  Id, TextContent, TenantId,AppId, RecipientTenantUserId,ReadState, EntityType, EntityId, CreatedOn, AdditionalInfo FROM ap.ASNotification             
             WHERE TenantId = @TenantId AND AppId = @AppId And RecipientTenantUserId = @TenantUserId ORDER BY createdon desc  OFFSET @FromCount ROWS FETCH NEXT @ToCount ROWS ONLY ";
                SqlParameter paramAppId = new SqlParameter("@AppId", appId);
                SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
                SqlParameter paramTenantUserId = new SqlParameter("@TenantUserId", TenantUserId);
                SqlParameter paramFromCount = new SqlParameter("@FromCount", fromCount);
                SqlParameter paramToCount = new SqlParameter("@ToCount", toCount);

                return await GetQueryEntityListAsync<ASNotificationDTO>(query, new SqlParameter[] { paramAppId, paramTenantId, paramTenantUserId, paramFromCount, paramToCount }, token);

            }
        }

        public async Task<List<ASNotificationDTO>> GetASNotificationListByAppAndTenantAndUserAndEntityTypeAsync(Guid appId, Guid tenantId, Guid tenantUserId, int entityType, int fromCount, int toCount, CancellationToken token = default(CancellationToken)) {

            if(toCount == 0) {
                string query = @"SELECT  Id, TextContent, TenantId, AppId, RecipientTenantUserId,ReadState, EntityType, EntityId, CreatedOn, AdditionalInfo FROM ap.ASNotification             
                                WHERE TenantId = @TenantId AND AppId = @AppId And RecipientTenantUserId = @TenantUserId AND EntityType=@EntityType ORDER BY createdon desc";
                SqlParameter paramAppId = new SqlParameter("@AppId", appId);
                SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
                SqlParameter paramTenantUserId = new SqlParameter("@TenantUserId", tenantUserId);
                SqlParameter paramEntityType = new SqlParameter("@EntityType", entityType);

                return await GetQueryEntityListAsync<ASNotificationDTO>(query, new SqlParameter[] { paramAppId, paramTenantId, paramTenantUserId, paramEntityType }, token);
            }
            else {
                string query = @"SELECT  Id, TextContent, TenantId,AppId, RecipientTenantUserId,ReadState, EntityType, EntityId, CreatedOn, AdditionalInfo FROM ap.ASNotification             
                                WHERE TenantId = @TenantId AND AppId = @AppId And RecipientTenantUserId = @TenantUserId AND EntityType=@EntityType ORDER BY createdon desc  OFFSET @FromCount ROWS FETCH NEXT @ToCount ROWS ONLY ";
                SqlParameter paramAppId = new SqlParameter("@AppId", appId);
                SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
                SqlParameter paramTenantUserId = new SqlParameter("@TenantUserId", tenantUserId);
                SqlParameter paramFromCount = new SqlParameter("@FromCount", fromCount);
                SqlParameter paramToCount = new SqlParameter("@ToCount", toCount);
                SqlParameter paramEntityType = new SqlParameter("@EntityType", entityType);

                return await GetQueryEntityListAsync<ASNotificationDTO>(query, new SqlParameter[] { paramAppId, paramTenantId, paramTenantUserId, paramFromCount, paramToCount, paramEntityType }, token);

            }
        }

        public async Task<List<ASNotificationDTO>> GetASNotificationListAsync(Guid appId, Guid tenantId, Guid TenantUserId, int fromCount, int toCount, int entityType, CancellationToken token = default(CancellationToken)) {
            if(toCount == 0) {
                string query = @"SELECT  Id, TextContent, TenantId, AppId, RecipientTenantUserId,ReadState, EntityType, EntityId, CreatedOn, AdditionalInfo FROM ap.ASNotification             
                                WHERE TenantId = @TenantId AND AppId = @AppId And RecipientTenantUserId = @TenantUserId AND EntityType=@EntityType 
                                ORDER BY createdon desc";
                SqlParameter paramAppId = new SqlParameter("@AppId", appId);
                SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
                SqlParameter paramTenantUserId = new SqlParameter("@TenantUserId", TenantUserId);
                SqlParameter paramEntityType = new SqlParameter("@EntityType", entityType);

                return await GetQueryEntityListAsync<ASNotificationDTO>(query, new SqlParameter[] { paramAppId, paramTenantId, paramTenantUserId, paramEntityType }, token);
            }
            else {
                string query = @"SELECT  Id, TextContent, TenantId,AppId, RecipientTenantUserId,ReadState, EntityType, EntityId, CreatedOn, AdditionalInfo FROM ap.ASNotification             
                                WHERE TenantId = @TenantId AND AppId = @AppId And RecipientTenantUserId = @TenantUserId 
                                AND EntityType=@EntityType 
                                ORDER BY createdon desc 
                                OFFSET @FromCount ROWS FETCH NEXT @ToCount ROWS ONLY ";
                SqlParameter paramAppId = new SqlParameter("@AppId", appId);
                SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
                SqlParameter paramTenantUserId = new SqlParameter("@TenantUserId", TenantUserId);
                SqlParameter paramFromCount = new SqlParameter("@FromCount", fromCount);
                SqlParameter paramToCount = new SqlParameter("@ToCount", toCount);
                SqlParameter paramEntityType = new SqlParameter("@EntityType", entityType);

                return await GetQueryEntityListAsync<ASNotificationDTO>(query, new SqlParameter[] { paramAppId, paramTenantId, paramTenantUserId, paramFromCount, paramToCount, paramEntityType }, token);
            }
        }


        public async Task<List<ASNotificationDTO>> GetUnreadASNotificationFromAppPortal(Guid appId, Guid tenantId, Guid TenantUserId, CancellationToken token = default(CancellationToken)) {

            string query = @"SELECT  Id, TextContent, TenantId, AppId, RecipientTenantUserId,ReadState, EntityType, EntityId, CreatedOn, AdditionalInfo FROM ap.ASNotification             
             WHERE TenantId = @TenantId AND AppId = @AppId And RecipientTenantUserId = @TenantUserId AND ReadState = 0 ORDER BY createdon desc";
            SqlParameter paramAppId = new SqlParameter("@AppId", appId);
            SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
            SqlParameter paramTenantUserId = new SqlParameter("@TenantUserId", TenantUserId);

            return await GetQueryEntityListAsync<ASNotificationDTO>(query, new SqlParameter[] { paramAppId, paramTenantId, paramTenantUserId }, token);

        }
    }
}
