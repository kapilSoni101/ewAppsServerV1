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
using ewApps.AppPortal.Data;
using ewApps.AppPortal.QData;
using ewApps.Core.NotificationService;

namespace ewApps.AppPortal.DS {
    public class PlatformNotificationRecipientDS:IPlatformNotificationRecipientDS {

        #region member variable

        IQPlatformNotificationRecipientRepository _notificationRecipientRepository;

        #endregion

        #region Constructor 

        public PlatformNotificationRecipientDS(IQPlatformNotificationRecipientRepository repository) {
            _notificationRecipientRepository = repository;
        }

        #endregion

        #region public methods 

        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedPlatformUser(Guid appId, Guid tenantId, Guid appUserId) {
            return _notificationRecipientRepository.GetInvitedPlatformUser(appId, tenantId, appUserId);
        }

        public List<NotificationRecipient> GetForgotPasswordPlatformUser(Guid tenantId, Guid appUserId) {
            return _notificationRecipientRepository.GetForgotPasswordPlatformUser(tenantId, appUserId);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedPublihserUser(Guid appId, Guid tenantId, Guid appUserId) {
            return _notificationRecipientRepository.GetInvitedPublisherUser(appId, tenantId, appUserId);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedUserWithPrefrence(Guid appId, Guid tenantId, Guid appUserId, long emailPreference) {
            return _notificationRecipientRepository.GetInvitedUserWithPrefrence(appId, tenantId, appUserId, emailPreference);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetAllPublisherUsers(Guid tenantId) {
            return _notificationRecipientRepository.GetAllPublisherUsers(tenantId);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetAllPlatformUserWithPrerence(Guid tenantId, long emailPreference) {
            return _notificationRecipientRepository.GetAllPlatformUsersWithPreference(tenantId, emailPreference);
        }

        public List<NotificationRecipient> GetPlatformUsersWithApplicationPermissionAndPreference(Guid tenantId, long emailPrefrence) {
            return _notificationRecipientRepository.GetPlatformUsersWithApplicationPermissionAndPreference(tenantId, emailPrefrence);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedBusinessPrimaryUserRecipientList(Guid appId, Guid tenantId, Guid appUserId) {
            return _notificationRecipientRepository.GetInvitedPlatformUser(appId, tenantId, appUserId);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedPublisherUserRecipientList(Guid appId, Guid tenantId, Guid appUserId) {
            return _notificationRecipientRepository.GetInvitedPlatformUser(appId, tenantId, appUserId);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetPublisherSupportUsersWithTicketStatusPreference() {
            return _notificationRecipientRepository.GetPublisherSupportUsersWithPreference((long)PlatformEmailPreferenceEnum.TenantTicketStatusChanged);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetPublisherSupportUsersWithTicketReassingedPreference() {
            return _notificationRecipientRepository.GetPublisherSupportUsersWithPreference((long)PlatformEmailPreferenceEnum.TenantTicketReassigned);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetPublisherSupportUsersWithTicketPriorityChangePreference() {
            return _notificationRecipientRepository.GetPublisherSupportUsersWithPreference((long)PlatformEmailPreferenceEnum.TenantTicketPriorityChanged);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetPublisherSupportUsersWithCommentAddedToTicketPreference() {
            return _notificationRecipientRepository.GetPublisherSupportUsersWithPreference((long)PlatformEmailPreferenceEnum.TenantTicketNewCommentAdded);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetPublisherSupportUsersWithAttcahmentAddedToTicketPreference() {
            return _notificationRecipientRepository.GetPublisherSupportUsersWithPreference((long)PlatformEmailPreferenceEnum.TenantTicketAttachmentAdded);
        }

        #endregion
    }
}