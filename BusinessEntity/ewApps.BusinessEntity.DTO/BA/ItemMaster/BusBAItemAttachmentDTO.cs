using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO {

    public class BusBAItemAttachmentDTO {

        public Guid ID {
            get; set;
        }

        public string ERPConnectorKey {
            get; set;
        }


        public string ERPItemMasterAttachmentKey {
            get; set;
        }

        public Guid ItemMasterId {
            get; set;
        }

        public string ERPItemMasterKey {
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