using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.NotificationService;
using Microsoft.EntityFrameworkCore;
using ewApps.BusinessEntity.DTO;

namespace ewApps.BusinessEntity.QData {
    public class BusinessEntityNotificationRecipientRepository:IBusinessEntityNotificationRecipientRepository {

        #region Local Variable

        QBusinessEntityDbContext _dbContext;

        #endregion Local Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="QBizPaymentNotificationRecipientData"/> class.
        /// </summary>
        /// <param name="_dbContext">The q application portal database context.</param>
        public BusinessEntityNotificationRecipientRepository(QBusinessEntityDbContext dbContext) {
            _dbContext = dbContext;
        }

        #endregion Constructor

        public async Task<List<NotificationRecipient>> GetARInvoiceNotificationBizRecipientForPayAppAsync(Guid appId, Guid businessTenantId, long permissionBitMask, int userType, int userStatus, long invoicePreferenceValue, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT DISTINCT TU.Id AS 'TenantUserId', TU.FullName, TU.Email, 
                            CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN Cast(1 as bit ) ELSE Cast(0 as bit ) END AS 'EmailPreference', 
                            CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN Cast(1 as bit ) ELSE Cast(0 as bit ) END AS  'ASPreference',
                            CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN Cast(1 as bit ) ELSE Cast(0 as bit ) END AS  'SMSPreference',
                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId , Tual.UserType, Cast(tu.Phone As varchar(100)) AS 'SMSRecipient'  
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
                            INNER JOIN ap.Business as b ON tual.TenantId=b.TenantId
                            INNER JOIN pay.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId AND ap.AppId=tual.AppId
                            INNER JOIN pay.RoleLinking as rl ON tu.ID=rl.TenantUserId
                            INNER JOIN pay.Role as r ON r.ID=rl.RoleId
                            Where tual.Deleted=0 AND tual.AppId=@AppId AND tual.TenantId=@BusinessTenantId
                            AND tual.UserType=@UserType AND tual.Status=@UserStatus AND ap.TenantId=@BusinessTenantId 
                            AND rl.TenantId=@BusinessTenantId AND rl.AppId=@AppId 
                            AND (r.PermissionBitMask & @InvoicePermissionBitMask)=@InvoicePermissionBitMask";

            SqlParameter appIdParam = new SqlParameter("AppId", appId);
            SqlParameter BusinessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter userStatusParam = new SqlParameter("UserStatus", (int)TenantUserInvitaionStatusEnum.Accepted);
            SqlParameter preferenceMaskParam = new SqlParameter("PreferenceMask", invoicePreferenceValue);
            SqlParameter invoicePermissionMaskParam = new SqlParameter("InvoicePermissionBitMask", permissionBitMask);
            SqlParameter userTypeParam = new SqlParameter("UserType", userType);

            return await _dbContext.NotificationRecipientQuery.FromSql(sql, new object[] { appIdParam, BusinessTenantIdParam, userStatusParam, preferenceMaskParam, invoicePermissionMaskParam, userTypeParam }).ToListAsync();
        }

        public async Task<List<NotificationRecipient>> GetARInvoiceNotificationBizRecipientForCustAppAsync(Guid appId, Guid businessTenantId, long permissionBitMask, int userType, int userStatus, long invoicePreferenceValue, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT DISTINCT TU.Id AS 'TenantUserId', TU.FullName, TU.Email, 
                            CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN Cast(1 as bit ) ELSE Cast(0 as bit ) END AS 'EmailPreference', 
                            CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN Cast(1 as bit ) ELSE Cast(0 as bit ) END AS  'ASPreference',
                            CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN Cast(1 as bit ) ELSE Cast(0 as bit ) END AS  'SMSPreference',
                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId , Tual.UserType, Cast(tu.Phone As varchar(100)) AS 'SMSRecipient'  
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
                            INNER JOIN ap.Business as b ON tual.TenantId=b.TenantId
                            INNER JOIN ap.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId AND ap.AppId=tual.AppId
                            INNER JOIN ap.RoleLinking as rl ON tu.ID=rl.TenantUserId
                            INNER JOIN ap.Role as r ON r.ID=rl.RoleId
                            Where tual.Deleted=0 AND tual.AppId=@AppId AND tual.TenantId=@BusinessTenantId
                            AND tual.UserType=@UserType AND tual.Status=@UserStatus AND ap.TenantId=@BusinessTenantId 
                            AND rl.TenantId=@BusinessTenantId AND rl.AppId=@AppId 
                            AND (r.PermissionBitMask & @InvoicePermissionBitMask)=@InvoicePermissionBitMask";

            SqlParameter appIdParam = new SqlParameter("AppId", appId);
            SqlParameter BusinessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter userStatusParam = new SqlParameter("UserStatus", (int)TenantUserInvitaionStatusEnum.Accepted);
            SqlParameter preferenceMaskParam = new SqlParameter("PreferenceMask", invoicePreferenceValue);
            SqlParameter invoicePermissionMaskParam = new SqlParameter("InvoicePermissionBitMask", permissionBitMask);
            SqlParameter userTypeParam = new SqlParameter("UserType", userType);

            return await _dbContext.NotificationRecipientQuery.FromSql(sql, new object[] { appIdParam, BusinessTenantIdParam, userStatusParam, preferenceMaskParam, invoicePermissionMaskParam, userTypeParam }).ToListAsync();
        }

