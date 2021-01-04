/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil nigam <anigam@eworkplaceapps.com>
 * Date: 12 Aug 2019

 */

using System;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {

    public interface IAuthenticationDS {

        /// <summary>
        /// Validates the subdomain asynchronous.
        /// </summary>
        /// <param name="subDomain">The sub domain.</param>
        /// <param name="pKey">The p key.</param>
        /// <param name="uType">Type of the user.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<LoginBrandingDTO> ValidateSubdomainAsync(string subDomain, string pKey, int uType, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the token information by token identifier asynchronous.
        /// </summary>
        /// <param name="tokenId">The token identifier.</param>
        /// <param name="pKey">The p key.</param>
        /// <param name="IdentityAppClientId">The identity application client identifier.</param>
        /// <param name="uType">Type of the user.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<TokenDataDTO> GetTokenInfoByTokenIdAsync(Guid tokenId, string pKey, string IdentityAppClientId, int uType, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the token information by subdomain asynchronous.
        /// </summary>
        /// <param name="subDomain">The sub domain.</param>
        /// <param name="pKey">The p key.</param>
        /// <param name="IdentityAppClientId">The identity application client identifier.</param>
        /// <param name="uType">Type of the user.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<TokenDataDTO> GetTokenInfoBySubdomainAsync(string subDomain, string pKey, string IdentityAppClientId, int uType, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Check the given token present in the DB.
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        Task<bool> CheckTokenAsync(Guid tokenId);

        /// <summary>
        /// Deleted tokeinfo 
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="tokenType"></param>
        /// <param name="identityUserId"></param>
        /// <param name="appKey"></param>
        /// <returns></returns>
        Task DeleteTokenInfoAsync(TokenInfoDTO tokenInfoDTO);
    }
}
