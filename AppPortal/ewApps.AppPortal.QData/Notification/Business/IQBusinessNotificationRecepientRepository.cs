using System;
using System.Collections.Generic;
using ewApps.Core.NotificationService;

namespace ewApps.AppPortal.QData {

    public interface IQBusinessNotificationRecepientRepository {


        List<NotificationRecipient> GetInvitedUserRecipientList(Guid appId, Guid tenantId, Guid appUserId);

        /// <summary>
        /// Get the invited business users where is is of businesss type.
        /// </summary>
        /// <param name="tenantId">tenant identifier for the user</param>
        /// <param name="tenantUserId"> tenant user identitfier for the tenant user</param>
        /// <param name="appId">application id for the user to which user belongs</param>
        /// <returns></returns>
        List<NotificationRecipient> GetInvitedBusinessUser(Guid tenantId, Guid tenantUserId, Guid appId);

        /// <summary>
        /// Get the invited business deleted users where is is of businesssPartner type.
        /// </summary>
        /// <param name="tenantId">tenant identifier for the user</param>
        /// <param name="tenantUserId"> tenant user identitfier for the tenant user</param>
        /// <param name="appId">application id for the user to which user belongs</param>
        /// <returns></returns>
        List<NotificationRecipient> GetInvitedDeletedBusinessUser(Guid tenantId, Guid tenantUserId, Guid appId);

        /// <summary>
        /// Get the invited business partner users where is is of businesssPartner type.
        /// </summary>
        /// <param name="tenantId">tenant identifier for the user</param>
        /// <param name="tenantUserId"> tenant user identitfier for the tenant user</param>
        /// <param name="appId">application id for the user to which user belongs</param>
        /// <returns></returns>
        List<NotificationRecipient> GetInvitedBusinessPartnerUser(Guid tenantId, Guid tenantUserId, Guid appId);

        /// <summary>
        /// Get the invited business partner deleted users where is is of businesssPartner type.
        /// </summary>
        /// <param name="tenantId">tenant identifier for the user</param>
        /// <param name="tenantUserId"> tenant user identitfier for the tenant user</param>
        /// <param name="appId">application id for the user to which user belongs</param>
        /// <returns></returns>
        List<NotificationRecipient> GetInvitedBusinessPartnerDeletedUser(Guid tenantId, Guid tenantUserId, Guid appId);

        /// <summary>
        /// Get all the publisher users with given permissons  and preference.
        /// </summary>
        /// <param name="tenantId">tenant identifier for the user</param>
        /// <param name="appId">application id for the user to which user belongs</param>
        /// <param name="firstPermission">permissions of the user that user should have</param>
        /// <param name="secondPermission">permissions of the user that user should have</param>
        /// <param name="eventPrefrence">prefrences that should be on to receive this mail</param>
        /// <returns></returns>
        List<NotificationRecipient> GetPublisherUserWithPermissionAndPrefrence(Guid tenantId, Guid appId, long firstPermission, long secondPermission, long eventPrefrence);

        /// <summary>
        /// Get all the business user of the particular tennat.
        /// </summary>
        /// <param name="tenantId">tenant identifier</param>
        /// <param name="appId">business application id to be passed.</param>
        /// <returns></returns>
        List<NotificationRecipient> GetAllBusinessUser(Guid tenantId, Guid appId);

        /// <summary>
        /// Get all the business user of the particular tennat and having prefeence flag.
        /// </summary>
        /// <param name="tenantId">tenant identifier</param>
        /// <param name="appId">business application id to be passed.</param>
        /// <param name="emailPreference">user prefence .</param>
        /// <returns></returns>
        List<NotificationRecipient> GetAllBusinessUserWithPrefrence(Guid tenantId, Guid appId, long emailPreference);

        /// <summary>
        /// Get all the business  user having particular permission(either first permission or having second permission) and a perticular notification perefence on.
        /// </summary>
        /// <param name="tenantId">tenant identifier for the tenantuser</param>
        /// <param name="appId">application identifier for the tenantuser</param>
        /// <param name="emailPreference">emeail preferece bitmask</param>
        /// <param name="smsPreference">sms preferece bitmask</param>
        /// <param name="firstpermission">first permission permission bitmask</param>
        /// <param name="secondPermission">sesond permission bitmask</param>
        /// <returns></returns>
        List<NotificationRecipient> GetAllBusinessUserWithPermissionAndPrefrence(Guid tenantId, Guid appId, long emailPreference, long smsPreference, long firstpermission, long secondPermission);

        /// <summary>
        /// Get all the business partner user having particular permission(either first permission or having second permission) and a perticular notification perefence on.
        /// </summary>
        /// <param name="tenantId">tenant identifier for the tenantuser</param>
        /// <param name="appId">application identifier for the tenantuser</param>
        /// <param name="businessPastnerTenantId">business partner tenant identifier to identify to wich customer the partner belongs</param>
        /// <param name="emailPreference">emeail preferece bitmask</param>
        /// <param name="smsPreference">sms preferece bitmask</param>
        /// <param name="firstpermission">first permission permission bitmask</param>
        /// <param name="secondPermission">sesond permission bitmask</param>
        /// <returns></returns>
        List<NotificationRecipient> GetAllBusinessPartnerUserWithPermissionAndPrefrence(Guid tenantId, Guid appId, Guid businessPastnerTenantId, long emailPreference, long smsPreference, long firstpermission, long secondPermission);

        List<NotificationRecipient> GetForgotPasswordBusinessUser(Guid tenantId, Guid tenantUserId);
    }
}