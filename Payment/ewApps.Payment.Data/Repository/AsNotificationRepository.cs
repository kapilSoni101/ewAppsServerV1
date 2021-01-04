
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;

using ewApps.Core.UserSessionService;
using ewApps.Payment.DTO;
using ewApps.Payment.Entity;

namespace ewApps.Payment.Data {
    public class ASNotificationRepository:BaseRepository<ASNotification, PaymentDbContext>, IASNotificationRepository {

        public ASNotificationRepository(PaymentDbContext context, IUserSessionManager userSession) : base(context, userSession) {

        }

        public async Task<List<ASNotificationDTO>> GetASNotificationList(Guid appId, Guid tenantId, Guid TenantUserId, int fromCount, int toCount, CancellationToken token = default(CancellationToken)) {

            if(toCount == 0) {
                string query = @"SELECT  Id, TextContent, TenantId,AppId, RecipientTenantUserId,ReadState, EntityType, EntityId, CreatedOn, AdditionalInfo FROM pay.ASNotification             
             WHERE TenantId = @TenantId AND AppId = @AppId And RecipientTenantUserId = @TenantUserId  ORDER BY createdon desc";
                SqlParameter paramAppId = new SqlParameter("@AppId", appId);
                SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
                SqlParameter paramTenantUserId = new SqlParameter("@TenantUserId", TenantUserId);

                return await GetQueryEntityListAsync<ASNotificationDTO>(query, new SqlParameter[] { paramAppId, paramTenantId, paramTenantUserId }, token);
            }
            else {
                string query = @"SELECT  Id, TextContent, TenantId,AppId, RecipientTenantUserId,ReadState, EntityType, EntityId, CreatedOn, AdditionalInfo FROM pay.ASNotification             
             WHERE TenantId = @TenantId AND AppId = @AppId And RecipientTenantUserId = @TenantUserId ORDER BY createdon desc  OFFSET @FromCount ROWS FETCH NEXT @ToCount ROWS ONLY ";
                SqlParameter paramAppId = new SqlParameter("@AppId", appId);
                SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
                SqlParameter paramTenantUserId = new SqlParameter("@TenantUserId", TenantUserId);
                SqlParameter paramFromCount = new SqlParameter("@FromCount", fromCount);
                SqlParameter paramToCount = new SqlParameter("@ToCount", toCount);

                return await GetQueryEntityListAsync<ASNotificationDTO>(query, new SqlParameter[] { paramAppId, paramTenantId, paramTenantUserId, paramFromCount, paramToCount }, token);

            }
        }

        public async Task<List<ASNotificationDTO>> GetASNotificationListAsync(Guid appId, Guid tenantId, Guid TenantUserId, int fromCount, int toCount, int entityType, CancellationToken token = default(CancellationToken)) {
            if(toCount == 0) {
                string query = @"SELECT  Id, TextContent, TenantId,AppId, RecipientTenantUserId,ReadState, EntityType, EntityId, CreatedOn, AdditionalInfo FROM pay.ASNotification             
                                WHERE TenantId = @TenantId AND AppId = @AppId And RecipientTenantUserId = @TenantUserId AND EntityType=@EntityType 
                                ORDER BY createdon desc";


                SqlParameter paramAppId = new SqlParameter("@AppId", appId);
                SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
                SqlParameter paramTenantUserId = new SqlParameter("@TenantUserId", TenantUserId);
                SqlParameter paramEntityType = new SqlParameter("@EntityType", entityType);
        
                return await GetQueryEntityListAsync<ASNotificationDTO>(query, new SqlParameter[] { paramAppId, paramTenantId, paramTenantUserId, paramEntityType}, token);
            }
            else {
                string query = @"SELECT  Id, TextContent, TenantId,AppId, RecipientTenantUserId,ReadState, EntityType, EntityId, CreatedOn, AdditionalInfo FROM pay.ASNotification             
                                WHERE TenantId = @TenantId AND AppId = @AppId And RecipientTenantUserId = @TenantUserId 
                                AND EntityType=@EntityType 
                                ORDER BY createdon desc  OFFSET @FromCount ROWS FETCH NEXT @ToCount ROWS ONLY ";

                SqlParameter paramAppId = new SqlParameter("@AppId", appId);
                SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
                SqlParameter paramTenantUserId = new SqlParameter("@TenantUserId", TenantUserId);
                SqlParameter paramFromCount = new SqlParameter("@FromCount", fromCount);
                SqlParameter paramToCount = new SqlParameter("@ToCount", toCount);
                SqlParameter paramEntityType = new SqlParameter("@EntityType", entityType);

                return await GetQueryEntityListAsync<ASNotificationDTO>(query, new SqlParameter[] { paramAppId, paramTenantId, paramTenantUserId, paramFromCount, paramToCount, paramEntityType }, token);
            }
        }

        public async Task<List<ASNotificationDTO>> GetUnreadASNotificationList(Guid appId, Guid tenantId, Guid TenantUserId, CancellationToken token = default(CancellationToken)) {
            string query = @"SELECT  Id, TextContent, TenantId,AppId, RecipientTenantUserId,ReadState, EntityType, EntityId, CreatedOn, AdditionalInfo FROM pay.ASNotification             
             WHERE TenantId = @TenantId AND AppId = @AppId And RecipientTenantUserId = @TenantUserId AND ReadState = 0 ORDER BY createdon desc";
            SqlParameter paramAppId = new SqlParameter("@AppId", appId);
            SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
            SqlParameter paramTenantUserId = new SqlParameter("@TenantUserId", TenantUserId);

            return await GetQueryEntityListAsync<ASNotificationDTO>(query, new SqlParameter[] { paramAppId, paramTenantId, paramTenantUserId }, token);


        }
    }
}
