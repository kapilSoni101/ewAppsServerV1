using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DS;
using ewApps.BusinessEntity.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.BusinessEntity.WebApi {

    /// <summary>
    /// Provide add/update/get method for ERPConnector Configuration.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ERPConnectorConfigController:ControllerBase {

        #region Local Variables

        IERPConfigDS _erpConfigDS;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// initlize local variables.
        /// </summary>
        /// <param name="erpConfigDS"></param>
        public ERPConnectorConfigController(IERPConfigDS erpConfigDS) {
            _erpConfigDS = erpConfigDS;
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Establish app connection with SAPB1
        /// </summary>
        /// <returns></returns>        
        [HttpGet]
        [Route("getconnectorconfig/{tenantId}")]
        public async Task<List<ERPConnectorConfigDQ>> GetBusinessAppConnectorConfigByBusinessIdAsync([FromRoute]Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _erpConfigDS.GetBusinessAppConnectorConfigByBusinessIdAsync(tenantId, token);
        }

        #endregion Get

        #region Add/Update

        /// <summary>
        /// Add ERP connector setting.
        /// </summary>
        /// <param name="configDTOList">List of connector setting</param>
        /// <param name="token"></param>
        [AllowAnonymous]
        [HttpPost("addconnectorconfig")]
        public async Task AddBusinessConnectorConfigsAsync([FromBody]List<ERPConnectorConfigDTO> configDTOList, CancellationToken token = default(CancellationToken)) {
            await _erpConfigDS.AddBusinessConnectorConfigsAsync(configDTOList, token);
        }

        /// <summary>
        /// Add/Update ERP connector setting.
        /// </summary>
        /// <param name="configDTOList">List of connector setting</param>
        /// <param name="tenantId">Tenantid</param>
        /// <param name="token"></param>
        [Route("addupdateconfig/{tenantId}")]
        [HttpPost]        
        public async Task AddUpdateBusinessConnectorConfigsAsync([FromBody]List<ERPConnectorConfigDTO> configDTOList, [FromRoute]Guid tenantId, CancellationToken token = default(CancellationToken)) {
            
            await _erpConfigDS.UpdateBusinessConnectorConfigsAsync(configDTOList, tenantId, token);
        }

        #endregion Add/Update

    }
}
