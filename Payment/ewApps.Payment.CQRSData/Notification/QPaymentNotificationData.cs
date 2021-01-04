using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ewApps.Core.BaseService;
using ewApps.Core.NotificationService;
using ewApps.Payment.DTO;
using Microsoft.EntityFrameworkCore;

namespace ewApps.Payment.QData {
    public class QPaymentNotificationData:IQPaymentNotificationData {

        #region Local Variable

        QPaymentDBContext _qPaymentDBContext;

        #endregion Local Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="QPaymentNotificationData"/> class.
        /// </summary>
        /// <param name="qAppPortalDbContext">The q application portal database context.</param>
        public QPaymentNotificationData(QPaymentDBContext qPaymentDBContext) {
            _qPaymentDBContext = qPaymentDBContext;
        }

    #endregion Constructor

        #region Recepients

      //    CAST('True' AS bit) As 'EmailPreference', 
      //CAST('True' AS bit) As 'ASPreference', 
      //CAST('True' AS bit) As 'SMSPreference

        public List<NotificationRecipient> GetBusinessUserPaymentAppUsers(Guid appId, Guid businessTenantId, int paymentPermissionMask, int userPreferenceMask) {
          string sql = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email, 

                            CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END As 'EmailPreference', 
                            CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN cast(1 as bit) ELSE cast(0 as bit) END As 'ASPreference', 
                            CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END As 'SMSPreference',

                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId , Tual.UserType, TU.Phone AS 'SMSRecipient'  
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
                            INNER JOIN ap.Business as b ON tual.TenantId=b.TenantId

                            INNER JOIN pay.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId 
                            AND ap.AppId=tual.AppId AND b.TenantId = ap.TenantId

                            Where tual.Deleted=0 AND tual.AppId=@AppId AND tual.TenantId=@BusinessTenantId 
                            AND tual.UserType=@UserType AND tual.Status=@ActiveUserStatus";

            SqlParameter appIdParam = new SqlParameter("AppId", appId);
            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter userTypeParam = new SqlParameter("UserType", (int)UserTypeEnum.Business);
            //SqlParameter userStatusParam = new SqlParameter("ActiveUserStatus", (int)TenantUserInvitaionStatusEnum.Invited);
            SqlParameter userStatusParam = new SqlParameter("ActiveUserStatus", (int)TenantUserInvitaionStatusEnum.Accepted);

            SqlParameter invoicePermissionMaskParam = new SqlParameter("CustomerPermissionBitMask", paymentPermissionMask);
            SqlParameter preferenceMaskparam = new SqlParameter("@PreferenceMask", userPreferenceMask);

            return _qPaymentDBContext.NotificationRecipientQuery.FromSql(sql, new object[] { appIdParam, businessTenantIdParam, userTypeParam, userStatusParam, invoicePermissionMaskParam, preferenceMaskparam }).ToList();
        }

    //    CAST('True' AS bit) As 'EmailPreference', 
    //CAST('True' AS bit) As 'ASPreference', 
    //CAST('True' AS bit) As 'SMSPreference',
    public List<NotificationRecipient> GetCustomerUserPaymentAppUsers(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, int paymentPermissionMask, int userPreferenceMask) {  
          string sql = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email, 

                      CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END As 'EmailPreference', 
                      CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN cast(1 as bit) ELSE cast(0 as bit) END As 'ASPreference', 
                      CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END As 'SMSPreference',


                                c.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId , Tual.UserType, TU.Phone AS 'SMSRecipient'  
                                FROM am.TenantUser TU
                                INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
                                INNER JOIN ap.Customer as c ON tual.BusinessPartnerTenantId=c.BusinessPartnerTenantId

INNER JOIN pay.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId 
                      AND ap.AppId=tual.AppId AND c.TenantId = ap.TenantId

                                Where tual.Deleted=0 AND tual.AppId=@AppId AND tual.TenantId=@BusinessTenantId 
                                AND tual.BusinessPartnerTenantId=@BusinessPartnerTenantId
                                AND tual.UserType=@UserType AND tual.Status=@ActiveUserStatus";

          SqlParameter appIdParam = new SqlParameter("AppId", appId);
          SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
          SqlParameter businessPartnerTenantIdParam = new SqlParameter("businessPartnerTenantId", businessPartnerTenantId);
          SqlParameter userTypeParam = new SqlParameter("UserType", (int)UserTypeEnum.Customer);
          //SqlParameter userStatusParam = new SqlParameter("ActiveUserStatus", (int)TenantUserInvitaionStatusEnum.Invited);
          SqlParameter userStatusParam = new SqlParameter("ActiveUserStatus", (int)TenantUserInvitaionStatusEnum.Accepted);
          SqlParameter invoicePermissionMaskParam = new SqlParameter("CustomerPermissionBitMask", paymentPermissionMask);
          SqlParameter preferenceMaskparam = new SqlParameter("@PreferenceMask", userPreferenceMask);

          return _qPaymentDBContext.NotificationRecipientQuery.FromSql(sql, new object[] { appIdParam, businessTenantIdParam, userTypeParam, userStatusParam, businessPartnerTenantIdParam
,invoicePermissionMaskParam, preferenceMaskparam}).ToList();
        }

