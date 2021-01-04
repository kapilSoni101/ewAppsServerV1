using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Entity {

    [Table("BADeliveryAttachment", Schema = "be")]
    public class BADeliveryAttachment:BaseEntity {

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
        public string ERPDeliveryAttachmentKey {
            get; set;
        }

        public Guid DeliveryId {
            get; set;
        }

        [MaxLength(100)]
        public string ERPDeliveryKey {
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
