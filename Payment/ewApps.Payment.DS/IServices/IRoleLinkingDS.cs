/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
 
using ewApps.Payment.Entity;
using ewApps.Core.BaseService;
using System.Threading.Tasks;
using ewApps.Payment.DTO;
using System.Threading;

namespace ewApps.Payment.DS {

    /// <summary>
    /// Responsible for exposing all the methods that are intrecting with the DB for retriving the data related to RoleLinking entity.
    /// </summary>
    public interface IRoleLinkingDS:IBaseDS<RoleLinking> {

        /// <summary>
        /// Adds the user rolelinking for the shipment application.
        /// </summary>
        /// <param name="roleLinkingDTO">Data trasfer object collection of the data required for the creating the rolelinkin of the user.</param>
        /// <param name="cancellationToken">cacelllation token for the therading.</param>
        /// <returns></returns>
        Task AddPaymentRolelinkingAsync(TenantUserAppManagmentDTO roleLinkingDTO, CancellationToken cancellationToken = default(CancellationToken));

        Task<RoleUpdateResponseDTO> UpdatePaymentRoleAsync(TenantUserAppManagmentDTO tenantUserAppManagmentDTO, CancellationToken cancellationToken = default(CancellationToken));
    }
}
