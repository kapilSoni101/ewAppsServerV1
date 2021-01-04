using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.DTO.DBQuery;
using ewApps.Core.BaseService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.QData {
    public class QPublisherAndUserRepository:QBaseRepository<QAppPortalDbContext>, IQPublisherAndUserRepository {


        public QPublisherAndUserRepository(QAppPortalDbContext context) : base(context) {
        }


        #region GET branding
        /// <summary>
        /// Get Platform branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<PublisherBrandingDQ> GetPublisherBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken)) {

            //string query = @"SELECT ID,TenantId,Name,PoweredBy,Copyright,ThemeId,ThemeKey,ThumbnailId FROM  ap.vQPublisherBranding
            //WHERE TenantId = @TenantId AND AppId = @AppId";

            string query = @"SELECT ap.Publisher.ID,ap.Publisher.TenantId,ap.Publisher.Name,ap.Publisher.PoweredBy,ap.Publisher.ApplyPoweredBy,ap.Publisher.Copyright,ap.Publisher.LogoThumbnailId as ThumbnailId, PlatformThumbnailId =(select Top 1 ap.Platform.LogoThumbnailId from ap.Platform),ap.Publisher.CanUpdateCopyright,am.TenantSubscription.ThemeId,am.Theme.ThemeKey 
                             FROM ap.Publisher
                                    INNER JOIN am.TenantSubscription ON am.TenantSubscription.TenantId = ap.Publisher.TenantId AND am.TenantSubscription.AppId = @AppId
                                    INNER JOIN am.Theme ON am.Theme.ID = am.TenantSubscription.ThemeId
                             WHERE ap.Publisher.TenantId = @TenantId AND am.TenantSubscription.AppId = @AppId";
            SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
            SqlParameter paramAppId = new SqlParameter("@AppId", appId);
            return await GetQueryEntityAsync<PublisherBrandingDQ>(query, new SqlParameter[] { paramTenantId, paramAppId }, token);
        }
        #endregion

        #region GET Configuration
        public async Task<ConfigurationDTO> GetConfigurationDetailAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken)) {

            //string query = @"SELECT * from  ap.vQPublisherConfiguration;
            //WHERE am.UserTenantLinking.TenantId = @TenantId  AND am.TenantUserAppLinking.AppId = @AppId AND Tenant.ID=@TenantId AND am.TenantUserAppLinking.AppId=@AppId";

            string query = @"SELECT am.Tenant.ID AS TenantId, am.Tenant.Name,am.Tenant.SubDomainName, am.Tenant.Active, am.TenantUserAppLinking.InvitedOn, am.TenantUserAppLinking.JoinedDate,
                              TU.FullName AS InvitedBy, TUser.ID AS AdminUserId, TUser.FirstName AS AdminUserFirstName,
                              TUser.LastName AS AdminUserLastName, TUser.Email AS AdminUserEmail,
                              ap.Publisher.ID AS PublisherId, ap.Publisher.IdentityNumber, ap.Publisher.ContactPersonName, ap.Publisher.Website,
							  ap.Publisher.ContactPersonDesignation,ap.Publisher.ContactPersonEmail, ap.Publisher.ContactPersonPhone
                           FROM am.Tenant
                            INNER JOIN am.UserTenantLinking ON am.UserTenantLinking.TenantId = @TenantId AND am.UserTenantLinking.IsPrimary = '1'
                            INNER JOIN am.TenantUser AS TUser ON TUser.ID = am.UserTenantLinking.TenantUserId
                            INNER JOIN am.TenantUserAppLinking ON am.TenantUserAppLinking.TenantId = am.Tenant.ID AND am.TenantUserAppLinking.AppId = @AppId AND am.TenantUserAppLinking.TenantUserId = TUser.ID
                            INNER JOIN am.TenantUser AS TU ON am.TenantUserAppLinking.InvitedBy = TU.ID
                            INNER JOIN ap.Publisher ON am.Tenant.ID = ap.Publisher.TenantId
                          WHERE Tenant.ID = @TenantId AND am.TenantUserAppLinking.AppId = @AppId";

            SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
            SqlParameter paramAppId = new SqlParameter("@AppId", appId);
            return await GetQueryEntityAsync<ConfigurationDTO>(query, new SqlParameter[] { paramTenantId, paramAppId }, token);
        }
        #endregion

        #region Get

        //       /// <summary>
        //       /// Get appdetail bu publisher tenant and appid.
        //       /// </summary>
        //       /// <param name="appId"></param>
        //       /// <param name="pubTenantId"></param>
        //       /// <returns></returns>
        public async Task<AppDetailDQ> GetAppDetailsAsyncFromPubAppSettingAppAndTenantID(Guid appId, Guid pubTenantId, CancellationToken token = default(CancellationToken)) {
            string query = @"SELECT pas.ID,app.ID as AppID,pas.Deleted,pas.Name,pas.Active,pas.ThemeId,'' as Appdata,pas.CreatedOn, pas.CreatedBy, pas.UpdatedOn, pas.UpdatedBy, app.IdentityNumber,
                            pas.InactiveComment, 0 as servicecount ,0 as publishercount, 0 as PurchaseCount ,th.ThemeKey,u.FullName as CreaterName,
                            t.ID as ThumbnailId,t.FileName ,  ThumbnailUrl=null, app.AppKey 
                            FROM ap.PublisherAppSetting pas 
                            INNER JOIN am.App ON app.id = pas.appid AND app.ID = @appId 
                            AND pas.TenantId = @pubTenantId  
                            LEFT JOIN am.TenantUser u on pas.CreatedBy=u.ID 
                            LEFT JOIN Core.EntityThumbnail t on pas.ThumbnailId=t.ID
                            LEFT JOIN am.Theme th on th.ID =pas.ThemeId 
                            WHERE pas.deleted=0 ";
            SqlParameter appIdParam = new SqlParameter("@appId", appId);
            SqlParameter publisherTenantIdParam = new SqlParameter("@pubTenantId", pubTenantId);
            AppDetailDQ appDetailDTOs = await GetQueryEntityAsync<AppDetailDQ>(query, new object[] { appIdParam, publisherTenantIdParam }, token);
            return appDetailDTOs;
        }

        /// <summary>
        ///  Get subscribed application list by a publisher.
        /// </summary>
        /// <param name="publisherTenantID"></param>
        /// <param name="includeInactive">Include inactive application also.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<AppDQ>> GetPublisherSubscribedAppByTenantAsync(Guid publisherTenantID, bool includeInactive, CancellationToken token = default(CancellationToken)) {
            string sql = "";
            if(includeInactive) {
                sql = @"SELECT app.[ID],app.[CreatedBy],app.[CreatedOn],app.[UpdatedBy],app.[UpdatedOn],app.[Deleted],app.[IdentityNumber],pubSetting.[Name],
                        app.[ThemeId], CASE WHEN pubSetting.[Active] = 0 THEN pubSetting.[Active] ELSE app.[Active] END AS [Active]
                        ,app.[AppKey],app.[InactiveComment],app.[AppScope]  FROM App app 
                        INNER JOIN PublisherAppSetting pubSetting ON pubSetting.AppId = app.Id And app.Deleted = 0 
                        AND pubSetting.Deleted = 0 AND pubSetting.TenantId = @publisherTenantID";
            }
            else {
                sql = "SELECT pubSetting.Name,  app.[ID],app.[CreatedBy],app.[CreatedOn],app.[UpdatedBy],app.[UpdatedOn],app.[Deleted],app.[IdentityNumber], " +
                      " app.[ThemeId], app.[Active], app.[AppKey],app.[InactiveComment], app.[AppScope] " +
                      " FROM App app INNER JOIN PublisherAppSetting pubSetting ON pubSetting.AppId = app.Id And app.Deleted = 0 And app.Active = 1 ";
                sql += "AND pubSetting.Deleted = 0 AND pubSetting.Active = 1 AND pubSetting.TenantId = @publisherTenantID";
            }

            SqlParameter paramSubdomain = new SqlParameter("@publisherTenantID", publisherTenantID);
            return await GetQueryEntityListAsync<AppDQ>(sql, new SqlParameter[] { paramSubdomain }, token);
        }

        /// <summary>
        ///  Get subscribed application list by a publisher tenant subdomain.
        /// </summary>
        /// <param name="publisherTenantID"></param>
        /// <param name="includeInactive">Include inactive application also.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<AppDQ>> GetPublisherSubscribedAppSubdomainAsync(string subdomainName, bool includeInactive, CancellationToken token = default(CancellationToken)) {
            string sql = "";
            if(includeInactive) {
                sql = @"SELECT DISTINCT app.[ID],app.[CreatedBy],app.[CreatedOn],app.[UpdatedBy],app.[UpdatedOn],app.[Deleted],app.[IdentityNumber],
                        pubSetting.[Name],
                        app.[ThemeId], CASE WHEN pubSetting.[Active] = 0 THEN pubSetting.[Active] ELSE app.[Active] END AS [Active]
                        ,app.[AppKey],app.[InactiveComment],app.[AppScope], app.AppSubscriptionMode  FROM AM.Tenant tenant
                        INNER JOIN am.TenantLinking utl ON utl.PublisherTenantId = tenant.ID AND TenantType = @tenantType AND tenant.SubDomainName = @subDomainName 
                        INNER JOIN AP.PublisherAppSetting pubSetting ON pubSetting.Active = 1 AND pubSetting.Deleted = 0 AND pubSetting.TenantId = utl.PublisherTenantId 
                        INNER JOIN AM.App app ON app.ID = pubSetting.AppId AND app.Deleted = 0 ";
            }
            else {
                sql = @"SELECT DISTINCT app.[ID],app.[CreatedBy],app.[CreatedOn],app.[UpdatedBy],app.[UpdatedOn],app.[Deleted],app.[IdentityNumber],
                        pubSetting.[Name],
                        app.[ThemeId], CASE WHEN pubSetting.[Active] = 0 THEN pubSetting.[Active] ELSE app.[Active] END AS [Active]
                        ,app.[AppKey],app.[InactiveComment],app.[AppScope], app.AppSubscriptionMode  FROM AM.Tenant tenant
                        INNER JOIN am.TenantLinking utl ON utl.PublisherTenantId = tenant.ID AND TenantType = @tenantType AND tenant.SubDomainName = @subDomainName 
                        INNER JOIN AP.PublisherAppSetting pubSetting ON pubSetting.Deleted = 0 AND pubSetting.TenantId = utl.PublisherTenantId 
                        INNER JOIN AM.App app ON app.ID = pubSetting.AppId AND app.Deleted = 0 ";
            }

            SqlParameter tType = new SqlParameter("@tenantType", (int)TenantType.Publisher);
            SqlParameter paramSubdomain = new SqlParameter("@subDomainName", subdomainName);
            return await GetQueryEntityListAsync<AppDQ>(sql, new SqlParameter[] { paramSubdomain, tType }, token);
        }

        /// <summary>
        /// Get application sub services subscribed by a Tenant applications.
        /// </summary>
        /// <param name="tenantId">Id of publisher Tenant</param>
        /// <returns>return application sub services and Application id.</returns>
        public async Task<List<TenantAppServiceDQ>> GetPublisherAppServiceByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            //string sql = "SELECT DISTINCT tasl.ID,appS.Name, appS.ID as AppServiceId, appS.AppId, asAttr.Name AS  AttributeName,  ISNULL(taslAttr.ServiceAttributeId, 0x00) AS AttributeId, '' AS AccountJson " +
            //             "FROM TenantAppService tasl INNER JOIN AppService appS ON tasl.AppServiceId = appS.ID AND tasl.Deleted = 0 AND appS.Deleted = 0  And tasl.TenantId = @TenantId " +
            //             "LEFT JOIN TenantAppServiceLinking taslAttr ON taslAttr.Deleted = 0 And taslAttr.TenantId =  tasl.TenantId AND taslAttr.ServiceId = tasl.AppServiceId  " +
            //             "LEFT JOIN AppServiceAttribute asAttr ON asAttr.AppServiceID = taslAttr.ServiceId AND asAttr.Deleted = 0 AND asAttr.ID = taslAttr.ServiceAttributeId  ";
            string sql = @"SELECT DISTINCT tasl.ID,appS.Name, appS.ID as AppServiceId, appS.AppId, asAttr.Name AS  AttributeName,  
                            ISNULL(tasl.AppServiceAttributeId, 0x00) AS AttributeId, '' AS AccountJson 
                            FROM Ap.PublisherAppService tasl 
                            INNER JOIN AM.AppService appS ON tasl.AppServiceId = appS.ID AND tasl.Deleted = 0 AND appS.Deleted = 0  And tasl.TenantId = @TenantId 
                            LEFT JOIN AM.AppServiceAttribute asAttr ON asAttr.AppServiceID = tasl.AppServiceId AND asAttr.Deleted = 0 AND asAttr.ID = tasl.AppServiceAttributeId";

            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);

            return await GetQueryEntityListAsync<TenantAppServiceDQ>(sql, new object[] { tenantParam }, token);
        }

        /// <summary>
        /// Get publisher created subscription plan or master subscription plan.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="publisherTenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<TenantApplicationSubscriptionDQ>> GetAppSubscriptionAsync(Guid appId, Guid publisherTenantId, CancellationToken token = default(CancellationToken)) {
            //string sql = @"SELECT * FROM AM.SubscriptionPlan WHERE Deleted = 0 AND  AppId =@appId AND ( TenantId = @emptyTenantId OR TenantId = @publisherTenantId) ";
            string sql = @"SELECT 
                            CreatedBy,CreatedOn,Deleted,BusinessUserCount,
                            ID,PlanName AS Name, CustomerUserCount AS NumberOfUsers,PaymentCycle,
                            PriceInDollar, ID AS SubscriptionPlanId,TenantId,Term, TermUnit,UpdatedBy,UpdatedOn,TransactionCount, AllowUnlimitedTransaction
                            FROM AM.SubscriptionPlan
                            WHERE Deleted = 0 AND  AppId =@appId AND ( TenantId = @emptyTenantId OR TenantId = @publisherTenantId)";

            SqlParameter tenantParam = new SqlParameter("@publisherTenantId", publisherTenantId);
            SqlParameter appIdParam = new SqlParameter("@appId", appId);
            SqlParameter emptyTenantId = new SqlParameter("@emptyTenantId", Guid.Empty);
            return await GetQueryEntityListAsync<TenantApplicationSubscriptionDQ>(sql, new object[] { tenantParam, appIdParam, emptyTenantId }, token);
        }

        /// <inheritdoc/>
        public async Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByAppAndPubTenantIdAsync(Guid appId, Guid publisherTenantId, BooleanFilterEnum planState, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT DISTINCT pbsp.[ID], pbsp.[IdentityNumber], pbsp.[PlanName], pbsp.[AppId], pbsp.[Term], pbsp.[TransactionCount], pbsp.[TermUnit], 
                        pbsp.[PriceInDollar], pbsp.[Active], pbsp.[PaymentCycle], pbsp.[AllowUnlimitedTransaction], pbsp.[BusinessUserCount], 
                        pbsp.[CustomerUserCount], pbsp.[EndDate], pbsp.[OtherFeatures], pbsp.[StartDate], PAS.Name AS 'AppName',  
                        (SELECT count(DISTINCT APS.AppServiceID) from  ap.PubBusinessSubsPlanAppService APS WHERE pbsp.Id= APS.PubBusinessSubsPlanId AND  APS.AppId = @AppId ) as 'AppServiceCount', 
                        GETDATE() as CreatedOn, convert(uniqueidentifier, '00000000-0000-0000-0000-000000000000') as CreatedBy, '' as CreatedByName
                        , pbsp.[AutoRenewal], pbsp.[OneTimePlan], pbsp.[UserPerCustomerCount], pbsp.[ShipmentCount], pbsp.[ShipmentUnit], pbsp.[AllowUnlimitedShipment]
                        ,sp.UpdatedBy, sp.UpdatedOn,  Convert(bit, 0)  as CanDelete , '' as UpdatedByName , app.Appkey 
                        FROM am.[SubscriptionPlan] AS sp
                        INNER JOIN ap.PubBusinessSubsPlan as pbsp ON sp.ID=pbsp.SubscriptionPlanId AND sp.AppId=@AppId  
                        AND (pbsp.TenantId=@PublisherTenantId OR pbsp.TenantId=@EmptyGuid) 
                        INNER JOIN am.App AS app ON app.ID=pbsp.[AppId] 
                        INNER JOIN ap.PublisherAppSetting PAS ON PAS.TenantId = @PublisherTenantId AND PAS.AppId = @AppId 
                        LEFT JOIN ap.PubBusinessSubsPlanAppService as pbspas ON pbsp.Id= pbspas.PubBusinessSubsPlanId
                        Where pbsp.Active=@PlanState";

            SqlParameter appIdParam = new SqlParameter("AppId", appId);
            SqlParameter publisherTenantIdParam = new SqlParameter("PublisherTenantId", publisherTenantId);
            SqlParameter emptyGuidParam = new SqlParameter("EmptyGuid", Guid.Empty);
            SqlParameter planStateParam = new SqlParameter("PlanState", planState);
            SqlParameter[] paramList = new SqlParameter[] { appIdParam, publisherTenantIdParam, emptyGuidParam, planStateParam };
            return await GetQueryEntityListAsync<SubscriptionPlanInfoDTO>(sql, paramList);
        }


        ///<inheritdoc/>
        public async Task<List<AppDetailDTO>> GetAppDetailsPublisherAsync(Guid publisherTenantId, CancellationToken token = default(CancellationToken)) {
            string query = @"select pas.ID,app.ID as AppID,pas.Deleted,pas.Name,pas.Active,pas.ThemeId,pas.CreatedOn, pas.CreatedBy, pas.UpdatedOn, pas.UpdatedBy, 
                           app.IdentityNumber,pas.InactiveComment, (select count(distinct appserviceid) from ap.PubBusinessSubsPlanAppService where AppId= pas.appID and TenantId = @publisherTenantId)
                           servicecount ,0 as publishercount, (select  count(1) from am.TenantSubscription ts 
                           INNER JOIN am.Tenant t on t.ID=ts.TenantId  
                           INNER JOIN am.TenantLinking tl on tl.BusinessTenantId = ts.TenantId and tl.BusinessPartnerTenantId is null  
                           where AppId = pas.AppId and ts.Deleted = 0 and t.TenantType = 3 and t.Deleted = 0 and 
                           tl.PublisherTenantId = @publisherTenantId) PurchaseCount ,'' as ThemeKey,u.FullName as CreaterName,
                           null as ThumbnailId,'' as FileName , ThumbnailUrl=null, app.AppKey from ap.PublisherAppSetting pas 
                           INNER JOIN am.app on  app.id = pas.appid
                           LEFT JOIN am.TenantUser u on pas.CreatedBy=u.ID
                           where pas.deleted=0 and pas.TenantId = @publisherTenantId";

            SqlParameter publisherTenantIdParam = new SqlParameter("@publisherTenantId", publisherTenantId);
            List<AppDetailDTO> appDetailDTOs = await GetQueryEntityListAsync<AppDetailDTO>(query, new object[] { publisherTenantIdParam });
            return appDetailDTOs;
        }


        ///<inheritdoc/>
        public async Task<AppDetailDTO> GetAppDetailsFromPubAppSettingIDAsync(Guid pubAppSettingID, CancellationToken token = default(CancellationToken)) {
            string query = @"select pas.ID,app.ID as AppID,pas.Deleted,pas.Name,pas.Active,pas.ThemeId,pas.CreatedOn, pas.CreatedBy, pas.UpdatedOn, pas.UpdatedBy,
                           app.IdentityNumber, pas.InactiveComment, 0 as servicecount ,0 as publishercount, 0 as PurchaseCount ,th.ThemeKey,u.FullName as CreaterName,
                           t.ID as ThumbnailId,t.FileName ,  ThumbnailUrl=null, app.AppKey from ap.PublisherAppSetting pas 
                           INNER JOIN am.app on  app.id = pas.appid 
                           LEFT JOIN am.TenantUser u on pas.CreatedBy=u.ID
                           LEFT JOIN core.EntityThumbnail t on pas.ThumbnailId=t.ID
                           LEFT JOIN am.Theme th on th.ID =pas.ThemeId where pas.deleted=0 and pas.ID = @pubAppSettingID";

            SqlParameter publisherTenantIdParam = new SqlParameter("@pubAppSettingID", pubAppSettingID);
            AppDetailDTO appDetailDTOs = await GetQueryEntityAsync<AppDetailDTO>(query, new object[] { publisherTenantIdParam });
            return appDetailDTOs;
        }


        ///<inheritdoc/>
        public async Task<List<PublisherDetailsDTO>> GetAllPublishersAsync(CancellationToken cancellationToken = default(CancellationToken)) {
            //string cteSQL = @" WITH PublisherSubscribe(AppId, TenantId) AS (SELECT Distinct app.ID,pubSetting.TenantId FROM am.App app  
            //                 INNER JOIN ap.PublisherAppSetting pubSetting ON pubSetting.AppId = app.Id 
            //                 INNER JOIN am.TenantLinking AS tl ON pubSetting.TenantId=tl.PublisherTenantId
            //                 WHERE app.AppScope=@PublicAppScope AND app.Deleted = 0 AND tl.BusinessTenantId IS NOT NULL AND tl.BusinessPartnerTenantId IS NULL) ";
            //string query = cteSQL + @"
            //                SELECT pub.ID,pub.CreatedBy, au.FullName as CreatedByName,pub.CreatedOn, pub.UpdatedBy, pub.UpdatedOn, pub.Deleted, pub.Tenantid,pub.Name,pub.IdentityNumber, 
            //                pub.Active,pub.InactiveComment ,
            //                (
            //                SELECT COUNT(AppId)  from PublisherSubscribe where TenantId= pub.TenantId 
            //                ) as ApplicationCount,
            //                (SELECT COUNT(t.ID) FROM am.Tenant AS t
            //                INNER JOIN am.TenantLinking AS tl ON t.ID = tl.BusinessTenantId  
            //                WHERE tl.PublisherTenantId= pub.TenantId AND tl.BusinessTenantId IS NOT NULL
            //                AND tl.BusinessPartnerTenantId IS NULL AND t.Deleted=0 ) as TenantCount,
            //                (SELECT COUNT(tl.ID) FROM am.TenantLinking tl INNER JOIN am.Tenant T ON T.ID= TL.BusinessTenantId  WHERE PublisherTenantId= pub.TenantId AND tl.BusinessTenantId IS NOT NULL
            //                AND tl.BusinessPartnerTenantId IS NULL AND T.Active= 1 AND t.Deleted=0 )
            //                as ActiveTenantCount,
            //                (SELECT COUNT(ap.ID) FROM am.TenantUser ap INNER JOIN am.UserTenantLinking tu ON ap.ID= tu.TenantUserId AND  tu.TenantId=pub.Tenantid ) as UserCount 
            //                FROM ap.publisher as pub
            //                INNER JOIN am.TenantUser au ON au.ID= pub.CreatedBy Where pub.Deleted=0 ORDER BY pub.CreatedOn DESC";

            string sql = @"SELECT pub.ID, pub.CreatedBy, au.FullName AS CreatedByName,pub.CreatedOn,pub.UpdatedBy, pub.UpdatedOn, pub.Deleted,pub.Tenantid, pub.Name,pub.IdentityNumber, pub.Active,pub.InactiveComment, 
                        (SELECT COUNT(distinct a.ID) FROM am.App a
                        INNER JOIN ap.PublisherAppSetting ps on a.Id = ps.AppId
                        WHERE a.AppSubscriptionMode=@PublicAppScope AND a.Deleted = 0 AND  ps.TenantId = pub.TenantId AND ps.Deleted = 0) AS ApplicationCount, 
                        (SELECT COUNT(distinct t.Id) FROM am.TenantLinking tl 
                        INNER JOIN am.Tenant T ON T.ID= TL.BusinessTenantId 
                        WHERE PublisherTenantId= pub.TenantId 
                        AND tl.BusinessTenantId IS NOT NULL AND T.Deleted = 0 and t.TenantType = 3 
                        ) AS TenantCount, 
                        (SELECT COUNT(distinct t.Id) FROM am.TenantLinking tl 
                        INNER JOIN am.Tenant T ON T.ID= TL.BusinessTenantId 
                        WHERE PublisherTenantId= pub.TenantId AND tl.BusinessTenantId is not null and T.Active= 1 
                        AND T.Deleted = 0 ) AS ActiveTenantCount, 
                        (SELECT COUNT(distinct ap.ID) FROM am.TenantUser ap
                        INNER JOIN am.UserTenantLinking tu ON ap.ID= tu.TenantUserId 
                        AND tu.TenantId=pub.Tenantid ) AS UserCount, '' AS Status 
                        FROM ap.publisher AS pub
                        INNER JOIN am.TenantUser au ON au.ID= pub.CreatedBy
                        Where pub.Deleted=0 ORDER BY pub.CreatedOn DESC";

            SqlParameter publicAppScopeParam = new SqlParameter("PublicAppScope", 2);
            SqlParameter businessTenantTypeParam = new SqlParameter("BusinessTenantType", (int)TenantType.Buisness);
            SqlParameter[] paramList = new SqlParameter[] { publicAppScopeParam, businessTenantTypeParam };
            List<PublisherDetailsDTO> publisherDetailsDTOs = await GetQueryEntityListAsync<PublisherDetailsDTO>(sql, paramList, cancellationToken);
            return publisherDetailsDTOs;
        }

        ///<inheritdoc/>
        public async Task<List<AppDetailInfoDTO>> GetPubBusinessAppListByPubTenantIdAsync(Guid publisherTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            string query = @" SELECT app.ID, app.IdentityNumber ,pas.Name, pas.ThemeId, app.Active, App.AppKey FROM am.App
                              INNER JOIN AP.PublisherAppSetting AS PAS ON APP.ID=PAS.AppId
                              Where pas.TenantId=@PublisherTenantId ";
            SqlParameter publisherTenantIdParam = new SqlParameter("PublisherTenantId", publisherTenantId);
            SqlParameter[] paramList = new SqlParameter[] { publisherTenantIdParam };
            List<AppDetailInfoDTO> publisherDetailsDTOs = await GetQueryEntityListAsync<AppDetailInfoDTO>(query, paramList, cancellationToken);
            return publisherDetailsDTOs;
        }

        ///<inheritdoc/>
        public async Task<List<AppServiceDetailDTO>> GetPubBusinessAppServiceListByAppIdsAsync(List<Guid> appIdList, CancellationToken cancellationToken = default(CancellationToken)) {
            string query = @"SELECT distinct  aser.ID AS 'SerivceId', aser.Name as 'ServiceName', aser.Active, aser.AppId FROM am.App
                            INNER JOIN AP.PublisherAppSetting AS PAS ON APP.ID=PAS.AppId
                            INNER JOIN ap.PubBusinessSubsPlan as pbsp on pbsp.AppId=pas.AppId
                            inner join ap.PubBusinessSubsPlanAppService as pbspas on pbsp.ID=pbspas.PubBusinessSubsPlanId
                            inner join am.AppService as aser on pbspas.AppServiceId=aser.ID
                            Where aser.AppId in ({0})";
            string appIds = string.Format("{0}{1}{2}", "'", string.Join("','", appIdList), "'");
            query = string.Format(query, appIds);
            return await GetQueryEntityListAsync<AppServiceDetailDTO>(query, null, cancellationToken);
        }

        ///<inheritdoc/>
        public async Task<List<AppServiceAttributeDetailDTO>> GetPubBusinessAppServiceAttributeListByServiceIdsAsync(List<Guid> serviceIdList, CancellationToken cancellationToken = default(CancellationToken)) {
            string query = @"SELECT distinct  aserAttr.ID AS 'AttributeId', aserAttr.Name as 'AttributeName', aserAttr.Active, pbspas.AppServiceId FROM am.App
                            INNER JOIN AP.PublisherAppSetting AS PAS ON APP.ID= PAS.AppId
                            INNER JOIN ap.PubBusinessSubsPlan as pbsp on pbsp.AppId= pas.AppId
                            inner join ap.PubBusinessSubsPlanAppService as pbspas on pbsp.ID= pbspas.PubBusinessSubsPlanId
                            inner join am.AppServiceAttribute as aserAttr on pbspas.AppServiceAttributeId= aserAttr.ID
                            Where pbspas.AppServiceId in ({0})";
            string appIds = string.Format("{0}{1}{2}", "'", string.Join("','", serviceIdList), "'");
            query = string.Format(query, appIds);
            return await GetQueryEntityListAsync<AppServiceAttributeDetailDTO>(query, null, cancellationToken);
        }

        ///<inheritdoc/>
        public async Task<PublisherViewDTO> GetPublisherDetailByPublisherIdAsync(Guid publisherId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT p.ID, p.IdentityNumber, t.Name AS 'PublisherName', t.SubDomainName, t.JoinedOn, p.CreatedBy, tuCreatedBy.FullName AS 'CreatedByName', p.CreatedOn, p.Website, t.Active, 
                            tu.Id AS 'PrimaryUserId', tu.FirstName AS 'PrimaryUserFirstName', tu.LastName  AS 'PrimaryUserLastName', tu.FullName AS 'PrimaryUserFullName', 
                            tu.Email AS 'PrimaryUserEmail', p.ApplyPoweredBy, p.PoweredBy, p.CanUpdateCopyright, p.Copyright, 
                            p.CustomizedCopyright, p.ContactPersonName, p.ContactPersonDesignation, p.ContactPersonEmail, 
                            p.ContactPersonPhone, p.TenantId
                            FROM ap.Publisher as p
                            INNER JOIN am.Tenant AS t ON p.TenantId=t.ID
                            INNER JOIN am.UserTenantLinking AS utl ON t.ID=utl.TenantId 
                            INNER JOIN am.TenantUser AS tu ON utl.TenantUserId=tu.ID
                            INNER JOIN am.TenantUser AS tuCreatedBy ON p.CreatedBy = tuCreatedBy.ID
                            WHERE p.Id=@PublisherId AND utl.IsPrimary=1 AND p.Deleted=0";

            SqlParameter publisherIdParam = new SqlParameter("PublisherId", publisherId);
            SqlParameter[] sqlParameters = new SqlParameter[] { publisherIdParam };

            return await _context.PublisherViewDTOQuery.FromSql(sql, sqlParameters).FirstOrDefaultAsync(cancellationToken);
        }

        ///<inheritdoc/>
        public async Task<List<AppServiceDetailDTO>> GetPubBusinessAppServiceListByAppIdsAsync(Guid appId, CancellationToken cancellationToken = default(CancellationToken)) {
            string query = @"SELECT distinct  aser.ID AS 'SerivceId', aser.Name as 'ServiceName', aser.Active, aser.AppId FROM am.App
                            INNER JOIN AP.PublisherAppSetting AS PAS ON APP.ID=PAS.AppId
                            INNER JOIN ap.PubBusinessSubsPlan as pbsp on pbsp.AppId=pas.AppId
                            inner join ap.PubBusinessSubsPlanAppService as pbspas on pbsp.ID=pbspas.PubBusinessSubsPlanId
                            inner join am.AppService as aser on pbspas.AppServiceId=aser.ID
                            Where aser.AppId = @appId";

            SqlParameter appIdParam = new SqlParameter("@appId", appId);
            List<AppServiceDetailDTO> appServiceDetailDTO = await GetQueryEntityListAsync<AppServiceDetailDTO>(query, new object[] { appIdParam }, cancellationToken);
            return appServiceDetailDTO;


        }

        ///<inheritdoc/>
        public async Task<List<SubscriptionPlanInfoDTO>> GetPublishersBusinessSubscriptionPlanListByAppIdsAndPublisherTenantIdAsync(List<Guid> appIdList, Guid publisherTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            string query = @"SELECT 
                            DISTINCT sp.Id , sp.IdentityNumber, pbsp.PlanName, pbsp.PriceInDollar, pbsp.Term, pbsp.TermUnit, pbsp.Active, 
                            pbsp.PaymentCycle, pbsp.AllowUnlimitedTransaction, pbsp.BusinessUserCount, pbsp.CustomerUserCount, pbsp.StartDate, pbsp.EndDate, 
                            pbsp.OtherFeatures, pas.AppId, pas.Name AS AppName, pbsp.TransactionCount,  pbsp.CreatedOn, pbsp.CreatedBy, tu.FullName AS CreatedByName,
                            pbsp.AutoRenewal, pbsp.OneTimePlan, pbsp.UserPerCustomerCount, pbsp.ShipmentCount, pbsp.ShipmentUnit, pbsp.AllowUnlimitedShipment,
                            [AppServiceCount] = (Select  Count( Distinct ap.PubBusinessSubsPlanAppService.AppServiceId) from ap.PubBusinessSubsPlanAppService  where PubBusinessSubsPlanId=pbsp.ID )
                           ,sp.UpdatedBy, sp.UpdatedOn,  Convert(bit, 0)  as CanDelete  , '' as UpdatedByName  , '' as Appkey 
                            FROM ap.PublisherAppSetting as pas
                            INNER JOIN ap.PubBusinessSubsPlan as pbsp on pas.TenantId = pbsp.TenantId AND pas.AppId = pbsp.AppId
                            LEFT JOIN ap.PubBusinessSubsPlanAppService AS pbspas ON pbsp.ID = pbspas.PubBusinessSubsPlanId
                            INNER JOIN am.SubscriptionPlan AS sp ON pbsp.SubscriptionPlanId=sp.id
                            INNER JOIN am.TenantUser AS tu ON pbsp.CreatedBy = tu.ID
                            WHere pas.AppId IN ('{0}') AND pas.TenantId=@pubTenantIdParam AND pas.Deleted=0 AND pbsp.Deleted=0 AND (pbspas.Deleted=0 OR pbspas.Deleted IS NULL) AND tu.Deleted=0";

            query = string.Format(query, string.Join("','", appIdList));
            SqlParameter pubTenantIdParam = new SqlParameter("@pubTenantIdParam", publisherTenantId);
            List<SubscriptionPlanInfoDTO> subscriptionPlanList = await GetQueryEntityListAsync<SubscriptionPlanInfoDTO>(query, new object[] { pubTenantIdParam }, cancellationToken);
            return subscriptionPlanList;
        }



        /// <inheritdoc/>
        public async Task<List<PubAppSettingDTO>> GetPubAppSettingListByPublisherTenantIdAsync(Guid publisherTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            string query = @"SELECT AppId, pas.Name as 'AppName' FROM ap.PublisherAppSetting AS pas
                            INNER JOIN am.App ON pas.AppId=App.Id
                            WHERE pas.TenantId=@PublisherTenantId AND pas.Deleted=0 AND App.Constructed=1 And app.AppSubscriptionMode=2";

            SqlParameter pubTenantIdParam = new SqlParameter("PublisherTenantId", publisherTenantId);
            return await _context.PubAppSettingDTOQuery.FromSql(query, new object[] { pubTenantIdParam }).ToListAsync(cancellationToken);
        }

        public async Task<PublisherViewDTO> GetPublisherDetailByBusinessIdAsync(Guid businessTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT p.ID, p.IdentityNumber, t.Name AS 'PublisherName', t.SubDomainName, t.JoinedOn, p.CreatedBy, tuCreatedBy.FullName AS 'CreatedByName', p.CreatedOn, p.Website, t.Active, 
                            tu.Id AS 'PrimaryUserId', tu.FirstName AS 'PrimaryUserFirstName', tu.LastName  AS 'PrimaryUserLastName', tu.FullName AS 'PrimaryUserFullName', 
                            tu.Email AS 'PrimaryUserEmail', p.ApplyPoweredBy, p.PoweredBy, p.CanUpdateCopyright, p.Copyright, 
                            p.CustomizedCopyright, p.ContactPersonName, p.ContactPersonDesignation, p.ContactPersonEmail, 
                            p.ContactPersonPhone, p.TenantId
                            FROM ap.Publisher as p
                            INNER JOIN am.Tenant AS t ON p.TenantId=t.ID
                            INNER JOIN am.UserTenantLinking AS utl ON t.ID=utl.TenantId 
                            INNER JOIN am.TenantUser AS tu ON utl.TenantUserId=tu.ID
                            INNER JOIN am.TenantUser AS tuCreatedBy ON p.CreatedBy = tuCreatedBy.ID
                            INNER JOIN am.TenantLinking AS tl ON p.TenantId=tl.PublisherTenantId
                            WHERE tl.BusinessTenantId=@BusinessTenantId AND tl.BusinessPartnerTenantId IS NULL AND utl.IsPrimary=1 AND p.Deleted=0";

            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter[] sqlParameters = new SqlParameter[] { businessTenantIdParam };

            return await _context.PublisherViewDTOQuery.FromSql(sql, sqlParameters).FirstOrDefaultAsync(cancellationToken);
        }

       

        ///<inheritdoc/>
        public async Task<PublisherPermissionNotificationDTO> GetPublisherUserPermissionChangeNotificationGeneralDataAsync(Guid tenantUserId, Guid invitedTenantUserId, Guid tenantId) {
           

                string sql = @"SELECT p.Name as 'PublisherCompanyName', TN.SubDomainName as 'SubDomain',p.DateTimeFormat,p.TimeZone,
                            plat.Name as 'PlatformCompanyName',TU.FullName as 'InvitedUserFullName' , plat.Copyright as 'CopyRigthText' ,
                            TN.ID as 'InvitedUserTenantId', TU.ID as 'InvitedUserId' ,tus.FullName as InvitorUserFullName
                            FROM am.TenantUser TU
                            INNER JOIN am.Tenant TN ON TN.ID = @TenantId
							INNER JOIN am.TenantUser tus ON tus.ID = @UserId
                            INNER JOIN ap.Publisher as p on p.TenantId=TN.ID
                            INNER JOIN am.TenantLinking TL ON TL.PublisherTenantId = TN.ID AND TL.BusinessTenantId is null
                            INNER JOIN ap.Platform plat ON plat.TenantId = TL.PlatformTenantId							
                            WHERE TU.ID = @TenantUserId";

                SqlParameter tenantIdParam = new SqlParameter("TenantId", tenantId);
                SqlParameter invitedtenantUserIdParam = new SqlParameter("UserId", invitedTenantUserId);
                SqlParameter tenantUserIdParam = new SqlParameter("TenantUserId", tenantUserId);


                return await _context.PublisherPermissionNotificationDTOQuery.FromSql(sql, new object[] { tenantIdParam, tenantUserIdParam, invitedtenantUserIdParam }).FirstOrDefaultAsync();
            
        }
         

   


        #endregion Get

        #region User

        ///<inheritdoc/>
        public async Task<List<TenantUserDetailsDTO>> GetAllUsersByUserTypeAsync(int userType, Guid tenantId, Guid appId, bool deleted) {
            string query = string.Empty;
            if(deleted) {
                query = @"SELECT Distinct a.ID, a.FullName,FeatureCount=1,au.FullName as InvitedBy,UAL.joineddate as JoinedDate,Case when r.RoleKey='Admin' then CONVERT(bit,1) else CONVERT(bit,0) end as Admin 
                        , a.FirstName,a.LastName,UAL.InvitedOn as InvitedOn, a.email ,a.IdentityNumber
                        ,a.Phone, ual.Active , r.PermissionBitMask, r.ID as RoleId , tl.IsPrimary, UAL.BusinessPartnerTenantId , upd.FullName as UpdatedByName , TL.UpdatedOn as UpdatedOn , TL.Deleted 
                        FROM am.TenantUser a 
                        INNER JOIN am.UserTenantLinking TL on TL.TenantUserId= a.ID and TL.TenantId= @tenantId and tl.Deleted=@deleted
                        INNER JOIN ap.RoleLinking rl on rl.TenantUserId = a.ID AND rl.AppId = @AppId AND rl.tenantId= @tenantId  
                        INNER JOIN ap.Role r on r.ID = rl.RoleId 
                        INNER JOIN am.TenantUserAppLinking UAL on a.ID =UAL.TenantUserId and UAL.AppId= @AppId and UAL.tenantId= @tenantId  
                        INNER JOIN am.TenantUser au on UAL.InvitedBy = au.ID 
                        INNER JOIN am.TenantUser upd on upd.ID = TL.UpdatedBy
                        WHERE  UAL.UserType = @UserType  and UAL.AppId=@AppId  order by a.FullName ";
            }
            else {
                query = @"SELECT distinct a.ID, a.FullName,FeatureCount=1,inv.FullName as InvitedBy,UAL.joineddate as JoinedDate,Case when r.RoleKey='Admin' then CONVERT(bit,1) else CONVERT(bit,0) end as Admin 
                        , a.FirstName,a.LastName,UAL.InvitedOn as InvitedOn, a.email ,a.IdentityNumber
                        ,a.Phone, ual.Active , r.PermissionBitMask, r.ID as RoleId , tl.IsPrimary, UAL.BusinessPartnerTenantId , upd.FullName as UpdatedByName , UAL.UpdatedOn as UpdatedOn , UAL.Deleted 
                        FROM am.TenantUser a 
                        INNER JOIN am.UserTenantLinking TL on TL.TenantUserId= a.ID and TL.TenantId= @tenantId AND TL.Deleted=@deleted
                        INNER JOIN ap.RoleLinking rl on rl.TenantUserId = a.ID AND rl.AppId = @AppId AND rl.tenantId= @tenantId  AND rl.Deleted=@deleted
                        INNER JOIN ap.Role r on r.ID = rl.RoleId 
                        INNER JOIN am.TenantUserAppLinking UAL on a.ID =UAL.TenantUserId and UAL.AppId= @AppId and UAL.tenantId= @tenantId  AND UAL.Deleted=@deleted
                        INNER JOIN am.TenantUser upd on upd.ID = UAL.UpdatedBy 
                        INNER JOIN am.TenantUser inv on inv.ID = UAL.InvitedBy 
                        WHERE UAL.Deleted = @deleted  and UAL.UserType = @UserType  and UAL.AppId=@AppId  and TL.tenantId= @tenantId order by a.FullName ";
            }
            SqlParameter userTypeParam = new SqlParameter("@UserType", userType);
            SqlParameter appIdparam = new SqlParameter("@AppId", appId);
            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
            SqlParameter deletedParam = new SqlParameter("@deleted", deleted);

            List<TenantUserDetailsDTO> appUserDTO = await GetQueryEntityListAsync<TenantUserDetailsDTO>(query, new object[] { userTypeParam, appIdparam, tenantIdParam, deletedParam });
            return appUserDTO;
        }

        ///<inheritdoc/>
        public async Task<List<RoleKeyCountDTO>> CheckUserIsLastPublisherAdminUserAsync(Guid tenantId, int userType, Guid appId) {

            string query = @" SELECT RoleKey , RL.TenantUserId 
                            from am.UserTenantLinking UT 
                            INNER JOIN ap.RoleLinking RL ON RL.TenantUserId = UT.TenantUserId  AND RL.AppId= @AppId
                            INNER JOIN ap.Role r ON r.ID= RL.RoleId
                            WHERE UT.TenantId= @TenantId AND UT.BusinessPartnerTenantId is null AND RoleKey= @RoleKey AND UT.UserType= @UserType  AND RL.Deleted = 0  ";

            SqlParameter tenantIdparam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appIdparam = new SqlParameter("@AppId", appId);
            SqlParameter userTypeParam = new SqlParameter("@UserType", userType);
            SqlParameter roleKeyParam = new SqlParameter("@RoleKey", ewApps.AppPortal.Common.AppPortalConstants.AdminRoleKey);
            object[] paramlist = new object[] { tenantIdparam, userTypeParam, roleKeyParam, appIdparam };

            List<RoleKeyCountDTO> count = await GetQueryEntityListAsync<RoleKeyCountDTO>(query, paramlist);
            return count;
        }

        public async Task<TenantUserAndPermissionViewDTO> GetTenantUserDetails(Guid tenantUserId, Guid tenantId, string appKey, bool deleted) {

            string query = string.Empty;
            if(deleted) {
                query = @"select top 1 Tu.Id as 'TenantUserId', TU.FirstName, TU.LastName, TU.Email, TU.IdentityNumber,
                                    TU.CreatedOn , CTU.FullName as 'CreatedBy',TU.Phone , TL.JoinedDate, RR.PermissionBitMask, TL.Active, TL.[Status] as 'InvitationStatus'
                                    from am.TenantUser TU
                                    INNER JOIN am.App AP ON ap.AppKey = @appkey
                                    INNER JOIN am.TenantUserAppLinking TL ON TL.TenantUserId = @tenantUserId AND TL.AppId = ap.ID ANd TL.TenantId = @tenantId ANd TL.Deleted = @deleted
                                    INNER JOIN am.TenantUser CTU ON CTU.ID = TU.CreatedBy
                                    INNER JOIN ap.RoleLinking RL ON RL.TenantUserId = @tenantUserId AND RL.AppId = ap.ID ANd RL.TenantId = @tenantId
                                    INNER JOIN ap.[Role] RR ON RR.ID = RL.RoleId
                                    WHERE TU.ID = @tenantUserId";
            }
            else {
                query = @" select Tu.Id as 'TenantUserId', TU.FirstName, TU.LastName, TU.Email, TU.IdentityNumber,
                                TU.CreatedOn , CTU.FullName as 'CreatedBy',TU.Phone , TL.JoinedDate, RR.PermissionBitMask, TL.Active, TL.[Status] as 'InvitationStatus'
                                from am.TenantUser TU
                                INNER JOIN am.App AP ON ap.AppKey = @appkey
                                INNER JOIN am.TenantUserAppLinking TL ON TL.TenantUserId = @tenantUserId AND TL.AppId = ap.ID ANd TL.TenantId = @tenantId ANd TL.Deleted=@deleted
                                INNER JOIN am.TenantUser CTU ON CTU.ID = TU.CreatedBy 
                                INNER JOIN ap.RoleLinking RL ON RL.TenantUserId = @tenantUserId AND RL.AppId = ap.ID ANd RL.TenantId = @tenantId ANd RL.Deleted=@deleted
                                INNER JOIN ap.[Role] RR ON RR.ID = RL.RoleId
                                WHERE TU.ID = @tenantUserId AND TU.Deleted =@deleted ";
            }

            // Sql parameters.
            SqlParameter tenantUserIdParam = new SqlParameter("@tenantUserId", tenantUserId);
            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
            SqlParameter appKeyParam = new SqlParameter("@appkey", appKey);
            SqlParameter deletedParam = new SqlParameter("@deleted", deleted);

            TenantUserAndPermissionViewDTO tenantUserInfo = await GetQueryEntityAsync<TenantUserAndPermissionViewDTO>(query, new object[] { tenantUserIdParam, tenantIdParam, appKeyParam, deletedParam });
            return tenantUserInfo;
        }


        #endregion User

        public async Task<StringDTO> GetPublisherNameByBusinessTenantId(Guid businessTenantId, CancellationToken token = default(CancellationToken)) {
            string sql = @"SELECT p.* FROM ap.Publisher AS p
                     INNER JOIN am.TenantLinking AS tl on p.TenantId = tl.PublisherTenantId 
                     AND tl.BusinessTenantId=@BusinessTenantId 
                     AND tl.BusinessPartnerTenantId IS NULL";

            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);

            StringDTO appUserDTO = await GetQueryEntityAsync<StringDTO>(sql, new object[] { businessTenantIdParam }, token);
            return appUserDTO;

        }

        public async Task<StringDTO> GetPublisherNameByPartnerTenantId(Guid partnerTenantId, CancellationToken token = default(CancellationToken)) {
            string sql = @"SELECT  p.[Name] FROM ap.Publisher AS p
      INNER JOIN am.TenantLinking AS tl on p.TenantId = tl.PublisherTenantId 
      AND tl.BusinessPartnerTenantId=@PartnerTenantId";

            SqlParameter partnerTenantIdParam = new SqlParameter("PartnerTenantId", partnerTenantId);


            StringDTO appUserDTO = await GetQueryEntityAsync<StringDTO>(sql, new object[] { partnerTenantIdParam }, token);
            return appUserDTO;

        }

        public async Task<StringDTO> GetPublisherNameByTenantId(Guid tenantId, CancellationToken token = default(CancellationToken)) {

            string sql = @"SELECT t.[Name] FROM am.Tenant AS t where t.Id =@TenantId";

            SqlParameter partnerTenantIdParam = new SqlParameter("TenantId", tenantId);


            StringDTO appUserDTO = await GetQueryEntityAsync<StringDTO>(sql, new object[] { partnerTenantIdParam }, token);
            return appUserDTO;

        }

    }
}
