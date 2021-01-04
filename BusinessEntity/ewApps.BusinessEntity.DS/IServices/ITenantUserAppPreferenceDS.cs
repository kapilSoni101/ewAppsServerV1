/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Gaurav Katiyar <gkatiyar@eworkplaceapps.com>
 * Date: 18 December 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 18 December 2019
 */
using System;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {
    /// <summary>
    /// Responsible for exposing all the methods that are intrecting with the DB for retriving the data related to TenantUserAppPreference entity.
    /// </summary>
    public interface ITenantUserAppPreferenceDS:IBaseDS<TenantUserAppPreference> {

        Task<PreferenceViewDTO> GetUserPreferenceListByAppIdAsync(Guid appid, CancellationToken token = default(CancellationToken));

        ///// <summary>
        ///// Get business portal business entity app preference list
        ///// </summary>
        ///// <param name="appid"></param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //Task<PreferenceViewDTO> GetBusPayPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken));


        ///// <summary>
        ///// Get customer portal business entity app preference list
        ///// </summary>
        ///// <param name="appid"></param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //Task<PreferenceViewDTO> GetCustPayPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken));

        //Task<PreferenceViewDTO> GetVendorPayPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken));



        ///// <summary>
        ///// Update business portal business entity app preference list
        ///// </summary>
        ///// <param name="preferenceUpdateDTO"></param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //Task UpdateBusPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken));


        ///// <summary>
        ///// Update customer portal business entity app preference list
        ///// </summary>
        ///// <param name="preferenceUpdateDTO"></param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //Task UpdateCustPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken));

        //Task UpdateVendorPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken));


        Task UpdatePreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken));


        ///// <summary>
        ///// Add business portal business entity app preference list
        ///// </summary>
        ///// <param name="preferenceUpdateDTO"></param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //Task<bool> AddBusPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Add customer portal business entity app preference list
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> AddBEAppPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken));
    }
}
