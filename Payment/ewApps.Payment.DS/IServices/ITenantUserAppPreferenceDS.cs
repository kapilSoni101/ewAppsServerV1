/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar<abadgujar@batchmaster.com>
 * Date: 5 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 5 September 2019
 */

using System;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Payment.DTO;
using ewApps.Payment.Entity;

namespace ewApps.Payment.DS {
    public interface ITenantUserAppPreferenceDS : IBaseDS<TenantUserAppPreference> {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleLinkingAndPreferneceDTO"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        Task AddTenantUserAppPreferncesAsync(TenantUserAppManagmentDTO roleLinkingAndPreferneceDTO, int userType);

        /// <summary>
        /// Get business portal payment app preference list
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<PreferenceViewDTO> GetBusPayPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get customer portal payment app preference list
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<PreferenceViewDTO> GetCustPayPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Update business portal payment app preference list
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task UpdateBusPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Update customer portal payment app preference list.
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task UpdateCustPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Update vendor portal payment app preference list.
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task UpdateVendorPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken));


        ///// <summary>
        ///// Add business portal payment app preference list
        ///// </summary>
        ///// <param name="preferenceUpdateDTO"></param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //Task<bool> AddBusPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Add customer portal payment app preference list
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> AddPaymentPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken));

    }
}
