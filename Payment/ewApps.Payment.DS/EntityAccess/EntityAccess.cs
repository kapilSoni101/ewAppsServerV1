/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
* Unauthorized copying of this file, via any medium is strictly prohibited
* Proprietary and confidential
* 
* Author: Rajesh Thakur <rtakur@eworkplaceapps.com>
* Date: 24 September 2018
* 
* Contributor/s: Nitin Jain
* Last Updated On: 10 October 2018
*/

using System;
using ewApps.Core.Common;
using ewApps.Core.UserSessionService;
using ewApps.Payment.Common;

namespace ewApps.Payment.DS {

    /// <summary>
    /// This class enables permission relatrd logic for platform entities.
    /// </summary>
    /// <typeparam name="TEntity">Platform entity class.</typeparam>
    public abstract class EntityAccess<TEntity>:IEntityAccess<TEntity> where TEntity : class, new() {

        #region  Local variables

        protected IUserSessionManager _userSessionManager;
        //protected IRoleDS _roleDS;
        //protected Role _permission;

        #endregion Local variables

        #region Constructor

        public EntityAccess(IUserSessionManager sessionManager) {
            _userSessionManager = sessionManager;
            //_roleDS = roleDS;
        }

        #endregion Constructor

        /// <summary>
        /// GEts the permission bits for given entity id.
        /// </summary>
        /// <param name="entityId">Entity id to get permission.</param>
        /// <returns>Permission list array.</returns>
        public abstract bool[] AccessList(Guid entityId);

        // Gets the permission records from database.
        protected void InitPermission() {
            //_permission = GetLoginUserPermissionSet();
        }

        protected virtual object GetLoginUserPermissionSet() {
            //return _roleDS.GetEntityByAppUserAndAppId();
            return null;
        }

        // remove this method
        public virtual bool CheckAccessForBusiness(int operation, Guid entityId) {
            UserSession session = _userSessionManager.GetSession();
            InitPermission();

            //      PaymentBusinessUserPermissionEnum bitmask = (PaymentBusinessUserPermissionEnum)_permission.PermissionBitMask;
            //return (bitmask & (PaymentBusinessUserPermissionEnum)operation) == (PaymentBusinessUserPermissionEnum)operation;
            return true;
        }

        // remove this method
        public virtual bool CheckAccessForPartner(int operation, Guid entityId) {
            UserSession session = _userSessionManager.GetSession();
            InitPermission();

            //      PaymentBusinessPartnerUserPermissionEnum bitmask = (PaymentBusinessPartnerUserPermissionEnum)_permission.PermissionBitMask;
            //return (bitmask & (PaymentBusinessPartnerUserPermissionEnum)operation) == (PaymentBusinessPartnerUserPermissionEnum)operation;

            return true;
        }

    }
}