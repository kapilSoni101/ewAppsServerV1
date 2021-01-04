using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {

    /// <summary>
    /// Business class contains all add/update/delete/get methods for Business.    
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AppPortalController:ControllerBase {


        #region Local variables
        IAppPortalDS _appPortalDS;
        #endregion

        #region Constructor
        public AppPortalController(IAppPortalDS appPortalDS) {
            _appPortalDS = appPortalDS;
        }
        #endregion


        #region Get/Update shipment branding

        ///<summary>
        /// Get Ship App Portal branding detail
        /// <paramref name="appid">App Id </paramref>
        /// <paramref name="tenantid">tenant Id</paramref>
        ///</summary>
        [HttpGet]
        [Route("getshipappbranding/{tenantid}/{appid}")]
        public async Task<AppPortalBrandingDQ> GetShipAppBrandingAsync([FromRoute] Guid tenantid, [FromRoute] Guid appid) {
            return await _appPortalDS.GetShipAppBrandingAsync(tenantid, appid);
        }

        /// <summary>
        /// Update business Branding 
        /// </summary>
        /// <returns>ResponseModel</returns>
        /// <param name="appPortalBrandingDQ">Ship App model</param>
        [Route("updateshipappbranding")]
        [HttpPut]
        public async Task<ResponseModelDTO> UpdateShipAppBrandingAsync(AppPortalBrandingDQ appPortalBrandingDQ) {
            await _appPortalDS.UpdateShipAppBrandingAsync(appPortalBrandingDQ);
            return new ResponseModelDTO() {
                Id = Guid.NewGuid(),
                IsSuccess = true
            };
        }

        #endregion


        #region Get/Update pay-app branding

        ///<summary>
        /// Get Shipment App Portal branding detail
        /// <paramref name="appid">App Id </paramref>
        /// <paramref name="tenantid">tenant Id</paramref>
        ///</summary>
        [HttpGet]
        [Route("getpayappbranding/{tenantid}/{appid}")]
        public async Task<AppPortalBrandingDQ> GetPayAppBrandingAsync([FromRoute] Guid tenantid, [FromRoute] Guid appid) {
            return await _appPortalDS.GetPayAppBrandingAsync(tenantid, appid);
        }

        /// <summary>
        /// Update business Branding 
        /// </summary>
        /// <returns>ResponseModel</returns>
        /// <param name="appPortalBrandingDQ">Pay App model</param>
        [Route("updatepayappbranding")]
        [HttpPut]
        public async Task<ResponseModelDTO> UpdatePayAppBrandingAsync(AppPortalBrandingDQ appPortalBrandingDQ) {
            await _appPortalDS.UpdatePayAppBrandingAsync(appPortalBrandingDQ);
            return new ResponseModelDTO() {
                Id = Guid.NewGuid(),
                IsSuccess = true
            };
        }

        #endregion


        #region Get/Update cust-app branding

        ///<summary>
        /// Get Shipment App Portal branding detail
        /// <paramref name="appid">App Id </paramref>
        /// <paramref name="tenantid">tenant Id</paramref>
        ///</summary>
        [HttpGet]
        [Route("getcustappbranding/{tenantid}/{appid}")]
        public async Task<AppPortalBrandingDQ> GetCustAppBrandingAsync([FromRoute] Guid tenantid, [FromRoute] Guid appid) {
            return await _appPortalDS.GetCustAppBrandingAsync(tenantid, appid);
        }

        /// <summary>
        /// Update business Branding 
        /// </summary>
        /// <returns>ResponseModel</returns>
        /// <param name="appPortalBrandingDQ">Cust App model</param>
        [Route("updatecustappbranding")]
        [HttpPut]
        public async Task<ResponseModelDTO> UpdateCustAppBrandingAsync(AppPortalBrandingDQ appPortalBrandingDQ) {
            await _appPortalDS.UpdateCustAppBrandingAsync(appPortalBrandingDQ);
            return new ResponseModelDTO() {
                Id = Guid.NewGuid(),
                IsSuccess = true
            };
        }

        #endregion

        #region Get/Update vend-app branding

        ///<summary>
        /// Get Shipment App Portal branding detail
        /// <paramref name="appid">App Id </paramref>
        /// <paramref name="tenantid">tenant Id</paramref>
        ///</summary>
        [HttpGet]
        [Route("getvendappbranding/{tenantid}/{appid}")]
        public async Task<AppPortalBrandingDQ> GetVendAppBrandingAsync([FromRoute] Guid tenantid, [FromRoute] Guid appid) {
            return await _appPortalDS.GetVendAppBrandingAsync(tenantid, appid);
        }

        /// <summary>
        /// Update business Branding 
        /// </summary>
        /// <returns>ResponseModel</returns>
        /// <param name="appPortalBrandingDQ">Cust App model</param>
        [Route("updatevendappbranding")]
        [HttpPut]
        public async Task<ResponseModelDTO> UpdateVendAppBrandingAsync(AppPortalBrandingDQ appPortalBrandingDQ) {
            await _appPortalDS.UpdateVendAppBrandingAsync(appPortalBrandingDQ);
            return new ResponseModelDTO() {
                Id = Guid.NewGuid(),
                IsSuccess = true
            };
        }

        #endregion


        #region Get ThemeDetail
        ///<summary>
        /// Get ThemeList
        ///</summary>
        [HttpGet]
        [Route("getthemenameandthemekey")]
        public async Task<IEnumerable<ThemeResponseDTO>> GetThemeNameAndThemeKey() {
            return await _appPortalDS.GetThemeNameAndThemeKey();
        }
        #endregion

    }
}
