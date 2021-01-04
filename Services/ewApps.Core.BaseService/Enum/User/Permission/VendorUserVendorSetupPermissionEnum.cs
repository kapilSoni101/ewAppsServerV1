using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.BaseService {

    [Flags]
    public enum VendorUserVendorSetupPermissionEnum:long {

    None = 0,
    ViewVendor = 1,
    ManageVendor = 2,
    ViewItemMaster = 4,
    ManageItemMaster = 8,
    ViewPurchaseOrder = 16,
    ManagePurchaseOrder = 32,
    ViewPurchaseLine = 64,
    ViewApInvoice = 128,
    ManageApInvoice = 256,
    ViewVendorASN = 512,
    ManageVendorASN = 1024,
    AccessReport = 2048,
    ViewPortalSetting = 4096,
    ManagePortalSetting = 8192,
    ViewTicket = 16384,
    ManageTicket = 32768,

    /// <summary>
    /// Have aall permission
    /// </summary>
    All = None | ViewVendor | ManageVendor | ViewItemMaster | ManageItemMaster | ViewPurchaseOrder | ManagePurchaseOrder | ViewPurchaseLine |
    ViewApInvoice | ManageApInvoice | ViewVendorASN | ManageVendorASN | AccessReport | ViewPortalSetting | ManagePortalSetting |
    ViewTicket | ManageTicket
  }
}
