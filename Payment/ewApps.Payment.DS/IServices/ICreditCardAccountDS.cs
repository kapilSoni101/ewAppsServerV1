/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 25 February 2019
 * 
 * Contributor/s: 
 * Last Updated On: 18 September 2019
 */
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.Payment.DS {
    public interface ICreditCardAccountDS {

        /// <summary>
        /// Get credit card transection key.
        /// </summary>
        /// <param name="clientsessionid">session clientid</param>
        /// <param name="token"></param>
        /// <returns>return transectionKey</returns>
        Task<string> GetTSysTransectionKeyAsync(string clientsessionid, CancellationToken token = default(CancellationToken));

    }
}