        public async Task<List<NotificationRecipient>> GetAddCustomerNotificationRecipientAsync(Guid appId, Guid businessTenantId,Guid loginUserId, int customerPermissionMask, int userType, int userStatus, long customerPreferenceValue, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email,
                            CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN Cast(1 as bit ) ELSE Cast(0 as bit ) END AS 'EmailPreference', 
                            CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN Cast(1 as bit ) ELSE Cast(0 as bit ) END AS  'ASPreference',
                            CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN Cast(1 as bit ) ELSE Cast(0 as bit ) END AS  'SMSPreference',
                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId , Tual.UserType, Cast(tu.ID As varchar(100)) AS 'SMSRecipient'  
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
                            INNER JOIN ap.Business as b ON tual.TenantId=b.TenantId
                            INNER JOIN pay.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId AND ap.AppId=tual.AppId
                            INNER JOIN pay.RoleLinking as rl ON tu.ID=rl.TenantUserId
                            INNER JOIN pay.Role as r ON r.ID=rl.RoleId
                            Where tual.Deleted=0 AND tual.AppId=@AppId AND tual.TenantId=@BusinessTenantId
                            AND tual.UserType=@UserType AND tual.Status=@UserStatus AND ap.TenantId=@BusinessTenantId 
                            AND rl.TenantId=@BusinessTenantId AND rl.AppId=@AppId And TU.Id <> @LoginUserIdParam
                            AND (r.PermissionBitMask & @CustomerPermissionBitMask)=@CustomerPermissionBitMask
                            AND
                            (
                                (ap.EmailPreference & @PreferenceMask)=@PreferenceMask OR
                                (ap.SMSPreference & @PreferenceMask)=@PreferenceMask OR
                                (ap.ASPreference & @PreferenceMask)=@PreferenceMask 
                            )
                            ";

            SqlParameter appIdParam = new SqlParameter("AppId", appId);
            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter userStatusParam = new SqlParameter("UserStatus", (int)TenantUserInvitaionStatusEnum.Accepted);
            SqlParameter preferenceMaskParam = new SqlParameter("PreferenceMask", customerPreferenceValue);
            SqlParameter invoicePermissionMaskParam = new SqlParameter("CustomerPermissionBitMask", customerPermissionMask);
            SqlParameter userTypeParam = new SqlParameter("UserType", userType);
            SqlParameter loginUserIdParam = new SqlParameter("LoginUserIdParam", loginUserId);

      return await _dbContext.NotificationRecipientQuery.FromSql(sql, new object[] { appIdParam, businessTenantIdParam, userStatusParam, invoicePermissionMaskParam, userTypeParam, preferenceMaskParam, loginUserIdParam }).ToListAsync();
        }
    public async Task<List<NotificationRecipient>> GetAddCustomerNotificationRecipientForCustAppAsync(Guid appId, Guid businessTenantId, Guid loginUserId, int customerPermissionMask, int userType, int userStatus, long customerPreferenceValue, CancellationToken cancellationToken = default(CancellationToken))
    {
      string sql = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email,
                            CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN Cast(1 as bit ) ELSE Cast(0 as bit ) END AS 'EmailPreference', 
                            CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN Cast(1 as bit ) ELSE Cast(0 as bit ) END AS  'ASPreference',
                            CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN Cast(1 as bit ) ELSE Cast(0 as bit ) END AS  'SMSPreference',
                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId , Tual.UserType, Cast(tu.ID As varchar(100)) AS 'SMSRecipient'  
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
                            INNER JOIN ap.Business as b ON tual.TenantId=b.TenantId
                            INNER JOIN ap.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId AND ap.AppId=tual.AppId
                            INNER JOIN ap.RoleLinking as rl ON tu.ID=rl.TenantUserId
                            INNER JOIN ap.Role as r ON r.ID=rl.RoleId
                            Where tual.Deleted=0 AND tual.AppId=@AppId AND tual.TenantId=@BusinessTenantId
                            AND tual.UserType=@UserType AND tual.Status=@UserStatus AND ap.TenantId=@BusinessTenantId 
                            AND rl.TenantId=@BusinessTenantId AND rl.AppId=@AppId And TU.Id <> @LoginUserIdParam
                            AND (r.PermissionBitMask & @CustomerPermissionBitMask)=@CustomerPermissionBitMask
                            AND
                            (
                                (ap.EmailPreference & @PreferenceMask)=@PreferenceMask OR
                                (ap.SMSPreference & @PreferenceMask)=@PreferenceMask OR
                                (ap.ASPreference & @PreferenceMask)=@PreferenceMask 
                            )
                            ";

      SqlParameter appIdParam = new SqlParameter("AppId", appId);
      SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
      SqlParameter userStatusParam = new SqlParameter("UserStatus", (int)TenantUserInvitaionStatusEnum.Accepted);
      SqlParameter preferenceMaskParam = new SqlParameter("PreferenceMask", customerPreferenceValue);
      SqlParameter invoicePermissionMaskParam = new SqlParameter("CustomerPermissionBitMask", customerPermissionMask);
      SqlParameter userTypeParam = new SqlParameter("UserType", userType);
      SqlParameter loginUserIdParam = new SqlParameter("LoginUserIdParam", loginUserId);

      return await _dbContext.NotificationRecipientQuery.FromSql(sql, new object[] { appIdParam, businessTenantIdParam, userStatusParam, invoicePermissionMaskParam, userTypeParam, preferenceMaskParam, loginUserIdParam }).ToListAsync();
    }

