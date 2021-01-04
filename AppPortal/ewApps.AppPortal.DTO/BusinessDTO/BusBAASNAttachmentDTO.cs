using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class BusBAASNAttachmentDTO {

        public Guid ID {
            get; set;
        }

        public string ERPConnectorKey {
            get; set;
        }

        public string ERPASNAttachmentKey {
            get; set;
        }

        public Guid ASNId {
            get; set;
        }

        public string ERPASNKey {
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
