using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO {  

    public class BAARPurchaseOrderAttachmentDTO {

        public Guid ID {
            get; set;
        }

        public string ERPConnectorKey {
            get; set;
        }

        public string ERPARPurchaseOrderAttachmentKey {
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

        /// <summary>
        /// 
        /// </summary>
        public DateTime? AttachmentDate {
            get; set;
        }

    }
}
