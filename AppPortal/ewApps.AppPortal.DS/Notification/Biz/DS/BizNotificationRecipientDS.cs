using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.QData;
using ewApps.Core.BaseService;
using ewApps.Core.NotificationService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
    public class BizNotificationRecipientDS:IBizNotificationRecipientDS {

        IQBizNotificationRecipientData _qBizNotificationRecipientData;
        AppPortalAppSettings _appPortalAppSettings;
        IUserSessionManager _userSessionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="BizNotificationRecipientDS"/> class.
        /// </summary>
        /// <param name="qBizPaymentNotificationRecipientData">The q biz payment notification recipient data.</param>
        public BizNotificationRecipientDS(IQBizNotificationRecipientData qBizPaymentNotificationRecipientData, IOptions<AppPortalAppSettings> appPortalAppSettings, IUserSessionManager userSessionManager) {
            _qBizNotificationRecipientData = qBizPaymentNotificationRecipientData;
            _userSessionManager = userSessionManager;
            _appPortalAppSettings = appPortalAppSettings.Value;
        }

        public List<NotificationRecipient> GetBizUserAppOnBoardRecipients(Guid appId, Guid businessTenantId, Guid onboardedUserId, long preferenceValue) {
            return _qBizNotificationRecipientData.GetBizUserAppOnBoardRecipients(appId, businessTenantId, onboardedUserId, preferenceValue);
        }

        /// <inheritdoc/>
        public List<NotificationRecipient> GetBizPaymentUserOnBoardRecipients(Guid appId, Guid businessTenantId, Guid onboardedUserId) {
            return _qBizNotificationRecipientData.GetBizPaymentUserOnBoardRecipients(appId, businessTenantId, onboardedUserId);
        }

        /// <inheritdoc/>
        public List<NotificationRecipient> GetBizCustUserOnBoardRecipients(Guid appId, Guid businessTenantId, Guid onboardedUserId, long preferenceValue) {
            return _qBizNotificationRecipientData.GetBizCustUserOnBoardRecipients(appId, businessTenantId, onboardedUserId, preferenceValue);
        }

        /// <inheritdoc/>
        public List<NotificationRecipient> GetBizSetupUserOnBoardRecipients(Guid appId, Guid businessTenantId, Guid onboardedUserId) {
            return _qBizNotificationRecipientData.GetBizSetupUserOnBoardRecipients(appId, businessTenantId, onboardedUserId);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetAppUserOnBusiness(Guid tenantId, Guid tenantUserId, Guid appId) {
            return _qBizNotificationRecipientData.GetAppUserOnBusiness(tenantId, tenantUserId, appId);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetAppUserPermissionOnBusiness(Guid tenantId, Guid tenantUserId, Guid appId, int userType, int userStatus) {
            
            //Get AppInfo
            List<AppInfoDTO> appInfoDTOs = GetAppInfoByTenantId(tenantId);
            long bizPreferrenceEnumValue = 0;
            AppInfoDTO AppInfo = appInfoDTOs.FirstOrDefault(i => i.Id == appId);
            if(AppInfo.AppKey == AppKeyEnum.pay.ToString()) {
                bizPreferrenceEnumValue = (int)BusinessUserPaymentAppPreferenceEnum.MyPermissionUpdated;
            }            
            else if(AppInfo.AppKey == AppKeyEnum.cust.ToString()) {
                bizPreferrenceEnumValue = (int)BusinessUserCustomerAppPreferenceEnum.MyPermissionUpdated;
            }
            else if(AppInfo.AppKey == AppKeyEnum.biz.ToString()) {
                bizPreferrenceEnumValue = (int)BusinessUserBusinessSetupAppPreferenceEnum.MyPermissionUpdated;
            }
            //else if(AppInfo.AppKey == AppKeyEnum.ship.ToString()) {
            //    bizPreferrenceEnumValue = (int)BusinessUserShipemntAppPreferenceEnum.MyPermissionUpdated;
            //}

            return _qBizNotificationRecipientData.GetAppUserPermissionOnBusiness(tenantId, tenantUserId, appId, bizPreferrenceEnumValue, userType, userStatus);
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetBusinessUsersForNotes(Guid tenantId, Guid tenantUserId, Guid appId, Guid entityId, int userType, int userStatus) {
            UserSession userSession = _userSessionManager.GetSession();
            List<NotificationRecipient> notificationRecipients = new List<NotificationRecipient>();



            //Get AppInfo
            List<AppInfoDTO> appInfoDTOs = GetAppInfoByTenantId(tenantId);

            AppInfoDTO paymentAppInfo = appInfoDTOs.FirstOrDefault(i => i.AppKey == AppKeyEnum.pay.ToString());
            if(paymentAppInfo != null) {
                long paymentUserpermission = (int)BusinessUserPaymentAppPermissionEnum.ViewInvoices | (int)BusinessUserPaymentAppPermissionEnum.ManageInvoices;
                notificationRecipients = _qBizNotificationRecipientData.GetBusinessUsersForNotes(tenantId, tenantUserId, paymentAppInfo.Id, userType, userStatus, paymentUserpermission, (int)BusinessUserPaymentAppPreferenceEnum.NewNotesAdded);
            }

            AppInfoDTO customerAppInfo = appInfoDTOs.FirstOrDefault(i => i.AppKey == AppKeyEnum.cust.ToString());
            if(customerAppInfo != null) {
                long customerUserpermission = (int)BusinessUserCustomerAppPermissionEnum.ViewARInvoices | (int)BusinessUserCustomerAppPermissionEnum.ManageARInvoices;
                notificationRecipients.AddRange(_qBizNotificationRecipientData.GetBusinessCustomerUsersForNotes(tenantId, tenantUserId, customerAppInfo.Id, userType, userStatus, customerUserpermission, (int)BusinessUserCustomerAppPreferenceEnum.NewNotesAdded));
            }

            // check if recipient exist then make first record 
            ILookup<string, NotificationRecipient> groupedNotificationRecipients = notificationRecipients.ToLookup(i => i.Email);

            foreach(var loopupResult in groupedNotificationRecipients) {
                int indexer = 0;

                bool emailPref = loopupResult.Any(i => i.EmailPreference == true);
                bool smsPref = loopupResult.Any(i => i.SMSPreference == true);

                foreach(NotificationRecipient recipient in loopupResult) {

                    if(emailPref == true && indexer == 0) {
                        recipient.EmailPreference = true;
                    }
                    else {
                        recipient.EmailPreference = false;
                    }

                    if(smsPref == true && indexer == 0) {
                        recipient.SMSPreference = true;
                    }
                    else {
                        recipient.SMSPreference = false;
                    }

                    indexer++;
                }
            }

            return notificationRecipients;
        }

        public List<NotificationRecipient> GetCustomerUsersForNotes(Guid tenantId, Guid tenantUserId, Guid appId, Guid entityId, int userType, int userStatus) {
            UserSession userSession = _userSessionManager.GetSession();
            List<NotificationRecipient> notificationRecipients = new List<NotificationRecipient>();



            //Get AppInfo
            List<AppInfoDTO> appInfoDTOs = GetAppInfoByTenantId(tenantId);

            AppInfoDTO paymentAppInfo = appInfoDTOs.FirstOrDefault(i => i.AppKey == AppKeyEnum.pay.ToString());
            if(paymentAppInfo != null) {
                long customerPortPayAppPermission = (int)CustomerUserPaymentAppPermissionEnum.ViewInvoices;
                notificationRecipients = _qBizNotificationRecipientData.GetCustomerPayAppUsersForNotes(tenantId, entityId, paymentAppInfo.Id, (int)UserTypeEnum.Customer, userStatus, customerPortPayAppPermission, (int)CustomerUserPaymentAppPreferenceEnum.NewNotesAdded);
            }

            AppInfoDTO customerAppInfo = appInfoDTOs.FirstOrDefault(i => i.AppKey == AppKeyEnum.cust.ToString());
            if(customerAppInfo != null) {
                long customerPortCustAppPermission = (int)CustomerUserCustomerAppPermissionEnum.ViewAPInvoices;
                notificationRecipients.AddRange(_qBizNotificationRecipientData.GetCustomerUsersCustAppForNotes(tenantId, entityId, customerAppInfo.Id, (int)UserTypeEnum.Customer, userStatus, customerPortCustAppPermission, (int)CustomerUserCustomerAppPreferenceEnum.NewNotesAdded));
            }

            // check if recipient exist then make first record 
            ILookup<string, NotificationRecipient> groupedNotificationRecipients = notificationRecipients.ToLookup(i => i.Email);

            foreach(var loopupResult in groupedNotificationRecipients) {
                int indexer = 0;

                bool emailPref = loopupResult.Any(i => i.EmailPreference == true);
                bool smsPref = loopupResult.Any(i => i.SMSPreference == true);

                foreach(NotificationRecipient recipient in loopupResult) {

                    if(emailPref == true && indexer == 0) {
                        recipient.EmailPreference = true;
                    }
                    else {
                        recipient.EmailPreference = false;
                    }

                    if(smsPref == true && indexer == 0) {
                        recipient.SMSPreference = true;
                    }
                    else {
                        recipient.SMSPreference = false;
                    }

                    indexer++;
                }
            }
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetBusinessCustomerUsersForNotes(Guid tenantId, Guid tenantUserId, Guid appId, Guid entityId, int entityType, int userType, int userStatus, long paymentPermissionMask) {

            List<NotificationRecipient> notificationRecipient = new List<NotificationRecipient>();
            notificationRecipient = _qBizNotificationRecipientData.GetBusinessCustomerUsersForNotes(tenantId, tenantUserId, appId, userType, userStatus, paymentPermissionMask, (int)BusinessUserCustomerAppPreferenceEnum.NewNotesAdded);

            switch(entityType) {
                case (int)BizNotificationEventEnum.AddNoteOnSalesQuotationForBizUser:
                    notificationRecipient.AddRange(_qBizNotificationRecipientData.GetCustomerUsersCustAppSalesQuotationForNotes(tenantId, entityId, appId, (int)UserTypeEnum.Customer, (int)TenantUserInvitaionStatusEnum.Accepted, (int)CustomerUserCustomerAppPermissionEnum.ViewQuotations, (int)CustomerUserCustomerAppPreferenceEnum.NewNotesAdded));
                    break;
                case (int)BizNotificationEventEnum.AddNoteOnDeliveryForBizUser:
                    notificationRecipient.AddRange(_qBizNotificationRecipientData.GetCustomerUsersCustAppDeliveryForNotes(tenantId, entityId, appId, (int)UserTypeEnum.Customer, (int)TenantUserInvitaionStatusEnum.Accepted, (int)CustomerUserCustomerAppPermissionEnum.ViewDeliveries, (int)CustomerUserCustomerAppPreferenceEnum.NewNotesAdded));
                    break;
                case (int)BizNotificationEventEnum.AddNoteOnContractForBizUser:
                    notificationRecipient.AddRange(_qBizNotificationRecipientData.GetCustomerUsersCustAppContactForNotes(tenantId, entityId, appId, (int)UserTypeEnum.Customer, (int)TenantUserInvitaionStatusEnum.Accepted, (int)CustomerUserCustomerAppPermissionEnum.ViewContractManagement, (int)CustomerUserCustomerAppPreferenceEnum.NewNotesAdded));
                    break;
                case (int)BizNotificationEventEnum.AddNoteOnSalesOrderForBizUser:
                    notificationRecipient.AddRange(_qBizNotificationRecipientData.GetCustomerUsersCustAppSalesOrderForNotes(tenantId, entityId, appId, (int)UserTypeEnum.Customer, (int)TenantUserInvitaionStatusEnum.Accepted, (int)CustomerUserCustomerAppPermissionEnum.ViewOrders | (int)CustomerUserCustomerAppPermissionEnum.ManageOrders, (int)CustomerUserCustomerAppPreferenceEnum.NewNotesAdded));
                    break;
                case (int)BizNotificationEventEnum.AddNoteOnASNForBizUser:
                    notificationRecipient.AddRange(_qBizNotificationRecipientData.GetCustomerUsersCustAppDraftDeliveryForNotes(tenantId, entityId, appId, (int)UserTypeEnum.Customer, (int)TenantUserInvitaionStatusEnum.Accepted, (int)CustomerUserCustomerAppPermissionEnum.ViewDeliveries, (int)CustomerUserCustomerAppPreferenceEnum.NewNotesAdded));
                    break;

                default:
                    break;
            }

            return notificationRecipient;
        }


        ///<inheritdoc/>
        public List<NotificationRecipient> GetCustomerUser(Guid tenantId, Guid tenantUserId, Guid appId, int userType, int userStatus, long paymentPermissionMask) {
            List<NotificationRecipient> notificationRecipients = new List<NotificationRecipient>();

            notificationRecipients = _qBizNotificationRecipientData.GetCustomerUser(tenantId, tenantUserId, appId, userType, userStatus, (int)CustomerUserPaymentAppPermissionEnum.None, (int)CustomerUserPaymentAppPreferenceEnum.MyTicketIsUpdated);


//  code will comment due to customer not getting mail
/*
            //Get AppInfo
            List<AppInfoDTO> appInfoDTOs = GetAppInfoByTenantId(tenantId);

            AppInfoDTO paymentAppInfo = appInfoDTOs.FirstOrDefault(i => i.Id == appId);
            if(paymentAppInfo != null) {
                long paymentUserpermission = (int)BusinessUserPaymentAppPermissionEnum.ViewTickets | (int)BusinessUserPaymentAppPermissionEnum.ManageTickets;
                notificationRecipients = _qBizNotificationRecipientData.GetBusinessUsersForNotes(tenantId, tenantUserId, paymentAppInfo.Id, (int)UserTypeEnum.Business, userStatus, paymentUserpermission, (int)BusinessUserPaymentAppPreferenceEnum.NewNotesAdded);
            }

            AppInfoDTO customerAppInfo = appInfoDTOs.FirstOrDefault(i => i.Id == appId);
            if(customerAppInfo != null) {
                long customerUserpermission = (int)BusinessUserCustomerAppPermissionEnum.ViewCustomerTickets | (int)BusinessUserCustomerAppPermissionEnum.ManageCustomerTickets;
                notificationRecipients.AddRange(_qBizNotificationRecipientData.GetBusinessCustomerUsersForNotes(tenantId, tenantUserId, customerAppInfo.Id, (int)UserTypeEnum.Business, userStatus, customerUserpermission, (int)BusinessUserCustomerAppPreferenceEnum.NewNotesAdded));
            }*/

            return notificationRecipients;
        }


        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedBusinessUser(Guid tenantId, Guid tenantUserId, Guid appId) {
            return _qBizNotificationRecipientData.GetInvitedBusinessUser(tenantId, tenantUserId, appId);
        }

        private List<AppInfoDTO> GetAppInfoByTenantId(Guid tenantId) {
            return _qBizNotificationRecipientData.GetAppListByBusinessTenantIdAsync(tenantId);
        }

        public List<NotificationRecipient> GetAllPublisherUsersWithPreference(Guid tenantId) {
            return _qBizNotificationRecipientData.GetAllPublisherUsersWithPreference(tenantId);
        }

        public List<NotificationRecipient> GetAppUserAccessUpdateOnBusiness(Guid tenantId, Guid tenantUserId) {
            return _qBizNotificationRecipientData.GetAppUserAccessUpdateOnBusiness(tenantId, tenantUserId);
        }

    }
}
