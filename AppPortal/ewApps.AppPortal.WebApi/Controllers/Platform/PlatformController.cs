using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.DTO.DBQuery;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {

    /// <summary>
    /// Platform class contains all add/update/delete/get methods for platform.    
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController:ControllerBase {

        #region Local member

        IPlatformDS _platformDS;
        
        #endregion


        #region Constructor

        /// <summary>
        /// Platform entity Add/Update/Delete
        /// </summary>
        public PlatformController(IPlatformDS platformDS) {
            _platformDS = platformDS;
        }

        #endregion


        #region GET/UPDATE branding

        /// <summary>
        /// Platform branding details
        /// </summary>
        /// <param name="tenantid"></param>
        /// <param name="appid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getbranding/{tenantid}/{appid}")]
        public async Task<PlatformBrandingDQ> GetPlatformBrandingAsync([FromRoute] Guid tenantid, [FromRoute] Guid appid) {
            return await _platformDS.GetPlatformBrandingAsync(tenantid, appid);
        }

        ///<summary>
        /// Get ThemeList
        ///</summary>
        [HttpGet]
        [Route("getthemenameandthemekey")]
        public async Task<IEnumerable<ThemeResponseDTO>> GetThemeNameAndThemeKey() {
            return await _platformDS.GetThemeNameAndThemeKey();
        }


        /// <summary>
        /// Update Platfrom Branding 
        /// </summary>
        /// <returns>ResponseModel</returns>
        /// <param name="platformBrandingDQ">platform branding model</param>
        [Route("updatebranding")]
        [HttpPut]
        public ResponseModelDTO UpdatePlatformBranding(PlatformBrandingDQ platformBrandingDQ) {
            _platformDS.UpdatePlatformBranding(platformBrandingDQ);
            return new ResponseModelDTO() {
                Id = Guid.NewGuid(),
                IsSuccess = true
            };
        }

        #endregion


        #region GET Connector

        /// <summary>
        /// get platform Connector List
        /// </summary>
        [HttpGet]
        [Route("getplatformconnectorlist")]
        public async Task<List<ConnectorDQ>> GetPlatformConnectorListAsync() {
            return await _platformDS.GetPlatformConnectorListAsync();
        }

        #endregion


    }

}

