using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.QData {

    /// <summary>
    /// Provide customer and related service info.
    /// </summary>
    public class QBACustomerRepository:QBaseRepository<QAppPortalDbContext>, IQBACustomerRepository {

        #region Local         

        #endregion Local

        #region Constructor         

        /// <summary>
        /// Initializes a new instance of the <see cref="QBusinessAndUserRepository"/> class.
        /// </summary>
        /// <param name="context">Instance of CQRSAppPortalDbContext to executes database operations.</param>        
        public QBACustomerRepository(QAppPortalDbContext context) : base(context) {
        }

        #endregion Constructor

        #region Get 

        /// <summary>
        /// Get customer info by business entity customer id.
        /// </summary>
        /// <param name="baCustomerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<CustomerPaymentInfoDTO> GetCustomerInfoAsync(Guid baCustomerId, CancellationToken token = default(CancellationToken)) {
            string sql = @"SELECT baCust.ID AS BACustomerId, baCust.Currency, cust.ID AS CustomerId, baCust.CustomerName AS Name,baCust.BusinessPartnerTenantId, cust.TenantId
                           FROM BE.BACustomer baCust INNER JOIN AP.Customer cust 
                           ON cust.BusinessPartnerTenantId = baCust.BusinessPartnerTenantId AND baCust.ID = @baCustomerId";
            SqlParameter paramCustomerId = new SqlParameter("@baCustomerId", baCustomerId);
            return await GetQueryEntityAsync<CustomerPaymentInfoDTO>(sql, new SqlParameter[] { paramCustomerId }, token);
        }

        /// <summary>
        /// Get customer info by business entity customer id.
        /// </summary>
        /// <param name="businessPartnerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<CustomerPaymentInfoDTO> GetCustomerInfoByBusinessPartnerIdAsync(Guid businessPartnerId, CancellationToken token = default(CancellationToken)) {
            string sql = @"SELECT baCust.ID AS BACustomerId, baCust.Currency, cust.ID AS CustomerId, baCust.CustomerName AS Name,baCust.BusinessPartnerTenantId, cust.TenantId
                           FROM BE.BACustomer baCust INNER JOIN AP.Customer cust 
                           ON cust.BusinessPartnerTenantId = baCust.BusinessPartnerTenantId AND baCust.BusinessPartnerTenantId = @baCustomerId";
            SqlParameter paramCustomerId = new SqlParameter("@baCustomerId", businessPartnerId);
            return await GetQueryEntityAsync<CustomerPaymentInfoDTO>(sql, new SqlParameter[] { paramCustomerId }, token);
        }

        ///<inheritdoc/>
        public async Task<List<BusAppServiceDTO>> GetBusinessAppServiceListByAppIdsAndTenantIdAsync(List<Guid> appIdList, Guid tenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            // ToDo: Nitin - This query has been changed because of AppServiceId->SubsPlanAppserviceId
            //string query = @"SELECT distinct  aser.ID , aser.Name, aser.Active, aser.AppId, aser.ServiceKey 
            //                FROM am.TenantSubscription ts 
            //                INNER JOIN ap.PubBusinessSubsPlan as pbsp on pbsp.AppId=ts.AppId AND ts.SubscriptionPlanId = pbsp.ID AND ts.TenantId = @tenantId
            //                INNER JOIN ap.PubBusinessSubsPlanAppService 
            //                AS pbspas ON pbsp.ID=pbspas.PubBusinessSubsPlanId AND ts.AppId IN ({0}) 
            //                INNER JOIN am.SubscriptionPlanService as sps on pbspas.AppServiceId = sps.ID
            //                INNER JOIN am.AppService as aser on sps.AppServiceId=aser.ID";

            string query = @"SELECT distinct  aser.ID , aser.Name, aser.Active, aser.AppId, aser.ServiceKey 
                            FROM am.TenantSubscription ts 
                            INNER JOIN ap.PubBusinessSubsPlan as pbsp on pbsp.AppId=ts.AppId AND ts.SubscriptionPlanId = pbsp.ID AND ts.TenantId = @tenantId
                            INNER JOIN ap.PubBusinessSubsPlanAppService 
                            AS pbspas ON pbsp.ID=pbspas.PubBusinessSubsPlanId AND ts.AppId IN ({0}) 
                            INNER JOIN am.SubscriptionPlanService as sps on pbspas.SubsPlanAppServiceId = sps.ID
                            INNER JOIN am.AppService as aser on sps.AppServiceId=aser.ID";

            string appIds = string.Format("{0}{1}{2}", "'", string.Join("','", appIdList), "'");
            query = string.Format(query, appIds);
            SqlParameter paramtenantId = new SqlParameter("@tenantId", tenantId);

            return await GetQueryEntityListAsync<BusAppServiceDTO>(query, new SqlParameter[] { paramtenantId }, cancellationToken);
        }

        /// <summary>
        /// Get app service by appkey and tenantid.
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="tenantId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<BusAppServiceDTO>> GetBusinessAppServiceListByAppKeyAndTenantIdAsync(string appKey, Guid tenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            string query = @"SELECT distinct  aser.ID , aser.Name, aser.Active, aser.AppId, aser.ServiceKey 
                            FROM AM.App app
                            INNER JOIN am.TenantSubscription ts ON ts.AppId = app.ID AND app.AppKey = @appKey 
                            INNER JOIN ap.PubBusinessSubsPlan as pbsp on pbsp.AppId=ts.AppId AND ts.SubscriptionPlanId = pbsp.ID AND ts.TenantId = @tenantId
                            INNER JOIN ap.PubBusinessSubsPlanAppService 
                            AS pbspas ON pbsp.ID=pbspas.PubBusinessSubsPlanId
                            INNER JOIN am.SubscriptionPlanService as sps on pbspas.SubsPlanAppServiceId = sps.ID
                            INNER JOIN am.AppService as aser on sps.AppServiceId=aser.ID";
            
            SqlParameter paramtenantId = new SqlParameter("@tenantId", tenantId);
            SqlParameter paramAppKey = new SqlParameter("@appKey", appKey);

            return await GetQueryEntityListAsync<BusAppServiceDTO>(query, new SqlParameter[] { paramtenantId, paramAppKey }, cancellationToken);
        }

        ///<inheritdoc/>
        public async Task<List<BusAppServiceAttributeDTO>> GetBusinessAppServiceAttributeListByServiceIdsAsync(List<Guid> serviceIdList, CancellationToken cancellationToken = default(CancellationToken)) {
            // ToDo: Nitin - This query has been changed because of AppServiceAttributeId->SubsPlanAppServiceAttributeId
            //string query = @"SELECT distinct  aserAttr.ID, aserAttr.Name, aserAttr.Active, aserAttr.AppServiceId, aserAttr.AttributeKey  
            //                FROM am.TenantSubscription ts                            
            //                INNER JOIN ap.PubBusinessSubsPlan as pbsp on pbsp.AppId= ts.AppId
            //                inner join ap.PubBusinessSubsPlanAppService as pbspas on pbsp.ID= pbspas.PubBusinessSubsPlanId
            //                INNER JOIN am.SubscriptionPlanServiceAttribute as sps on pbspas.AppServiceAttributeId=sps.ID
            //                INNER JOIN am.AppServiceAttribute as aserAttr on sps.AppServiceAttributeId= aserAttr.ID
            //                Where aserAttr.AppServiceId in ({0})";

            string query = @"SELECT distinct  aserAttr.ID, aserAttr.Name, aserAttr.Active, aserAttr.AppServiceId, aserAttr.AttributeKey  
                            FROM am.TenantSubscription ts                            
                            INNER JOIN ap.PubBusinessSubsPlan as pbsp on pbsp.AppId= ts.AppId
                            inner join ap.PubBusinessSubsPlanAppService as pbspas on pbsp.ID= pbspas.PubBusinessSubsPlanId
                            INNER JOIN am.SubscriptionPlanServiceAttribute as sps on pbspas.SubsPlanAppServiceAttributeId=sps.ID
                            INNER JOIN am.AppServiceAttribute as aserAttr on sps.AppServiceAttributeId= aserAttr.ID
                            Where aserAttr.AppServiceId in ({0})";

            string appIds = string.Format("{0}{1}{2}", "'", string.Join("','", serviceIdList), "'");
            query = string.Format(query, appIds);
            return await GetQueryEntityListAsync<BusAppServiceAttributeDTO>(query, null, cancellationToken);
        }

        /// <summary>
        /// Get service account detail.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="entityId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<AppServiceAcctDetailDTO>> GetAppServiceAccountDetailByCustomerIdAsync(Guid tenantId, Guid entityId, CancellationToken token = default(CancellationToken)) {
            string payCapturedClosedStatus= PaymentCapturedStatusEnum.Closed.ToString();
            string sql = @"SELECT cust.ID, cust.AccountJson, cust.AccountType, pAuth.ID AS PreAuthPaymentID FROM AP.CustomerAccountDetail cust 
                     LEFT JOIN PAY.PreAuthPayment pAuth ON pAuth.CustomerAccountDetailId = cust.ID 
                     AND pAuth.MaxTotalPaymentCount > pAuth.CurrentPaymentSequenceNumber AND pAuth.Captured <> @payCapturedClosedStatus 
                     AND cust.CustomerId=@EntityId AND pAuth.ExpirationDate > @expDate AND pAuth.Status <> 'Voided'  
                     AND pAuth.Status <> 'Returned' AND cust.TenantId=@TenantId 
                     AND cust.Deleted=@Deleted  
                     WHERE cust.CustomerId=@EntityId AND cust.Deleted=@Deleted ";

            DateTime expDate = GetExpirationDate();

            SqlParameter tenantIdParam = new SqlParameter("TenantId", tenantId);
            SqlParameter entityIdParam = new SqlParameter("EntityId", entityId);
            SqlParameter deletedIdParam = new SqlParameter("Deleted", false);
            SqlParameter expDateParam = new SqlParameter("expDate", expDate);
            SqlParameter payCapturedClosedStatusParam = new SqlParameter("payCapturedClosedStatus", payCapturedClosedStatus);

            return await GetQueryEntityListAsync<AppServiceAcctDetailDTO>(sql, new object[] { entityIdParam, tenantIdParam, deletedIdParam, expDateParam, payCapturedClosedStatusParam }, token);
        }

        private DateTime GetExpirationDate() {
            DateTime todayDate = DateTime.Now.Date;
            return todayDate.AddMinutes(-1).ToUniversalTime(); //paymentDTO.OriginationDate;            
        }

        #endregion Get 

    }
}
