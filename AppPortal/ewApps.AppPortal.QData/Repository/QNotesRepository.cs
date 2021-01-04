using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.QData {
    public class QNotesRepository:QBaseRepository<QAppPortalDbContext>, IQNotesRepository {

        public QNotesRepository(QAppPortalDbContext context) : base(context) {
        }

        public async Task<List<NotesViewDTO>> GetNotesViewListByEntityId(Guid entityId, Guid tenantId) {
            string query = @" select NT.ID as 'NotesId' , NT.EntityId , NT.EntityType , TU.FullName AS 'CreatedByName' , NT.CreatedOn, NT.Content, NT.System, NT.Private, NT.Deleted ,NT.TenantId
                            From ap.Notes NT
                            INNER JOIN am.TenantUser TU On TU.ID = NT.CreatedBy 
                            where NT.EntityId = @entityId and  NT.TenantId = @tenantId and NT.Deleted =0 order by NT.CreatedOn desc ";
            SqlParameter entityIdParam = new SqlParameter("@entityId", entityId);
            SqlParameter tenantIdparam = new SqlParameter("@tenantId", tenantId);
            List<NotesViewDTO> notes = await GetQueryEntityListAsync<NotesViewDTO>(query, new object[] { entityIdParam, tenantIdparam });
            return notes;
        }

        public async Task<BusinessNotesNotificationDTO> GetBusinessUserListByEntityId(Guid entityId, Guid tenantId,  Guid appId, Guid userId,int userType) {
            string query = @"SELECT DISTINCT  pas.Name AS 'AppName',  a.AppKey,tu.FullName AS UserName,t.SubDomainName,n.CreatedOn,
                            p.Name AS 'PublisherName', b.TenantId AS 'BusinessTenantId',i.ERPDocNum, c.CustomerName,c.ERPCustomerKey AS CustomerId,
                            b.Name AS 'BusinessName',p.Copyright, tu.Id AS 'UserId', a.Id AS 'AppId',tual.UserType,b.TimeZone,b.DateTimeFormat							
                            FROM ap.Publisher AS p
                            INNER JOIN am.TenantLinking AS tl ON p.TenantId=tl.PublisherTenantId
                            INNER JOIN ap.Business as b ON b.TenantId=tl.BusinessTenantId
							INNER JOIN am.Tenant AS t ON t.ID = b.TenantId
							INNER JOIN be.BAARInvoice AS i ON i.TenantId = b.TenantId
                            INNER JOIN ap.Notes AS n ON n.TenantId = b.TenantId AND i.Id = n.EntityId      
							INNER JOIN be.BACustomer AS c ON c.ID  = i.CustomerId							
                            INNER JOIN ap.PublisherAppSetting AS pas ON p.TenantId=pas.TenantId
                            INNER JOIN am.App as a ON pas.AppId=a.ID
                            INNER JOIN am.TenantUserAppLinking as tual ON tual.AppId=a.ID
                            INNER JOIN am.TenantUser as tu ON tu.ID=tual.TenantUserId							
                            Where tl.BusinessTenantId=@BusinessTenantId 
							AND tual.TenantUserId=@UserId AND tual.AppId= @AppId
							AND i.ID = @InvoiceId AND tual.UserType=@UserType AND tual.Deleted = 0";
            SqlParameter entityIdParam = new SqlParameter("@InvoiceId", entityId);
            SqlParameter tenantIdParam = new SqlParameter("@BusinessTenantId", tenantId);
            SqlParameter userIdParam = new SqlParameter("@UserId", userId);
            SqlParameter userTypeparam = new SqlParameter("@UserType", userType);
            SqlParameter appIdparam = new SqlParameter("@AppId", appId);           
            BusinessNotesNotificationDTO notes = await GetQueryEntityAsync<BusinessNotesNotificationDTO>(query, new object[] { entityIdParam, tenantIdParam, userIdParam , userTypeparam, appIdparam });
            return notes;
        }

        public async Task<BusinessNotesNotificationDTO> GetBusinessInfoWithQuotationByEntityId(Guid entityId, Guid tenantId, Guid appId, Guid userId, int userType) {
            string query = @"SELECT DISTINCT  pas.Name AS 'AppName',  a.AppKey,tu.FullName AS UserName,t.SubDomainName,
                            p.Name AS 'PublisherName', b.TenantId AS 'BusinessTenantId',sq.ERPDocNum, c.CustomerName,c.ERPCustomerKey AS CustomerId,
                            b.Name AS 'BusinessName',p.Copyright, tu.Id AS 'UserId', a.Id AS 'AppId',tual.UserType							
                            FROM ap.Publisher AS p
                            INNER JOIN am.TenantLinking AS tl ON p.TenantId=tl.PublisherTenantId
                            INNER JOIN ap.Business as b ON b.TenantId=tl.BusinessTenantId
							INNER JOIN am.Tenant AS t ON t.ID = b.TenantId
							INNER JOIN be.BASalesQuotation AS sq ON sq.TenantId = b.TenantId
							INNER JOIN be.BACustomer AS c ON c.ID  = sq.CustomerId							
                            INNER JOIN ap.PublisherAppSetting AS pas ON p.TenantId=pas.TenantId
                            INNER JOIN am.App as a ON pas.AppId=a.ID
                            INNER JOIN am.TenantUserAppLinking as tual ON tual.AppId=a.ID
                            INNER JOIN am.TenantUser as tu ON tu.ID=tual.TenantUserId							
                            Where tl.BusinessTenantId=@BusinessTenantId 
							AND tual.TenantUserId=@UserId AND tual.AppId= @AppId
							AND sq.ID = @QuotationId AND tual.UserType=@UserType AND tual.Deleted = 0";
            SqlParameter entityIdParam = new SqlParameter("@QuotationId", entityId);
            SqlParameter tenantIdParam = new SqlParameter("@BusinessTenantId", tenantId);
            SqlParameter userIdParam = new SqlParameter("@UserId", userId);
            SqlParameter userTypeparam = new SqlParameter("@UserType", userType);
            SqlParameter appIdparam = new SqlParameter("@AppId", appId);
            BusinessNotesNotificationDTO notes = await GetQueryEntityAsync<BusinessNotesNotificationDTO>(query, new object[] { entityIdParam, tenantIdParam, userIdParam, userTypeparam, appIdparam });
            return notes;
        }

        public async Task<BusinessNotesNotificationDTO> GetBusinessInfoWithSalesOrderByEntityId(Guid entityId, Guid tenantId, Guid appId, Guid userId, int userType) {
            string query = @"SELECT DISTINCT  pas.Name AS 'AppName',  a.AppKey,tu.FullName AS UserName,t.SubDomainName,
                            p.Name AS 'PublisherName', b.TenantId AS 'BusinessTenantId',so.ERPDocNum, c.CustomerName,c.ERPCustomerKey AS CustomerId,
                            b.Name AS 'BusinessName',p.Copyright, tu.Id AS 'UserId', a.Id AS 'AppId',tual.UserType							
                            FROM ap.Publisher AS p
                            INNER JOIN am.TenantLinking AS tl ON p.TenantId=tl.PublisherTenantId
                            INNER JOIN ap.Business as b ON b.TenantId=tl.BusinessTenantId
							INNER JOIN am.Tenant AS t ON t.ID = b.TenantId
							INNER JOIN be.BASalesOrder AS so ON so.TenantId = b.TenantId
							INNER JOIN be.BACustomer AS c ON c.ID  = so.CustomerId							
                            INNER JOIN ap.PublisherAppSetting AS pas ON p.TenantId=pas.TenantId
                            INNER JOIN am.App as a ON pas.AppId=a.ID
                            INNER JOIN am.TenantUserAppLinking as tual ON tual.AppId=a.ID
                            INNER JOIN am.TenantUser as tu ON tu.ID=tual.TenantUserId							
                            Where tl.BusinessTenantId=@BusinessTenantId 
							AND tual.TenantUserId=@UserId AND tual.AppId= @AppId
							AND so.ID = @OrderId AND tual.UserType=@UserType AND tual.Deleted = 0";
            SqlParameter entityIdParam = new SqlParameter("@OrderId", entityId);
            SqlParameter tenantIdParam = new SqlParameter("@BusinessTenantId", tenantId);
            SqlParameter userIdParam = new SqlParameter("@UserId", userId);
            SqlParameter userTypeparam = new SqlParameter("@UserType", userType);
            SqlParameter appIdparam = new SqlParameter("@AppId", appId);
            BusinessNotesNotificationDTO notes = await GetQueryEntityAsync<BusinessNotesNotificationDTO>(query, new object[] { entityIdParam, tenantIdParam, userIdParam, userTypeparam, appIdparam });
            return notes;
        }

        public async Task<BusinessNotesNotificationDTO> GetBusinessInfoWithDeliveryByEntityId(Guid entityId, Guid tenantId, Guid appId, Guid userId, int userType) {
            string query = @"SELECT DISTINCT  pas.Name AS 'AppName',  a.AppKey,tu.FullName AS UserName,t.SubDomainName,
                            p.Name AS 'PublisherName', b.TenantId AS 'BusinessTenantId',d.ERPDocNum, c.CustomerName,c.ERPCustomerKey AS CustomerId,
                            b.Name AS 'BusinessName',p.Copyright, tu.Id AS 'UserId', a.Id AS 'AppId',tual.UserType							
                            FROM ap.Publisher AS p
                            INNER JOIN am.TenantLinking AS tl ON p.TenantId=tl.PublisherTenantId
                            INNER JOIN ap.Business as b ON b.TenantId=tl.BusinessTenantId
							INNER JOIN am.Tenant AS t ON t.ID = b.TenantId
							INNER JOIN be.BADelivery AS d ON d.TenantId = d.TenantId
							INNER JOIN be.BACustomer AS c ON c.ID  = d.CustomerId							
                            INNER JOIN ap.PublisherAppSetting AS pas ON p.TenantId=pas.TenantId
                            INNER JOIN am.App as a ON pas.AppId=a.ID
                            INNER JOIN am.TenantUserAppLinking as tual ON tual.AppId=a.ID
                            INNER JOIN am.TenantUser as tu ON tu.ID=tual.TenantUserId							
                            Where tl.BusinessTenantId=@BusinessTenantId 
							AND tual.TenantUserId=@UserId AND tual.AppId= @AppId
							AND d.ID = @DeliveryId AND tual.UserType=@UserType AND tual.Deleted = 0";
            SqlParameter entityIdParam = new SqlParameter("@DeliveryId", entityId);
            SqlParameter tenantIdParam = new SqlParameter("@BusinessTenantId", tenantId);
            SqlParameter userIdParam = new SqlParameter("@UserId", userId);
            SqlParameter userTypeparam = new SqlParameter("@UserType", userType);
            SqlParameter appIdparam = new SqlParameter("@AppId", appId);
            BusinessNotesNotificationDTO notes = await GetQueryEntityAsync<BusinessNotesNotificationDTO>(query, new object[] { entityIdParam, tenantIdParam, userIdParam, userTypeparam, appIdparam });
            return notes;
        }

        public async Task<BusinessNotesNotificationDTO> GetBusinessInfoWithDraftDeliveryByEntityId(Guid entityId, Guid tenantId, Guid appId, Guid userId, int userType) {
            string query = @"SELECT DISTINCT  pas.Name AS 'AppName',  a.AppKey,tu.FullName AS UserName,t.SubDomainName,
                            p.Name AS 'PublisherName', b.TenantId AS 'BusinessTenantId',d.ERPDocNum, c.CustomerName,c.ERPCustomerKey AS CustomerId,
                            b.Name AS 'BusinessName',p.Copyright, tu.Id AS 'UserId', a.Id AS 'AppId',tual.UserType							
                            FROM ap.Publisher AS p
                            INNER JOIN am.TenantLinking AS tl ON p.TenantId=tl.PublisherTenantId
                            INNER JOIN ap.Business as b ON b.TenantId=tl.BusinessTenantId
							INNER JOIN am.Tenant AS t ON t.ID = b.TenantId
							INNER JOIN be.BAASN AS d ON d.TenantId = d.TenantId
							INNER JOIN be.BACustomer AS c ON c.ID  = d.CustomerId							
                            INNER JOIN ap.PublisherAppSetting AS pas ON p.TenantId=pas.TenantId
                            INNER JOIN am.App as a ON pas.AppId=a.ID
                            INNER JOIN am.TenantUserAppLinking as tual ON tual.AppId=a.ID
                            INNER JOIN am.TenantUser as tu ON tu.ID=tual.TenantUserId							
                            Where tl.BusinessTenantId=@BusinessTenantId 
							AND tual.TenantUserId=@UserId AND tual.AppId= @AppId
							AND d.ID = @DeliveryId AND tual.UserType=@UserType AND tual.Deleted = 0";
            SqlParameter entityIdParam = new SqlParameter("@DeliveryId", entityId);
            SqlParameter tenantIdParam = new SqlParameter("@BusinessTenantId", tenantId);
            SqlParameter userIdParam = new SqlParameter("@UserId", userId);
            SqlParameter userTypeparam = new SqlParameter("@UserType", userType);
            SqlParameter appIdparam = new SqlParameter("@AppId", appId);
            BusinessNotesNotificationDTO notes = await GetQueryEntityAsync<BusinessNotesNotificationDTO>(query, new object[] { entityIdParam, tenantIdParam, userIdParam, userTypeparam, appIdparam });
            return notes;
        }

        public async Task<BusinessNotesNotificationDTO> GetBusinessInfoWithContractByEntityId(Guid entityId, Guid tenantId, Guid appId, Guid userId, int userType) {
            string query = @"SELECT DISTINCT  pas.Name AS 'AppName',  a.AppKey,tu.FullName AS UserName,t.SubDomainName,
                            p.Name AS 'PublisherName', b.TenantId AS 'BusinessTenantId',bac.ERPDocNum, c.CustomerName,c.ERPCustomerKey AS CustomerId,
                            b.Name AS 'BusinessName',p.Copyright, tu.Id AS 'UserId', a.Id AS 'AppId',tual.UserType							
                            FROM ap.Publisher AS p
                            INNER JOIN am.TenantLinking AS tl ON p.TenantId=tl.PublisherTenantId
                            INNER JOIN ap.Business as b ON b.TenantId=tl.BusinessTenantId
							INNER JOIN am.Tenant AS t ON t.ID = b.TenantId
							INNER JOIN be.BAContract AS bac ON bac.TenantId = bac.TenantId
							INNER JOIN be.BACustomer AS c ON c.ID  = bac.CustomerId							
                            INNER JOIN ap.PublisherAppSetting AS pas ON p.TenantId=pas.TenantId
                            INNER JOIN am.App as a ON pas.AppId=a.ID
                            INNER JOIN am.TenantUserAppLinking as tual ON tual.AppId=a.ID
                            INNER JOIN am.TenantUser as tu ON tu.ID=tual.TenantUserId							
                            Where tl.BusinessTenantId=@BusinessTenantId 
							AND tual.TenantUserId=@UserId AND tual.AppId= @AppId
							AND bac.ID = @ContractId AND tual.UserType=@UserType AND tual.Deleted = 0";
            SqlParameter entityIdParam = new SqlParameter("@ContractId", entityId);
            SqlParameter tenantIdParam = new SqlParameter("@BusinessTenantId", tenantId);
            SqlParameter userIdParam = new SqlParameter("@UserId", userId);
            SqlParameter userTypeparam = new SqlParameter("@UserType", userType);
            SqlParameter appIdparam = new SqlParameter("@AppId", appId);
            BusinessNotesNotificationDTO notes = await GetQueryEntityAsync<BusinessNotesNotificationDTO>(query, new object[] { entityIdParam, tenantIdParam, userIdParam, userTypeparam, appIdparam });
            return notes;
        }
    }
}
