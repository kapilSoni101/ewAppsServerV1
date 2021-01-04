using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Entity {
    [Table("BASalesOrderAttachment", Schema = "be")]
    public class BASalesOrderAttachment:BaseEntity {

        
        [MaxLength(100)]
        public string ERPConnectorKey {
            get; set;
        }

        [MaxLength(100)]
        public string ERPSalesOrderAttachmentKey {
            get; set;
        }

        public Guid SalesOrderId {
            get; set;
        }

        [MaxLength(100)]
        public string ERPSalesOrderKey {
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
