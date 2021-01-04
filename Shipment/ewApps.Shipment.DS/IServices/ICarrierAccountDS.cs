/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author:Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 12 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 12 September 2019
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Shipment.DTO;

namespace ewApps.Shipment.DS {

    //This Class User For Carrier Acount Operation 
    public interface ICarrierAccountDS {

        /// <summary>
        /// add/update/delete shipper account info in connector.
        /// </summary>
        /// <param name="listAddUpdateAccount"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task AddUpdateCarrierAccountInfoAsync(List<AppServiceAccDetailIdDTO> listAddUpdateAccount, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Save carrier account detail for shipper.
        /// </summary>
        /// <param name="shipperAccountRequestDTO"> entity to be added</param>
        /// <param name="isAdd">Whether add/update account detail.</param>
        /// <returns>return account detail for shipper.</returns>
        Task<ShipperAccountDTO> AddUpdateShipperAccountDetailAsync(ShipperAccountDTO shipperAccountRequestDTO, bool isAdd, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Delete shipper account detail.
        /// </summary>
        /// <param name="shipperAccountRequestDTO"> shipper account to delete</param>
        Task DeleteShipperAccountDetailAsync(ShipperAccountDTO shipperAccountRequestDTO, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get shipper account list by shipperid and carriercode.
        /// </summary>
        /// <param name="shipperId">Shipperid, for example TenantId or CustomerId.</param>
        /// <param name="carrierCode">CarrierCode for example FedEx, UPS etc.</param>
        /// <returns>Added entity</returns>
        Task<List<ShipperAccountDTO>> GetShipperAccountListAsync(string shipperId, string carrierCode, CancellationToken token = default(CancellationToken));

    }
}
