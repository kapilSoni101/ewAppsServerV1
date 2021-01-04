/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agarwal <sagrawal@ewoorkplaceapps.com>
 * Date: 13 August 2019
 * 
 * Contributor/s: Sourabh agrawal
 * Last Updated On: 13 August 2019
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Entity {

    [Table("BusinessAddress", Schema = "ap")]
    public class BusinessAddress:BaseEntity {
        /// <summary>
        /// The Id  of ParentEntity.
        /// </summary>
        /// 
        [Required]
        public Guid BusinessId {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string Label {
            get; set;
        }

        /// <summary>
        /// AddressStreet1.
        /// </summary>
        [MaxLength(100)]
        public string AddressStreet1 {
            get; set;
        }

        /// <summary>
        /// AddressStreet2.
        /// </summary>
        [MaxLength(100)]
        public string AddressStreet2 {
            get; set;
        }

        /// <summary>
        /// AddressStreet3.
        /// </summary>
        [MaxLength(100)]
        public string AddressStreet3 {
            get; set;
        }

        /// <summary>
        /// City.
        /// </summary>
        [MaxLength(100)]// 
        public string City {
            get; set;
        }

        /// <summary>
        /// Country.
        /// </summary>
        [MaxLength(100)]
        public string Country {
            get; set;
        }

        /// <summary>
        /// State.
        /// </summary>
        [MaxLength(100)]
        public string State {
            get; set;
        }

        /// <summary>
        /// ZipCode.
        /// </summary>
        [MaxLength(100)]
        public string ZipCode {
            get; set;
        }

        /// <summary>
        /// FaxNumber.
        /// </summary>
        [MaxLength(100)]
        public string FaxNumber {
            get; set;
        }

        /// <summary>
        /// Phone.
        /// </summary>
        [MaxLength(100)]
        public string Phone {
            get; set;
        }

        /// <summary>
        /// AddressType.
        /// </summary>
        public int AddressType {
            get; set;
        }

    }
}
