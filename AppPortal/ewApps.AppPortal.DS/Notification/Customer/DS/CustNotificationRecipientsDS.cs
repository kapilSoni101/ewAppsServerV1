using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.QData;
using ewApps.Core.BaseService;
using ewApps.Core.NotificationService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.DS {
    public class CustNotificationRecipientsDS:ICustNotificationRecipientsDS {

        IQCustNotificationRecipientsData _qCustNotificationRecipientsData;
        IUserSessionManager _userSessionManager;
        

        public CustNotificationRecipientsDS(IQCustNotificationRecipientsData qCustNotificationRecipientsData, IUserSessionManager userSessionManager) {
            _qCustNotificationRecipientsData = qCustNotificationRecipientsData;
            _userSessionManager = userSessionManager;
        }

        public List<NotificationRecipient> GetCustPaymentUserOnBoardRecipients(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, Guid onboardedUserId) {
            return _qCustNotificationRecipientsData.GetCustPaymentUserOnBoardRecipients(appId, businessTenantId, businessPartnerTenantId, onboardedUserId);
        }


        public List<NotificationRecipient> GetCustCustomerUserOnBoardRecipients(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, Guid onboardedUserId) {
            return _qCustNotificationRecipientsData.GetCustCustomerUserOnBoardRecipients(appId, businessTenantId, businessPartnerTenantId, onboardedUserId);
        }

        public List<NotificationRecipient> GetCustUserAppOnBoardRecipients(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, Guid onboardedUserId) {
            return _qCustNotificationRecipientsData.GetCustUserAppOnBoardRecipients(appId, businessTenantId, businessPartnerTenantId, onboardedUserId);
        }

        public List<NotificationRecipient> GetCustomerUserOnAppDeletedRecipients(Guid businessTenantId, Guid tenantUserId, Guid appId) {
            return _qCustNotificationRecipientsData.GetCustomerUserOnAppDeletedRecipients(businessTenantId, tenantUserId, appId);
        }

        public List<NotificationRecipient> GetCustomerUserOnAppPermissionRecipients(Guid businessTenantId, Guid tenantUserId, Guid appId, int userType, int userStatus) {
            
            //Get AppInfo
            List<AppInfoDTO> appInfoDTOs = GetAppInfoByTenantId(businessTenantId);
            long custPreferrenceEnumValue = 0;
            AppInfoDTO AppInfo = appInfoDTOs.FirstOrDefault(i => i.Id == appId);
            if(AppInfo != null) {
                if(AppInfo.AppKey == AppKeyEnum.pay.ToString()) {
                    custPreferrenceEnumValue = (int)CustomerUserPaymentAppPreferenceEnum.MyPermissionUpdated;
                }
                else if(AppInfo.AppKey == AppKeyEnum.cust.ToString()) {
                    custPreferrenceEnumValue = (int)CustomerUserCustomerAppPreferenceEnum.MyPermissionUpdated;
                }
                else {
                    custPreferrenceEnumValue = 0;
                }
            }
            return _qCustNotificationRecipientsData.GetCustomerUserOnAppPermissionRecipients(businessTenantId, tenantUserId, appId, custPreferrenceEnumValue, userType, userStatus);
        }

        public List<NotificationRecipient> GetCustomerUsersForNotesNotification(Guid tenantId, Guid tenantUserId, Guid appId,  int userType, int userStatus) {
            UserSession userSession = _userSessionManager.GetSession();
            List<NotificationRecipient> notificationRecipients = new List<NotificationRecipient>();

            //Get AppInfo
            List<AppInfoDTO> appInfoDTOs = GetAppInfoByTenantId(tenantId);

            AppInfoDTO paymentAppInfo = appInfoDTOs.FirstOrDefault(i => i.AppKey == AppKeyEnum.pay.ToString());
            if(paymentAppInfo != null) {
                long customerPortPayAppPermission = (int)CustomerUserPaymentAppPermissionEnum.ViewInvoices;               
                notificationRecipients = _qCustNotificationRecipientsData.GetCustomerPayUsersForNotes(tenantId, tenantUserId, paymentAppInfo.Id, (int)UserTypeEnum.Customer, userStatus, customerPortPayAppPermission, (int)CustomerUserPaymentAppPreferenceEnum.NewNotesAdded);
            }

            AppInfoDTO customerAppInfo = appInfoDTOs.FirstOrDefault(i => i.AppKey == AppKeyEnum.cust.ToString());
            if(customerAppInfo != null) {
                long customerPortCustAppPermission = (int)CustomerUserCustomerAppPermissionEnum.ViewAPInvoices;                
                notificationRecipients.AddRange(_qCustNotificationRecipientsData.GetCustomerUsersForNotes(tenantId, tenantUserId, customerAppInfo.Id, (int)UserTypeEnum.Customer, userStatus, customerPortCustAppPermission, (int)CustomerUserCustomerAppPreferenceEnum.NewNotesAdded));
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

        public List<NotificationRecipient> GetBusinessUsersForNotesNotification(Guid tenantId, Guid tenantUserId, Guid appId,int userType, int userStatus) {
            UserSession userSession = _userSessionManager.GetSession();
            List<NotificationRecipient> notificationRecipients = new List<NotificationRecipient>();

            //Get AppInfo
            List<AppInfoDTO> appInfoDTOs = GetAppInfoByTenantId(tenantId);

            AppInfoDTO paymentAppInfo = appInfoDTOs.FirstOrDefault(i => i.AppKey == AppKeyEnum.pay.ToString());
            if(paymentAppInfo != null) {                
                long paymentUserpermission = (int)BusinessUserPaymentAppPermissionEnum.ViewInvoices | (int)BusinessUserPaymentAppPermissionEnum.ManageInvoices;
                notificationRecipients = _qCustNotificationRecipientsData.GetBusinessUsersForNotes(tenantId, paymentAppInfo.Id, userType, userStatus, paymentUserpermission, (int)BusinessUserPaymentAppPreferenceEnum.NewNotesAdded);                
            }

            AppInfoDTO customerAppInfo = appInfoDTOs.FirstOrDefault(i => i.AppKey == AppKeyEnum.cust.ToString());
            if(customerAppInfo != null) {                
                long customerUserpermission = (int)BusinessUserCustomerAppPermissionEnum.ViewARInvoices | (int)BusinessUserCustomerAppPermissionEnum.ManageARInvoices;
                notificationRecipients.AddRange(_qCustNotificationRecipientsData.GetBusinessCustomerUsersForNotes(tenantId, customerAppInfo.Id, userType, userStatus, customerUserpermission, (int)BusinessUserCustomerAppPreferenceEnum.NewNotesAdded));               
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

        public List<NotificationRecipient> GetCustomerUsersForNotes(Guid tenantId, Guid tenantUserId, Guid appId, int entityType, int userType, int userStatus, long permissionMask) {

            List<NotificationRecipient> notificationRecipient = new List<NotificationRecipient>();
            notificationRecipient = _qCustNotificationRecipientsData.GetCustomerUsersForNotes(tenantId, tenantUserId, appId, userType, userStatus, permissionMask, (int)CustomerUserCustomerAppPreferenceEnum.NewNotesAdded);

            switch(entityType) {
                case (int)CustNotificationEventEnum.AddNoteOnSalesQuotationForCustomerUser:
                    notificationRecipient.AddRange(_qCustNotificationRecipientsData.GetBusinessCustomerUsersForNotes(tenantId,  appId, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted, (int)BusinessUserCustomerAppPermissionEnum.ViewSalesQuotations | (int)BusinessUserCustomerAppPermissionEnum.ManageSalesQuotations, (int)BusinessUserCustomerAppPreferenceEnum.NewNotesAdded));
                    break;
                case (int)CustNotificationEventEnum.AddNoteOnDeliveryForCustomerUser:
                    notificationRecipient.AddRange(_qCustNotificationRecipientsData.GetBusinessCustomerUsersForNotes(tenantId,  appId, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted, (int)BusinessUserCustomerAppPermissionEnum.ViewDeliveries |(int)BusinessUserCustomerAppPermissionEnum.ManageDeliveries, (int)BusinessUserCustomerAppPreferenceEnum.NewNotesAdded));
                    break;
                case (int)CustNotificationEventEnum.AddNoteOnContractForCustomerUser:
                    notificationRecipient.AddRange(_qCustNotificationRecipientsData.GetBusinessCustomerUsersForNotes(tenantId,  appId, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted, (int)BusinessUserCustomerAppPermissionEnum.ViewContractManagement | (int)BusinessUserCustomerAppPermissionEnum.ManageContractManagement, (int)BusinessUserCustomerAppPreferenceEnum.NewNotesAdded));
                    break;
                case (int)CustNotificationEventEnum.AddNoteOnSalesOrderForCustomerUser:
                    notificationRecipient.AddRange(_qCustNotificationRecipientsData.GetBusinessCustomerUsersForNotes(tenantId,  appId, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted, (int)BusinessUserCustomerAppPermissionEnum.ViewSalesOrders | (int)BusinessUserCustomerAppPermissionEnum.ManageSalesOrders, (int)BusinessUserCustomerAppPreferenceEnum.NewNotesAdded));
                    break;
                case (int)CustNotificationEventEnum.AddNoteOnASNForCustomerUser:
                    notificationRecipient.AddRange(_qCustNotificationRecipientsData.GetBusinessCustomerUsersForNotes(tenantId,  appId, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted, (int)BusinessUserCustomerAppPermissionEnum.ViewASN | (int)BusinessUserCustomerAppPermissionEnum.ManageASN, (int)BusinessUserCustomerAppPreferenceEnum.NewNotesAdded));
                    break;

                default:
                    break;
            }

            return notificationRecipient;
        }

    public List<NotificationRecipient> GetBusinessUsersForSupportTicketNotification(Guid tenantId, Guid tenantUserId, Guid appId, int userType, int userStatus) {
      UserSession userSession = _userSessionManager.GetSession();
      List<NotificationRecipient> notificationRecipients = new List<NotificationRecipient>();

      //Get AppInfo
      List<AppInfoDTO> appInfoDTOs = GetAppInfoByTenantId(tenantId);

      AppInfoDTO paymentAppInfo = appInfoDTOs.FirstOrDefault(i => i.Id == appId);
      if(paymentAppInfo != null) {
        long paymentUserpermission = (int)BusinessUserPaymentAppPermissionEnum.ViewTickets | (int)BusinessUserPaymentAppPermissionEnum.ManageTickets;
        notificationRecipients = _qCustNotificationRecipientsData.GetBusinessUsersForNotes(tenantId, paymentAppInfo.Id, userType, userStatus, paymentUserpermission, (int)BusinessUserPaymentAppPreferenceEnum.NewNotesAdded);
      }

      AppInfoDTO customerAppInfo = appInfoDTOs.FirstOrDefault(i => i.Id == appId);
      if(customerAppInfo != null) {
        long customerUserpermission = (int)BusinessUserCustomerAppPermissionEnum.ViewCustomerTickets | (int)BusinessUserCustomerAppPermissionEnum.ManageCustomerTickets;
        notificationRecipients.AddRange(_qCustNotificationRecipientsData.GetBusinessCustomerUsersForNotes(tenantId, customerAppInfo.Id, userType, userStatus, customerUserpermission, (int)BusinessUserCustomerAppPreferenceEnum.NewNotesAdded));
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

        private List<AppInfoDTO> GetAppInfoByTenantId(Guid tenantId) {
            return _qCustNotificationRecipientsData.GetAppListByBusinessTenantIdAsync(tenantId);
        }

        public List<NotificationRecipient> GetAllPublisherUsersWithPreference(Guid tenantId) {
            return _qCustNotificationRecipientsData.GetAllPublisherUsersWithPreference(tenantId);
        }


    }
}
