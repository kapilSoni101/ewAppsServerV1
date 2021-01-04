/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil Nigam <anigam@eworkplaceapps.com>
 * Date: 08 Aug -2019
 * 
 */

using System.Collections.Generic;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    /// Provide functionality to write bussiness logic on connector entity by creating an object to this class.
    /// </summary>
    public interface IERPConnectorDS:IBaseDS<ERPConnector> {

        
        /// <summary>
        /// Get Connector list
        /// </summary>
        /// <returns></returns>
        Task<List<ERPConnectorDQ>> GetConnctorListAsync();

    }
}