    //CAST('True' AS bit) As 'EmailPreference', 
    //CAST('True' AS bit) As 'ASPreference', 
    //CAST('True' AS bit) As 'SMSPreference',

    public List<NotificationRecipient> GetPaymentAppCustomerUserOnRefundForCust(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, int paymentPermissionMask, int userPreferenceMask)
    {
      string sql = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email, 

                      CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END As 'EmailPreference', 
                      CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN cast(1 as bit) ELSE cast(0 as bit) END As 'ASPreference', 
                      CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END As 'SMSPreference',

                      c.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId , Tual.UserType, TU.Phone AS 'SMSRecipient'  
                      FROM am.TenantUser TU
                      INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
                      INNER JOIN ap.Customer as c ON tual.BusinessPartnerTenantId=c.BusinessPartnerTenantId

                      INNER JOIN pay.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId 
                      AND ap.AppId=tual.AppId
                      INNER JOIN pay.RoleLinking as rl ON tu.ID=rl.TenantUserId
                      INNER JOIN pay.Role as r ON r.ID=rl.RoleId

                      Where tual.Deleted=0 AND tual.AppId=@AppId AND tual.TenantId=@BusinessTenantId 
                      AND tual.BusinessPartnerTenantId=@BusinessPartnerTenantId
                      AND tual.UserType=@UserType AND tual.Status=@ActiveUserStatus
                      AND ap.TenantId=@BusinessTenantId 
                      AND rl.TenantId=@BusinessTenantId AND rl.AppId=@AppId 
                      AND (r.PermissionBitMask & @CustomerPermissionBitMask)=@CustomerPermissionBitMask";

      SqlParameter appIdParam = new SqlParameter("AppId", appId);
      SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
      SqlParameter businessPartnerTenantIdParam = new SqlParameter("businessPartnerTenantId", businessPartnerTenantId);
      SqlParameter userTypeParam = new SqlParameter("UserType", (int)UserTypeEnum.Customer);
      SqlParameter invoicePermissionMaskParam = new SqlParameter("CustomerPermissionBitMask", paymentPermissionMask);
      SqlParameter preferenceMaskparam = new SqlParameter("@PreferenceMask", userPreferenceMask);
      SqlParameter userStatusParam = new SqlParameter("ActiveUserStatus", (int)TenantUserInvitaionStatusEnum.Accepted);

//      return _qPaymentDBContext.NotificationRecipientQuery.FromSql(sql, new object[] { appIdParam, businessTenantIdParam, userTypeParam, userStatusParam, businessPartnerTenantIdParam, invoicePermissionMaskParam }).ToList();
      return _qPaymentDBContext.NotificationRecipientQuery.FromSql(sql, new object[] { appIdParam, businessTenantIdParam, userTypeParam, userStatusParam, businessPartnerTenantIdParam, invoicePermissionMaskParam, preferenceMaskparam }).ToList();

    }

