using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.DbConProvider;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.BusinessEntity.DTO;

namespace ewApps.BusinessEntity.Data {
    public class ERPConnectorRepository:BaseRepository<ERPConnector, BusinessEntityDbContext>, IERPConnectorRepository {
        #region Constructor 

        /// <summary>
        ///  Constructor initializing the base variables
        /// </summary>
        public ERPConnectorRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor 


        /// <summary>
        /// Get Connector List
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<ERPConnectorDQ>> GetConnectorListAsync(CancellationToken token = default(CancellationToken)) {
            //string query = @"SELECT Name, ConnectorKey FROM Connector";
            //return await GetQueryEntityListAsync<ConnectorDQ>(query, null);

            List<ERPConnector> connector = _context.ERPConnector.Select(i => i).ToList();
            List<ERPConnectorDQ> connectorDQ = new List<ERPConnectorDQ>();

            foreach(ERPConnector index in connector) {
                connectorDQ.Add(new ERPConnectorDQ() { ConnectorName = index.Name, ConnectorKey = index.ConnectorKey, Active = index.Active });
            }
            return connectorDQ;
        }

    }
}
