using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO {

    public class BAASNAttachmentSyncDTO {

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

        /// <summary>
        /// Map model object to entity object and return it.
        /// </summary>
        /// <returns></returns>
        public static BAASNAttachment MapToEntity(BAASNAttachmentSyncDTO model) {
            BAASNAttachment entity = new BAASNAttachment();
            entity.ERPConnectorKey = model.ERPConnectorKey;
            entity.ERPASNKey = model.ERPASNKey;
            entity.ERPASNAttachmentKey = model.ERPASNAttachmentKey;
            entity.ASNId = model.ASNId;
            entity.FreeText = model.FreeText;
            entity.Name = model.Name;
            entity.AttachmentDate = model.AttachmentDate;

            return entity;
        }
    }
}