    /// <summary>
    /// Get customer payment preference.
    /// </summary>
    /// <param name="appId">ApplicationId</param>
    /// <param name="businessTenantId">Business tenant id.</param>
    /// <param name="businessPartnerTenantId">Business partner tenant id.</param>
    /// <param name="paymentPermissionMask">Assigned permission.</param>
    /// <param name="userPreferenceMask">Notification preference is on.</param>
    /// <returns></returns>
    public List<NotificationRecipient> GetPaymentAppCustomerUserOnPaymentForCust(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, int paymentPermissionMask, int userPreferenceMask) {
            string sql = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email, 
                      CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END As 'EmailPreference', 
                      CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN cast(1 as bit) ELSE cast(0 as bit) END As 'ASPreference', 
                      CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END As 'SMSPreference',
                      c.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId , Tual.UserType, TU.Phone AS 'SMSRecipient'  
                      FROM am.TenantUser TU
                      INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
                      INNER JOIN ap.Customer as c ON tual.BusinessPartnerTenantId=c.BusinessPartnerTenantId

                      INNER JOIN pay.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId 
                      AND ap.AppId=tual.AppId
                      INNER JOIN pay.RoleLinking as rl ON tu.ID=rl.TenantUserId
                      INNER JOIN pay.Role as r ON r.ID=rl.RoleId

                      Where tual.Deleted=0 AND tual.AppId=@AppId AND tual.TenantId=@BusinessTenantId 
                      AND tual.BusinessPartnerTenantId=@BusinessPartnerTenantId
                      AND tual.UserType=@UserType AND tual.Status=@ActiveUserStatus  
                      AND ap.TenantId=@BusinessTenantId 
                      AND rl.TenantId=@BusinessTenantId AND rl.AppId=@AppId 
                      AND (r.PermissionBitMask & @CustomerPermissionBitMask)=@CustomerPermissionBitMask";

            SqlParameter appIdParam = new SqlParameter("AppId", appId);
            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter businessPartnerTenantIdParam = new SqlParameter("businessPartnerTenantId", businessPartnerTenantId);
            SqlParameter userTypeParam = new SqlParameter("UserType", (int)UserTypeEnum.Customer);
            SqlParameter userStatusParam = new SqlParameter("ActiveUserStatus", (int)TenantUserInvitaionStatusEnum.Accepted);
            //SqlParameter userStatusParam2 = new SqlParameter("ActiveUserStatus2", (int)TenantUserInvitaionStatusEnum.Invited);
            SqlParameter invoicePermissionMaskParam = new SqlParameter("CustomerPermissionBitMask", paymentPermissionMask);
            SqlParameter preferenceMaskparam = new SqlParameter("@PreferenceMask", userPreferenceMask);

            return _qPaymentDBContext.NotificationRecipientQuery.FromSql(sql, new object[] { appIdParam, businessTenantIdParam, userTypeParam, userStatusParam, businessPartnerTenantIdParam, invoicePermissionMaskParam, preferenceMaskparam }).ToList();
        }        

