/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 19 December 2018
 */
using ewApps.Report.Common;
using ewApps.Report.QData;
using ewApps.Report.QDS;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ewApps.Report.DI {

    public static class ReportServiceCollection {

        public static IServiceCollection AddReportDependency(this IServiceCollection services, IConfiguration Configuration) {
            AddReportDataDependency(services);
            AddReportDataServiceDependency(services);
            AddOtherDependency(services, Configuration);



            return services;
        }

        public static IServiceCollection AddReportDataDependency(this IServiceCollection services) {
            services.AddDbContext<QReportDbContext>();

            #region Report Dependency
            services.AddScoped<IQApplicationReportRepository, QApplicationReportRepository>();
            services.AddScoped<IQInvoiceReportRepository, QInvoiceReportRepository>();
            services.AddScoped<IQCustomerReportRepository, QCustomerReportRepository>();
            services.AddScoped<IQSupportTicketReportRepository, QSupportTicketReportRepository>();
            services.AddScoped<IQAppUserReportRepository, QAppUserReportRepository>();
            services.AddScoped<IQSubcriptionReportRepository, QSubcriptionReportRepository>();
            services.AddScoped<IQTenantReportRepository, QTenantReportRepository>();
            services.AddScoped<IQPaymentReportRepository, QPaymentReportRepository>();
            services.AddScoped<IQPublisherReportRepository, QPublisherReportRepository>();
            services.AddScoped<IQTransactionJournalReportRepository, QTransactionJournalReportRepository>();
            services.AddScoped<IQSalesQuotationsReportRepository, QSalesQuotationsReportRepository>();
            services.AddScoped<IQSalesOrdersReportRepository, QSalesOrdersReportRepository>();
            services.AddScoped<IQDeliveriesReportRepository, QDeliveriesReportRepository>();
            services.AddScoped<IQVendorASNReportRepository, QVendorASNReportRepository>();
            services.AddScoped<IQItemMasterReportRepository, QItemMasterReportRepository>();
            services.AddScoped<IQOpenPurchaseLineReportRepository, QOpenPurchaseLineReportRepository>();
            services.AddScoped<IQPurchaseOrdersReportRepository, QPurchaseOrdersReportRepository>();
            #endregion

            #region Dashboard Dependency
            services.AddScoped<IQPlatDashboardRepository, QPlatDashboardRepository>();
            services.AddScoped<IQPubDashboardRepository, QPubDashboardRepository>();
            services.AddScoped<IQBizPayDashboardRepository, QBizPayDashboardRepository>();
            services.AddScoped<IQCustPayDashboardRepository, QCustPayDashboardRepository>();
            services.AddScoped<IQBizCustDashboardRepository, QBizCustDashboardRepository>();
            services.AddScoped<IQCustPortCustDashboardRepository, QCustPortCustDashboardRepository>();
            services.AddScoped<IQBizVendDashboardRepository, QBizVendDashboardRepository>();
            services.AddScoped<IQVendDashboardRepository, QVendDashboardRepository>();

            #endregion

            return services;
        }

        public static IServiceCollection AddReportDataServiceDependency(this IServiceCollection services) {
            
            #region Report Dependency
            services.AddScoped<IQInvoiceReportDS, QInvoiceReportDS>();
            services.AddScoped<IQCustomerReportDS, QCustomerReportDS>();
            services.AddScoped<IQApplicationReportDS, QApplicationReportDS>();
            services.AddScoped<IQSupportTicketReportDS, QSupportTicketReportDS>();
            services.AddScoped<IQAppUserReportDS, QAppUserReportDS>();
            services.AddScoped<IQSubcriptionReportDS, QSubcriptionReportDS>();
            services.AddScoped<IQTenantReportDS, QTenantReportDS>();
            services.AddScoped<IQPaymentReportDS, QPaymentReportDS>();
            services.AddScoped<IQPublisherReportDS, QPublisherReportDS>();
            services.AddScoped<IQTransactionJournalReportDS, QTransactionJournalReportDS>();
            services.AddScoped<IQSalesQuotationsReportDS, QSalesQuotationsReportDS>();
            services.AddScoped<IQSalesOrdersReportDS, QSalesOrdersReportDS>();
            services.AddScoped<IQDeliveriesReportDS, QDeliveriesReportDS>();
            services.AddScoped<IQVendorASNReportDS, QVendorASNReportDS>();
            services.AddScoped<IQItemMasterReportDS, QItemMasterReportDS>();
            services.AddScoped<IQOpenPurchaseLineReportDS, QOpenPurchaseLineReportDS>();
            services.AddScoped<IQPurchaseOrdersReportDS, QPurchaseOrdersReportDS>();
            
            #endregion

            #region Dashboard Dependency
            services.AddScoped<IQPlatDashboardDS, QPlatDashboardDS>();
            services.AddScoped<IQPubDashboardDS, QPubDashboardDS>();
            services.AddScoped<IQBizPayDashboardDS, QBizPayDashboardDS>();
            services.AddScoped<IQCustPayDashboardDS, QCustPayDashboardDS>();
            services.AddScoped<IQBizCustDashboardDS, QBizCustDashboardDS>();
            services.AddScoped<IQCustPortCustDashboardDS, QCustPortCustDashboardDS>();
            services.AddScoped<IQBizVendDashboardDS, QBizVendDashboardDS>();
            services.AddScoped<IQVendDashboardDS, QVendDashboardDS>();
            
            #endregion

            return services;
        }

        public static IServiceCollection AddOtherDependency(this IServiceCollection services, IConfiguration Configuration) {
            //// Report Config Settings  
            var reportSection = Configuration.GetSection("ReportAppSettings");
            services.Configure<ReportAppSettings>(reportSection);
           
            return services;
        }

       
    }
}
