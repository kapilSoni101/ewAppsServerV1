using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.BaseService {
    [Flags]
    public enum BusinessUserCustomerAppPermissionEnum:long {

        None = 0,

        ViewCustomers = 1,

        ManageCustomers = 2, // new

        ViewItemMaster = 4,

        ManageItemMaster = 8, // new 

        ViewPackageMaster = 16,

        ManagePackageMaster = 32,// new

        ViewSalesInquiries = 64,

        ViewSalesQuotations = 128,

        ManageSalesQuotations = 256,// new

        ViewSalesOrders = 512,

        ManageSalesOrders = 1024,// new

        ViewARInvoices = 2048,

        ManageARInvoices = 4096,// new

        ViewDeliveries = 8192,

        ManageDeliveries = 16384,// new

        ViewContractManagement = 32768,

        ManageContractManagement = 65536,// new

        ViewASN = 131072,

        ManageASN = 262144,// new

        AccessTransactionHistory = 524288,

        AllowPaymentActivities = 1048576,

        ManageCustomerPaymentInfo = 2097152,

        ManageBusinessPaymentInfo = 4194304,

        AccessShipmentHistory = 8388608,

        AllowShipmentActivities = 16777216,

        ManageShipmentInfo = 33554432,

        AccessReports = 67108864,

        ManageCustomerPortalSettings = 134217728,

        ViewCustomerTickets = 268435456,

        ManageCustomerTickets = 536870912,

        All = None | ViewCustomers | ManageCustomers | ViewItemMaster | ManageItemMaster | ViewPackageMaster | ManagePackageMaster | ViewSalesInquiries | ViewSalesQuotations |
        ManageSalesQuotations | ViewSalesOrders | ManageSalesOrders | ViewARInvoices | ManageARInvoices | ViewDeliveries | ManageDeliveries |
        ViewContractManagement | ManageContractManagement | ViewASN | ManageASN | AccessTransactionHistory | AllowPaymentActivities | ManageCustomerPaymentInfo | ManageBusinessPaymentInfo | AccessShipmentHistory | AllowShipmentActivities | ManageShipmentInfo | AccessReports |
        ManageCustomerPortalSettings | ViewCustomerTickets | ManageCustomerTickets
    }
}
