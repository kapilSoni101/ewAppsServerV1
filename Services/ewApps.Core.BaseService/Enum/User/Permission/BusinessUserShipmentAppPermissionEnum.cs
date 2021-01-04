using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.BaseService {
    [Flags]
    public enum BusinessUserShipmentAppPermissionEnum:long {

        /// <summary>
        /// Dont have any permission.
        /// </summary>
        None = 0,

        /// <summary>
        /// View Partners permission.
        /// </summary>
        ViewPartners = 1,

        /// <summary>
        /// Manage Partners permission.
        /// </summary>
        ManagePartners = 2,

        /// <summary>
        /// View sales order permission.
        /// </summary>
        ViewSalesOrders = 4,

        /// <summary>
        /// Manage sales order permission.
        /// </summary>
        ManageSalesOrders = 8,

        /// <summary>
        /// View access permission.
        /// </summary>
        AccessShippingHistory = 16,

        /// <summary>
        /// Manage Shipment Info permissions.
        /// </summary>
        ManageShipmentInfo = 32,

        /// <summary>
        /// Ship Activity  permissions
        /// </summary>
        ShipActivity = 64,

        /// <summary>
        ///  View delivery permission
        /// </summary>
        ViewDeliveries = 128,

        /// <summary>
        /// Manage delivery permission.
        /// </summary>
        ManageDeliveries = 256,

        /// <summary>
        /// View item master permission.
        /// </summary>
        ViewItemsMaster = 512,

        /// <summary>
        /// Manage item master permission.
        /// </summary>
        ManagerItemsMaster = 1024,

        /// <summary>
        /// View packaging master permission.
        /// </summary>
        ViewPackagingMaster = 2048,

        /// <summary>
        /// Manahe packaging master permission.
        /// </summary>
        ManagePackagingMaster = 4096,

        /// <summary>
        /// Permission to access reports
        /// </summary>
        AccessReports = 8192,

        /// <summary>
        /// Manage business portal settings.
        /// </summary>
        ManageBusinessPortalSettings = 16384,

        /// <summary>
        /// Have all permission.
        /// </summary>
        All = None | ViewPartners | ManagePartners | ViewSalesOrders | ManageSalesOrders
                   | AccessShippingHistory | ManageShipmentInfo | ShipActivity | ViewDeliveries
                   | ManageDeliveries | ViewItemsMaster | ManagerItemsMaster | ViewPackagingMaster
                   | ManagePackagingMaster | AccessReports | ManageBusinessPortalSettings
    }
}
