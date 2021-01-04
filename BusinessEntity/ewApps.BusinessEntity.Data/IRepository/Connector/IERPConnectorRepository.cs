using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;


namespace ewApps.BusinessEntity.Data {
    public interface IERPConnectorRepository:IBaseRepository<ERPConnector> {


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<ERPConnectorDQ>> GetConnectorListAsync(CancellationToken token = default(CancellationToken));

    }
}
