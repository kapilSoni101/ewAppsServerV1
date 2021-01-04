using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.DTO.PreferenceDTO;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {

    /// <summary>
    /// Get preferences list
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PreferenceController:ControllerBase {

        #region Local variables

        ITenantUserAppPreferenceDS _tenantUserAppPreferenceDS;

        #endregion Local variables

        #region Constructor

        public PreferenceController(ITenantUserAppPreferenceDS tenantUserAppPreferenceDS) {
            _tenantUserAppPreferenceDS = tenantUserAppPreferenceDS;

        }

        #endregion

        #region Get Preference

        /// <summary>
        /// Get platform preferences list
        /// <paramref name="appid"></paramref>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getplatformpreference/{appid}")]
        public async Task<PreferenceViewDTO> GetPlatformPreferenceListAsync([FromRoute] Guid appid) {
            return await _tenantUserAppPreferenceDS.GetPlatformPreferenceListAsync(appid);
        }

        /// <summary>
        /// Get publisher preferences list
        /// <paramref name="appid"></paramref>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getpublisherpreference/{appid}")]
        public async Task<PreferenceViewDTO> GetPublisherPreferenceListAsync([FromRoute] Guid appid) {
            return await _tenantUserAppPreferenceDS.GetPublisherPreferenceListAsync(appid);
        }

        #region Business User Preference
        /// <summary>
        /// Get business setup preferences list
        /// <paramref name="appid"></paramref>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getbussetuppreference/{appid}")]
        public async Task<PreferenceViewDTO> GetBusSetupPreferenceListAsync([FromRoute] Guid appid) {
            //return await _tenantUserAppPreferenceDS.GetBusSetupPreferenceListAsync(appid);
            return await _tenantUserAppPreferenceDS.GetBusinessPreferenceListByAppIdAsync(appid);
        }

        /// <summary>
        /// Get business customer preferences list
        /// <paramref name="appid"></paramref>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getbuscustpreference/{appid}")]
        public async Task<PreferenceViewDTO> GetBusCustPreferenceListAsync([FromRoute] Guid appid) {
            //return await _tenantUserAppPreferenceDS.GetBusCustPreferenceListAsync(appid);
            return await _tenantUserAppPreferenceDS.GetBusinessPreferenceListByAppIdAsync(appid);
        }

        /// <summary>
        /// Get business payment preferences list
        /// <paramref name="appid"></paramref>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getbuspaypreference/{appid}")]
        public async Task<PreferenceViewDTO> GetBusPayPreferenceListAsync([FromRoute] Guid appid) {
            return await _tenantUserAppPreferenceDS.GetBusPayPreferenceListAsync(appid);
        }

        [HttpGet]
        [Route("getbusvendorpreference/{appid}")]
        public async Task<PreferenceViewDTO> GetBusVendorPreferenceListAsync([FromRoute] Guid appid) {
            //return await _tenantUserAppPreferenceDS.GetBusVendorPreferenceListAsync(appid);
            return await _tenantUserAppPreferenceDS.GetBusinessPreferenceListByAppIdAsync(appid);
        }
        #endregion

        #region Customer User Preference
        /// <summary>
        /// Get customer setup preferences list
        /// <paramref name="appid"></paramref>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getcustsetuppreference/{appid}")]
        public async Task<PreferenceViewDTO> GetCustSetupPreferenceListAsync([FromRoute] Guid appid) {
            return await _tenantUserAppPreferenceDS.GetCustSetupPreferenceListAsync(appid);
        }


        /// <summary>
        /// Get customer Cust App preferences list
        /// <paramref name="appid"></paramref>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getcustcustapppreference/{appid}")]
        public async Task<PreferenceViewDTO> GetCustCustAppPreferenceListAsync([FromRoute] Guid appid) {
            return await _tenantUserAppPreferenceDS.GetCustCustAppPreferenceListAsync(appid);
        }


        /// <summary>
        /// Get customer Cust App preferences list
        /// <paramref name="appid"></paramref>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getcustpayapppreference/{appid}")]
        public async Task<PreferenceViewDTO> GetCustPayAppPreferenceListAsync([FromRoute] Guid appid) {
            return await _tenantUserAppPreferenceDS.GetCustPayAppPreferenceListAsync(appid);
        }
        #endregion

        #endregion Get

        #region Update Preference

        /// <summary> 
        /// Update platform preferences list
        /// </summary>
        [HttpPut]
        [Route("updateplatformpreference")]
        public async Task UpdatePlatformPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO) {
            await _tenantUserAppPreferenceDS.UpdatePlatformPreferenceListAsync(preferenceUpdateDTO);
        }


        /// <summary> 
        /// Update publisher preferences list
        /// </summary>
        [HttpPut]
        [Route("updatepublisherpreference")]
        public async Task UpdatePublisherPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO) {
            await _tenantUserAppPreferenceDS.UpdatePublisherPreferenceListAsync(preferenceUpdateDTO);
        }

        #region Business User Preference
        /// <summary> 
        /// Update business setup preferences list
        /// </summary>
        /// <param name = "preferenceUpdateDTO" ></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updatebussetuppreference")]
        public async Task UpdateBusSetupPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO) {
            //await _tenantUserAppPreferenceDS.UpdateBusSetupPreferenceListAsync(preferenceUpdateDTO);
            await _tenantUserAppPreferenceDS.UpdateBusinessPreferenceListAsync(preferenceUpdateDTO);
        }

        /// <summary> 
        /// Update business customer preferences list
        /// </summary>
        /// <param name = "preferenceUpdateDTO" ></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updatebuscustpreference")]
        public async Task UpdateBusCustPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO) {
            //await _tenantUserAppPreferenceDS.UpdateBusCustPreferenceListAsync(preferenceUpdateDTO);
            await _tenantUserAppPreferenceDS.UpdateBusinessPreferenceListAsync(preferenceUpdateDTO);
        }


        /// <summary> 
        /// Update business payment preferences list
        /// </summary>
        /// <param name = "preferenceUpdateDTO" ></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updatebuspaypreference")]
        public async Task UpdateBusPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO) {
            await _tenantUserAppPreferenceDS.UpdateBusPayPreferenceListAsync(preferenceUpdateDTO);
        }

        /// <summary> 
        /// Update business vendor app preferences list
        /// </summary>
        /// <param name = "preferenceUpdateDTO" ></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updatebusvendorpreference")]
        public async Task UpdateBusVendorPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO) {
            //await _tenantUserAppPreferenceDS.UpdateBusVendorPreferenceListAsync(preferenceUpdateDTO);
            await _tenantUserAppPreferenceDS.UpdateBusinessPreferenceListAsync(preferenceUpdateDTO);
        }
        #endregion

        #region Customer User Preference

        /// <summary> 
        /// Update customer customer app preferences list
        /// </summary>
        [HttpPut]
        [Route("updatecustcustpreference")]
        public async Task UpdateCustCustPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO) {
            await _tenantUserAppPreferenceDS.UpdateCustCustPreferenceListAsync(preferenceUpdateDTO);
        }


        /// <summary> 
        /// Update Customer pay app preferences list
        /// </summary>
        [HttpPut]
        [Route("updatecustpayapppreference")]
        public async Task UpdateCustPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO) {
            await _tenantUserAppPreferenceDS.UpdateCustPayPreferenceListAsync(preferenceUpdateDTO);
        }

        #endregion

        #endregion Put

    }
}
