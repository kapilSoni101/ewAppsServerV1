using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Shipment.Common
{
  public enum ShipmentEnitityTypeEnum
  {
    ItemMaster = 1,
    PackageMaster = 2,
    SalesOrder = 3,
    SalesOrderItem = 4,
    SalesOrderPackage = 5,
    SalesOrderPkgDetail = 6,
    Shipment = 7,
    ShipmentItem = 8,
    ShipmentPkgDetail = 9,
    VerifiedAddress = 10,
  }
}
