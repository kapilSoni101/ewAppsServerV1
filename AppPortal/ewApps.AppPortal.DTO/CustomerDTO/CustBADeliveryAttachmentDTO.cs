using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class CustBADeliveryAttachmentDTO {

        public Guid ID {
            get; set;
        }

        public string ERPConnectorKey {
            get; set;
        }

        public string ERPDeliveryAttachmentKey {
            get; set;
        }

        public Guid DeliveryId {
            get; set;
        }

        public string ERPDeliveryKey {
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