        /// <summary>
        /// Get business user and their payment preference.
        /// </summary>
        /// <param name="appId">ApplicationId</param>
        /// <param name="businessTenantId">Business tenant id.</param>
        /// <param name="paymentPermissionMask">Assigned permission.</param>
        /// <param name="userPreferenceMask">Notification preference is on.</param>
        /// <returns></returns>
        public List<NotificationRecipient> GetPaymentAppBusinessUserOnPaymentForBusiness(Guid appId, Guid businessTenantId, int paymentPermissionMask, int userPreferenceMask) {
            string sql = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email, 
CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END As 'EmailPreference', 
                      CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN cast(1 as bit) ELSE cast(0 as bit) END As 'ASPreference', 
                      CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END As 'SMSPreference',
                      bus.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId , Tual.UserType, TU.Phone AS 'SMSRecipient'  
                      FROM am.TenantUser TU
                      INNER JOIN am.TenantUserAppLinking as tual ON 
                      tu.id=tual.TenantUserId AND tual.UserType=@UserType AND tual.AppId=@AppId 
                      INNER JOIN AP.Business bus ON bus.TenantId = @BusinessTenantId AND bus.TenantId = tual.TenantId
                      INNER JOIN pay.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId 
                      AND ap.AppId=tual.AppId AND bus.TenantId = ap.TenantId
                      INNER JOIN pay.RoleLinking as rl ON tu.ID=rl.TenantUserId
                      INNER JOIN pay.Role as r ON r.ID=rl.RoleId 
                      Where tual.Deleted=0 AND tual.AppId=@AppId AND tual.TenantId=@BusinessTenantId 
                      AND tual.BusinessPartnerTenantId IS NULL 
                      AND tual.UserType=@UserType AND tual.Status=@ActiveUserStatus
                      AND ap.TenantId=@BusinessTenantId 
                      AND rl.TenantId=@BusinessTenantId AND rl.AppId=@AppId 
                      AND (r.PermissionBitMask & @busPermissionBitMask)=@busPermissionBitMask";

            SqlParameter appIdParam = new SqlParameter("AppId", appId);
            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);            
            SqlParameter userTypeParam = new SqlParameter("UserType", (int)UserTypeEnum.Business);
            SqlParameter userStatusParam = new SqlParameter("ActiveUserStatus", (int)TenantUserInvitaionStatusEnum.Accepted);
            SqlParameter invoicePermissionMaskParam = new SqlParameter("busPermissionBitMask", paymentPermissionMask);
            SqlParameter preferenceMaskparam = new SqlParameter("@PreferenceMask", userPreferenceMask);

            return _qPaymentDBContext.NotificationRecipientQuery.FromSql(sql, new object[] { appIdParam, businessTenantIdParam, userTypeParam, userStatusParam, invoicePermissionMaskParam, preferenceMaskparam }).ToList();
        }

        #endregion Recepients

        #region Notification Data

        public PaymentRelatedDataDTO GetPaymentNotificationInfo(Guid paymentId, Guid tenantUserId, string AppKey) {

            string sql = @"SELECT AP.ID as 'AppId' , Pub.Name as 'PublisherCompanyName' , TN.Name as 'BusinessCompanyName' , CUST.CustomerName  as 'CustomerCompanyName' ,
                        CUST.ERPCustomerKey  as 'CustomerCompanyId' , TN.SubDomainName as 'SubDomain' , Pub.Copyright as 'CopyRightText' , TU.FullName as 'UserFullName' , TU.IdentityNumber as 'UserId',
                        PA.TransactionId as 'TransactionId', PA.IdentityNumber , PA.Amount as 'TransactionAmount' , PA.CreatedOn as 'TransactionDate' , PA.PaymentTransectionCurrency as 'Currency', apCust.BusinessPartnerTenantId 
                        , pubSetting.Name as PaymentApplicationName, CUST.Currency AS CustomerCurrency 
                        ,pa.Status as TransactionStatus,pa.CustomerAccountNumber as AccountNumber,aps.Name as TransactionService, '' as  TransactionMode, cust.ERPCustomerKey as CustomerRefId,
                        bus.TimeZone, bus.DateTimeFormat
                        FROM pay.Payment PA
                        INNER JOIN am.Tenant TN ON TN.ID = PA.TenantId
                        INNER JOIN ap.Business AS bus ON bus.TenantId = TN.ID
                        INNER JOIN am.TenantLinking TL ON TL.BusinessTenantId = TN.ID AND TL.BusinessPartnerTenantId is null
                        INNER JOIN ap.Publisher Pub ON Pub.TenantId = TL.PublisherTenantId
                        INNER JOIN am.App AP on AP.AppKey = @AppKey
                        INNER JOIN ap.PublisherAppSetting pubSetting on pubSetting.TenantId = pub.TenantId and pubSetting.AppId=ap.Id 
                        INNER JOIN be.BACustomer CUST ON CUST.ID = PA.PartnerId
                        INNER JOIN ap.Customer apCust ON apCust.BusinessPartnerTenantId = CUST.BusinessPartnerTenantId
                        INNER JOIN am.TenantUser TU ON TU.ID = @TenantUserId
                        INNER JOIN am.AppService aps on aps.ID= pa.AppServiceId 
                        WHERE PA.ID = @PaymentId ";

            SqlParameter paymentIdParam = new SqlParameter("@PaymentId", paymentId);
            SqlParameter tenantUserIdParam = new SqlParameter("@TenantUserId", tenantUserId);
            SqlParameter appKeyParam = new SqlParameter("@AppKey", AppKey);

            return _qPaymentDBContext.PaymentRelatedDataDTOQuery.FromSql(sql, new object[] { paymentIdParam, tenantUserIdParam, appKeyParam }).ToList().FirstOrDefault();
        }

