/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 8 august 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 8 august 2019
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DS {

    /// <summary>
    /// Implementing all the methods for the userlinking entity.
    /// </summary>
    public class UserTenantLinkingDS:BaseDS<UserTenantLinking>, IUserTenantLinkingDS {

        #region Local members

        IUserTenantLinkingRepository _tenantUserLinkingRepository;

        #endregion Local members

        #region Constructor

        /// <summary>
        /// Initialize the local members through dependency injection.
        /// </summary>
        /// <param name="tenantUserLinkingRepository"></param>
        public UserTenantLinkingDS(IUserTenantLinkingRepository tenantUserLinkingRepository) : base(tenantUserLinkingRepository) {
            _tenantUserLinkingRepository = tenantUserLinkingRepository;
        }

        #endregion

        #region Get

        ///<inheritdoc/>
        public async Task<Guid> GetTenantPrimaryUserAsync(Guid tenantId, int userType, CancellationToken token = default(CancellationToken)) {
            UserTenantLinking userTenantLinking = await FindAsync(utl => utl.TenantId == tenantId && utl.UserType == userType
                                                                    && utl.IsPrimary == true);
            return userTenantLinking.TenantUserId;
        }

        ///<inheritdoc/>
        public async Task<bool> CheckPrimaryUserExistsForCustomer(Guid businessPartnerTenantId) {
            // Get all the user for the customer.
            List<UserTenantLinking> userTenantLinkings = (await FindAllAsync(utl => utl.BusinessPartnerTenantId == businessPartnerTenantId)).ToList();
            if(userTenantLinkings != null && userTenantLinkings.Count > 0) {
                foreach(UserTenantLinking item in userTenantLinkings) {
                    if(item.IsPrimary) {
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion Get

    }
}
