using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Entity {

    /// <summary>
    /// Theme entity represting all PurchaseOrder properties.
    /// </summary>
    /// 
    [Table("BAPurchaseOrderAttachment", Schema = "be")]
    public class BAPurchaseOrderAttachment: BaseEntity {

        /// <summary>
        /// The entity name.
        /// </summary>
        public const string EntityName = "BAPurchaseOrderAttachment";

        public string ERPConnectorKey {
            get; set;
        }

        public string ERPPurchaseOrderAttachmentKey {
            get; set;
        }

        public Guid PurchaseOrderId {
            get; set;
        }

        public string ERPPurchaseOrderKey {
            get; set;
        }

        public string Name {
            get; set;
        }

        public string FreeText {
            get; set;
        }

        public DateTime? AttachmentDate {
            get; set;
        }

    }
}