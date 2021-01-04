using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {
    [Route("api/[controller]")]
    [ApiController]
    public class ViewSettingController:ControllerBase {

        #region Local member

        IViewSettingsDS _viewSettingsDS;

        #endregion Local member

        #region cunstructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewSettingsDS"></param>
        public ViewSettingController(IViewSettingsDS viewSettingsDS) {
            _viewSettingsDS = viewSettingsDS;
        }

        #endregion cunstructor

        #region Add & Update Method

        /// <summary>
        /// Add Update View Settings
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("addupdateviewsettings")]
        public async Task<ResponseModelDTO> AddFavoriteMenu(ViewSettingDTO viewSettingDTO, CancellationToken token = default(CancellationToken)) {
            ResponseModelDTO responseModelDTO = await _viewSettingsDS.AddUpdateViewSettings(viewSettingDTO, token);
            return responseModelDTO;

        }

        #endregion Add & Update Method

        #region Get method

        /// <summary>
        /// Get view settings 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getviewsettings/{screenkey}/{appkey}")]
        public async Task<List<ViewSettingDTO>> GetViewSettingsListAsync([FromRoute] string screenkey, [FromRoute] string appkey, CancellationToken token = default(CancellationToken)) {
            return await _viewSettingsDS.GetViewSettingsListAsync(screenkey, appkey, token);
        }

        #endregion Get method

        #region Delete method

        /// <summary>
        /// delete view settings
        /// </summary>
        /// <param name="viewSettingDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("deleteviewsettings/{id}")]
        public async Task<ResponseModelDTO> DeleteViewSettings([FromRoute] Guid id,CancellationToken token = default(CancellationToken)) {
            ResponseModelDTO responseModelDTO = await _viewSettingsDS.DeleteViewSettings(id, token);
            return responseModelDTO;

        }

        #endregion Delete method
    }
}
