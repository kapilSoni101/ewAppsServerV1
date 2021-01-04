/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.QData;
using ewApps.Core.BaseService;
using ewApps.Core.NotificationService;

namespace ewApps.AppPortal.DS {

    public class PublisherNotificationRecipientDS:IPublisherNotificationRecipientDS {

        #region Local Member

        IQPublisherNotificationRecipientRepository _notificationRecipientRepository;
        IPlatformNotificationRecipientDS _platformNotificationRecipientDataService;

        #endregion

        #region Constructor 

        public PublisherNotificationRecipientDS(IQPublisherNotificationRecipientRepository repository, IPlatformNotificationRecipientDS platformNotificationRecipientDataService) {
            _notificationRecipientRepository = repository;
            _platformNotificationRecipientDataService = platformNotificationRecipientDataService;
        }

        #endregion

        #region public methods 

        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedUser(Guid appId, Guid tenantId, Guid appUserId) {
            return _notificationRecipientRepository.GetInvitedUser(appId, tenantId, appUserId);
        }

        public List<NotificationRecipient> GetForgotPasswordPublisherUser(Guid tenantId, Guid tenantUserId) {
            return _notificationRecipientRepository.GetForgotPasswordPublisherUser(tenantId, tenantUserId);
        }     

        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedUserWithPrefrence(Guid tenantId, Guid tenantUserId, long emailPreference, int userType, int userStatus) {
            return _notificationRecipientRepository.GetInvitedUserWithPrefrence(tenantId, tenantUserId, emailPreference, userType, userStatus);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetPlatformUsersWithApplicationPermissionAndPrefrence(Guid tenantId, long emailPrefence) {
            return _platformNotificationRecipientDataService.GetPlatformUsersWithApplicationPermissionAndPreference(tenantId, emailPrefence);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetBusinessUserWithApplicationAccess(Guid tenantId, Guid appId) {
            return _notificationRecipientRepository.GetBusinessUserWithApplicationAccess(tenantId, appId);
        }

        public List<NotificationRecipient> GetBusinessUserWithApplicationAccessWithoutStatus(Guid tenantId, Guid appId) {
            return _notificationRecipientRepository.GetBusinessUserWithApplicationAccessWithoutStatus(tenantId, appId);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetAllBusinessUsers(Guid tenantId) {
            return _notificationRecipientRepository.GetAllBusinessUsers(tenantId);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetAllBusinessPartnerUsers(Guid tenantId, Guid businessPartnerTenantId) {
            return _notificationRecipientRepository.GetAllBusinessPartnerUsers(tenantId, businessPartnerTenantId);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetBusinessPartnerUserWithApplicationAccess(Guid businessTenantId, Guid businessPartnerTenantId, Guid appId) {
            return _notificationRecipientRepository.GetBusinessPartnerUserWithApplicationAccess(businessTenantId, businessPartnerTenantId, appId);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetPublisherUserWithApplicationPermission(Guid tenantId, Guid appId) {
            return _notificationRecipientRepository.GetPublisherUserWithPermissionAndPrefrence(tenantId, appId, (long)PublisherUserPublisherAppPermissionEnum.ViewApplications, (long)PublisherUserPublisherAppPermissionEnum.ManageApplications, 0);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetPublisherUsersWithTenantPermissionAndPrefrence(Guid tenantId, Guid appId) {
            return _notificationRecipientRepository.GetPublisherUserWithPermissionAndPrefrence(tenantId, appId, (long)PublisherUserPublisherAppPermissionEnum.ViewBusinesses, (long)PublisherUserPublisherAppPermissionEnum.ManageBusinesses, (long)PublisherEmailPreferenceEnum.TenantActiveInactive);
        }

        public List<NotificationRecipient> GetAllPlatformUsersWithPreference() {
            return _notificationRecipientRepository.GetAllPlatformUsersWithPreference();
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetPublisherSupportUsers() {
            return _notificationRecipientRepository.GetPublisherSupportUsers();
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetPublisherSupportUsersWithTicketStatusPreference() {
            return _notificationRecipientRepository.GetPublisherSupportUsersWithPreference((long)PublisherEmailPreferenceEnum.TenantTicketStatusChanged);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetPublisherSupportUsersWithTicketReassingedPreference() {
            return _notificationRecipientRepository.GetPublisherSupportUsersWithPreference((long)PublisherEmailPreferenceEnum.TenantTicketReassigned);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetPublisherSupportUsersWithTicketPriorityChangePreference() {
            return _notificationRecipientRepository.GetPublisherSupportUsersWithPreference((long)PublisherEmailPreferenceEnum.TenantTicketPriorityChanged);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetPublisherSupportUsersWithCommentAddedToTicketPreference() {
            return _notificationRecipientRepository.GetPublisherSupportUsersWithPreference((long)PublisherEmailPreferenceEnum.TenantTicketNewCommentAdded);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetPublisherSupportUsersWithAttcahmentAddedToTicketPreference() {
            return _notificationRecipientRepository.GetPublisherSupportUsersWithPreference((long)PublisherEmailPreferenceEnum.TenantTicketAttachmentAdded);
        }

        #endregion
    }
}

