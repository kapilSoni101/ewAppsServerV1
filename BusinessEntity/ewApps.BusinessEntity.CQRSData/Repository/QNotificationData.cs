using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using Microsoft.EntityFrameworkCore;

namespace ewApps.BusinessEntity.QData {
    public class QNotificationData:IQNotificationData {

        #region Local Variable

        QBusinessEntityDbContext _qBusinessEntityDbContext;

        #endregion Local Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="QBizPaymentNotificationRecipientData"/> class.
        /// </summary>
        public QNotificationData(QBusinessEntityDbContext qBusinessEntityDbContext) {
            _qBusinessEntityDbContext = qBusinessEntityDbContext;
        }

        #endregion Constructor

        public async Task<ARInvoiceNotificationDTO> GetARInvoiceDetailByInvoiceERPKeyAsync(string invoiceERPKey, Guid businessTenantId, string appKey, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT DISTINCT
                            I.ID AS 'InvoiceId', i.ERPARInvoiceKey, i.AppliedAmount, i.TotalPaymentDue, i.LocalCurrency, i.PostingDate, i.DocumentDate, i.DueDate, tu.FullName, tu.IdentityNumber AS 'UserIdentityNo' , 
                            t.SubDomainName, c.CustomerName, c.ERPCustomerKey, p.Name AS 'PublisherName', b.Name as 'BusinessName', p.Copyright, b.DateTimeFormat, a.ID AS 'AppId', a.AppKey
                            , b.TenantId As 'BusinessTenantId', pas.Name AS 'AppName', i.UpdatedOn, p.TenantId AS 'PublisherTenantId',c.BusinessPartnerTenantId,b.TimeZone
                            FROM be.BAARInvoice as i
                            INNER JOIN be.BACustomer as c on i.CustomerId=c.ID
                            INNER JOIN am.TenantUser as tu on i.CreatedBy=tu.ID
                            INNER JOIN am.Tenant as t on i.TenantId=t.ID
                            INNER JOIN am.TenantLinking as tl on i.TenantId=tl.BusinessTenantId
                            INNER JOIN ap.PublisherAppSetting as pas ON pas.TenantId=tl.PublisherTenantId
                            INNER JOIN ap.Publisher as p on pas.TenantId=p.TenantId
                            INNER JOIN ap.Business as b on b.TenantId=i.TenantId
                            INNER JOIN am.App as a ON pas.AppId=a.ID
                            WHERE i.ERPARInvoiceKey=@ERPARInvoiceKey AND i.TenantId=@BusinessTenantId 
                            AND tl.BusinessPartnerTenantId IS NOT NULL AND a.AppKey=@AppKey ";

            SqlParameter invoiceERPKeyParam = new SqlParameter("ERPARInvoiceKey", invoiceERPKey);
            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter appKeyParam = new SqlParameter("AppKey", appKey);

            return await _qBusinessEntityDbContext.ARInvoiceNotificationDTOQuery.FromSql(sql, new object[] { invoiceERPKeyParam, businessTenantIdParam, appKeyParam }).FirstAsync(cancellationToken);

        }

