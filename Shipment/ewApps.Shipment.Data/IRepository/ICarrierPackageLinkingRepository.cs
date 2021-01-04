/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 15 May 2019
 * 
 * Contributor/s: Amit Mundra
 * Last Updated On: 15 May 2019
 */

using ewApps.Core.BaseService;
using ewApps.Shipment.Entity;

namespace ewApps.Shipment.Data {

    /// <summary>
    /// This interface provides method to execute add,updated,deleted query agianst respective entity type.
    /// </summary>
    public interface ICarrierPackageLinkingRepository:IBaseRepository<CarrierPackageLinking> {

    }
}
