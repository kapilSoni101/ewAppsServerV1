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
  public  class PublisherRepository : BaseRepository<Entity.Publisher , AppPortalDbContext> , IPublisherRepository{


        /// <summary>
        ///  Constructor initializing the base variables
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        public PublisherRepository(AppPortalDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #region Get

        /// <summary>
        /// Get publisher by publisher tenantid.
        /// </summary>
        /// <param name="pubTenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<Publisher> GetPublisherByPublisherTenantIdAsync(Guid pubTenantId, CancellationToken token = default(CancellationToken)) {
            return await _context.Publisher.Where(pub => pub.TenantId == pubTenantId).AsNoTracking().FirstOrDefaultAsync(token);
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<StringDTO>> GetPublisherListByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
           // return _context.Publisher.Where(p => p.Active == true && p.Deleted == false).Select(p => p.Name);

            string query = @"select p.name from ap.Publisher as p
                           INNER JOIN ap.PublisherAppSetting pubapp on pubapp.tenantId = p.TenantId    
                           where p.deleted=0 and pubapp.AppId =  @AppId";
           
            SqlParameter paramAppId = new SqlParameter("@AppId", appId);
            IEnumerable<StringDTO>  pubName= await GetQueryEntityListAsync<StringDTO>(query, new SqlParameter[] { paramAppId }, token);

            // return await GetQueryEntityAsync<PlatformBrandingDQ>(query, new SqlParameter[] { paramTenantId, paramAppId }, token);
            return pubName;
        }

        #endregion Get
    }
}