        public async Task<ASNNotificationDTO> GetASNDetailByASNERPKeyAsync(string aSNERPKey, Guid businessTenantId, string appKey, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT DISTINCT
                           asn.ID, asn.ERPASNKey,tu.IdentityNumber AS 'UserIdentityNo', p.TenantId As 'PublisherTenantId',asn.ShipDate,asn.ExpectedDate,
                            t.SubDomainName, c.CustomerName, c.ERPCustomerKey, p.Name AS 'PublisherName', b.Name as 'BusinessName', p.Copyright,b.DateTimeFormat,b.TimeZone,
                            asn.TotalAmount,asn.TrackingNo,asn.PackagingSlipNo,asn.ShipmentTypeText,ShipmentPlan,tu.FullName as UserName,a.AppKey,a.ID AS 'AppId', b.TenantId As 'BusinessTenantId'
                            FROM be.BAASN as asn
                            INNER JOIN be.BACustomer as c on asn.CustomerId=c.ID
                            INNER JOIN am.TenantUser as tu on asn.CreatedBy=tu.ID
                            INNER JOIN am.Tenant as t on asn.TenantId=t.ID
                            INNER JOIN am.TenantLinking as tl on asn.TenantId=tl.BusinessTenantId
                            INNER JOIN ap.PublisherAppSetting as pas ON pas.TenantId=tl.PublisherTenantId
                            INNER JOIN ap.Publisher as p on pas.TenantId=p.TenantId
                            INNER JOIN ap.Business as b on b.TenantId=asn.TenantId
                            INNER JOIN am.App as a ON pas.AppId=a.ID
                            WHERE asn.ERPASNKey=@ERPASNKey AND asn.TenantId=@BusinessTenantId 
                            AND tl.BusinessPartnerTenantId IS NOT NULL AND a.AppKey=@AppKey";

            SqlParameter invoiceERPKeyParam = new SqlParameter("ERPASNKey", aSNERPKey);
            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter appKeyParam = new SqlParameter("AppKey", appKey);

            return await _qBusinessEntityDbContext.ASNNotificationDTOQuery.FromSql(sql, new object[] { invoiceERPKeyParam, businessTenantIdParam, appKeyParam }).FirstAsync(cancellationToken);

        }

        public async Task<ContractNotificationDTO> GetContractDetailByContractERPKeyAsync(string contractERPKey, Guid businessTenantId, string appKey, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT DISTINCT
                           con.ID, con.ERPContractKey,tu.IdentityNumber AS 'UserIdentityNo' ,b.TenantId AS 'BusinessTenantId',p.TenantId as PublisherTenantId,b.DateTimeFormat,b.TimeZone,
                            t.SubDomainName, c.CustomerName, c.ERPCustomerKey, p.Name AS 'PublisherName', b.Name as 'BusinessName', p.Copyright,
                            tu.FullName as UserName,a.AppKey,a.ID AS 'AppId',con.StartDate,con.EndDate,con.TerminationDate,con.SigningDate,con.Description
                            FROM be.BAContract as con
                            INNER JOIN be.BACustomer as c on con.CustomerId=c.ID
                            INNER JOIN am.TenantUser as tu on con.CreatedBy=tu.ID
                            INNER JOIN am.Tenant as t on con.TenantId=t.ID
                            INNER JOIN am.TenantLinking as tl on con.TenantId=tl.BusinessTenantId
                            INNER JOIN ap.PublisherAppSetting as pas ON pas.TenantId=tl.PublisherTenantId
                            INNER JOIN ap.Publisher as p on pas.TenantId=p.TenantId
                            INNER JOIN ap.Business as b on b.TenantId=con.TenantId
                            INNER JOIN am.App as a ON pas.AppId=a.ID
                            WHERE con.ERPContractKey=@ERPContractKey AND con.TenantId=@BusinessTenantId 
                            AND tl.BusinessPartnerTenantId IS NOT NULL AND a.AppKey=@AppKey";

            SqlParameter invoiceERPKeyParam = new SqlParameter("ERPContractKey", contractERPKey);
            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter appKeyParam = new SqlParameter("AppKey", appKey);

            return await _qBusinessEntityDbContext.ContractNotificationDTOQuery.FromSql(sql, new object[] { invoiceERPKeyParam, businessTenantIdParam, appKeyParam }).FirstAsync(cancellationToken);

        }

