using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.NotificationService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.QData {
    public class QVendorNotificationRecipientsData:IQVendorNotificationRecipientsData {

        #region Local Variable

        QAppPortalDbContext _qAppPortalDbContext;

        #endregion Local Variable

        public QVendorNotificationRecipientsData(QAppPortalDbContext qAppPortalDbContext) {
            _qAppPortalDbContext = qAppPortalDbContext;
        }

        /// <inheritdoc/>
        public List<NotificationRecipient> GetVendorUserAppOnBoardRecipients(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, Guid onboardedUserId) {
            string sql = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email, 
                            CAST(1 as bit)  AS 'EmailPreference',
                            CAST(0 as bit) AS 'SMSPreference',
                            CAST(1 as bit) AS 'ASPreference',                        
                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId , Tual.UserType,  TU.Phone AS 'SMSRecipient',  te.SubDomainName AS SubDomain  
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
                            INNER JOIN ap.Business as b ON tual.TenantId=b.TenantId
                            INNER JOIN pay.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId
                            INNER JOIN am.Tenant as te ON b.TenantId = te.ID
                            Where tual.Deleted=0 AND tual.AppId=@AppId AND tual.TenantId=@BusinessTenantId 
                            AND tual.UserType=@UserType AND tu.Id<>@OnBoardedUser AND tual.Status=@ActiveUserStatus ANd Tual.BusinessPartnerTenantId = @BusinessPartnerTenantId";

            SqlParameter appIdParam = new SqlParameter("AppId", appId);
            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter businessTenantpartnerIdParam = new SqlParameter("BusinessPartnerTenantId", businessPartnerTenantId);
            SqlParameter userTypeParam = new SqlParameter("UserType", (int)UserTypeEnum.Vendor);
            SqlParameter onBoardedUserParam = new SqlParameter("OnBoardedUser", onboardedUserId);
            SqlParameter userStatusParam = new SqlParameter("ActiveUserStatus", (int)TenantUserInvitaionStatusEnum.Accepted);

            return _qAppPortalDbContext.NotificationRecipientQuery.FromSql(sql, new object[] { appIdParam, businessTenantIdParam, businessTenantpartnerIdParam, userTypeParam, onBoardedUserParam, userStatusParam }).ToList();
        }

    }
}
