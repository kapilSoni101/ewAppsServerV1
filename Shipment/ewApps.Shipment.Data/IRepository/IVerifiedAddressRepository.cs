/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agrawal
 * Date: 09 April 2019
 * 
 * Contributor/s: Sourabh Agrawal 
 * Last Updated On: 08 August 2019
 */

using ewApps.Core.BaseService;
using ewApps.Shipment.Entity;

namespace ewApps.Shipment.Data {

    /// <summary>
    /// This interface provides method to execute add,updated,deleted query agianst respective entity type.
    /// </summary>
    public interface IVerifiedAddressRepository:IBaseRepository<VerifiedAddress> {

    }
}