        public async Task<DeliveryNotificationDTO> GetDeliveryDetailByDeliveryERPKeyAsync(string deliveryERPKey, Guid businessTenantId, string appKey, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT DISTINCT
                           d.ID, d.ERPDeliveryKey,tu.IdentityNumber AS 'UserIdentityNo', p.TenantId As 'PublisherTenantId',
                            t.SubDomainName, c.CustomerName, c.ERPCustomerKey, p.Name AS 'PublisherName', b.Name as 'BusinessName', p.Copyright,b.DateTimeFormat,b.TimeZone,
                            tu.FullName as UserName,a.AppKey,a.ID AS 'AppId', b.TenantId As 'BusinessTenantId',
							 d.TotalPaymentDue,d.TrackingNo,d.ShippingTypeText,d.DeliveryDate
                            FROM be.BADelivery as d
                            INNER JOIN be.BACustomer as c on d.CustomerId=c.ID
                            INNER JOIN am.TenantUser as tu on d.CreatedBy=tu.ID
                            INNER JOIN am.Tenant as t on d.TenantId=t.ID
                            INNER JOIN am.TenantLinking as tl on d.TenantId=tl.BusinessTenantId
                            INNER JOIN ap.PublisherAppSetting as pas ON pas.TenantId=tl.PublisherTenantId
                            INNER JOIN ap.Publisher as p on pas.TenantId=p.TenantId
                            INNER JOIN ap.Business as b on b.TenantId=d.TenantId
                            INNER JOIN am.App as a ON pas.AppId=a.ID
                            WHERE d.ERPDeliveryKey=@ERPDeliveryKey AND d.TenantId=@BusinessTenantId 
                            AND tl.BusinessPartnerTenantId IS NOT NULL AND a.AppKey=@AppKey";

            SqlParameter invoiceERPKeyParam = new SqlParameter("ERPDeliveryKey", deliveryERPKey);
            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter appKeyParam = new SqlParameter("AppKey", appKey);

            return await _qBusinessEntityDbContext.DeliveryNotificationDTOQuery.FromSql(sql, new object[] { invoiceERPKeyParam, businessTenantIdParam, appKeyParam }).FirstAsync(cancellationToken);

        }

        public async Task<ARInvoiceNotificationDTO> GetARInvoiceDetailByInvoiceIdAsync(Guid invoiceId, Guid businessTenantId, string appKey, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT DISTINCT
                            I.ID AS 'InvoiceId', i.ERPARInvoiceKey, i.AppliedAmount, i.TotalPaymentDue, i.LocalCurrency, i.PostingDate, i.DocumentDate, i.DueDate, tu.FullName, tu.IdentityNumber AS 'UserIdentityNo' , 
                            t.SubDomainName, c.CustomerName, c.ERPCustomerKey, p.Name AS 'PublisherName', b.Name as 'BusinessName', p.Copyright, b.DateTimeFormat, a.ID AS 'AppId', a.AppKey
                            , b.TenantId As 'BusinessTenantId', pas.Name AS 'AppName', i.UpdatedOn, p.TenantId AS 'PublisherTenantId',b.TimeZone
                            FROM be.BAARInvoice as i
                            INNER JOIN be.BACustomer as c on i.CustomerId=c.ID
                            INNER JOIN am.TenantUser as tu on i.CreatedBy=tu.ID
                            INNER JOIN am.Tenant as t on i.TenantId=t.ID
                            INNER JOIN am.TenantLinking as tl on i.TenantId=tl.BusinessTenantId
                            INNER JOIN ap.PublisherAppSetting as pas ON pas.TenantId=tl.PublisherTenantId
                            INNER JOIN ap.Publisher as p on pas.TenantId=p.TenantId
                            INNER JOIN ap.Business as b on b.TenantId=i.TenantId
                            INNER JOIN am.App as a ON pas.AppId=a.ID
                            where i.ID=@InvoiceId AND i.TenantId=@BusinessTenantId 
                            AND tl.BusinessPartnerTenantId IS NOT NULL AND a.AppKey=@AppKey ";

            SqlParameter invoiceERPKeyParam = new SqlParameter("InvoiceId", invoiceId);
            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter appKeyParam = new SqlParameter("AppKey", appKey);

            return await _qBusinessEntityDbContext.ARInvoiceNotificationDTOQuery.FromSql(sql, new object[] { invoiceERPKeyParam, businessTenantIdParam, appKeyParam }).FirstAsync(cancellationToken);

        }

