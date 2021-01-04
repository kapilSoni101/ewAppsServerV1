/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
* Unauthorized copying of this file, via any medium is strictly prohibited
* Proprietary and confidential
* 
* Author: Rajesh Thakur <rthakur@eworkplaceapps.com>
* Date: 24 September 2018
* 
* Contributor/s: Nitin Jain
* Last Updated On: 10 October 2018
*/

using ewApps.Core.BaseService;
using ewApps.Core.Common;
using ewApps.Core.UserSessionService;
using ewApps.Payment.Common;
using System;

namespace ewApps.Payment.DS {

    /// <summary>
    /// Thisclasss implements permission related operations for payment entity.
    /// </summary>
    public class PaymentAccess:EntityAccess<ewApps.Payment.Entity.Payment>, IPaymentAccess {

        public PaymentAccess(IUserSessionManager sessionManager) : base(sessionManager) {
        }

        public override bool[] AccessList(Guid entityId) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Provides permisison array for logged in user;
        /// </summary>
        /// <returns>permission array of boolean values</returns>
        public bool[] AccessList() {
            bool[] accessVector = new bool[2];
            //accessVector[(int)BusinessPermissionEnum.ViewInvoices] = CheckAccess<BusinessPermissionEnum>((int)BusinessPermissionEnum.ViewInvoices, Guid.Empty);
            //accessVector[(int)BusinessPermissionEnum.ManageInvoices] = CheckAccess<BusinessPermissionEnum>((int)BusinessPermissionEnum.ManageInvoices, Guid.Empty);

            return accessVector;
        }

        public override bool CheckAccessForBusiness(int operation, Guid entityId) {
            bool hasPermission = false;
            UserSession session = _userSessionManager.GetSession();
            InitPermission();

            // Get the permission bit mask.
            //      PaymentBusinessUserPermissionEnum bitmask = (PaymentBusinessUserPermissionEnum)_permission.PermissionBitMask;

            //switch(operation) {

            //  case (int)OperationType.Add:
            //  case (int)OperationType.Update:
            //  case (int)OperationType.Delete:
            //    hasPermission = (bitmask & PaymentBusinessUserPermissionEnum.ManageInvoices) == PaymentBusinessUserPermissionEnum.ManageInvoices;
            //    break;
            //}

            //return hasPermission;

            return true;
        }

        public override bool CheckAccessForPartner(int operation, Guid entityId) {
            bool hasPermission = false;
            UserSession session = _userSessionManager.GetSession();
            InitPermission();

            // Get the permission bit mask.
            //      PaymentBusinessPartnerUserPermissionEnum bitmask = (PaymentBusinessPartnerUserPermissionEnum)_permission.PermissionBitMask;

            //switch(operation) {

            //  case (int)OperationType.Add:
            //  case (int)OperationType.Update:
            //  case (int)OperationType.Delete:
            //    hasPermission = (bitmask & PaymentBusinessPartnerUserPermissionEnum.ManageInvoices) == PaymentBusinessPartnerUserPermissionEnum.ManageInvoices;
            //    break;
            //}

            //return hasPermission;

            return true;
        }
    }
}
