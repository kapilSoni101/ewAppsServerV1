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
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ewApps.Shipment.Entity {

    /// <summary>
    /// VerifiedAddress table represting all VerifiedAddress Detail Information.
    /// </summary>    
    [Table("VerifiedAddress", Schema = "ship")]
    public class VerifiedAddress:BaseEntity {

        /// <summary>
        /// ID of carrier in which adrress is verified .
        /// </summary>
        [Required]
        public Guid CarrierId {
            get; set;
        }

        /// <summary>
        /// Unique id for address.
        /// </summary>
        [Required]
        public Guid AddressId {
            get; set;
        }

        /// <summary>
        /// On Which Date and Time Address Verified .
        /// </summary>
        [Required]
        public DateTime VarifiedOn {
            get; set;
        }

        /// <summary>
        ///  Id of user whom is verified address.
        /// </summary>
        [Required]
        public Guid VerifiedBy {
            get; set;
        }
        
        /// <summary>
        /// The Verified Flag for Address
        /// </summary>
        [Required]
        public bool Verified {
            get; set;
        }

        /// <summary>
        /// Verified description of address
        /// </summary>
        [MaxLength(4000)]
        public string Description {
            get; set;
        }
    }
}
