using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DS;
using ewApps.BusinessEntity.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.BusinessEntity.WebApi {

    /// <summary>
    /// Provide add/update/get method for ERPConnector 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ERPConnectorController :ControllerBase {

        #region Local Variables

        IERPConnectorDS _erpConnectorDS;

        #endregion

        #region Constructor

        /// <summary>
        /// Connector List
        /// </summary>
        /// <param name="erpConnectorDS"></param>
        public ERPConnectorController( IERPConnectorDS erpConnectorDS) {
            _erpConnectorDS = erpConnectorDS;
        }

        #endregion


        #region GET

        /// <summary>
        /// Get connection list
        /// </summary>
        /// <returns></returns>
        [HttpGet("getconnector")]
        public async Task<List<ERPConnectorDQ>> GetERPConnectorAsync(CancellationToken token = default(CancellationToken)) {
            return await _erpConnectorDS.GetConnctorListAsync();
        }

        #endregion 
    }
}
