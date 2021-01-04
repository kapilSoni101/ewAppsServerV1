using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DS;
using ewApps.BusinessEntity.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.BusinessEntity.WebApi {
    /// <summary>
    /// Get the preferences list
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class BEPreferenceController:ControllerBase {
        #region Local variables
        ITenantUserAppPreferenceDS _tenantUserAppPreferenceDS;
        #endregion Local variables

        #region Constructor

        /// <summary>
        /// BusPaymentPreferenceController
        /// </summary>
        /// <param name="tenantUserAppPreferenceDS"></param>
        public BEPreferenceController(ITenantUserAppPreferenceDS tenantUserAppPreferenceDS) {
            _tenantUserAppPreferenceDS = tenantUserAppPreferenceDS;
        }

        #endregion Constructor

        #region Get Preference

        /// <summary>
        /// Get Business Protal payment preferences list
        /// <paramref name="appid"></paramref>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getbusbepreference/{appid}")]
        public async Task<PreferenceViewDTO> GetBusBEPreferenceListAsync([FromRoute] Guid appid) {
            return await _tenantUserAppPreferenceDS.GetUserPreferenceListByAppIdAsync(appid);
        }


        /// <summary>
        /// Get Customer Protal payment preferences list
        /// <paramref name="appid"></paramref>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getcustbepreference/{appid}")]
        public async Task<PreferenceViewDTO> GetCustBEPreferenceListAsync([FromRoute] Guid appid) {
            return await _tenantUserAppPreferenceDS.GetUserPreferenceListByAppIdAsync(appid);
        }

        /// <summary>
        /// Get Customer Protal payment preferences list
        /// <paramref name="appid"></paramref>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getvendorbepreference/{appid}")]
        public async Task<PreferenceViewDTO> GetVendorBEPreferenceListAsync([FromRoute] Guid appid) {
            return await _tenantUserAppPreferenceDS.GetUserPreferenceListByAppIdAsync(appid);
        }

        #endregion Get

        #region Put Preference

        /// <summary>
        /// Update business portal payment app preferece list
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updatebusbepreference")]
        public async Task UpdateBusBEPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO) {
            await _tenantUserAppPreferenceDS.UpdatePreferenceListAsync(preferenceUpdateDTO);
        }

        /// <summary>
        /// Update customer potal payment app preferece list
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updatecustbepreference")]
        public async Task UpdateCustBEPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO) {
            await _tenantUserAppPreferenceDS.UpdatePreferenceListAsync(preferenceUpdateDTO);
        }

        /// <summary>
        /// Update customer potal payment app preferece list
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updatevendorbepreference")]
        public async Task UpdateVendorBEPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO) {
            await _tenantUserAppPreferenceDS.UpdatePreferenceListAsync(preferenceUpdateDTO);
        }

        #endregion Put

        #region Post Preference

        /// <summary>
        /// Add business portal payment app preferece list
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addbusbepreference")]
        public async Task<bool> AddBusBEPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO) {
            await _tenantUserAppPreferenceDS.AddBEAppPreferenceListAsync(preferenceUpdateDTO);
            return true;
        }

        /// <summary>
        /// Add customer potal payment app preferece list
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addcustbepreference")]
        public async Task<bool> AddCustBEPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO) {
            await _tenantUserAppPreferenceDS.AddBEAppPreferenceListAsync(preferenceUpdateDTO);
            return true;
        }

        [HttpPost]
        [Route("addvendorbepreference")]
        public async Task<bool> AddVendorBEPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO) {
            await _tenantUserAppPreferenceDS.AddBEAppPreferenceListAsync(preferenceUpdateDTO);
            return true;
        }

        #endregion Post
    }
}