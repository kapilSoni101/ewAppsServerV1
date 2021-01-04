/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Souarbh Agrawal <sagrawal@eworkplaceapps.com>
 * Date: 19 December 2018
 * 
 * Contributor/s:Souarbh Agrawal
 * Last Updated On: 26 December 2018
 */





using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Data {
  // <summary>
  /// This interface provides methods to execute all database operations to get InvoiceItem and
  /// related data transfer objects.
  // </summary>
  public interface IBAPurchaseOrderItemRepository: IBaseRepository<BAPurchaseOrderItem> {
  }
}
