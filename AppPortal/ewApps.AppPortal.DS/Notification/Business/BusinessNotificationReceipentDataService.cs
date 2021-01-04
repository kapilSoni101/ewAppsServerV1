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

using System;
using System.Collections.Generic;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.QData;
using ewApps.Core.BaseService;
using ewApps.Core.NotificationService;

namespace ewApps.AppPortal.DS {

    /// <summary>DataService implementing Business notification receipent service</summary>
    public class BusinessNotificationReceipentDataService:IBusinessNotificationReceipentDataService {

        #region Local member

        IQBusinessNotificationRecepientRepository _businessNotificationRecepientRepository;

        #endregion Local member

        #region Construnctor

        /// <summary>
        /// Initializing the interfaces.
        /// </summary>
        public BusinessNotificationReceipentDataService(IQBusinessNotificationRecepientRepository businessNotificationRecepientRepository) {
            _businessNotificationRecepientRepository = businessNotificationRecepientRepository;
        }

        #endregion


        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedUser(Guid appId, Guid tenantId, Guid tenantUserId) {
            return _businessNotificationRecepientRepository.GetInvitedUserRecipientList(appId, tenantId, tenantUserId);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedBusinessUser(Guid tenantId, Guid tenantUserId, Guid appId) {
            return _businessNotificationRecepientRepository.GetInvitedBusinessUser(tenantId, tenantUserId, appId);
        }

        public List<NotificationRecipient> GetForgotPasswordBusinessUser(Guid tenantId, Guid tenantUserId) {
            return _businessNotificationRecepientRepository.GetForgotPasswordBusinessUser(tenantId, tenantUserId);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedDeletedBusinessUser(Guid tenantId, Guid tenantUserId, Guid appId) {
            return _businessNotificationRecepientRepository.GetInvitedDeletedBusinessUser(tenantId, tenantUserId, appId);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedBusinessPartnerUser(Guid tenantId, Guid tenantUserId, Guid appId) {
            return _businessNotificationRecepientRepository.GetInvitedBusinessPartnerUser(tenantId, tenantUserId, appId);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedBusinessPartnerDeletedUser(Guid tenantId, Guid tenantUserId, Guid appId) {
            return _businessNotificationRecepientRepository.GetInvitedBusinessPartnerDeletedUser(tenantId, tenantUserId, appId);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetAllBusinessUser(Guid tenantId, Guid appId) {
            return _businessNotificationRecepientRepository.GetAllBusinessUser(tenantId, appId);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetAllBusinessUserWithPrefence(Guid tenantId, Guid appId, long eventId) {
            switch(eventId) {
                //case (long)BusinessNotificationEventEnum.NewBusinessUserOnBoard:
                //    return _businessNotificationRecepientRepository.GetAllBusinessUserWithPrefrence(tenantId, appId, (long)BusinessUserEmailPrefrenceEnum.NewBusinessUserOnboard);
                default:
                    return null;
            }
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetAllBusinessUserWithPermissionsAndPrefernces(Guid tenantId, Guid appId, long eventId) {
            switch(eventId) {
                //case (long)BusinessNotificationEventEnum.ApplicationRemovedForCustomerToBusinessUser:
                //    return _businessNotificationRecepientRepository.GetAllBusinessUserWithPermissionAndPrefrence(tenantId, appId, 0, 0, (long)BusinessUserBusinessSetupAppPermissionEnum.All, 0);
                //case (long)BusinessNotificationEventEnum.BusinessPartnerDeletedToBusinessUser:
                //    return _businessNotificationRecepientRepository.GetAllBusinessUserWithPermissionAndPrefrence(tenantId, appId, 0, 0, (long)BusinessUserBusinessSetupAppPermissionEnum.All, 0);
                //case (long)BusinessNotificationEventEnum.ExistingBusinessPartnerUserDeletedToBusinessUser:
                //    return _businessNotificationRecepientRepository.GetAllBusinessUserWithPermissionAndPrefrence(tenantId, appId, 0, 0, (long)BusinessUserBusinessSetupAppPermissionEnum.All, 0);

                default:
                    return null;
            }
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetPublisherUserWithPermissionAndPrefrence(Guid tenantId, Guid appId, long eventId) {
            switch(eventId) {
                //case (long)BusinessNotificationEventEnum.NewBusinessOnBoard:
                //    return _businessNotificationRecepientRepository.GetPublisherUserWithPermissionAndPrefrence(tenantId, appId, (long)PublisherUserPublisherAppPermissionEnum.ViewBusinesses, (long)PublisherUserPublisherAppPermissionEnum.ManageBusinesses, (long)PublisherEmailPreferenceEnum.NewTenantOnboard);
                default:
                    return null;
            }
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetAllBusinessPartnerUserWithPermissionAndPrefrence(Guid tenantId, Guid appId, Guid businessPastnerTenantId, long eventId) {
            switch(eventId) {
                //case (long)BusinessNotificationEventEnum.ApplicationRemovedForCustomerToBusinessPartnerUser:
                //    return _businessNotificationRecepientRepository.GetAllBusinessPartnerUserWithPermissionAndPrefrence(tenantId, appId, businessPastnerTenantId, 0, 0, 0, 0);
                default:
                    return null;

            }
        }
    }
}
