/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Kuldeep Singh Chauhan <kchauhan@eworkplaceapps.com>
 * Date: 08 Aug 2019
 */

using ewApps.Core.BaseService;
using ewApps.Shipment.Entity;

namespace ewApps.Shipment.Data {

    /// <summary>
    /// This interface provides method to execute add,updated,deleted query agianst respective entity type.
    /// </summary>
    public interface IPackageMasterRepository:IBaseRepository<PackageMaster> {

    }
}
