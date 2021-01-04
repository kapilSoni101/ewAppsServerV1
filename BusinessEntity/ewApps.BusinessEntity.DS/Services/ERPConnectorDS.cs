/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil Nigam <anigam@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 */

using System.Collections.Generic;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    /// This class implements standard business logic and operations for connector entity.
    /// </summary>
    public class ERPConnectorDS:BaseDS<ERPConnector>, IERPConnectorDS {

        #region Local Member
        IERPConnectorRepository _connectorRepository;

        #endregion

        #region Constructor 

        /// <summary>
        /// Initializing local variables
        /// </summary>
        public ERPConnectorDS(IERPConnectorRepository connectorRepository) : base(connectorRepository) {
            _connectorRepository = connectorRepository;
        }

        #endregion Constructor        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<ERPConnectorDQ>> GetConnctorListAsync() {
            List<ERPConnectorDQ> connectorDQ = await _connectorRepository.GetConnectorListAsync();
            return connectorDQ;
        }

    }
}

