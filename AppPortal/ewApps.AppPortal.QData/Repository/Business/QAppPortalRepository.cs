using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.QData {
    public class QAppPortalRepository:QBaseRepository<QAppPortalDbContext>, IQAppPortalRepository {


        #region Local Member
        #endregion

        #region Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="QBusinessAndUserRepository"/> class.
        /// </summary>
        /// <param name="context">Instance of CQRSAppPortalDbContext to executes database operations.</param>        
        public QAppPortalRepository(QAppPortalDbContext context) : base(context) {
        }
        
        #endregion

        
        #region GET Ship-App branding
        /// <summary>
        /// Get Ship-App branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<AppPortalBrandingDQ> GetShipAppBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken)) {
            string query = @"SELECT ap.PublisherAppSetting.ID, ts.TenantId, b.Name, ts.ThemeId,ts.AppId,am.Theme.ThemeKey
                             FROM am.TenantSubscription ts
							 INNER JOIN ap.Business b ON b.TenantId = ts.TenantId  
                             INNER JOIN am.TenantLinking ON am.TenantLinking.BusinessTenantId = @TenantId and am.TenantLinking.BusinessPartnerTenantId is null
                             INNER JOIN ap.PublisherAppSetting ON ap.PublisherAppSetting.TenantId = am.TenantLinking.PublisherTenantId and ts.AppId = ap.PublisherAppSetting.AppId
                             INNER JOIN am.Theme ON am.Theme.ID = ts.ThemeId
             WHERE ts.AppId = @AppId and ts.TenantId = @TenantId";
            
            SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
            SqlParameter paramAppId = new SqlParameter("@AppId", appId);

            return await GetQueryEntityAsync<AppPortalBrandingDQ>(query, new SqlParameter[] { paramTenantId, paramAppId }, token);
        }
        #endregion

        #region GET Pay-App branding
        /// <summary>
        /// Get Pay-App branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<AppPortalBrandingDQ> GetPayAppBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken)) {
            string query = @"SELECT ap.PublisherAppSetting.ID, ts.TenantId, b.Name, ts.ThemeId,ts.AppId,am.Theme.ThemeKey
                             FROM am.TenantSubscription ts
							 INNER JOIN ap.Business b ON b.TenantId = ts.TenantId  
                             INNER JOIN am.TenantLinking ON am.TenantLinking.BusinessTenantId = @TenantId and am.TenantLinking.BusinessPartnerTenantId is null
                             INNER JOIN ap.PublisherAppSetting ON ap.PublisherAppSetting.TenantId = am.TenantLinking.PublisherTenantId and ts.AppId = ap.PublisherAppSetting.AppId
                             INNER JOIN am.Theme ON am.Theme.ID = ts.ThemeId
                             WHERE ts.AppId = @AppId and ts.TenantId = @TenantId";
            SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
            SqlParameter paramAppId = new SqlParameter("@AppId", appId);
            return await GetQueryEntityAsync<AppPortalBrandingDQ>(query, new SqlParameter[] { paramTenantId, paramAppId }, token);
        }
        #endregion

        #region GET Cust-App branding
        /// <summary>
        /// Get Cust-App branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<AppPortalBrandingDQ> GetCustAppBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken)) {
            string query = @"SELECT ap.PublisherAppSetting.ID, ts.TenantId, b.Name, ts.ThemeId,ts.AppId,am.Theme.ThemeKey
                             FROM am.TenantSubscription ts
							 INNER JOIN ap.Business b ON b.TenantId = ts.TenantId  
                             INNER JOIN am.TenantLinking ON am.TenantLinking.BusinessTenantId = @TenantId and am.TenantLinking.BusinessPartnerTenantId is null
                             INNER JOIN ap.PublisherAppSetting ON ap.PublisherAppSetting.TenantId = am.TenantLinking.PublisherTenantId and ts.AppId = ap.PublisherAppSetting.AppId
                             INNER JOIN am.Theme ON am.Theme.ID = ts.ThemeId
                             WHERE ts.AppId = @AppId and ts.TenantId = @TenantId";

            SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
            SqlParameter paramAppId = new SqlParameter("@AppId", appId);

            return await GetQueryEntityAsync<AppPortalBrandingDQ>(query, new SqlParameter[] { paramTenantId, paramAppId }, token);
        }
        #endregion

        #region GET Vend-App branding
        /// <summary>
        /// Get Cust-App branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<AppPortalBrandingDQ> GetVendAppBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken)) {
            string query = @"SELECT ap.PublisherAppSetting.ID, ts.TenantId, b.Name, ts.ThemeId,ts.AppId,am.Theme.ThemeKey
                             FROM am.TenantSubscription ts
							 INNER JOIN ap.Business b ON b.TenantId = ts.TenantId  
                             INNER JOIN am.TenantLinking ON am.TenantLinking.BusinessTenantId = @TenantId and am.TenantLinking.BusinessPartnerTenantId is null
                             INNER JOIN ap.PublisherAppSetting ON ap.PublisherAppSetting.TenantId = am.TenantLinking.PublisherTenantId and ts.AppId = ap.PublisherAppSetting.AppId
                             INNER JOIN am.Theme ON am.Theme.ID = ts.ThemeId
                             WHERE ts.AppId = @AppId and ts.TenantId = @TenantId";

            SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
            SqlParameter paramAppId = new SqlParameter("@AppId", appId);

            return await GetQueryEntityAsync<AppPortalBrandingDQ>(query, new SqlParameter[] { paramTenantId, paramAppId }, token);
        }
        #endregion
    }
}
