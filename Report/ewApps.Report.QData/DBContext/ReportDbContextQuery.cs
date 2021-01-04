/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar<abadgujar@batchmaster.com>
 * Date: 2 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 2 September 2019
 */


using ewApps.Report.DTO;
using Microsoft.EntityFrameworkCore;

namespace ewApps.Report.QData {

    public partial class QReportDbContext {

        #region Dashboard

        /// <summary>
        /// This is use to get BACInvoiceStatusCountDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BACInvoiceStatusCountDTOQuery.
        /// </remarks>
        public DbQuery<BACInvoiceStatusCountDTO> BACInvoiceStatusCountDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get AppAndBusinessCountDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of AppAndBusinessCountDTOQuery.
        /// </remarks>
        public DbQuery<AppAndBusinessCountDTO> AppAndBusinessCountDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get BusinessAndSubscriptionCountDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusinessAndSubscriptionCountDTOQuery.
        /// </remarks>
        public DbQuery<BusinessAndSubscriptionCountDTO> BusinessAndSubscriptionCountDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get BusinessCountDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusinessCountDTOQuery.
        /// </remarks>
        public DbQuery<BusinessCountDTO> BusinessCountDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get ApplicationUserCountDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of ApplicationUserCountDTOQuery.
        /// </remarks>
        public DbQuery<ApplicationUserCountDTO> ApplicationUserCountDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get BusinessAddedCountAndMonthDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusinessAddedCountAndMonthDTOQuery.
        /// </remarks>
        public DbQuery<BusinessAddedCountAndMonthDTO> BusinessAddedCountAndMonthDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get RoleKeyCountDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of RoleKeyCountDTOQuery.
        /// </remarks>
        public DbQuery<BusinessNameAndSumCount> BusinessNameAndSumCountQuery {
            get;
            set;
        }



        /// <summary>
        /// This is use to get InoviceAndMonthNameDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of InoviceAndMonthNameDTOQuery.
        /// </remarks>
        public DbQuery<InoviceAndMonthNameDTO> InoviceAndMonthNameDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get PubDashboardAppBusinessAndSubcriptionCountQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PubDashboardAppBusinessAndSubcriptionCountQuery.
        /// </remarks>
        public DbQuery<PubDashboardAppBusinessAndSubcriptionCount> PubDashboardAppBusinessAndSubcriptionCountQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get UpComingPaymentDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of UpComingPaymentDTOQuery.
        /// </remarks>
        public DbQuery<UpComingPaymentDTO> UpComingPaymentDTOQuery {
            get;
            set;
        }


        /// <summary>
        /// This is use to get RecentPaymentDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of RecentPaymentDTOQuery.
        /// </remarks>
        public DbQuery<RecentPaymentDTO> RecentPaymentDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get ApplicationPublisherCountDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of ApplicationPublisherCountQuery.
        /// </remarks>
        public DbQuery<ApplicationPublisherCountDTO> ApplicationPublisherCountQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get PublisherTenantCountDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PublisherTenantCountDTOQuery.
        /// </remarks>
        public DbQuery<PublisherTenantCountDTO> PublisherTenantCountDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get PlatDashboardAppBusinessAndPublisherCount view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PlatDashboardAppBusinessAndPublisherCountDTOQuery.
        /// </remarks>
        public DbQuery<PlatDashboardAppBusinessAndPublisherCount> PlatDashboardAppBusinessAndPublisherCountDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get PlatAppAndBusinessCountDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PlatAppAndBusinessCountDTO.
        /// </remarks>
        public DbQuery<PlatAppAndBusinessCountDTO> PlatAppAndBusinessCountDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get AppDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of AppDTO.
        /// </remarks>
        public DbQuery<AppDTO> AppDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get RecentInvoicesDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of RecentInvoicesDTO.
        /// </remarks>
        public DbQuery<RecentInvoicesDTO> RecentInvoicesDTOQuery {
            get;
            set;
        }

