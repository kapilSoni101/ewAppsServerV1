/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
namespace ewApps.Core.BaseService {
    /// <summary>
    /// This enum defines system fields in form of mask enum.
    /// </summary>
    [System.Flags]
    public enum SystemFieldMask {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,


        /// <summary>
        /// The identifier
        /// </summary>
        Id = 1,
        CreatedBy = 2,
        CreatedOn = 4,
        UpdatedBy = 8,
        UpdatedOn = 16,
        TenantId = 32,
        Deleted = 64,

        AddOpSystemFields = Id | CreatedBy | CreatedOn | UpdatedBy | UpdatedOn | TenantId,
        UpdateOpSystemFields = UpdatedOn | UpdatedBy,
        DeleteOpSystemFields = UpdatedOn | UpdatedBy | Deleted,

        All = Id | CreatedBy | CreatedOn | UpdatedBy | UpdatedOn | TenantId | Deleted
    }
}
