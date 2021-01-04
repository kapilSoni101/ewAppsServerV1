/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
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
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.DbConProvider;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppMgmt.Data {

    /// <summary>
    /// Class will add the tenant(business).    
    /// Add/update/delete tenant.    
    /// </summary>
    public class TenantRepository:BaseRepository<Tenant, AppMgmtDbContext>, ITenantRepository {


        #region Constructor

        /// <summary>
        /// Parameter conatins the DBContent and SessionManager, It will be used by a parent class.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        public TenantRepository(AppMgmtDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor

        #region Get

        ///<inheritdoc/>
        public async Task<UpdateTenantModelDQ> GetBusinessTenantDetailModelDTOAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            string sql = "SELECT u.FullName as CreatedByName, t.* FROM am.Tenant t  " +
                          "INNER JOIN am.TenantUser u on t.CreatedBy = u.ID ";
            sql = string.Format(sql + " And t.Id= '{0}' ", tenantId.ToString());
            List<UpdateTenantModelDQ> list = await GetQueryEntityListAsync<UpdateTenantModelDQ>(sql, null, token);
            if(list != null && list.Count > 0)
                return list[0];
            return null;
        }

        #endregion Get

        #region Public Methods

        ///<inheritdoc/>
        public async Task<bool> IsSubdomainExistAsync(string subdomain, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            if(tenantId != Guid.Empty) {
                if(await _context.Tenant.AnyAsync(o => o.ID != tenantId && o.SubDomainName.ToLower() == subdomain.ToLower(), token))
                    return true;
            }
            else {
                if(await _context.Tenant.AnyAsync(o => o.SubDomainName.ToLower() == subdomain.ToLower(), token))
                    return true;
            }
            return false;
        }

        ///<inheritdoc/>
        public async Task<TenantInfoDTO> GetTenantInfoByTenantIdAsync(Guid tenantId, int uType, CancellationToken token = default(CancellationToken)) {

            string query = "";
            switch(uType) {
                case (int)UserTypeEnum.Platform:
                    query = @"SELECT t.ID AS TenantId, t.SubDomainName, t.Name AS TenantName, null as  PlatformTenantId,null as  PublisherTenantId,null as BusinessTenantId, 
                            null as  BusinessPartnerTenantId , t.active as Active, t.deleted as Deleted FROM am.Tenant t 
                            WHERE t.ID = @tenantID ";
                    break;
                case (int)UserTypeEnum.Publisher:
                    query = @"SELECT t.ID AS TenantId, t.SubDomainName, t.Name AS TenantName, tl.PlatformTenantId as  PlatformTenantId,null as  PublisherTenantId,null as BusinessTenantId, 
                            null as  BusinessPartnerTenantId , t.active as Active, t.deleted as Deleted FROM am.Tenant t 
                            INNER JOIN am.tenantlinking tl on tl.PublisherTenantId = t.id 
                            WHERE t.ID = @tenantID and businessTenantId is null";
                    break;
                case (int)UserTypeEnum.Business:
                    query = @"SELECT t.ID AS TenantId, t.SubDomainName, t.Name AS TenantName, tl.PlatformTenantId as  PlatformTenantId,tl.PublisherTenantId as  PublisherTenantId,null as BusinessTenantId, 
                            null as  BusinessPartnerTenantId , t.active as Active, t.deleted as Deleted FROM am.Tenant t 
                            INNER JOIN am.tenantlinking tl on tl.BusinessTenantId = t.id 
                            WHERE t.ID = @tenantID and businessPartnerTenantId is null";
                    break;
                case (int)UserTypeEnum.Customer:
                    query = @"SELECT t.ID AS TenantId, t.SubDomainName, t.Name AS TenantName, tl.PlatformTenantId as  PlatformTenantId,tl.PublisherTenantId as  PublisherTenantId,null as BusinessTenantId, 
                            null as  BusinessPartnerTenantId , t.active as Active, t.deleted as Deleted FROM am.Tenant t 
                            INNER JOIN am.tenantlinking tl on tl.BusinessTenantId = t.id 
                            WHERE t.ID = @tenantID and businessPartnerTenantId is null";
                    break;
        case (int)UserTypeEnum.Vendor:
          query = @"SELECT t.ID AS TenantId, t.SubDomainName, t.Name AS TenantName, tl.PlatformTenantId as  PlatformTenantId,tl.PublisherTenantId as  PublisherTenantId,null as BusinessTenantId, 
                            null as  BusinessPartnerTenantId , t.active as Active, t.deleted as Deleted FROM am.Tenant t 
                            INNER JOIN am.tenantlinking tl on tl.BusinessTenantId = t.id 
                            WHERE t.ID = @tenantID and businessPartnerTenantId is null";
          break;
      }

            SqlParameter paramTenantID = new SqlParameter("@tenantID", tenantId);

            return await GetQueryEntityAsync<TenantInfoDTO>(query, new SqlParameter[] { paramTenantID }, token);
        }

        ///<inheritdoc/>
        public async Task<TenantInfoDTO> GetTenantInfoBySubdomainAsync(string subdomain, int uType, CancellationToken token = default(CancellationToken)) {

            string query = "";
            switch(uType) {
                case (int)UserTypeEnum.Platform:
                    query = @"SELECT t.ID AS TenantId, t.SubDomainName, t.Name AS TenantName, null as  PlatformTenantId,null as  PublisherTenantId,null as BusinessTenantId, 
                            null as  BusinessPartnerTenantId , t.active as Active, t.deleted as Deleted FROM am.Tenant t 
                            WHERE t.SubDomainName = @subDomainName ";
                    break;
                case (int)UserTypeEnum.Publisher:
                    query = @"SELECT t.ID AS TenantId, t.SubDomainName, t.Name AS TenantName, tl.PlatformTenantId as  PlatformTenantId,null as  PublisherTenantId,null as BusinessTenantId, 
                            null as  BusinessPartnerTenantId , t.active as Active, t.deleted as Deleted FROM am.Tenant t 
                            INNER JOIN am.tenantlinking tl on tl.PublisherTenantId = t.id 
                            WHERE t.SubDomainName = @subDomainName and businessTenantId is null";
                    break;
                case (int)UserTypeEnum.Business:
                    query = @"SELECT t.ID AS TenantId, t.SubDomainName, t.Name AS TenantName, tl.PlatformTenantId as  PlatformTenantId,tl.PublisherTenantId as  PublisherTenantId,null as BusinessTenantId, 
                            null as  BusinessPartnerTenantId , t.active as Active, t.deleted as Deleted FROM am.Tenant t 
                            INNER JOIN am.tenantlinking tl on tl.BusinessTenantId = t.id 
                            WHERE t.SubDomainName = @subDomainName and businessPartnerTenantId is null";
                    break;
                case (int)UserTypeEnum.Customer:
                    query = @"SELECT t.ID AS TenantId, t.SubDomainName, t.Name AS TenantName, tl.PlatformTenantId as  PlatformTenantId,tl.PublisherTenantId as  PublisherTenantId,null as BusinessTenantId, 
                            null as  BusinessPartnerTenantId , t.active as Active, t.deleted as Deleted FROM am.Tenant t 
                            INNER JOIN am.tenantlinking tl on tl.BusinessTenantId = t.id 
                            WHERE t.SubDomainName = @subDomainName and businessPartnerTenantId is null";
                    break;
        case (int)UserTypeEnum.Vendor:
          query = @"SELECT t.ID AS TenantId, t.SubDomainName, t.Name AS TenantName, tl.PlatformTenantId as  PlatformTenantId,tl.PublisherTenantId as  PublisherTenantId,null as BusinessTenantId, 
                            null as  BusinessPartnerTenantId , t.active as Active, t.deleted as Deleted FROM am.Tenant t 
                            INNER JOIN am.tenantlinking tl on tl.BusinessTenantId = t.id 
                            WHERE t.SubDomainName = @subDomainName and businessPartnerTenantId is null";
          break;
      }

            SqlParameter paramSubDomain = new SqlParameter("@subDomainName", subdomain);

            return await GetQueryEntityAsync<TenantInfoDTO>(query, new SqlParameter[] { paramSubDomain }, token);
        }

        ///<inheritdoc/>
        public async Task<List<string>> GetBusinessNameByAppIdAsync(Guid appId, Guid publisherTenantId, CancellationToken token = default(CancellationToken)) {

            List<string> list = _context.TenantSubscription.Join(_context.Tenant, ts => ts.TenantId, t => t.ID, (ts, t) => new { t, ts }).Join(_context.TenantLinking, b => b.t.ID, tl => tl.BusinessTenantId, (b, tl) => new { b, tl }).Where(x => x.b.ts.AppId == appId && x.b.t.Deleted == false && x.b.ts.Deleted == false && x.tl.PublisherTenantId == publisherTenantId && x.tl.BusinessPartnerTenantId == null).OrderBy(z => z.b.t.Name).Select(y => y.b.t.Name).ToList();
            return list;

        }

        ///<inheritdoc/>
        public async Task<List<string>> GetBusinessNameByAppIdPlatAsync(Guid appId, CancellationToken token = default(CancellationToken)) {

            List<string> list = await _context.TenantSubscription.Join(_context.Tenant, ts => ts.TenantId, t => t.ID, (ts, t) => new { t.Name, ts.AppId, t.Deleted, t.TenantType }).OrderBy(a => a.Name).Where(a => a.AppId == appId && a.Deleted == false && a.TenantType == 3).Select(s => s.Name).ToListAsync();
            return list;

        }
        #endregion Public Methods


    }
}