        public async Task<NotificationCommonDetailDTO> GetNotificationCommonDetailDTOAsync(Guid businessTenantId, string appKey, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT t.SubDomainName,p.Name AS 'PublisherName', b.Name AS 'BusinessName', p.Copyright, 
                            b.DateTimeFormat, pas.AppId, a.AppKey, tl.BusinessTenantId, pas.Name AS 'AppName', p.TenantId AS 'PublisherTenantId' 
                            FROM ap.Business as b
                            INNER JOIN am.Tenant as t on t.ID=b.TenantId
                            INNER JOIN am.TenantLinking as tl ON b.TenantId=tl.BusinessTenantId
                            INNER JOIN ap.Publisher as p on tl.PublisherTenantId=p.TenantId
                            inner join ap.PublisherAppSetting as pas on p.TenantId=pas.TenantId
                            INNER JOIN am.App as a on pas.AppId=a.ID
                            Where b.TenantId=@BusinessTenantID AND tl.BusinessPartnerTenantId IS NULL
                            and a.AppKey=@AppKey";

            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantID", businessTenantId);
            SqlParameter appKeyParam = new SqlParameter("AppKey", appKey);
            return await _qBusinessEntityDbContext.NotificationCommonDetailDTOQuery.FromSql(sql, new object[] { businessTenantIdParam, appKeyParam }).SingleAsync(cancellationToken);
        }

        public async Task<List<AppInfoDTO>> GetAppListByBusinessTenantIdAsync(Guid publisherTenantId, Guid businessTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            //string sql = @"Select a.ID, a.AppKey, a.Active, a.IdentityNumber, a.Name, a.ThemeId FROM am.App AS a
            //                INNER JOIN ap.TenantAppLinking AS tal ON a.ID=tal.AppId
            //                Where tal.TenantId=@BusinessTenantId";

            string sql = @"Select a.ID, a.AppKey, pas.Active, a.IdentityNumber, pas.Name, pas.ThemeId,CAST(0 as  bigint ) as 'PermissionBitMask'
                FROM ap.TenantAppLinking as tal 
INNER JOIN ap.PublisherAppSetting as pas on tal.AppId=pas.AppId
INNER JOIN am.App as a on pas.AppId=a.ID
WHERE tal.TenantId=@BusinessTenantId AND tal.Deleted=0 AND pas.Active=1 AND pas.TenantId=@PublisherTenantId AND a.Active=1";

            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter publisherTenantIdParam = new SqlParameter("PublisherTenantId", publisherTenantId);
            return await _qBusinessEntityDbContext.AppInfoDTOQuery.FromSql(sql, new object[] { publisherTenantIdParam, businessTenantIdParam }).ToListAsync(cancellationToken);
        }

        public async Task<List<AppInfoDTO>> GetAppListByCustomerTenantIdAsync(Guid publisherTenantId, Guid customerTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            //string sql = @"Select a.ID, a.AppKey, a.Active, a.IdentityNumber, a.Name, a.ThemeId FROM am.App AS a
            //                INNER JOIN ap.TenantAppLinking AS tal ON a.ID=tal.AppId
            //                Where tal.TenantId=@CustomerTenantId";
            string sql = @"Select a.ID, a.AppKey, pas.Active, a.IdentityNumber, pas.Name, pas.ThemeId,,CAST(0 as  bigint ) as 'PermissionBitMask'
FROM ap.TenantAppLinking as tal 
INNER JOIN ap.PublisherAppSetting as pas on tal.AppId=pas.AppId
INNER JOIN am.App as a on pas.AppId=a.ID
WHERE tal.TenantId=@BusinessPartnerTenantId AND tal.Deleted=0 AND pas.Active=1 AND pas.TenantId=@PublisherTenantId AND a.Active=1";
            SqlParameter partnerTenantIdParam = new SqlParameter("BusinessPartnerTenantId", customerTenantId);
            SqlParameter publisherTenantIdParam  = new SqlParameter("PublisherTenantId", publisherTenantId);
            return await _qBusinessEntityDbContext.AppInfoDTOQuery.FromSql(sql, new object[] { partnerTenantIdParam, publisherTenantIdParam }).ToListAsync(cancellationToken);
        }