    public async Task<List<NotificationRecipient>> GetSalesOrderNotificationRecipientAsync(Guid appId, Guid businessTenantId, long permissionBitMask, int userType, int userStatus, long preferenceEnum, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email, 
                            CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN Cast(1 as bit ) ELSE Cast(0 as bit ) END AS 'EmailPreference', 
                            CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN Cast(1 as bit ) ELSE Cast(0 as bit ) END AS  'ASPreference',
                            CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN Cast(1 as bit ) ELSE Cast(0 as bit ) END AS  'SMSPreference',
                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId, Tual.UserType, Cast(tu.ID As varchar(100)) AS 'SMSRecipient'  
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
                            INNER JOIN ap.Business as b ON tual.TenantId=b.TenantId
                            INNER JOIN ap.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId AND ap.AppId=tual.AppId
                            INNER JOIN ap.RoleLinking as rl ON tu.ID=rl.TenantUserId
                            INNER JOIN ap.Role as r ON r.ID=rl.RoleId
                            Where tual.Deleted=0 AND tual.AppId=@AppId AND tual.TenantId=@BusinessTenantId
                            AND tual.UserType=@UserType AND tual.Status=@UserStatus AND ap.TenantId=@BusinessTenantId 
                            AND rl.TenantId=@BusinessTenantId AND rl.AppId=@AppId 
                            AND (r.PermissionBitMask & @SOPermissionBitMask)=@SOPermissionBitMask";

            SqlParameter appIdParam = new SqlParameter("AppId", appId);
            SqlParameter BusinessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter userStatusParam = new SqlParameter("UserStatus", (int)TenantUserInvitaionStatusEnum.Accepted);
            SqlParameter preferenceMaskParam = new SqlParameter("PreferenceMask", preferenceEnum);
            SqlParameter invoicePermissionMaskParam = new SqlParameter("SOPermissionBitMask", permissionBitMask);
            SqlParameter userTypeParam = new SqlParameter("UserType", userType);

            return await _dbContext.NotificationRecipientQuery.FromSql(sql, new object[] { appIdParam, BusinessTenantIdParam, userStatusParam, invoicePermissionMaskParam, userTypeParam, preferenceMaskParam }).ToListAsync();
        }

