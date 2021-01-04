using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.QData {
    public class QContactUsRepository:QBaseRepository<QAppPortalDbContext>, IQContactUsRepository {

        public QContactUsRepository(QAppPortalDbContext context) : base(context) {
        }

        ///<inheritdoc/>
        public async Task<UserEmailDTO> GetPubEmailRecipent(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            string query = @"SELECT TU.Email
                                FROM am.TenantUser TU
                                INNER JOIN am.UserTenantLinking uTl ON TU.ID= uTl.TenantUserId 
                                INNER JOIN am.TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = uTl.TenantId								 
								INNER JOIN am.TenantLinking tl ON tl.PublisherTenantId = uTl.TenantId  
						        INNER JOIN am.Tenant t ON t.ID = tl.BusinessTenantId AND tl.BusinessPartnerTenantId is null
                                WHERE utl.UserType = @UserType AND tl.BusinessTenantId = @TenantId";

            // Input parameters
            SqlParameter userTypeParam = new SqlParameter("@UserType", (int)UserTypeEnum.Publisher);
            SqlParameter tenantIdParam = new SqlParameter("@TenantId", tenantId);
            UserEmailDTO userEmailDTO = _context.Query<UserEmailDTO>().FromSql(query, new object[] { userTypeParam, tenantIdParam }).FirstOrDefault();
            return userEmailDTO;
        }

        ///<inheritdoc/>
        public async Task<UserEmailDTO> GetPlatEmailRecipent(CancellationToken token = default(CancellationToken)) {
            string query = @"SELECT TU.Email
                                FROM am.TenantUser TU
                                INNER JOIN am.UserTenantLinking T ON TU.ID= t.TenantUserId 
                                INNER JOIN am.TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = T.TenantId
                                WHERE T.UserType = @UserType";

            // Input parameters
            SqlParameter userTypeParam = new SqlParameter("@UserType", (int)UserTypeEnum.Platform);
            UserEmailDTO userEmailDTO = _context.Query<UserEmailDTO>().FromSql(query, new object[] { userTypeParam }).FirstOrDefault();
            return userEmailDTO;
        }
    }
}