        #region Customer App
        /// <summary>
        /// This is use to get DeliveriesStatusCountDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of DeliveriesStatusCountDTO.
        /// </remarks>
        public DbQuery<DeliveriesStatusCountDTO> DeliveriesStatusCountDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get UpcomingDeliveriesDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of UpcomingDeliveriesDTO.
        /// </remarks>
        public DbQuery<UpcomingDeliveriesDTO> UpcomingDeliveriesDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get RecentDeliveriesDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of RecentDeliveriesDTO.
        /// </remarks>
        public DbQuery<RecentDeliveriesDTO> RecentDeliveriesDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get RecentSalesOrdersDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of RecentSalesOrdersDTO.
        /// </remarks>
        public DbQuery<RecentSalesOrdersDTO> RecentSalesOrdersDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get RecentSalesQuotationsDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of RecentSalesQuotationsDTO.
        /// </remarks>
        public DbQuery<RecentSalesQuotationsDTO> RecentSalesQuotationsDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get SalesQuotationsAndOrdersStatusCountDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of SalesQuotationsAndOrdersStatusCountDTO.
        /// </remarks>
        public DbQuery<SalesQuotationsAndOrdersStatusCountDTO> SalesQuotationsAndOrdersStatusCountDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get RecentPurchaseQuotationsFCDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of RecentPurchaseQuotationsFCDTO.
        /// </remarks>
        public DbQuery<RecentPurchaseQuotationsFCDTO> RecentPurchaseQuotationsFCDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get RecentSalesOrdersFCDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of RecentSalesOrdersFCDTO.
        /// </remarks>
        public DbQuery<RecentSalesOrdersFCDTO> RecentSalesOrdersFCDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get UpcomingDeliveriesFCDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of UpcomingDeliveriesFCDTO.
        /// </remarks>
        public DbQuery<UpcomingDeliveriesFCDTO> UpcomingDeliveriesFCDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get RecentDeliveriesFCDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of RecentDeliveriesFCDTO.
        /// </remarks>
        public DbQuery<RecentDeliveriesFCDTO> RecentDeliveriesFCDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get DeliveriesStatusCountFCDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of DeliveriesStatusCountFCDTO.
        /// </remarks>
        public DbQuery<DeliveriesStatusCountFCDTO> DeliveriesStatusCountFCDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get PurchaseAndMonthNameDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PurchaseAndMonthNameDTO.
        /// </remarks>
        public DbQuery<PurchaseAndMonthNameFCDTO> PurchaseAndMonthNameDTOQuery {
            get;
            set;
        }

        

        #endregion

        #region Shipment DTOs

        /// <summary>
        /// This is use to get ValuableCustomerDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of ValuableCustomerDTO.
        /// </remarks>
        public DbQuery<ValuableCustomerDTO> ValuableCustomerDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get TotalDeliveriesMonthWiseDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of TotalDeliveriesMonthWiseDTO.
        /// </remarks>
        public DbQuery<TotalDeliveriesMonthWiseDTO> TotalDeliveriesMonthWiseDTOQuery {
            get;
            set;
        }


        /// <summary>
        /// This is use to get SaleOrderCreatedMonthWiseDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of SaleOrderCreatedMonthWiseDTO.
        /// </remarks>
        public DbQuery<SaleOrderCreatedMonthWiseDTO> SaleOrderCreatedMonthWiseDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get ShippingStatusDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of ShippingStatusDTO.
        /// </remarks>
        public DbQuery<ShippingStatusDTO> ShippingStatusDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get SalesOrderStatusDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of SalesOrderStatusDTO.
        /// </remarks>
        public DbQuery<SalesOrderStatusDTO> SalesOrderStatusDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get TotalSalesMonthWiseDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of TotalSalesMonthWiseDTO.
        /// </remarks>
        public DbQuery<TotalSalesMonthWiseDTO> TotalSalesMonthWiseDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get UpcomingShipmentsDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of UpcomingShipmentsDTO.
        /// </remarks>
        public DbQuery<UpComingShipmentsDTO> UpcomingShipmentsDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get RecentShipmentsDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of RecentShipmentsDTO.
        /// </remarks>
        public DbQuery<RecentShipmentsDTO> RecentShipmentsDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get ShipmentServiceNameAndCountDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of ShipmentServiceNameAndCountDTO.
        /// </remarks>
        public DbQuery<ShipmentServiceNameAndCountDTO> ShipmentServiceNameAndCountDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get ShipmentBusinessNameAndSumCountQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of ShipmentBusinessNameAndSumCount.
        /// </remarks>
        public DbQuery<ShipmentBusinessNameAndSumCount> ShipmentBusinessNameAndSumCountQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get ShipmentBusinessAddedCountAndMonthDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of ShipmentBusinessAddedCountAndMonthDTO.
        /// </remarks>
        public DbQuery<ShipmentBusinessAddedCountAndMonthDTO> ShipmentBusinessAddedCountAndMonthDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get ShipmentAapNameAndBusinessCountDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of ShipmentAapNameAndBusinessCountDTO.
        /// </remarks>
        public DbQuery<ShipmentAapNameAndBusinessCountDTO> ShipmentAapNameAndBusinessCountDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get ShipmentBusinessAndSubscriptionCountDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of ShipmentBusinessAndSubscriptionCountDTO.
        /// </remarks>
        public DbQuery<ShipmentBusinessAndSubscriptionCountDTO> ShipmentBusinessAndSubscriptionCountDTOQuery {
            get;
            set;
        }