        public async Task<CustomerNotificationDTO> GetCustomerDetailByCustomerERPKeyAsync(string customerERPKey, Guid businessTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT c.ID AS 'CustomerId', p.Name AS 'PublisherName', b.Name AS 'BusinessName', c.CustomerName, c.ERPCustomerKey, tu.FullName AS 'CreatedByName', tu.IdentityNumber AS 'CreatedByNo',
                            t.SubDomainName, p.Copyright, tuUpdated.FullName as 'UpdatedByName', tuUpdated.IdentityNumber as 'UpdatedByNo', b.DateTimeFormat,c.CreatedOn,c.UpdatedOn,p.TenantId as 'PublisherTenantId',
                            tl.BusinessTenantId,b.TimeZone
                            FROM be.BACustomer as c 
                            INNER JOIN ap.Customer as apc on c.BusinessPartnerTenantId=apc.BusinessPartnerTenantId
                            INNER JOIN am.TenantLinking as tl on c.BusinessPartnerTenantId=tl.BusinessPartnerTenantId
                            INNER JOIN ap.Publisher as p on tl.PublisherTenantId=p.TenantId
                            INNER JOIN ap.Business as b on tl.BusinessTenantId=b.TenantId
                            INNER JOIN am.TenantUser as tu on c.CreatedBy=tu.ID
                            INNER JOIN am.TenantUser as tuUpdated on c.UpdatedBy=tuUpdated.ID
                            INNER JOIN am.Tenant as t ON b.TenantId=t.ID
                            WHERE c.ERPCustomerKey=@CustomerERPKey AND c.TenantId=@BusinessTenantId ";

            SqlParameter customerERPKeyParam = new SqlParameter("CustomerERPKey", customerERPKey);
            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);

            return await _qBusinessEntityDbContext.CustomerNotificationDTOQuery.FromSql(sql, new object[] { customerERPKeyParam, businessTenantIdParam }).FirstAsync(cancellationToken);

        }

        public async Task<SONotificationDTO> GetSODetailBySOERPKeyAsync(string soERPKey, Guid businessTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT DISTINCT
                            so.ID AS 'SalesOrderId', so.ERPSalesOrderKey, so.TotalPaymentDue, so.LocalCurrency, so.PostingDate, so.DocumentDate, so.DeliveryDate, tu.FullName, tu.IdentityNumber AS 'UserIdentityNo' , 
                            t.SubDomainName, c.CustomerName, c.ERPCustomerKey, p.Name AS 'PublisherName', b.Name as 'BusinessName', p.Copyright, b.DateTimeFormat, b.TenantId As 'BusinessTenantId', so.CreatedOn,
                            p.TenantId AS 'PublisherTenantId',b.TimeZone
                            FROM be.BASalesOrder as so
                            INNER JOIN be.BACustomer as c on so.CustomerId=c.ID
                            INNER JOIN am.TenantUser as tu on so.CreatedBy=tu.ID
                            INNER JOIN am.Tenant as t on so.TenantId=t.ID
                            INNER JOIN am.TenantLinking as tl on so.TenantId=tl.BusinessTenantId
                            INNER JOIN ap.Publisher as p on tl.PublisherTenantId=p.TenantId
                            INNER JOIN ap.Business as b on b.TenantId=so.TenantId
                            where so.ERPSalesOrderKey=@ERPSalesOrderKey AND so.TenantId=@BusinessTenantId 
                            AND tl.BusinessPartnerTenantId IS NOT NULL";

            SqlParameter invoiceERPKeyParam = new SqlParameter("ERPSalesOrderKey", soERPKey);
            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);

            return await _qBusinessEntityDbContext.SONotificationDTOQuery.FromSql(sql, new object[] { invoiceERPKeyParam, businessTenantIdParam }).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