        public async Task<List<NotificationRecipient>> GetARInvoiceCustomerRecipientAsync(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, int invoicePreferenceValue, int permissionBitMask, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT DISTINCT TU.Id AS 'TenantUserId', TU.FullName, TU.Email, 
                            CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN Cast(1 as bit ) ELSE Cast(0 as bit ) END AS 'EmailPreference', 
                            CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN Cast(1 as bit ) ELSE Cast(0 as bit ) END AS  'ASPreference',
                            CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN Cast(1 as bit ) ELSE Cast(0 as bit ) END AS  'SMSPreference',
                            c.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId , Tual.UserType, Cast(tu.Phone As varchar(100)) AS 'SMSRecipient'  
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking AS tual ON tu.id=tual.TenantUserId
                            INNER JOIN ap.Customer AS c ON tual.BusinessPartnerTenantId=c.BusinessPartnerTenantId
                            {0}
                            Where tual.Deleted=0 AND tual.AppId=@AppId AND tual.BusinessPartnerTenantId =@BusinessPartnerTenantId
                            AND tual.UserType=@UserType AND tual.Status=@UserStatus AND ap.TenantId=@BusinessTenantId 
                            AND rl.TenantId=@BusinessTenantId AND rl.AppId=@AppId 
                            AND (r.PermissionBitMask & @InvoicePermissionBitMask)=@InvoicePermissionBitMask";

      string custappInnerJoin = @"INNER JOIN ap.TenantUserAppPreference AS ap ON tu.ID=ap.TenantUserId AND ap.AppId=tual.AppId
                            INNER JOIN ap.RoleLinking AS rl ON tu.ID=rl.TenantUserId
                            INNER JOIN ap.[Role] AS r ON r.ID=rl.RoleId ";
      string payappInnerJoin = @"INNER JOIN pay.TenantUserAppPreference AS ap ON tu.ID=ap.TenantUserId AND ap.AppId=tual.AppId
                            INNER JOIN pay.RoleLinking AS rl ON tu.ID=rl.TenantUserId
                            INNER JOIN pay.[Role] AS r ON r.ID=rl.RoleId ";
      

      if ((int)CustomerUserPaymentAppPermissionEnum.ViewInvoices == permissionBitMask)
      {
        sql = string.Format(sql, payappInnerJoin);
      }
      else if ((int)CustomerUserCustomerAppPermissionEnum.ViewAPInvoices == permissionBitMask)
      {
        sql = string.Format(sql, custappInnerJoin);
      }    

      SqlParameter appIdParam = new SqlParameter("AppId", appId);
            SqlParameter BusinessPartnerTenantIdParam = new SqlParameter("BusinessPartnerTenantId", businessPartnerTenantId);
            SqlParameter BusinessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter userStatusParam = new SqlParameter("UserStatus", (int)TenantUserInvitaionStatusEnum.Accepted);
            SqlParameter preferenceMaskParam = new SqlParameter("PreferenceMask", invoicePreferenceValue);
            SqlParameter invoicePermissionMaskParam = new SqlParameter("InvoicePermissionBitMask", permissionBitMask);
            SqlParameter userTypeParam = new SqlParameter("UserType", Convert.ToString((int)UserTypeEnum.Customer));

            return await _dbContext.NotificationRecipientQuery.FromSql(sql, new object[] { appIdParam, BusinessTenantIdParam, userStatusParam, preferenceMaskParam, invoicePermissionMaskParam, userTypeParam, BusinessPartnerTenantIdParam }).ToListAsync();


        }

        public async Task<List<NotificationRecipient>> GetSalesQuotationNotificationRecipientAsync(Guid appId, Guid businessTenantId, long permissionBitMask, int userType, int userStatus, long preferenceEnum, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email, 
                            CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN Cast(1 as bit ) ELSE Cast(0 as bit ) END AS 'EmailPreference', 
                            CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN Cast(1 as bit ) ELSE Cast(0 as bit ) END AS  'ASPreference',
                            CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN Cast(1 as bit ) ELSE Cast(0 as bit ) END AS  'SMSPreference',
                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId, Tual.UserType, Cast(tu.ID As varchar(100)) AS 'SMSRecipient'  
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
                            INNER JOIN ap.Business as b ON tual.TenantId=b.TenantId
                            INNER JOIN ap.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId AND ap.AppId=tual.AppId
                            INNER JOIN ap.RoleLinking as rl ON tu.ID=rl.TenantUserId
                            INNER JOIN ap.Role as r ON r.ID=rl.RoleId
                            Where tual.Deleted=0 AND tual.AppId=@AppId AND tual.TenantId=@BusinessTenantId
                            AND tual.UserType=@UserType AND tual.Status=@UserStatus AND ap.TenantId=@BusinessTenantId 
                            AND rl.TenantId=@BusinessTenantId AND rl.AppId=@AppId 
                            AND (r.PermissionBitMask & @SQPermissionBitMask)=@SQPermissionBitMask
                            AND 
                            (
                                (ap.EmailPreference & @PreferenceMask)=@PreferenceMask OR
                                (ap.SMSPreference & @PreferenceMask)=@PreferenceMask OR
                                (ap.ASPreference & @PreferenceMask)=@PreferenceMask 
                            )
                            ";

            SqlParameter appIdParam = new SqlParameter("AppId", appId);
            SqlParameter BusinessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter userStatusParam = new SqlParameter("UserStatus", (int)TenantUserInvitaionStatusEnum.Accepted);
            SqlParameter preferenceMaskParam = new SqlParameter("PreferenceMask", preferenceEnum);
            SqlParameter invoicePermissionMaskParam = new SqlParameter("SQPermissionBitMask", permissionBitMask);
            SqlParameter userTypeParam = new SqlParameter("UserType", userType);

            return await _dbContext.NotificationRecipientQuery.FromSql(sql, new object[] { appIdParam, BusinessTenantIdParam, userStatusParam, invoicePermissionMaskParam, userTypeParam, preferenceMaskParam }).ToListAsync();
        }
        
    }
}
