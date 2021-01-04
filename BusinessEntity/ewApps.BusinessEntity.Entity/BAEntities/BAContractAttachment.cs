/* Copyright © 2018 eWorkplace Apps(https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 *
 * Author: Sourabh Agrawal<sagrawal @eworkplaceapps.com>
 * Date:05 september 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Entity {
    /// <summary>
    /// Represents properties for BAContract entity.
    /// </summary>
    [Table("BAContractAttachment", Schema = "be")]
    public class BAContractAttachment :BaseEntity {

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Guid ID {
            get; set;
        }

        [MaxLength(100)]
        public string ERPConnectorKey {
            get; set;
        }

        [MaxLength(100)]
        public string ERPContractAttachmentKey {
            get; set;
        }

        public Guid ContractId {
            get; set;
        }

        [MaxLength(100)]
        public string ERPContractKey {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public string Name {
            get; set;
        }

        /// <summary>
        /// 
        ///</summary>
        [MaxLength(100)]
        public string FreeText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? AttachmentDate {
            get; set;
        }
    }
}
