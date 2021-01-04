/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra
* Contributor: Asha Sharda
 * Date: 10 Jan 2019
 */

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ewApps.Core.CommonService;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;

namespace ewApps.Core.AppDeeplinkService {

    /// <summary>
    /// Database context for AppDeeplink Operations.
    /// Save the AppDeeplink
    /// AppDeepLinkAccesslog  
    /// </summary>
    public class AppDeeplinkDBContext:DbContext {

        #region Constructor and Veriable

        private AppDeeplinkAppSettings _connOptions;
        private string _connString;

        /// <summary>
        /// Constructor with Context Options
        /// </summary>
        /// <param name="context"></param>
        /// <param name="connString"></param>
        public AppDeeplinkDBContext(DbContextOptions<AppDeeplinkDBContext> context, string connString) : base(context) {
            _connString = connString;
        }

        /// <summary>
        /// Constructor with AppSetting
        /// </summary>
        /// <param name="options"></param>
        /// <param name="appSetting"></param>
        /// <param name="loggingService"></param>
        public AppDeeplinkDBContext(DbContextOptions<AppDeeplinkDBContext> context, IOptions<AppDeeplinkAppSettings> appSetting) : base(context) {
            _connOptions = appSetting.Value;// appSetting.Value.WebhookConnectionStrings;  
            _connString = _connOptions.ConnectionString;
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
        }
        /// <summary>
        /// Defines all the configuratiion option for the Database
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(_connString);//Use Sql Server as Backend
            base.OnConfiguring(optionsBuilder);
        }
        #endregion

        #region DataSets

        /// <summary>
        /// AppDeeplink Server DataSet
        /// </summary>
        public virtual DbSet<AppDeeplink> AppDeeplink {
            get;
            set;
        }

        /// <summary>
        /// AppDeeplinkAccessLog DataSet
        /// </summary>
        public virtual DbSet<AppDeeplinkAccessLog> AppDeeplinkAccessLog {
            get;
            set;
        }

        /// <summary>
        /// AppDeeplinkPayloadDTO queried model.
        /// </summary>
        public DbQuery<AppDeeplinkPayloadDTO> AppDeeplinkPayloadDTOQuery {
            get; set;
        }


        #endregion

        #region AppDeeplink Methods

        /// <summary>
        /// Get deep link detail from short url.
        /// </summary>
        /// <param name="shortUrl">unique value of generated number.</param>
        /// <returns>return AppDeeplinkPayloadDTO</returns>
        public async Task<AppDeeplinkPayloadDTO> GetDeeplinkAsync(string shortUrl, CancellationToken token = default(CancellationToken)) {
            return await AppDeeplink.Where(link => link.ShortUrlKey == shortUrl).Select(link =>
               new AppDeeplinkPayloadDTO() {
                   ID = link.ID,
                   ActionData = link.ActionData,
                   ActionName = link.ActionName,
                   ExpirationDate = link.ExpirationDate,
                   MaxUseCount = link.MaxUseCount,
                   UserAccessCount = link.UserAccessCount,
                   UserId = link.UserId,
                   TenantId = link.TenantId,
                   ActionEndpointUrl = link.ActionEndpointUrl,
                   Password = link.Password
               }).FirstOrDefaultAsync(token);
        }

        /// <summary>
        /// Get deep link detail from numberId.
        /// </summary>
        /// <param name="numberId">unique generated number.</param>
        /// <returns>return detail object of AppDeeplinkPayloadDTO</returns>
        public async Task<AppDeeplinkPayloadDTO> GetDeeplinkByNumberAsync(long numberId, CancellationToken token = default(CancellationToken)) {
            return await AppDeeplink.Where(link => link.NumberId == numberId).Select(link =>
               new AppDeeplinkPayloadDTO() {
                   ID = link.ID,
                   ActionData = link.ActionData,
                   ActionName = link.ActionName,
                   ExpirationDate = link.ExpirationDate,
                   MaxUseCount = link.MaxUseCount,
                   UserAccessCount = link.UserAccessCount,
                   UserId = link.UserId,
                   TenantId = link.TenantId,
                   ActionEndpointUrl = link.ActionEndpointUrl,
                   Password = link.Password
               }).FirstOrDefaultAsync(token);
        }

        /// <summary>
        /// Get deep link detail from numberId.
        /// </summary>
        /// <param name="numberId">unique generated number.</param>
        /// <returns>return detail object of AppDeeplinkPayloadDTO</returns>
        public async Task<bool> IsNumberExistAsync(long numberId, CancellationToken token = default(CancellationToken)) {
            return await AppDeeplink.AnyAsync(link => link.NumberId == numberId, token);
        }

        /// <summary>
        /// Asynchronously gets the entity (of type V) list based on input SQL and parameters.
        /// </summary>
        /// <typeparam name="V">The type of entity of result.</typeparam>
        /// <param name="sql">A valid SQL string that should contains all properties of TEntity entity.</param>
        /// <param name="parameters">The list of sql parameters (if null none of sql parameters applied).</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns>Returns entity (of type V) list based on input SQL and parameters.</returns>
        private async Task<V> GetQueryEntityListAsync<V>(string sql, object[] parameters, CancellationToken token = default(CancellationToken)) where V : class {
            IQueryable<V> querable;

            if(parameters != null && parameters.Length > 0) {
                querable = Query<V>().FromSql(sql, parameters);
            }
            else {
                querable = Query<V>().FromSql(sql);
            }

            return await querable.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method will expire the deeplink.
        /// </summary>
        /// <param name="shortUrl">Unique short url key</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task ExpireDeepLink(string shortUrl, CancellationToken token = default(CancellationToken)) {            
           AppDeeplink appLink = await AppDeeplink.Where(link => link.ShortUrlKey == shortUrl).FirstOrDefaultAsync(token);
            if(appLink != null) {
                appLink.ExpirationDate = DateTime.Now.AddSeconds(-5);
                //appLink.UserAccessCount = appLink.MaxUseCount;
                SaveChanges();
            }
        }

        /// <summary>
        /// Method will expire the deeplink.
        /// </summary>
        /// <param name="tenantId">tenantId</param>
        /// <param name="entityId">Action entityId.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task ExpireDeepLink(Guid tenantId, Guid entityId, CancellationToken token = default(CancellationToken)) {
            List<AppDeeplink> appLink = await AppDeeplink.Where(link => link.TenantId == tenantId && link.ActionData.Contains(entityId.ToString())).ToListAsync(token);
            if(appLink != null) {
                for(int i = 0; i < appLink.Count; i++) {
                    appLink[i].ExpirationDate = DateTime.Now.AddSeconds(-5);
                }
                //appLink.UserAccessCount = appLink.MaxUseCount;
                SaveChanges();
            }
        }

        #endregion AppDeeplink Methods
    }
}
