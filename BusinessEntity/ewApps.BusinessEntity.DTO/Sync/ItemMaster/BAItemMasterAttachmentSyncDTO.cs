using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO {

  public  class BAItemMasterAttachmentSyncDTO {

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


        public DateTime? AttachmentDate {
            get; set;
        }

        /// <summary>
        /// Map model object to entity object and return it.
        /// </summary>
        /// <returns></returns>
        public static BAItemMasterAttachment MapToEntity(BAItemMasterAttachmentSyncDTO model) {
            BAItemMasterAttachment entity = new BAItemMasterAttachment();
            entity.ERPConnectorKey = model.ERPConnectorKey;
            entity.ERPItemKey = model.ERPItemMasterKey;
            entity.ERPItemAttachmentKey = model.ERPItemMasterAttachmentKey;
            entity.ItemId = model.ItemMasterId;
            entity.FreeText = model.FreeText;
            entity.Name = model.Name;
            entity.AttachmentDate = model.AttachmentDate;

            return entity;
        }
    }
}
