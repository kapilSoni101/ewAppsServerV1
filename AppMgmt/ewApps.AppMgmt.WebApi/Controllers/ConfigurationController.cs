using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.AppMgmt.DS;
using ewApps.AppMgmt.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppMgmt.WebApi {

    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController {

        #region Local member
        IConfigurationDS _configurationDS;
        #endregion

        #region Consturctor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationDS"></param>
        public ConfigurationController(IConfigurationDS configurationDS) {
            _configurationDS = configurationDS;
        }
        #endregion


        #region Update Publisher branding
        /// <summary>
        /// Update Publisher configuration
        /// </summary>
        /// <returns>ResponseModel</returns>
        /// <param name="publisherConfigurationDTO">publisher configuration model</param>
        [Route("updatepublisherconfiguration")]
        [HttpPut]
        public async Task UpdatePublisherConfigurationDetail([FromBody] ConfigurationDTO publisherConfigurationDTO) {
            await _configurationDS.UpdatePublisherConfigurationDetail(publisherConfigurationDTO);
        }
        #endregion

    }
}
