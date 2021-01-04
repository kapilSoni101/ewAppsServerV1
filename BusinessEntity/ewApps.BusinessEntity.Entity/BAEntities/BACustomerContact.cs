/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agarwal
 * Date: 08 August 2019
 * 
 * Contributor/s: Sourabh agrawal
 * Last Updated On: 08 August 2019
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;

namespace ewApps.BusinessEntity.Entity{

    /// <summary>
    /// Represents properties for BACustomerContact entity.
    /// </summary>
    [Table("BACustomerContact", Schema = "be")]
    public class BACustomerContact:BaseEntity {

        /// <summary>
        /// SAP contact key .
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string ERPContactKey {
            get; set;
        }


        /// <summary>
        /// SAP connector key .
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string ERPConnectorKey {
            get; set;
        }

        /// <summary>
        /// Unique id of customer .
        /// </summary>
        [Required]
        public Guid CustomerId {
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
        /// 
        /// </summary>
        public bool Default {
            get; set;
        }


        /// <summary>
        /// custome contact first name.
        /// </summary>
        [MaxLength(100)]
        public string FirstName {
            get; set;
        }

        /// <summary>
        /// custome contact last name.
        /// </summary>
        [MaxLength(100)]
        public string LastName {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public string Title {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public string Position {
            get; set;
        }

        /// <summary>
        /// custome contact address.
        /// </summary>
        [MaxLength(4000)]
        public string Address {
            get; set;
        }

        /// <summary>
        /// custome contact telephone number.
        /// </summary>
        [MaxLength(50)]
        public string Telephone {
            get; set;
        }

        /// <summary>
        /// custome contact email.
        /// </summary>
        [MaxLength(50)]
        public string Email {
            get; set;
        }
    }
}
