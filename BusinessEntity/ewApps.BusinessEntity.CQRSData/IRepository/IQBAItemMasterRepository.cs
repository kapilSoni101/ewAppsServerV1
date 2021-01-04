using System;
using ewApps.BusinessEntity.DTO;

namespace ewApps.BusinessEntity.QData {
    public interface IQBAItemMasterRepository {

        BusBAItemMasterViewDTO GetItemMasterByBusTenantIdAndItemId(Guid tenantId, Guid itemId, string itemType);

        CustBAItemMasterViewDTO GetItemMasterByBusTenantIdAndItemIdForCust(Guid tenantId, Guid itemId);
    }
}
