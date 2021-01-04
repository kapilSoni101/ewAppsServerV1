using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.Payment.DS;
using ewApps.Payment.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.Payment.WebApi {

    /// <summary>
    /// Get preferences list
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentPreferenceController:ControllerBase {

        #region Local variables
        ITenantUserAppPreferenceDS _tenantUserAppPreferenceDS;
        #endregion Local variables

        #region Constructor

        /// <summary>
        /// BusPaymentPreferenceController
        /// </summary>
        /// <param name="tenantUserAppPreferenceDS"></param>
        public PaymentPreferenceController(ITenantUserAppPreferenceDS tenantUserAppPreferenceDS) {
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
        [Route("getbuspaypreference/{appid}")]
        public async Task<PreferenceViewDTO> GetBusPayPreferenceListAsync([FromRoute] Guid appid) {
            return await _tenantUserAppPreferenceDS.GetBusPayPreferenceListAsync(appid);
        }


        /// <summary>
        /// Get Customer Protal payment preferences list
        /// <paramref name="appid"></paramref>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getcustpaypreference/{appid}")]
        public async Task<PreferenceViewDTO> GetCustPayPreferenceListAsync([FromRoute] Guid appid) {
            return await _tenantUserAppPreferenceDS.GetCustPayPreferenceListAsync(appid);
        }

        #endregion Get

        #region Put Preference

        /// <summary>
        /// Update business portal payment app preferece list
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updatebuspaypreference")]
        public async Task UpdateBusPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO) {
            await _tenantUserAppPreferenceDS.UpdateBusPayPreferenceListAsync(preferenceUpdateDTO);
        }

        /// <summary>
        /// Update customer potal payment app preferece list
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updatecustpaypreference")]
        public async Task UpdateCustPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO) {
            await _tenantUserAppPreferenceDS.UpdateCustPayPreferenceListAsync(preferenceUpdateDTO);
        }

        /// <summary>
        /// Update vendor potal payment app preferece list.
        /// </summary>
        [HttpPut]
        [Route("updatevendorpaypreference")]
        public async Task UpdateVendorPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO) {
            await _tenantUserAppPreferenceDS.UpdateVendorPayPreferenceListAsync(preferenceUpdateDTO);
        }

        #endregion Put


        #region Post Preference

        /// <summary>
        /// Add business portal payment app preferece list
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addbuspaypreference")]
        public async Task<bool> AddBusPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO) {
            await _tenantUserAppPreferenceDS.AddPaymentPreferenceListAsync(preferenceUpdateDTO);
            return true;
        }

        /// <summary>
        /// Add customer potal payment app preferece list
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addcustpaypreference")]
        public async Task<bool> AddCustPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO) {
            await _tenantUserAppPreferenceDS.AddPaymentPreferenceListAsync(preferenceUpdateDTO);
            return true;
        }

        /// <summary>
        /// Add customer potal payment app preferece list
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addvendpaypreference")]
        public async Task<bool> AddVendorPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO) {
            await _tenantUserAppPreferenceDS.AddPaymentPreferenceListAsync(preferenceUpdateDTO);
            return true;
        }

        #endregion Post

    }
}
