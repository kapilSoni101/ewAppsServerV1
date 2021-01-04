using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.QData;
using ewApps.Core.BaseService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.Data {
    public class QPubBusinessSubsPlanAppServiceRepository:IQPubBusinessSubsPlanAppServiceRepository {

        #region Local 

        QAppPortalDbContext _context;

        #endregion Local

        #region Constructor         

        /// <summary>
        /// Initializes a new instance of the <see cref="QPubBusinessSubsPlanAppServiceRepository"/> class.
        /// </summary>
        /// <param name="context">Instance of CQRSAppPortalDbContext to executes database operations.</param>        
        public QPubBusinessSubsPlanAppServiceRepository(QAppPortalDbContext context) {
            _context = context;
        }
        #endregion

        #region Get Methods

        /// <inheritdoc/>
        public async Task<List<SubsPlanServiceInfoDTO>> GetSubscriptionPlanServiceByPlanIdAsync(Guid pubBusSubsPlanId, CancellationToken cancellationToken = default(CancellationToken)) {
            string query = @"SELECT pbspSer.ID AS 'SubscriptionPlanServiceId', pbsp.ID AS 'SubscriptionPlanId', pbspSer.AppServiceId, aser.Name AS 'ServiceName', aser.Active
                                FROM ap.PubBusinessSubsPlanAppService as pbspSer
                                INNER JOIN ap.PubBusinessSubsPlan as pbsp ON pbspSer.PubBusinessSubsPlanId=pbsp.ID
                                INNER JOIN am.AppService as aser ON pbspSer.AppServiceId=aser.ID
                                WHERE PubBusinessSubsPlanId=@PubBusSubsPlanId";

            SqlParameter pubBusSubsPlanIdParam = new SqlParameter("PubBusSubsPlanId", pubBusSubsPlanId);
            SqlParameter[] paramList = new SqlParameter[] { pubBusSubsPlanIdParam };

            return await _context.SubsPlanServiceInfoDTOQuery.FromSql<SubsPlanServiceInfoDTO>(query, paramList).ToListAsync(cancellationToken);

        }

        /// <summary>
        /// Get subscribed services by PlanId.
        /// </summary>
        /// <param name="pubBusSubsPlanId">PubBusinessSubsPlanId</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<ServiceInfoDTO>> GetPlanServicesByPlanIdAsync(Guid pubBusSubsPlanId, CancellationToken cancellationToken = default(CancellationToken)) {
            string query = @"SELECT Distinct ServiceName, AppServiceId, PubBusinessSubsPlanId
                            FROM ap.PubBusinessSubsPlanAppService  as pbspas
                            WHERE PubBusinessSubsPlanId = @PubBusSubsPlanId";

            SqlParameter pubBusSubsPlanIdParam = new SqlParameter("PubBusSubsPlanId", pubBusSubsPlanId);
            SqlParameter[] paramList = new SqlParameter[] { pubBusSubsPlanIdParam };

            return await _context.ServiceInfoDTOQuery.FromSql<ServiceInfoDTO>(query, paramList).ToListAsync(cancellationToken);

        }

        public async Task<List<SubsPlanServiceAttributeInfoDTO>> GetSubscriptionPlanAttributeByPlanIdAsync(Guid pubBusSubsPlanId, CancellationToken cancellationToken = default(CancellationToken)) {
            string query = @"SELECT pbspSer.ID AS 'SubsPlanServiceAttributeId', pbsp.ID AS 'SubscriptionPlanId', pbspSer.AppServiceId AS 'SubscriptionPlanServiceId', aserattr.ID AS 'AppServiceAttributeId', 
                             aserattr.Name AS 'AttributeName', aserattr.Active
                             FROM ap.PubBusinessSubsPlanAppService as pbspSer
                             INNER JOIN ap.PubBusinessSubsPlan as pbsp ON pbspSer.PubBusinessSubsPlanId=pbsp.ID
                             INNER JOIN am.AppServiceAttribute as aserattr ON pbspSer.AppServiceAttributeId=aserattr.ID
                             WHERE PubBusinessSubsPlanId=@PubBusSubsPlanId ";

            SqlParameter pubBusSubsPlanIdParam = new SqlParameter("PubBusSubsPlanId", pubBusSubsPlanId);
            SqlParameter[] paramList = new SqlParameter[] { pubBusSubsPlanIdParam };

            return await _context.SubsPlanServiceAttributeInfoDTOQuery.FromSql<SubsPlanServiceAttributeInfoDTO>(query, paramList).ToListAsync(cancellationToken);

        }



        #endregion
    }
}