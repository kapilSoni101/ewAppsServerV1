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
using ewApps.Shipment.Data;
using ewApps.Shipment.Entity;

namespace ewApps.Shipment.DS {

    /// <summary>
    /// This class performs basic CRUD operation on VerifiedAddress entity.
    /// </summary>
    public class VerifiedAddressDS:BaseDS<VerifiedAddress>, IVerifiedAddressDS {


        #region Local member
        IVerifiedAddressRepository _verifiedAddressRepository;
        #endregion Local member

        #region Constructor

        public VerifiedAddressDS(IVerifiedAddressRepository verifiedAddressRepository) : base(verifiedAddressRepository) {
            _verifiedAddressRepository = verifiedAddressRepository;
        }

        #endregion Constructor

    }
}