        #endregion    


        #endregion Dashboard

        #region Report

        #region Publisher Portal 

        // ==========================Publisher ============================

        /// <summary>
        /// This is use to get NameListQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of NameDTO.
        /// </remarks>
        public DbQuery<NameDTO> NameListQuery {
            get; set;
        }


        /// <summary>
        /// This is use to get ApplicationListQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of ApplicationReportDTO.
        /// </remarks>
        public DbQuery<ApplicationReportDTO> ApplicationListQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get TenantListQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of TenantReportDTO.
        /// </remarks>
        public DbQuery<TenantReportDTO> TenantListQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get SubscriptionListQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of SubscriptionReportDTO.
        /// </remarks>
        public DbQuery<SubscriptionReportDTO> SubscriptionListQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get PubSuportTicketListQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PubSupportTicketReportDTO.
        /// </remarks>
        public DbQuery<PubSupportTicketReportDTO> PubSuportTicketListQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get PubAppUserListQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PubAppUserReportDTO.
        /// </remarks>
        public DbQuery<PubAppUserReportDTO> PubAppUserListQuery {
            get; set;
        }
        #endregion

        #region Business Portal 

        #region Payment App
        // ==========================Business ============================
        /// <summary>
        /// This is use to get BizAppUserListQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BizAppUserReportDTO.
        /// </remarks>
        public DbQuery<BizAppUserReportDTO> BizAppUserListQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BizSupportTicketListQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BizSupportTicketReportDTO.
        /// </remarks>
        public DbQuery<BizSupportTicketReportDTO> BizSupportTicketListQuery {
            get; set;
        }


        /// <summary>
        /// This is use to get CustomerListQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of CustomerReportDTO.
        /// </remarks>
        public DbQuery<CustomerReportDTO> CustomerListQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BizInvoiceListQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BizInvoiceReportDTO.
        /// </remarks>
        public DbQuery<BizInvoiceReportDTO> BizInvoiceListQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BizPaymentRecListQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BizPaymentReceivedReportDTO.
        /// </remarks>
        public DbQuery<BizPaymentReceivedReportDTO> BizPaymentRecListQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BizCustomerWisePaymentListQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BizCustomerWisePaymentReportDTO.
        /// </remarks>
        public DbQuery<BizCustomerWisePaymentReportDTO> BizCustomerWisePaymentListQuery {
            get; set;
        }
        #endregion

        #region Customer App

        /// <summary>
        /// This is use to get BizCustSalesOrdersReportDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BizCustSalesOrdersReportDTO.
        /// </remarks>
        public DbQuery<BizCustSalesOrdersReportDTO> BizCustSalesOrdersReportDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BizCustSalesQuotationsReportDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BizCustSalesQuotationsReportDTO.
        /// </remarks>
        public DbQuery<BizCustSalesQuotationsReportDTO> BizCustSalesQuotationsReportDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BizCustSalesDeliveriesReportDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BizCustSalesDeliveriesReportDTO.
        /// </remarks>
        public DbQuery<BizCustSalesDeliveriesReportDTO> BizCustSalesDeliveriesReportDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get SalesOrdersItemsCountDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of SalesOrdersItemsCountDTO.
        /// </remarks>
        public DbQuery<SalesOrdersItemsCountFCDTO> SalesOrdersItemsCountDTOQuery {
            get; set;
        }



        #endregion

        #endregion

        #region Business Partner

        // ================================Partner ================================


        /// <summary>
        /// This is use to get PartItemMasterReportDTOuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PartItemMasterReportDTO.
        /// </remarks>
        public DbQuery<PartItemMasterReportDTO> PartItemMasterReportDTOuery {
            get; set;
        }

        #region Payment App
        /// <summary>
        /// This is use to get PartAppUserListQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PartAppUserReportDTO.
        /// </remarks>
        public DbQuery<PartAppUserReportDTO> PartAppUserListQuery {
            get; set;
        }


        /// <summary>
        /// This is use to get PartSupportTicketListQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PartSupportTicketReportDTO.
        /// </remarks>
        public DbQuery<PartSupportTicketReportDTO> PartSupportTicketListQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get PartInvoiceListQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PartInvoiceReportDTO.
        /// </remarks>
        public DbQuery<PartInvoiceReportDTO> PartInvoiceListQuery {
            get; set;
        }


        /// <summary>
        /// This is use to get PartPaymentListQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PartPaymentReportDTO.
        /// </remarks>
        public DbQuery<PartPaymentReportDTO> PartPaymentListQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get PartReportSupportTicketParamQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PartReportSupportTicketParamDTO.
        /// </remarks>
        public DbQuery<PartReportSupportTicketParamDTO> PartReportSupportTicketParamQuery {
            get; set;
        }
        #endregion

