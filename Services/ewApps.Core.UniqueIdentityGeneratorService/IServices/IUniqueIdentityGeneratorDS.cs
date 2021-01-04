/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agrawal <sagrawal@eworkplaceapps.com>
 * Date: 16 November 2018
 * 
 * Contributor/s: Sourabh Agrawal
 * Last Updated On: 09 August 2019
 */

using ewApps.Core.BaseService;
using System;

namespace ewApps.Core.UniqueIdentityGeneratorService {

  /// <summary>
  ///  Methods to implement business logic for Identity
  /// </summary>
  public interface IUniqueIdentityGeneratorDS : IBaseDS<UniqueIdentityGenerator> {

    /// <summary>
    /// Get new identityno basis of tenantid,entitytype,prefixstring
    /// </summary>
    /// <param name="TenantId">Tenantid for entity</param>
    /// <param name="EntityType">Entity tyoe</param>
    /// <param name="PrefixString">prefix string</param>
    /// <param name="identityNumber">Identity number</param>
    /// <returns> new identityNumber </returns>
    int GetIdentityNo(Guid TenantId, int EntityType, string PrefixString, int identityNumber);
  }
}
