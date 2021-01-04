using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Entity {
    [Table("BASalesQuotationAttachment", Schema = "be")]
    public class BASalesQuotationAttachment:BaseEntity {

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
        public string ERPSalesQuotationAttachmentKey {
            get; set;
        }

        public Guid SalesQuotationId {
            get; set;
        }

        [MaxLength(100)]
        public string ERPSalesQuotationKey {
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
