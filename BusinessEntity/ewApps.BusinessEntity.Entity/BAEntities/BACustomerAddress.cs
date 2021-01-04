/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agarwal <sagrawal@eworkplaceapps.com>
 * Date: 08 August 2019
 * 
 * Contributor/s: Sourabh agrawal
 * Last Updated On: 08 August 2019
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;

namespace ewApps.BusinessEntity.Entity {

    /// <summary>
    /// Represents properties for CustomerAddress entity.
    /// </summary>
    [Table("BACustomerAddress", Schema = "be")]
    public class BACustomerAddress:BaseEntity {

        /// <summary>
        /// SAP connector key .
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string ERPConnectorKey {
            get; set;
        }

        /// <summary>
        /// SAP customer key .
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string ERPCustomerKey {
            get; set;
        }

        /// <summary>
        /// customer unique id .
        /// </summary>
        [Required]
        public Guid CustomerId {
            get; set;
        }

        /// <summary>
        /// Customer address label.
        /// </summary>
        [MaxLength(100)]
        public string Label {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int ObjectType {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string ObjectTypeText {
            get; set;
        }

        /// <summary>
        /// Customer address name.
        /// </summary>
        [MaxLength(100)]
        public string AddressName {
            get; set;
        }

        /// <summary>
        /// Customer address line 1.
        /// </summary>
        [MaxLength(100)]
        public string Line1 {
            get; set;
        }

        /// <summary>
        /// Customer address line 2.
        /// </summary>
        [MaxLength(100)]
        public string Line2 {
            get; set;
        }

        /// <summary>
        /// Customer address line 3.
        /// </summary>
        [MaxLength(100)]
        public string Line3 {
            get; set;
        }

        /// <summary>
        /// Customer street .
        /// </summary>
        [MaxLength(100)]
        public string Street {
            get; set;
        }

        /// <summary>
        /// Customer street no .
        /// </summary>
        [MaxLength(100)]
        public string StreetNo {
            get; set;
        }

        /// <summary>
        /// Customer city.
        /// </summary>
        [MaxLength(20)]
        public string City {
            get; set;
        }

        /// <summary>
        /// Customer ZipCode.
        /// </summary>
        [MaxLength(20)]
        public string ZipCode {
            get; set;
        }

        /// <summary>
        /// Customer state. 
        /// </summary>
        [MaxLength(20)]
        public string State {
            get; set;
        }

        /// <summary>
        /// Customer country.
        /// </summary>
        [MaxLength(20)]
        public string Country {
            get; set;
        }
    }
}