        /// <summary>
        /// Get preauth payment detail.
        /// </summary>
        /// <param name="preAuthPaymentId"></param>
        /// <param name="tenantUserId"></param>
        /// <param name="AppKey"></param>
        /// <returns>return authorized payment information.</returns>
        public PreAuthPaymentRelatedDataDTO GetPreAuthPaymentNotificationInfo(Guid preAuthPaymentId, Guid tenantUserId, string AppKey) {
            string sql = @"SELECT AP.ID as 'AppId' , Pub.Name as 'PublisherCompanyName' , TN.Name as 'BusinessCompanyName' , CUST.CustomerName  as 'CustomerCompanyName' ,
                        apCust.IdentityNumber  as 'CustomerCompanyId' , TN.SubDomainName as 'SubDomain' , Pub.Copyright as 'CopyRightText' , TU.FullName as 'UserFullName' , TU.IdentityNumber as 'UserId',
                        PA.TransactionId as 'TransactionId' , PA.Amount as 'TransactionAmount' , PA.CreatedOn as 'TransactionDate' , PA.PaymentTransectionCurrency as 'Currency', apCust.BusinessPartnerTenantId 
                        , pubSetting.Name as PaymentApplicationName, CUST.Currency AS CustomerCurrency, AppSrvc.Name AS ServiceName, SrvcAttr.Name  AS ServiceAttributeName, CUST.ERPCustomerKey,
                        bus.TimeZone, bus.DateTimeFormat
                        FROM pay.PreAuthPayment PA 
                        INNER JOIN am.Tenant TN ON TN.ID = PA.TenantId and PA.ID = @PaymentId 
                        INNER JOIN ap.Business AS bus ON bus.TenantId = TN.ID
                        INNER JOIN am.TenantLinking TL ON TL.BusinessTenantId = TN.ID AND TL.BusinessPartnerTenantId is null
                        INNER JOIN ap.Publisher Pub ON Pub.TenantId = TL.PublisherTenantId
                        INNER JOIN am.App AP on AP.AppKey = @AppKey
                        INNER JOIN ap.PublisherAppSetting pubSetting on pubSetting.TenantId = pub.TenantId and pubSetting.AppId=ap.Id 
                        INNER JOIN be.BACustomer CUST ON CUST.ID = PA.BACustomerId
                        INNER JOIN ap.Customer apCust ON apCust.BusinessPartnerTenantId = CUST.BusinessPartnerTenantId
                        INNER JOIN am.TenantUser TU ON TU.ID = @TenantUserId
                        INNER JOIN AM.AppService AppSrvc ON AppSrvc.ID = PA.AppServiceId  
                        INNER JOIN AM.AppServiceAttribute SrvcAttr ON SrvcAttr.ID = PA.AppServiceAttributeId 
                        WHERE PA.ID = @PaymentId ";

            SqlParameter paymentIdParam = new SqlParameter("@PaymentId", preAuthPaymentId);
            SqlParameter tenantUserIdParam = new SqlParameter("@TenantUserId", tenantUserId);
            SqlParameter appKeyParam = new SqlParameter("@AppKey", AppKey);

            return _qPaymentDBContext.PreAuthPaymentRelatedDataDTOQuery.FromSql(sql, new object[] { paymentIdParam, tenantUserIdParam, appKeyParam }).ToList().FirstOrDefault();
        }

        #endregion Notification Data
    }
}