        #region Customer App

        /// <summary>
        /// This is use to get PartCustOrdersReportDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PartCustOrdersReportDTO.
        /// </remarks>
        public DbQuery<PartCustOrdersReportDTO> PartCustOrdersReportDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get PartCustQuotationsReportDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PartCustQuotationsReportDTO.
        /// </remarks>
        public DbQuery<PartCustQuotationsReportDTO> PartCustQuotationsReportDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get PartCustDeliveriesReportDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PartCustDeliveriesReportDTO.
        /// </remarks>
        public DbQuery<PartCustDeliveriesReportDTO> PartCustDeliveriesReportDTOQuery {
            get; set;
        }

        #endregion

        #region Vendor App
      

        /// <summary>
        /// This is use to get VendOpenLinesListDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of VendOpenLinesListDTO.
        /// </remarks>
        public DbQuery<VendOpenLinesListDTO> VendOpenLinesListDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get VendOrdersLineStatusDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of VendOrdersLineStatusDTO.
        /// </remarks>
        public DbQuery<VendOrdersLineStatusDTO> VendOrdersLineStatusDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get VendOrderStatusDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of VendOrderStatusDTO.
        /// </remarks>
        public DbQuery<VendOrderStatusDTO> VendOrderStatusDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get VendApInvoiceStatusDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of VendApInvoiceStatusDTO.
        /// </remarks>
        public DbQuery<VendApInvoiceStatusDTO> VendApInvoiceStatusDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get VendRecentAPInvoicesQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of VendRecentAPInvoices.
        /// </remarks>
        public DbQuery<VendRecentAPInvoices> VendRecentAPInvoicesQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get VendRecentPurchaseOrderQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of VendRecentPurchaseOrder.
        /// </remarks>
        public DbQuery<VendRecentPurchaseOrder> VendRecentPurchaseOrderQuery {
            get; set;
        }

        #endregion

        #endregion

        #region Platform Portal

        // ================================Platform ================================


        /// <summary>
        /// This is use to get PlatApplicationListQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PlatApplicationReportDTO.
        /// </remarks>
        public DbQuery<PlatApplicationReportDTO> PlatApplicationListQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get PlatAppUserListQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PlatAppUserReportDTO.
        /// </remarks>
        public DbQuery<PlatAppUserReportDTO> PlatAppUserListQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get PlatPublisherListQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PlatPublisherReportDTO.
        /// </remarks>
        public DbQuery<PlatPublisherReportDTO> PlatPublisherListQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get PlatSupportTicketListQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PlatSupportTicketReportDTO.
        /// </remarks>
        public DbQuery<PlatSupportTicketReportDTO> PlatSupportTicketListQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get AapNameAndBusinessCountDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of AapNameAndBusinessCountDTO.
        /// </remarks>
        public DbQuery<AapNameAndBusinessCountDTO> AapNameAndBusinessCountDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get TransactionJournalReportDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of TransactionJournalReportDTO.
        /// </remarks>
        public DbQuery<TransactionJournalReportDTO> TransactionJournalReportDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get PlatformTenantDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PlatformTenantDTO.
        /// </remarks>
        public DbQuery<PlatformTenantDTO> PlatformTenantDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get PlatSubscriptionReportDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PlatSubscriptionReportDTO.
        /// </remarks>
        public DbQuery<PlatSubscriptionReportDTO> PlatSubscriptionReportDTOQuery {
            get; set;
        }



        #endregion

        #region Vendor App

        /// <summary>
        /// This is use to get VendAPInvoicesReportDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of VendAPInvoicesReportDTO.
        /// </remarks>
        public DbQuery<VendAPInvoicesReportDTO> VendAPInvoicesReportDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get VendPurchaseOrdersReportDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of VendPurchaseOrdersReportDTO.
        /// </remarks>
        public DbQuery<VendPurchaseOrdersReportDTO> VendPurchaseOrdersReportDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get VendOpenPurchaseLineReportDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of VendOpenPurchaseLineReportDTO.
        /// </remarks>
        public DbQuery<VendOpenPurchaseLineReportDTO> VendOpenPurchaseLineReportDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get VendItemMasterReportDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of VendItemMasterReportDTO.
        /// </remarks>
        public DbQuery<VendItemMasterReportDTO> VendItemMasterReportDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get VendorASNReportDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of VendorASNReportDTO.
        /// </remarks>
        public DbQuery<VendorASNReportDTO> VendorASNReportDTOQuery {
            get; set;
        }

        


        #endregion

        #endregion Report
    }
}
