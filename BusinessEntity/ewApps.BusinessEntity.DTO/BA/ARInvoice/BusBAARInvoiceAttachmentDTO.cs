using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO {
    public class BusBAARInvoiceAttachmentDTO {

        public Guid ID {
            get; set;
        }

        public string ERPConnectorKey {
            get; set;
        }

        public string ERPARInvoiceAttachmentKey {
            get; set;
        }

        public Guid ARInvoiceId {
            get; set;
        }


        public string ERPARInvoiceKey {
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
